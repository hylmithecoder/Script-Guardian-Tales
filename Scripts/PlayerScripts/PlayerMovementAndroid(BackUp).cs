// // This Source Code Create by CopyRight 2024 (c) Hylmi Muhammad Fiary Mahdi
// // To use this chat me on hylmimuhammadfiarymahdi@gmail.com
// // Hint : Public void...(), Private void...(), Protected void...() Itu adalah Function atau fungsi
// //       - Source code Ini berhubungan dengan dialog, sword attack, dan lain-lain
// //       - Ke SlimeNgejar Untuk Perbaiki atau update otak Musuh
// //       - Fix Kan dlu untuk model character kalau terkena damage

// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;
// using System;
// using System.Collections;

// public class PlayerMovementAndroid : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
// {
//     // Peletakan Variabel
//     [SerializeField] GameObject SFX;
//     public statprototype stat;
//     public Joystick gerakanJoystick;
//     public float kecepatanGerak;
//     public float kecepatanSprint;
//     private Animator animasiCharacter;
//     public Animator TukarTombolSprint; // Animasi Pertukaran tombol sprint ke action
//     private Vector2 input;
//     private Vector2 lastMoveDirection;    
//     private bool isMoving;
//     private bool isSprinting;
//     private bool isInteracting;
//     private bool isAttacking;
//     private bool isInBattle = false;
//     private bool isPaused = false;
//     private Rigidbody2D rb;
//     public Button sprintButton;
//     public Button basicAttack;
//     public Button skillButton;
//     public Button specialSkill;
//     public LayerMask Interaktif;    
//     public LayerMask objekKeras;
//     public LayerMask LayerPertarungan;
//     public BasicAttack swordAttack;
//     // public statprototype stat;
//     public AudioSource SFXSword;
//     private SoundManager lagu;
//     public SFXManager soundEffect;
//     public float detectionRange = 5.0f; // Add this variable for detection range

//     // Batas Peletakan Variabel
//     private void Awake() 
//     {
//         animasiCharacter = GetComponent<Animator>();
//     }
//     private void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
        
//         // Menambah Triger sprint button untuk lari
//         if (sprintButton != null)
//         {
//             AddEventTrigger(sprintButton.gameObject, EventTriggerType.PointerDown, (data) => OnPointerDown((PointerEventData)data));
//             AddEventTrigger(sprintButton.gameObject, EventTriggerType.PointerUp, (data) => OnPointerUp((PointerEventData)data));
//         }

//         // Menambah Triger sword button untuk attack
//         if (basicAttack != null)
//         {
//             AddEventTrigger(basicAttack.gameObject, EventTriggerType.PointerDown, (data) => OnPointerDownforSword((PointerEventData)data));
//             AddEventTrigger(basicAttack.gameObject, EventTriggerType.PointerUp, (data) => OnPointerUpforSword((PointerEventData)data));
//         }
//         // Menambah triger skill
//         if (skillButton != null)
//         {
//             AddEventTrigger(skillButton.gameObject, EventTriggerType.PointerDown, (data) => PointerDownUntukSkill((PointerEventData)data));
//             AddEventTrigger(skillButton.gameObject, EventTriggerType.PointerUp, (data) => PointerUpUntukSkill((PointerEventData)data));
//         }
//         // Menambah trigger special skill
//         if (specialSkill != null)
//         {
//             AddEventTrigger(specialSkill.gameObject, EventTriggerType.PointerDown,  (data) => PointerDownUntukSpesialSkill((PointerEventData)data));
//             AddEventTrigger(specialSkill.gameObject, EventTriggerType.PointerUp,  (data) => PointerUpUntukSpesialSkill((PointerEventData)data));
//         }
//         // if (LaguBattle != null)
//         // {
//         //     LaguBattle.Stop();
//         // }
        
//     }

//     // Ini buat untuk dialog juga jadi jangan pernah diubah lagi
//     public void HandleUpdate()
//     {
//         // statement pause
//         if (isPaused) return;
//         // Get movement input from Joystick
//         input = gerakanJoystick.Direction;

//         // Update animator based on movement
//         animasiCharacter.SetBool("isMoving", input.magnitude > 0.1f);
//         isMoving = input.magnitude > 0.1f; // Update internal variable

//         // Update arah character terakhir kali joystick bergerak 
//         if (isMoving)
//         {
//             lastMoveDirection = input;            
//         }

//         // Update animasi jalan ke arah yang sesuai
//         animasiCharacter.SetFloat("moveX", lastMoveDirection.x);
//         animasiCharacter.SetFloat("moveY", lastMoveDirection.y);

