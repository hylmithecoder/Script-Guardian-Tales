// This Script As GameController in lobby
// note jangan lupa tambahkan [System.Serializable] untuk system class
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    [SerializeField] private Interface_Source uiStaminanya;
    [SerializeField] private Interface_Source2 uiDiamondnya;
    [SerializeField] GameObject alertUi;
    public Button backButton;
    public Button exitButton;
    public Button adventureButton;    
    public Button tambahStamina;
    public LevelLoader levelLoader;
    private Stamina stamina;
    private SFXManager sfx; 
    private ItemTerpenting dataLobby;
    private Diamond diamond;


    private void Awake() 
    {
        sfx = GetComponent<SFXManager>();
        stamina = new Stamina();
        diamond = new Diamond();
        dataLobby = new ItemTerpenting();        
    }
    [System.Obsolete]
    void Start()
    {
        uiStaminanya.SetInventory(stamina);
        uiDiamondnya.SetInventory(diamond);
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        QualitySettings.vSyncCount = 0;

        tambahStamina.onClick.AddListener(() => TambahStamina());
        adventureButton.onClick.AddListener(() => Adventure());        
        
        backButton.onClick.AddListener(Kembali);
        exitButton.onClick.AddListener(Exit);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sfx.buttonSound.Play();            
            alertUi.SetActive(true);
        }
    }

    public void Adventure()
    {
        sfx.buttonSound.Play();        
        UseStamina();        
        // StaminaData staminaData = SaveSystem.Load();

        // dataLobby.jumlah = staminaData.amount;
        // dataLobby.maxStamina = staminaData.maxStamina;
    }
    public void SfxButton()
    {
        sfx.buttonSound.Play();
    }

     public void TambahStamina()
    {
        sfx.buttonSound.Play();   
        stamina.AddStamina();
        uiStaminanya.UpdateInventory(stamina); 
    }

// fungsi Kurangin stamina
    public void UseStamina() 
    {
        sfx.buttonSound.Play();

        if (stamina.HasEnoughStaminaItem())
        {
            stamina.UseStaminaForMainStory();

            uiStaminanya.UpdateInventory(stamina);

            StartCoroutine(levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

            Debug.Log("Stamina Berkurang!");             
        }
        else
        {
            Debug.Log("Stamina Tidak Cukup untuk adventure"); 
        }
    }

    public void Kembali()
    {
        sfx.buttonSound.Play();
        alertUi.SetActive(false);
    }

    public void Exit()
    {
        sfx.buttonSound.Play();
        Application.Quit();
    }

// function of save item, tpi blum worth
    public void Save()
    {
        Debug.Log("Kamu save item");
        dataLobby.Save();
    } 
// function of load item, tpi blum worth
    public void Load()
    {
        Debug.Log("kamu load item");
        dataLobby.Load();
    }
}
