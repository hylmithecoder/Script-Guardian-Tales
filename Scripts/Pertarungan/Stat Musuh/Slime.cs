// penambahan UI teks damage

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeStats : MonoBehaviour, IDamageable
{
   // Variabel Stat
    public float damageSlime;
    public statprototype darahPlayer;
    public GameObject teksDamagePrefab;
    public Animator animasiSlime;
    public SFXManager sfx;
    public float health;
    public HealthBar healthBar; // Tambahkan ini
    public float maxHealth; // Nilai maksimal health
    private Rigidbody2D rb;
    // Variabel Quest
    private AiMember questVariabel;
    private static int SlimenyangMati = 0;
    private BaseStatHero player;

    private void Awake() 
    {
        player = GameObject.Find("Player").GetComponent<BaseStatHero>();
        darahPlayer = GameObject.Find("Player").GetComponent<statprototype>();
        healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();        
    }
    public float HealthPoint
   {
        set 
        {
            if (value < health)
            {
                animasiSlime.SetTrigger("Hit");  
                sfx.hitSfxObject.SetActive(true);
                sfx.hitSound.Play();                  
            }
            health = value;
            healthBar.SetHP(health, gameObject.tag); // Perbarui health bar
            if (health <= 0)
            {
                // animasiSlime.SetBool("isMoving", false);      
                questVariabel.requeirementVariabel += 1;  
                Debug.Log("var quest from slime "+questVariabel.GetCountVariabelQuest());    
                animasiSlime.SetBool("isAlive", false);
                SlimenyangMati += 1;
                Debug.Log("Slime yang Mati udah "+SlimenyangMati);
            }
        } 
        get
        {
            return health;
        }  
   }

    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    bool isAlive = true;
    public void Start() 
    {
        questVariabel = GameObject.Find("Knight").GetComponent<AiMember>();
        health = maxHealth; // Inisialisasi health ke nilai maksimal
        healthBar.SetMaxHP(maxHealth); // Set nilai maksimal health bar
        animasiSlime = GetComponent<Animator>();
        animasiSlime.SetBool("isAlive", isAlive); // Baris Ini Kalo False Troll asli itu musuhnya
        // healthBar.gameObject.SetActive(false);
        
        rb = GetComponent<Rigidbody2D>();

        sfx = GameObject.Find("GameController").GetComponent<SFXManager>();
    }
    bool setTimeHealtbar = false;
    private void Update()
    {
        // OnCollisionEnter2D(self);
        float timeHealtBar = 1;
        timeHealtBar -= Time.deltaTime;
        if (timeHealtBar >= 0 && setTimeHealtbar == false)
        {
            healthBar.gameObject.SetActive(false);
            setTimeHealtbar = true;
            Debug.Log("Time Healtbar = "+timeHealtBar);
        }
    }


    public void HapusMusuh()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback)
    {

        // Implementasi jauhnya knockback
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage)
    {
        health -= damage;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        IDamageable damageable = col.gameObject.GetComponent<IDamageable>();
        
        if (damageable != null)
        {
            if (col.gameObject.tag == "Player")
            {
                if (darahPlayer != null)
                {
                    float knockbackForce = 20;
                    Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;

                    Vector2 direction = (Vector2) ( col.gameObject.transform.position - parentPosition).normalized;
                    Vector2 knockback = direction * knockbackForce;

                    float actualDamage = damageSlime;
                    damageable.OnHit(actualDamage, knockback);
                    darahPlayer.HealthPoint -= actualDamage;
                    Debug.Log("Kamu di hit rimuru\nDarah mu "+darahPlayer.HealthPoint);
                }                
            }
        }
       
    }

    public int GetJumlahSlimeMati()
    {
        return SlimenyangMati;
    }
}