//         Pendeteksi();
//     }

//     // Tukar Tombol Sprint Ke Tombol action
//     void Pendeteksi()
//     {
//         var ArahMuka = new Vector3(animasiCharacter.GetFloat("moveX"), animasiCharacter.GetFloat("moveY"));
//         var PosisiInteraksi = transform.position + ArahMuka;
//         var collider = Physics2D.OverlapCircle(PosisiInteraksi, 0.2f, Interaktif);
//         isInteracting = collider != null;
//         Debug.DrawLine(transform.position, PosisiInteraksi, Color.cyan, 1f);
//         AnimasiButtonSprintTukarKeAction();
//     }
// // Tukar Tombol Sprint Ke Tombol action
//     private void AnimasiButtonSprintTukarKeAction()
//     {
//         if (isInteracting)
//         {
//             TukarTombolSprint.SetBool("ActionButton", true);
//             TukarTombolSprint.SetBool("SprintButton", false);
//             isSprinting = false; // Offkan Fungsi Sprinting ketika akan dialog
//         }
//         else
//         {
//             TukarTombolSprint.SetBool("ActionButton", false);
//             TukarTombolSprint.SetBool("SprintButton", true);
//         }
//     }

//     // Button Dialog
//     void Interaksi()
//     {
//         var ArahMuka = new Vector3(animasiCharacter.GetFloat("moveX"), animasiCharacter.GetFloat("moveY"));
//         var PosisiInteraksi = transform.position + ArahMuka;
//         var collider = Physics2D.OverlapCircle(PosisiInteraksi, 0.2f, Interaktif | LayerPertarungan);
//         collider?.GetComponent<Interactable>()?.Interaksi();
//         // soundEffect.runObjekSound.SetActive(false);
//         isAttacking = false;
//     }

// //  Pergerakan, Sprint, Action Button..............
//     private void FixedUpdate()
//     {
//         float movementSpeed = isSprinting ? kecepatanSprint : kecepatanGerak;
//         Vector2 movementDirection = isSprinting && !isMoving ? lastMoveDirection : input;
//         Vector2 targetPosition = rb.position + movementDirection * movementSpeed * Time.fixedDeltaTime;

//         // Check if the path is clear
//         if (IsPathClear(targetPosition))
//         {
//             rb.velocity = movementDirection * movementSpeed;
//         }
//         else
//         {
//             rb.velocity = Vector2.zero;
//         }
//         TriggerPertarungan(); 
//     }

//     //  untuk memulai pertarungan tapi layernya gak kepake boleh dihapus tpi devnya gak mau
//     private void TriggerPertarungan()
//     {
//         var tubuh = Physics2D.OverlapCircle(transform.position, 0.2f, LayerPertarungan);
//         if (tubuh != null && !isInBattle)
//         {
//             isInBattle = true;
//             lagu.laguBattle.Play();
//         }
//         else if (tubuh == null && isInBattle)
//         {
//             isInBattle = false;
//             StartCoroutine(FadeOut(lagu.laguBattle, 1f)); // Fade out over 1 second
//         }
//     }
// //  Efek Fading Out Audio
//     private IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
//     {
//         float startVolume = audioSource.volume;

//         while (audioSource.volume > 0)
//         {
//             audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
//             yield return null;
//         }

