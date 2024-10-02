// This Source Code Create by CopyRight 2024 (c) Hylmi Muhammad Fiary Mahdi
// To use this chat me on hylmimuhammadfiarymahdi@gmail.com
//       - Source code Ini berhubungan dengan dialog, sword attack, dan lain-lain jadi hati hati menggunakan nya
//       - Ke SlimeNgejar Untuk Perbaiki atau update otak Musuh
//       - Gunakan Flag Untuk membatasi variabel dan mengurangi kemungkinan error

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementAndroid : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // Variabel
    [SerializeField] GameObject SFX;
    [SerializeField] private InterfaceInventory interfaceInventory;
    // public Vector3 tempatSpawnItemSword;
    // public Vector3 tempatSpawnPurpleCoin;
    // public Vector3 tempatSpawnPurpleCoin2;
    public statprototype stat;
    public Joystick gerakanJoystick;
    public float kecepatanGerak;
    public float kecepatanSprint;
    private Animator animasiCharacter;
    public Animator TukarTombolSprint; // Animasi Pertukaran tombol sprint ke action
    public Vector2 setSpawnEnemy;
    private Vector2 input;
    private Vector2 lastMoveDirection;    
    private bool isMoving;
    public bool isSprinting;
    private bool isInteractable;
    private bool isAttacking;
    private Transform enemySpawn;
    // private bool isInBattle = false;
    private bool isPaused = false;
    private Rigidbody2D rb;
    public Button actionButton;
    public Button basicAttack;
    public Button skillButton;
    public Button specialSkill;
    public LayerMask Interaktif;    
    public LayerMask objekKeras;
    public LayerMask questTrigger;
    public BasicAttack swordAttack;
    public DrownedSkillButton skillAttack;
    // public statprototype stat;
    public AudioSource SFXSword;
    [SerializeField] GameObject pfEnemy; 
    private SoundManager lagu;
    public SFXManager soundEffect;
    private bool isDialogNow = false;
    private bool isTeleporting = false;
    private GameControllers gameControllers;
    private List<GameObject> slimeList = new List<GameObject>();
    private int maxSlimeCount = 3;
    // public float detectionRange = 5.0f; // Add this variable for detection range
    private Inventory inventory;

// Batas Peletakan Variabel
    private void Awake() 
    {        
        animasiCharacter = GetComponent<Animator>();    

        // Set Untuk Inventory System Agar Muncul DI UI Canvas Ini sangat penting
        // inventory = new Inventory();
        // interfaceInventory.SetInventory(inventory);
        // MunculkanItemPedangOp(50);
    }

    // Tinggal hilangkan tanda komentar ya kalau mau digunakan
    // void MunculkanItemPedangOp(int jumlahItem)
    // {        
    //     ItemWorld.MunculkanItem(tempatSpawnItemSword, new Item {itemType = Item.ItemType.Sword, jumlah = jumlahItem });
    //     ItemWorld.MunculkanItem(tempatSpawnPurpleCoin2, new Item { itemType = Item.ItemType.Purple_Coin, jumlah = 100 });
    //     ItemWorld.MunculkanItem(tempatSpawnPurpleCoin, new Item { itemType = Item.ItemType.Purple_Coin, jumlah = jumlahItem });
    // }
    private void OnTriggerEnter2D(Collider2D collision) 
    {   
        // Fungsi Kalau Nyentuh Item World
        ItemWorld itemDunia = collision.GetComponent<ItemWorld>();
        if (itemDunia != null)
        {
            // Debug.Log("Kamu Mengambil Item " + itemDunia.GetItem().itemType);
            inventory.AddItem(itemDunia.GetItem());
            itemDunia.DestroySelf();
        }
    }
    
