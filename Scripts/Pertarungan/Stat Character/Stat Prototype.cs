using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statprototype : MonoBehaviour, IDamageable
{
    public int level;
    public float waktuBermain;
    public GameObject gameOverUi;
    public Animator animasiGameOver;
    private bool disableMovement = false;
    public float currentHealth;
    public float maxHealth;
    private Animator animator;
    public HealthBarForPartyTeam healthBar;
    public SlimeStats slimeStats;
    private Rigidbody2D rb;
    private SoundManager bgm;
    public LevelLoader refresh;
    const string boolGameOver = "isGameOver";
    public GameObject penutupMap;
    // private bool isDead = false;
    public static statprototype Instance {get; private set;}
    public float HealthPoint
    {
        set
        {
            currentHealth = value;
            healthBar.SetHP(currentHealth);

            // trigger kalau darah player 0 ke bawah
            if (value <= 0)
            {
                disableMovement = true;
                animator.SetBool("isAlive", false);
                Debug.Log("Kamu Mati Dari Game");
                gameOverUi.SetActive(true);
                animasiGameOver.SetBool(boolGameOver, true);
                Debug.Log(isAlive);
                StartCoroutine(bgm.FadeOutEffect(bgm.laguDunia, 2));
                bgm.gameOverBgm.Play();
                StartCoroutine(bgm.FadeOutEffect(bgm.laguBattle, 2));
            }
        }
        get 
        {
            return currentHealth;
        }
    }

    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    bool isAlive = true;
    public void Start() 
    {
        Instance = this;
        bgm = GameObject.FindWithTag("GameController").GetComponent<SoundManager>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.currentHealthPoint.text = currentHealth.ToString();
        healthBar.SetMaxHP(maxHealth);
        animator.SetBool("isAlive", isAlive);
        rb = GetComponent<Rigidbody2D>();
        SavePlayer();
    }

    public void Update() 
    {
        // Debug.Log("tes auto save");
        // SavePlayer();
        waktuBermain -= Time.deltaTime;
        // if (waktuBermain < 0)
        // {
        //    isDead = true;
        //    if (isDead == true)
        //     {
        //         KuranginDarah(9999);
        //     }
            
        // }
    }

    public void KuranginDarah(float damage)
    {
        statprototype stat = GetComponent<statprototype>();
        stat.HealthPoint -= damage;
        if (disableMovement)
        {
            rb.simulated = false;
        }

        healthBar.SetHP(currentHealth);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        damage = slimeStats.damageSlime;
        currentHealth -= damage;
        healthBar.SetHP(currentHealth);
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage)
    {
        
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    } 

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        HealthPoint = data.health;
        rb.simulated = true;
        animator.SetBool("isAlive",isAlive);
        animasiGameOver.SetBool(boolGameOver, false);
        bgm.laguDunia.Play();
        StartCoroutine(bgm.FadeOutEffect(bgm.gameOverBgm, 2));
        penutupMap.SetActive(false);
        isAlive = true;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
}
