// Dialog untuk main quest dan blum menggunakan scriptable masih manual

using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StoryDialogue : MonoBehaviour
{
    [SerializeField] GameObject storyDialogUi;
    [SerializeField] GameObject UI;
    [SerializeField] GameObject Analog;
    [SerializeField] GameObject healthbar;
    [SerializeField] int wpm;
    [SerializeField] Button opsiYes;
    [SerializeField] Button opsiNo;
    public SFXManager sfx;
    // public event Action OnTampilkanDialog;
    // public event Action OnSembunyikanDialog;
    public static StoryDialogue Instance { get; private set; }
    public TextMeshProUGUI teksNya;
    public TextMeshProUGUI no;
    public TextMeshProUGUI yes;
    // private StoryDialog dialog;
    private bool sudahInteract = false;
    private Rigidbody2D rbPlayer;
    private bool disableMovement = false;
    private Animator playerAnimasi;

    private void Awake()
    {
        playerAnimasi = GameObject.Find("Player").GetComponent<Animator>();
        Instance = this;        
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        if (playerAnimasi != null)
        {
            Debug.Log("Berhasil mendapatkan animator player");
        }

        no.SetText("Malas");
        yes.SetText("Ok");
        opsiYes.onClick.AddListener(YesButton);
        opsiNo.onClick.AddListener(ButtonMerah);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Player")
        {
            if (sudahInteract == false | disableMovement == false)
            {
                teksNya.SetText("Your mission is interact the knight and Wizard");
                storyDialogUi.SetActive(true);
                UI.SetActive(false);
                sudahInteract = true;
                disableMovement = true;
                if (disableMovement == true)
                {
                    playerAnimasi.SetBool("isMoving", false);
                    rbPlayer.simulated = false;
                }
            } 
        }        
    }
    private void YesButton()
    {
        storyDialogUi.SetActive(false);
        UI.SetActive(true);
        rbPlayer.simulated = true;
        SFXManager sfx = GameObject.FindWithTag("GameController").GetComponent<SFXManager>();
        sfx.buttonSound.Play();
    }

    private void ButtonMerah()
    {
        storyDialogUi.SetActive(false);
        UI.SetActive(true);
        rbPlayer.simulated = true;
        Application.Quit();
        SFXManager sfx = GameObject.FindWithTag("GameController").GetComponent<SFXManager>();
        sfx.buttonSound.Play();
    } 
}