// perbedaan start dengan awake kalau start dia langsung ke play alias aktif
    private void Start()
    {               
        enemySpawn = Instantiate(pfEnemy).GetComponent<Transform>();
        Transform Musuh = GameObject.Find("Musuh").GetComponent<Transform>();
        enemySpawn.SetParent(Musuh.transform);
        enemySpawn.position = setSpawnEnemy;
        Debug.Log("Spawn enemy : " + enemySpawn);
        
        gameControllers = GameObject.Find("GameController").GetComponent<GameControllers>();
        rb = GetComponent<Rigidbody2D>();
        // cek apakah ada error pada button...
        if (actionButton | basicAttack | skillButton | specialSkill != null)
        {
            Debug.Log("Semua buttons di Mainscene Berfungsi");
        }
        else
        {
            Debug.LogError("Ada button yang tidak berfungsi ");
        }

        if (actionButton != null)
        {
            AddEventTrigger(actionButton.gameObject, EventTriggerType.PointerDown, (data) => OnPointerDown((PointerEventData)data));
            AddEventTrigger(actionButton.gameObject, EventTriggerType.PointerUp, (data) => OnPointerUp((PointerEventData)data));
        }

        if (basicAttack != null)
        {
            // AddEventTrigger(basicAttack.gameObject, EventTriggerType.PointerDown, (data) => OnPointerDownforSword((PointerEventData)data));
            // AddEventTrigger(basicAttack.gameObject, EventTriggerType.PointerUp, (data) => OnPointerUpforSword((PointerEventData)data));
        }

        if (skillButton != null)
        {
            AddEventTrigger(skillButton.gameObject, EventTriggerType.PointerDown, (data) => PointerDownUntukSkill((PointerEventData)data));
            AddEventTrigger(skillButton.gameObject, EventTriggerType.PointerUp, (data) => PointerUpUntukSkill((PointerEventData)data));
        }

        if (specialSkill != null)
        {
            AddEventTrigger(specialSkill.gameObject, EventTriggerType.PointerDown,  (data) => PointerDownUntukSpesialSkill((PointerEventData)data));
            AddEventTrigger(specialSkill.gameObject, EventTriggerType.PointerUp,  (data) => PointerUpUntukSpesialSkill((PointerEventData)data));
        }
    }

// Update Ini Dikontrol oleh update dari class gamecontroller...
// biar gk bingung
    public void HandleUpdate()
    {   
        // int spawnChance = UnityEngine.Random.Range(0, 100);
        // if (spawnChance <= 20) // Spawn only if chance is 20 or below (20% chance)
        // {
        //     enemySpawn = Instantiate(pfEnemy).GetComponent<Transform>();
        //     Transform Musuh = GameObject.Find("Musuh").GetComponent<Transform>();
        //     enemySpawn.SetParent(Musuh.transform);
        //     enemySpawn.position = setSpawnEnemy;
        // }
        
        // if (enemySpawn != null)
        // {
        //     Debug.Log("Slime Di spawn");
        // }        
        // statement pause
        if (isPaused) return;
        // Get movement input from Joystick
        input = gerakanJoystick.Direction;

        // Update animator based on movement
        animasiCharacter.SetBool("isMoving", input.magnitude > 0.1f);
        isMoving = input.magnitude > 0.1f; // Update internal variable

        // Update arah character terakhir kali joystick bergerak 
        if (isMoving)
        {
            lastMoveDirection = input;            
        }

        // Update animasi jalan ke arah yang sesuai
        animasiCharacter.SetFloat("moveX", lastMoveDirection.x);
        animasiCharacter.SetFloat("moveY", lastMoveDirection.y);

        Sensor();
    }

// Tukar Tombol Sprint Ke Tombol action
    void Sensor()
    {
        var ArahMuka = new Vector3(animasiCharacter.GetFloat("moveX"), animasiCharacter.GetFloat("moveY"));
        var PosisiInteraksi = transform.position + ArahMuka;
        var collider = Physics2D.OverlapCircle(PosisiInteraksi, 0.5f, Interaktif);
        bool previousInteractableState = isInteractable;
        isInteractable = collider != null;

        Debug.DrawLine(transform.position, PosisiInteraksi, Color.cyan, 1f);

        if (isInteractable != previousInteractableState)
        {
            // Hanya ganti tombol jika status interaksi berubah
            AnimasiButtonSprintTukarKeAction();
        }      
    }
