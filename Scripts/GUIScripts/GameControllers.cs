// Pengatur Time dan Game

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public enum GameStates { FreeRoam, Battle, Dialog, Main_Quest, Inventory, Menu, Paused, Save }
public class GameControllers : MonoBehaviour
{
    [SerializeField] PlayerMovementAndroid playercontroller;   
    [SerializeField] AiMusuh aiSlime;
    [SerializeField] AudioSource sfxButton;
    [SerializeField] BasicAttack sword;

    [SerializeField] Button mapButton;
    [SerializeField] Button pauseButton;
    [SerializeField] Button pauseButtonafter;
    [SerializeField] Button ResetGameButton;
    [SerializeField] public GameObject UI;
    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject Analog;
    [SerializeField] GameObject map;
    private SFXManager sfx;
    public LevelLoader levelLoader;
    private statprototype playerData;
    private Teleport teleport;
    private bool isPaused;
    GameStates state;

    public void Start() 
    {       
        playerData = GameObject.Find("Player").GetComponent<statprototype>(); 
        sfx = GetComponent<SFXManager>();
        // isPaused = PauseUI;
        if (pauseButton)
        {
            AddEventTrigger(pauseButton.gameObject, EventTriggerType.PointerDown, (data) => Paused());
        }

        if (mapButton)
        {
            AddEventTrigger(mapButton.gameObject, EventTriggerType.PointerDown, (data) => Paused());
        }

        if (pauseButtonafter)
        {
            AddEventTrigger(pauseButtonafter.gameObject, EventTriggerType.PointerDown, (data) => Paused());
        }

        if (ResetGameButton)
        {
            AddEventTrigger(ResetGameButton.gameObject, EventTriggerType.PointerDown, (data) => ToLobby());
        }
        // Manager Of The Dialog Game
        DialogManager.Instance.OnTampilkanDialog += () =>
        {
            state = GameStates.Dialog;
            state = GameStates.FreeRoam;
        };
        DialogManager.Instance.OnSembunyikanDialog += () =>
        {
            if (state == GameStates.Dialog)
                state = GameStates.FreeRoam;
        };    
    }

// Update Controller
    private void Update() 
    {
        if (state == GameStates.FreeRoam)
        {
            playercontroller.HandleUpdate();
            // Teleport_Manager.Instance.HandleUpdate();
        } else if (state == GameStates.Battle)
        {
            aiSlime.Update();
        } else if (state == GameStates.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        } else if (state == GameStates.Inventory)
        {

        } else if (state == GameStates.Menu)
        {

        } else if (state == GameStates.Paused)
        {
            Paused();
        } else if (state == GameStates.Save)
        {
            // playerData.Update();
        } 
    } 
    // pause
    public void Paused()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Analog.SetActive(false);
            UI.SetActive(false);
            PauseUI.SetActive(true);
            // sound effect nya
            sfx.buttonSfxObject.SetActive(true);
            sfx.buttonSound.Play();
        }
        else
        {
            sfx.buttonSound.Play();
            Analog.SetActive(true);
            Time.timeScale = 1f;
            UI.SetActive(true);
            PauseUI.SetActive(false);
        }
    }

    private void AddEventTrigger(GameObject obj, EventTriggerType eventType, Action<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action(eventData));
        trigger.triggers.Add(entry);
    }

    public void ToLobby()
    {
        // referensi kalau mau panggil fungsi skrip tpi pake private
        // levelLoader = GameObject.Find("CrossFade").GetComponent<LevelLoader>();
        StartCoroutine(levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
        StartCoroutine(levelLoader.AnimateLoadingText());
        Time.timeScale = 1f;
        sfx.buttonSound.Play();
        PauseUI.SetActive(false);
        SaveSystem.Load();
    }
}
