using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BaseStatHero : MonoBehaviour, IDamageable
{
    public HealthBarForPartyTeam healthBar;
    public float baseHealth;
    public float maxHealth;
    public int level;
    public GameObject gameOverUi;
    public Animator gameOver;
    // public GameObject penutupMap;

    private SoundManager bgm;    
    private SlimeStats slimeStats;
    private Animator animator;
    private bool disableMovement = false;
    private Rigidbody2D rb;
    public static BaseStatHero Instance {get; private set;}
    public LevelLoader levelLoader;
    public GameObject penutupMap;

    public float HealthPoint
    {
        set 
        {
            baseHealth = value;
            healthBar.SetHP(baseHealth);

            if (value <= 0)
            {
                disableMovement = true;
                animator.SetBool("isAlive", false);
                Debug.Log("Kamu Mati Dari Game");
                gameOverUi.SetActive(true);
                gameOver.SetBool("isGameOver", true);
                StartCoroutine(bgm.FadeOutEffect(bgm.laguDunia, 2));
                bgm.gameOverBgm.Play();
                StartCoroutine(bgm.FadeOutEffect(bgm.laguBattle, 2));
            }
        }
        get
        {
            return baseHealth;
        }
    }

    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake() 
    {
        bgm = GameObject.FindWithTag("GameController").GetComponent<SoundManager>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        slimeStats = GameObject.FindWithTag("Slime").GetComponent<SlimeStats>();
    }

    bool isAlive = true;
    void Start() 
    {
        baseHealth = maxHealth;
        healthBar.currentHealthPoint.text = baseHealth.ToString();   
        animator.SetBool("isAlive", isAlive);
        Instance = this;
        healthBar.SetMaxHP(maxHealth);
        SavePlayer();
    }

    public void TesMati(int damage)
    {
        HealthPoint -= damage;
        Debug.LogError("Tes Meninggal");
        if (disableMovement)
        {
            rb.simulated = false;
        }
        healthBar.SetHP(HealthPoint);
    }

    public void OnHit(float damage, Vector2 knockback)
    {
        damage = slimeStats.damageSlime;
        baseHealth -= damage;
        healthBar.SetHP(baseHealth);
        rb.AddForce(knockback, ForceMode2D.Impulse);
    }

    public void OnHit(float damage)
    {
        
    }

    public void SavePlayer()
    {
        // SaveSystem.SavePlayer(this);
    } 

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        HealthPoint = data.health;
        rb.simulated = true;
        animator.SetBool("isAlive",isAlive);
        gameOver.SetBool("isGameOver", false);
        bgm.laguDunia.Play();
        StartCoroutine(bgm.FadeOutEffect(bgm.gameOverBgm, 2));
        // penutupMap.SetActive(false);

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }
    
    // public void OnHit(float damage)
    // {
        
    // }
}