// Animasi Tukar Tombol Sprint Ke Tombol action
    private void AnimasiButtonSprintTukarKeAction()
    {
        if (isInteractable)
        {
            TukarTombolSprint.SetBool("ActionButton", true);
            TukarTombolSprint.SetBool("SprintButton", false);
            isSprinting = false; // Offkan Fungsi Sprinting ketika akan dialog
        }
        else
        {
            TukarTombolSprint.SetBool("ActionButton", false);
            TukarTombolSprint.SetBool("SprintButton", true);
        }
    }

    // Button Dialog
    void Interaksi()
    {
        var ArahMuka = new Vector3(animasiCharacter.GetFloat("moveX"), animasiCharacter.GetFloat("moveY"));
        var PosisiInteraksi = transform.position + ArahMuka;
        var collider = Physics2D.OverlapCircle(PosisiInteraksi, .5f, Interaktif );
        collider?.GetComponent<Interactable>()?.Interaksi();
        soundEffect.runObjekSound.SetActive(false);
        isAttacking = false;
        isDialogNow = false;
        Debug.Log("Kamu berinteraksi dengan "+collider.name.ToString());
        // Debug.Log("Ini Dialog");
    }

    public void Teleport()
    {
        var ArahMuka = new Vector3(animasiCharacter.GetFloat("moveX"), animasiCharacter.GetFloat("moveY"));
        var PosisiInteraksi = transform.position + ArahMuka;
        var collider = Physics2D.OverlapCircle(PosisiInteraksi, .5f, Interaktif );
        collider?.GetComponent<ITeleport>()?.Teleport();
        soundEffect.runObjekSound.SetActive(false);
        isAttacking = false;
        isTeleporting = false;
        // Debug.Log("Ini teleport");
    }

