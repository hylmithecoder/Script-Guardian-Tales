// This Source Code Is AI or Brain Of enemy
// TODO : Jangan Pernah Pakai lagu = GetComponent<AudioSource>(); untuk lagu karena udah public
//        Jangan Pernah pakai Mathf2.Atan nnti jadi horizontal 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMusuh : MonoBehaviour
{
    // Klo mau buat lagu diambil dari yang lain
    public SoundManager lagu;
    [SerializeField] Collider2D hitBoxbasicAttack;
    [SerializeField] GameObject pembatasMap;
    public GameObject victorySfx; // Variabel Ini di kontrol di SworAttack.cs
    public LayerMask PenghalangGerak;
    public GameObject Character;
    public float kecepatanNgejar;
    public float jangkauanPendeteksi;
    public float jarakBerhenti;
    public float menjauh;
    public Animator animasiMusuh;
    private bool isNgejar = false;  // To track if the slime is chasing
    private static bool isBattleAudioPlaying = false;  // To track if the battle audio is playing
    private float distance;
    private Vector3 posisiawal;

    private void Start() 
    {
        lagu = GameObject.Find("GameController").GetComponent<SoundManager>();
        Character = GameObject.Find("Player");
        // victorySfx = GameObject.Find("SFX Menang");
        if (victorySfx != null && lagu != null)
        {
            Debug.Log("Sfx enable");
        }
        hitBoxbasicAttack = Character.GetComponent<BoxCollider2D>();
        if (Character == null)
        {
            Debug.Log("Character not found");
        }
        if (lagu.laguBattle != null)
        {
            lagu.laguBattle.Stop();
        }    
        posisiawal = transform.position;
    }

    public void Update()
    {
        distance = Vector2.Distance(transform.position, Character.transform.position);
        Vector2 arah = Character.transform.position - transform.position;
        arah.Normalize();
        
        // Fungsi Trigger Ngejar
        if (distance < jangkauanPendeteksi && distance > jarakBerhenti)
        {
            if (!isNgejar)
            {
                // Start chasing
                isNgejar = true;
                if (!isBattleAudioPlaying && lagu.laguBattle != null)
                {
                    Debug.Log("Kamu memasuki pertarungan");
                    pembatasMap.SetActive(true);
                    lagu.laguBattle.Play();
                    lagu.laguDunia.Stop();
                    lagu.laguMenang.Stop();
                    isBattleAudioPlaying = true;
                }
            }
            transform.position = Vector2.MoveTowards(this.transform.position, Character.transform.position, kecepatanNgejar * Time.deltaTime);
            // statement animasi pergerakan musuh
            if (arah != Vector2.zero)
            {
                animasiMusuh.SetBool("isMoving", true);
                animasiMusuh.SetFloat("moveX", arah.x);
                animasiMusuh.SetFloat("moveY", arah.y);
            }           
        }
        // statement kalau musuh sudah dekat dengan variabel jarakberhenti dan bisa menjauh dari pemain
        else if (distance <= jarakBerhenti)
        {
            animasiMusuh.SetBool("isMoving", false);
            animasiMusuh.SetFloat("moveX", 0);
            animasiMusuh.SetFloat("moveY", 0);

            // Statement menjauh dari player
            if (/*Character.transform.position.x < transform.position.x &&*/ distance < menjauh)
            {
                // Slime menjauh dari pemain
                animasiMusuh.SetBool("isMoving", true);
                Vector2 arahMenjauh = transform.position - Character.transform.position;
                arahMenjauh.Normalize();
                transform.position = Vector2.MoveTowards(transform.position, (Vector3)transform.position + (Vector3)arahMenjauh, kecepatanNgejar * Time.deltaTime);
                animasiMusuh.SetFloat("moveX", arahMenjauh.x);
                animasiMusuh.SetFloat("moveY", arahMenjauh.y);
            }
        }
        
        // statement kalau kita keluar dari jangkauan musuhnya
        else if (distance > jangkauanPendeteksi)
        {
            if (isNgejar)
            {
                // Stop chasing
                print("Kamu keluar dari pertarungan");
                isNgejar = false;
                if (lagu.laguBattle != null)
                {
                    isBattleAudioPlaying = false;
                    if (!lagu.laguDunia.isPlaying)
                    {
                        pembatasMap.SetActive(false);
                        lagu.laguDunia.Play();
                        StartCoroutine(EfekHilang(lagu.laguBattle, 2.5f));
                    }
                }
            }
            MusuhBalikKeposisiawal();
        }
    }

// Fungsi musuh untuk balik ke posisi awal
    private void MusuhBalikKeposisiawal()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, posisiawal, kecepatanNgejar * Time.deltaTime);
            
        if (Vector2.Distance(transform.position, posisiawal) < 0.1f)
        {
            animasiMusuh.SetBool("isMoving", false);
            animasiMusuh.SetFloat("moveX", 0);
            animasiMusuh.SetFloat("moveY", 0);
        }
        else
        {
            Vector2 arahBalik = posisiawal - transform.position;
            arahBalik.Normalize();
            animasiMusuh.SetBool("isMoving", true);
            animasiMusuh.SetFloat("moveX", arahBalik.x);
            animasiMusuh.SetFloat("moveY", arahBalik.y);
        }
    }
    // Efek Hilang lagu
    private IEnumerator EfekHilang(AudioSource lagu, float fade)
    {
        float start = lagu.volume;

        while (lagu.volume > 0)
        {
            lagu.volume -= start * Time.deltaTime / fade;
            yield return null;
        }

        lagu.Stop();
        lagu.volume = start;
    }
}
