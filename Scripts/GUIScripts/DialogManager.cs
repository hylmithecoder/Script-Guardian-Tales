using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public RectTransform dialogBoxTransform;
    private Transform currentNpc; // NPC yang sedang berbicara
    public Vector2 offset = new Vector2(0, 100f); // Jarak dialog box dari NPC
    [SerializeField] GameObject pfDialogueBox; // Prefab untuk dialog box
    [SerializeField] GameObject papanHealthbar; // Untuk mengontrol healthbar NPC
    [SerializeField] GameObject healthbar; // Untuk memunculkan atau menyembunyikan healthbar
    [SerializeField] Image avatarPrefab;
    [SerializeField] Image avatar;
    [SerializeField] Text DialogueText; // Teks yang menampilkan dialog
    [SerializeField] int pesanPerDetik = 30; // Kecepatan penulisan dialog
    public SFXManager sfx; // Sound effects manager
    public Animator animator; 

    public event Action OnTampilkanDialog; // Event ketika dialog ditampilkan
    public event Action OnSembunyikanDialog; // Event ketika dialog disembunyikan

    public static DialogManager Instance { get; private set; }

    private Dialog dialog;
    private int garis = 0; // Index untuk mengakses baris dialog
    private bool isTyping; // Apakah dialog sedang ditulis
    private bool dialogShownOnce = false; // Apakah dialog pernah ditampilkan
    private Camera mainCamera;
    private RectTransform dialogueTransform; // RectTransform untuk dialog box
    private bool isDialogBoxActive = false; // Flag apakah dialog box sedang aktif
    private bool isPositionFirst = false;
    private Vector2 InitScreenPosition;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    public void StartDialog(Dialog dialog, Transform npc)
    {
        currentNpc = npc; // NPC yang sedang berinteraksi
        // Debug.Log(currentNpc.name);
        StartCoroutine(ShowDialog(dialog));
    }

    public IEnumerator ShowDialog(Dialog dialog)
    {
        // Memulai dialog
        yield return new WaitForEndOfFrame();
        OnTampilkanDialog?.Invoke();

        this.dialog = dialog;
        garis = 0;
        dialogShownOnce = true;

        if (!isDialogBoxActive)
        {
            // Inisialisasi dialog box hanya saat belum ada
            dialogueTransform = Instantiate(pfDialogueBox).GetComponent<RectTransform>();
            // Set Ke parent dialog and healtbar ui
            Canvas DialogdanHealthBar = FindObjectOfType<Canvas>();
            dialogueTransform.SetParent(DialogdanHealthBar.transform);
            dialogueTransform.anchoredPosition = Vector2.zero;
            dialogueTransform.gameObject.SetActive(true); // Aktifkan dialog box
            avatarPrefab = dialogueTransform.Find("Border").GetComponent<Image>();
            avatarPrefab.gameObject.SetActive(false);
            avatar = avatarPrefab.transform.Find("Avatar").GetComponent<Image>();
            
            // Set Text pada dialog box
            DialogueText = dialogueTransform.Find("TeksDialog").GetComponent<Text>();
            
            if (!isPositionFirst)
            {
                InitiateDialogueBox();
                InitScreenPosition = dialogueTransform.position;
                isPositionFirst = true;
            }
            else
            {
                // Gunakan posisi yang telah diinisialisasi sebelumnya
                dialogueTransform.position = InitScreenPosition;
            }
            if (currentNpc != null)
            {
                if (currentNpc.name == "Knight")
                {
                    avatarPrefab.gameObject.SetActive(true);
                    avatar.gameObject.SetActive(true);
                    avatar.sprite = Resources.Load<Sprite>("Avatar/Knight");
                }
                Debug.Log("Nama NPC: "+currentNpc.name);
            }
            InitiateDialogueBox();
            isDialogBoxActive = true;
        }

        StopAllCoroutines();
        StartCoroutine(TypeDialog(dialog.Baris[0])); // Mulai menampilkan baris pertama
    }

    private void ShowNextDialogLine()
    {
        if (dialogShownOnce)
        {
            if (garis < dialog.Baris.Count - 1)
            {
                // Tampilkan baris dialog berikutnya
                healthbar.SetActive(false);
                garis++;
                StartCoroutine(TypeDialog(dialog.Baris[garis]));
            }
            else
            {
                // Jika dialog sudah selesai, sembunyikan dialog box
                dialogueTransform.gameObject.SetActive(false);
                OnSembunyikanDialog?.Invoke();
                dialogShownOnce = false;
                isDialogBoxActive = false;
                Destroy(dialogueTransform.gameObject); // Hapus dialog box saat selesai
            }
        }
        else
        {
            StartCoroutine(ShowDialog(dialog));
        }
    }

    // Coroutine untuk menampilkan dialog secara bertahap
    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        DialogueText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(1f / pesanPerDetik);
        }
        isTyping = false;

        yield return new WaitForSeconds(1.5f);
        ShowNextDialogLine();
    }

    // Update posisi dialog box mengikuti NPC
    private void InitiateDialogueBox()
    {
        if (currentNpc != null)
        {
            Vector3 newPosition = mainCamera.WorldToScreenPoint(currentNpc.position);
            dialogueTransform.position = newPosition + (Vector3)offset;
        }
    }

    public void HandleUpdate()
    {
        // InitiateDialogueBox();
    }

    void Update()
    {

    }
}