//  Pergerakan, Sprint......
    void FixedUpdate()
    {
        float movementSpeed = isSprinting ? kecepatanSprint : kecepatanGerak;
        Vector2 movementDirection = isSprinting && !isMoving ? lastMoveDirection : input;
        Vector2 targetPosition = rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime;

        // Check if the path is clear
        if (IsPathClear(targetPosition))
        {
            rb.velocity = movementDirection * movementSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
// Fungsi Klik Tombol 
//  Tombol Sprint dan action (tap)
    public void OnPointerDown(PointerEventData eventData)
    {                
        if (actionButton != null)
        {        
            if (isInteractable == true)
            {
                if (!isDialogNow && !isTeleporting)
                {
                    isDialogNow = true;
                    Interaksi();
                    isTeleporting = true;
                    Teleport();
                }
            }    
            if (isInteractable == false)
            {
                if (isDialogNow == false || isTeleporting == false)
                {
                    soundEffect.runObjekSound.SetActive(true);
                    soundEffect.runSound.Play();
                    isSprinting = true;   
                    Invoke("MunculkanUi", 0f);
                }
            }
            
        }     
        
    }
//  Tombol Attack (tap)
    public void OnPointerDownforSword()
    {
        // if (basicAttack != null && !isAttacking)
        // {
            isAttacking = true;
            SFX.SetActive(true);
            SFXSword.Play();
            BasicAttack();
            animasiCharacter.SetTrigger("SwordAttack");    
            animasiCharacter.SetBool("isMoving", false);        
            // Debug.Log("Kamu Menekan Tombol Attack");            
        // }
    }
// Tombol Skill
    public void PointerDownUntukSkill(PointerEventData eventData)
    {
        if (skillButton != null && !isAttacking)
        {
            // Debug.Log("Kamu Menekan Tombol Skill");
            SkillAttack();            
            isAttacking = true;
        }
    }
// Tombol Special skill
    public void PointerDownUntukSpesialSkill(PointerEventData eventData)
    {
        if (specialSkill != null)
        {
            statprototype.Instance.KuranginDarah(1000);
            Debug.Log("You are using special skill");
        }
    }
//==========================================================================
// Fungsi Tombol Di Lepas
//  Tombol Sprint dan Action
    public void OnPointerUp(PointerEventData eventData)
    {
        // fungsi tombol lari
        if (actionButton != null)
        {
            soundEffect.runSound.Stop();
            soundEffect.runObjekSound.SetActive(false);
            isSprinting = false;
        }
    }
//  Tombol Attack
    public void OnPointerUpforSword()
    {        
        // if (basicAttack != null && isAttacking)
        // {
            isAttacking = false;
            animasiCharacter.ResetTrigger("SwordAttack");
            // Debug.Log("Kamu melepas Tombol Attack");
        // }
    }
//  Tombol Skill
    public void PointerUpUntukSkill(PointerEventData eventData)
    {
        if (skillButton != null && isAttacking)
        {
            isAttacking = false;
            // buat kalo mau tambahin skill
        }
    }
//  Tombol Special skill
    public void PointerUpUntukSpesialSkill(PointerEventData eventData)
    {
        if (specialSkill != null)
        {
            // buat kalo mau tambahin special skill
        }
    }
//==========================================================================
//  menambahkan trigger
    private void AddEventTrigger(GameObject obj, EventTriggerType eventType, Action<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
        entry.callback.AddListener((eventData) => action(eventData));
        trigger.triggers.Add(entry);
    }
//  Ini jangan dihapus bisa error
    private bool IsPathClear(Vector2 targetPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, targetPosition - rb.position, Vector2.Distance(rb.position, targetPosition), objekKeras);
        return hit.collider == null;
    }
//  Serangan
    public void BasicAttack()
{
    // Check if auto-detect should be used, for example, if no movement or nearby enemy.
    if (lastMoveDirection == Vector2.zero || swordAttack.DetectNearestEnemy() != null)
    {
        swordAttack.Attack();  // Auto-detect the direction based on the nearest enemy
    }
    else
    {
        // Manual direction based on movement input
        if (lastMoveDirection.x < 0)
        {
            swordAttack.AttackLeft();
        }
        else if (lastMoveDirection.x > 0)
        {
            swordAttack.AttackRight();
        }
        if (lastMoveDirection.y < 0)
        {
            swordAttack.AttackDown();
        }
        else if (lastMoveDirection.y > 0)
        {
            swordAttack.AttackUp();
        }
    }
}


    bool isCooldown = false;
    // skill attack
    public void SkillAttack()
    {
        if (isCooldown)
        {
            // Jika sedang cooldown, jangan melakukan serangan atau memutar suara
            Debug.Log("Skill sedang cooldown "+skillAttack.cooldownTeks.text+" detik");
            skillAttack.peringatanCooldown.SetActive(true);
            Invoke("PeringatanOff", 0.5f);
            return;
        }

        // Lanjutkan serangan jika tidak sedang cooldown
        if (lastMoveDirection.x < 0)
        {
            skillAttack.AttackHadapKiri();
            isCooldown = true;
            // manggil off cooldown dan berapa detik yang udh di atur di drowned skill
            Invoke("OnKanBoolCooldown", skillAttack.cooldown);
        } 
        else if (lastMoveDirection.x > 0)
        {
            skillAttack.AttackHadapKanan();
            isCooldown = true;
            Invoke("OnKanBoolCooldown", skillAttack.cooldown);
        }
    
    // Putar suara skill jika tidak sedang cooldown
    soundEffect.skillSound.Play();
    StartCoroutine(skillAttack.HitungCooldown());
    }
    public void OnKanBoolCooldown()
    {
        isCooldown = false;
    }
    public void PeringatanOff()
    {
        skillAttack.peringatanCooldown.SetActive(false);
    }

    public void MunculkanUi()
    {
        gameControllers.UI.SetActive(true);
    }

    public void Test()
    {
        Debug.Log("Kamu menekan tombol");
    }
}
