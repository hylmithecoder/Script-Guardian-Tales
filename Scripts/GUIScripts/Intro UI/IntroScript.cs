using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class IntroScript : MonoBehaviour
{
    public RawImage mentahanCanvasVideoIntro;    
    public float waktuHilang;
    public float WaktuTampil;
    public float loadingPackage;
    public GameObject alertUi;
    public AudioSource laguIntro;
    public LevelLoader levelLoader;
    public PerulanganStartTeks touchToStart;
    public Color teksColor;
    public GameObject title;
    public Button kembaliButton;
    public Button skipButton;
    private SFXManager sound;
    private Animator titleTeksAnim;
    private TextMeshProUGUI teksTitle;
    private TextMeshProUGUI tapToStart;
    private Text notice;
    // private AdderEventTriggerManager triggerManager;
    private Color originalColor;
    private Color targetColor;

    [System.Obsolete]
    
    private void Awake() 
    {
        // triggerManager = new AdderEventTriggerManager();
        notice = GameObject.Find("Peringatannya").GetComponent<Text>();
        notice.text = "Would You Like To Exit ?";
        titleTeksAnim = title.GetComponent<Animator>();
        teksTitle = title.GetComponent<TextMeshProUGUI>();
        // refresh rate bergantung layar yang support
        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        QualitySettings.vSyncCount = 0;

        tapToStart = GameObject.Find("Notice").GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {      
        
        teksTitle.color = teksColor;
        StartCoroutine(touchToStart.LoopActivation());         
        
        kembaliButton.onClick.AddListener(() => Kembali());


        if (alertUi != null)
        {
            alertUi.SetActive(false);
        } 

        skipButton.onClick.AddListener(() => Keluar());
        sound = GetComponent<SFXManager>();
        StartCoroutine(AppearTitle());
        tapToStart.SetText("Checkly Update...");
    }

     private void Update() 
    {
        loadingPackage -= Time.deltaTime;
        Debug.Log(loadingPackage);
        if (loadingPackage <= 0)
        {
            tapToStart.SetText("Tap To Start");            
        } 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            alertUi.SetActive(true);
        }       
    }

    public void Kembali()
    {
        alertUi.SetActive(false);
        sound.buttonSfxObject.SetActive(true);
        sound.buttonSound.Play();
        // exitAlert.SetActive(false);
    }
// Masih Blum Worth Kalau Untuk VideoPlayer, itu pakai audiosource ya
    private void NextScene()
    {
        alertUi.SetActive(false);
        StartCoroutine(levelLoader.LoadLevelWithString("PrologScene"));
        StartCoroutine(levelLoader.AnimateLoadingText());
    }

    public void Keluar()
    {
        Application.Quit();        
    }

    private IEnumerator AppearTitle()
    {
        while(true)
        {
            title.SetActive(true);
            
            if (titleTeksAnim != null)
            {
                // string trigger = Random.value > 0.5f ? "Appear_W" : "Appear_B";

                titleTeksAnim.SetTrigger("Appear_W");
            }
            yield return new WaitForSeconds(waktuHilang);
            title.SetActive(false);
            yield return new WaitForSeconds(WaktuTampil);
        }
    }
    public void CanvasOnPointerUp()
    {                    
        loadingPackage -= Time.deltaTime;
        
        if (loadingPackage <= 0)
        {
            NextScene();
            sound.buttonSound.Play();
        }
        else 
        {
            // do something
        }
    }
}