//         audioSource.Stop();
//         audioSource.volume = startVolume; // Reset volume for next use
//     }
// // Fungsi Klik Tombol 
// //  Tombol Sprint dan action (tap)
//     public void OnPointerDown(PointerEventData eventData)
//     {
//         if (sprintButton != null)
//         {
//             soundEffect.runObjekSound.SetActive(true);
//             soundEffect.runSound.Play();
//             isSprinting = true;
//             //animasiCharacter.SetFloat("moveX", lastMoveDirection.x);
//             //animasiCharacter.SetFloat("moveY", lastMoveDirection.y);            
//             TukarTombolSprint.SetBool("ActionButton", false);
//             TukarTombolSprint.SetBool("SprintButton", true);
//             Interaksi();
//         }
//     }
// //  Tombol Attack (tap)
//     public void OnPointerDownforSword(PointerEventData eventData)
//     {
//         if (basicAttack != null && !isAttacking)
//         {
//             isAttacking = true;
//             SFX.SetActive(true);
//             SFXSword.Play();
//             AdjustFacingDirection(); // Add this line to adjust facing direction
//             BasicAttack();
//             animasiCharacter.SetTrigger("SwordAttack");            
//             // Debug.Log("Kamu Menekan Tombol Attack");            
//         }
//     }
// // Tombol Skill
//     public void PointerDownUntukSkill(PointerEventData eventData)
//     {
//         if (skillButton != null)
//         {
//             Debug.Log("Kamu Menekan Tombol Skill");
//         }
//     }
// // Tombol Special skill
//     public void PointerDownUntukSpesialSkill(PointerEventData eventData)
//     {
//         if (specialSkill != null)
//         {
//             soundEffect.buttonSfxObject.SetActive(true);
//             soundEffect.buttonSound.Play();
//             stat.KuranginDarah(20);
//             Debug.Log("Kamu Menekan Tombol Spesial Skill");
//         }
//     }
// //==========================================================================
// // Fungsi Tombol Di Lepas
// //  Tombol Sprint dan Action
//     public void OnPointerUp(PointerEventData eventData)
//     {
//         // fungsi tombol lari
//         if (sprintButton != null)
//         {
//             soundEffect.runSound.Stop();
//             soundEffect.runObjekSound.SetActive(false);
//             isSprinting = false;
//             animasiCharacter.SetFloat("moveX", lastMoveDirection.x);
//             animasiCharacter.SetFloat("moveY", lastMoveDirection.y);
//             animasiCharacter.SetBool("isMoving", true);
//             animasiCharacter.SetBool("isMoving", input.magnitude > 0.1f);
//         }
//     }
// //  Tombol Attack
//     public void OnPointerUpforSword(PointerEventData eventData)
//     {        
//         if (basicAttack != null && isAttacking)
//         {
//             isAttacking = false;
//             animasiCharacter.ResetTrigger("SwordAttack");
//             // Debug.Log("Kamu melepas Tombol Attack");
//         }
//     }
// //  Tombol Skill
//     public void PointerUpUntukSkill(PointerEventData eventData)
//     {
//         if (skillButton != null)
//         {
//             // buat kalo mau tambahin skill
//         }
//     }
// //  Tombol Special skill
//     public void PointerUpUntukSpesialSkill(PointerEventData eventData)
//     {
//         if (specialSkill != null)
//         {
//             // buat kalo mau tambahin special skill
//         }
//     }
// //==========================================================================
// //  menambahkan trigger
//     private void AddEventTrigger(GameObject obj, EventTriggerType eventType, Action<BaseEventData> action)
//     {
//         EventTrigger trigger = obj.GetComponent<EventTrigger>() ?? obj.AddComponent<EventTrigger>();
//         EventTrigger.Entry entry = new EventTrigger.Entry { eventID = eventType };
//         entry.callback.AddListener((eventData) => action(eventData));
//         trigger.triggers.Add(entry);
//     }
// //  Cek Path
//     private bool IsPathClear(Vector2 targetPosition)
//     {
//         RaycastHit2D hit = Physics2D.Raycast(rb.position, targetPosition - rb.position, Vector2.Distance(rb.position, targetPosition), objekKeras);
//         return hit.collider == null;
//     }
// //  Serangan
//     public void BasicAttack()
//     {
//         if (lastMoveDirection.x < 0)
//         {
//             swordAttack.AttackLeft();
//         } else if (lastMoveDirection.x > 0)
//         {
//             swordAttack.AttackRight();
//         }
//     }

//     // Menyesuaikan Arah Auto Aim Muka Karakter Tapi Masih Belum Work Juga
//     private void AdjustFacingDirection()
//     {
//         Collider2D musuhTerdekat = FindNearestEnemy();
//         if (musuhTerdekat != null)
//         {
//             Vector2 directionToEnemy = (musuhTerdekat.transform.position - transform.position).normalized;
//             lastMoveDirection = directionToEnemy;

//             animasiCharacter.SetFloat("moveX", lastMoveDirection.x);
//             animasiCharacter.SetFloat("moveY", lastMoveDirection.y);
//         }
//     }

//     // Auto AIM Ke Musuh Tapi Masih belum work
//     private Collider2D FindNearestEnemy()
//     {
//         Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, detectionRange, LayerPertarungan);
//         Collider2D musuhTerdekat = null;
//         float shortestDistance = Mathf.Infinity;

//         foreach (Collider2D enemy in enemies)
//         {
//             float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
//             if (distanceToEnemy < shortestDistance)
//             {
//                 shortestDistance = distanceToEnemy;
//                 musuhTerdekat = enemy;
//             }
//         }

//         return musuhTerdekat;
//     }
// }
