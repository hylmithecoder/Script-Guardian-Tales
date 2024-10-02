// penambahan UI teks damage
// Ini backup knockback dan bisa kenak damage

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DamageableChar : MonoBehaviour, IDamageable
{
   // Variabel Stat
    public float damage;
    public GameObject teksDamage;
    public Animator animasi;
    private SFXManager sfx;
    public float health;
    public HealthBar healthBar; // Tambahkan ini
    public float maxHealth; // Nilai maksimal health
    private Rigidbody2D rb;

    public float HealthPoint
   {
        set 
        {
            if (value < health)
            {
                animasi.SetTrigger("Hit");  
                sfx.hitSfxObject.SetActive(true);
                sfx.hitSound.Play();
                RectTransform teksDamageTransform = Instantiate(teksDamage).GetComponent<RectTransform>();
                teksDamageTransform.SetParent(transform);
                teksDamageTransform.anchoredPosition = new Vector2(0, 0.5f);
                teksDamageTransform.GetComponent<TextMesh>().text = "-" + value.ToString();
            }
            health = value;
            healthBar.SetHP(health, gameObject.tag); // Perbarui health bar
            if (health <= 0)
            {
                // animasiSlime.SetBool("isMoving", false);                
                animasi.SetBool("isAlive", false);
                Debug.Log("Slime Darahnya : " + health);
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
        health = maxHealth; // Inisialisasi health ke nilai maksimal
        healthBar.SetMaxHP(maxHealth); // Set nilai maksimal health bar
        animasi = GetComponent<Animator>();
        animasi.SetBool("isAlive", isAlive); // Baris Ini Kalo False Troll asli itu musuhnya
        
        rb = GetComponent<Rigidbody2D>();

        sfx = GameObject.Find("GameController").GetComponent<SFXManager>();
    }


    public void HapusMusuh()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback)
    {

        // Implementasi jauhnya knockback
        rb.AddForce(knockback, ForceMode2D.Impulse);
        Debug.Log("Kordinat KnockBack : " + knockback);
    }

    public void OnHit(float damage)
    {
        health -= damage;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        IDamageable damageable = col.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.OnHit(damage);
        }
    }
}
