using System.Collections;
using TMPro;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    public GameObject pedangnya;
    public GameObject teksDamagePrefab;
    public Collider2D hitboxsword;
    public float knockbackForce = 500f;
    public float detectionRange = 5f; // Range for auto-detecting enemies
    public int JumlahSlime;
    private AiMusuh slime;
    public SoundManager lagu;
    public SFXManager sfxSound;
    public StatPedangOP stat;
    private float damage;
    private float critchance;
    private float critdamage;   
    public Vector3 faceRight = new Vector3(0.14f, -0.099f, 0);  
    public Vector3 faceLeft = new Vector3(-0.14f, -0.099f, 0);  
    public Vector3 faceUp = new Vector3(0.026f, 0.234f, 0);  
    public Vector3 faceDown = new Vector3(0.026f, -0.234f, 0);
    [SerializeField] GameObject pembatasMap;
    [SerializeField] GameObject healtBar;
    [SerializeField] GameObject papanhealthBar;
    bool isMoving = false;
    private AiMember knight;

    private void Awake() 
    {
        knight = GameObject.Find("Knight").GetComponent<AiMember>();
    }
    
    public void Start() 
    {
        slime = GameObject.FindWithTag("Slime").GetComponent<AiMusuh>();
        if (hitboxsword == null)
        {
            Debug.LogError("Hitbox sword not set on " + gameObject.name);
        }

        damage = stat.damage;
        critchance = stat.critchance;
        critdamage = stat.critdamage;
    }

    public void Attack()
    {
        Transform targetEnemy = DetectNearestEnemy();

        if (targetEnemy != null)
        {
            // Determine attack direction based on the position of the nearest enemy
            Vector2 direction = (targetEnemy.position - transform.position).normalized;

            if (direction.x > 0.5f) AttackRight();
            else if (direction.x < -0.5f) AttackLeft();
            else if (direction.y > 0.5f) AttackUp();
            else if (direction.y < -0.5f) AttackDown();
        }
        else
        {
            Debug.LogWarning("No enemy detected within range.");
        }
    }

    public Transform DetectNearestEnemy()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, detectionRange);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemiesInRange)
        {
            if (enemy.CompareTag("Slime")) // Assuming "Slime" is the tag for enemies
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = enemy.transform;
                }
            }
        }

        return nearestEnemy;
    }

    public void AttackRight()
    {
        Debug.LogWarning("kanan");
        hitboxsword.enabled = true;
        transform.localPosition = faceRight;
        transform.localRotation = Quaternion.identity;
        Invoke("StopAttack", 0.5f);
    }

    public void AttackLeft()
    {
        Debug.LogWarning("Kiri");
        hitboxsword.enabled = true;
        transform.localPosition = faceLeft;
        transform.localRotation = Quaternion.identity;
        Invoke("StopAttack", 0.5f);
    }

    public void AttackUp()
    {
        Debug.LogWarning("Atas");
        hitboxsword.enabled = true;
        transform.localPosition = faceUp;
        transform.localRotation = Quaternion.Euler(0, 0, 90);
        Invoke("StopAttack", 0.5f);
    }

    public void AttackDown()
    {
        Debug.LogWarning("Bawah");
        hitboxsword.enabled = true;
        transform.localPosition = faceDown;
        transform.localRotation = Quaternion.Euler(0, 0, -90);
        Invoke("StopAttack", 0.5f);
    }

    public void StopAttack()
    {
        hitboxsword.enabled = false;
        Invoke("HideHealthBar", 3f);
    }

    void HideHealthBar()
    {
        healtBar.SetActive(false);
        papanhealthBar.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {            
            if (collider.tag == "Slime")
            {
                SlimeStats slimeStats = collider.GetComponent<SlimeStats>();

                if (slimeStats != null)
                {
                    Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
                    Vector2 direction = (Vector2)(collider.gameObject.transform.position - parentPosition).normalized;
                    Vector2 knockback = direction * knockbackForce;
                    
                    healtBar.SetActive(true);
                    papanhealthBar.SetActive(true);

                    float actualDamage = damage;

                    RectTransform textTransform = Instantiate(teksDamagePrefab).GetComponent<RectTransform>();
                    textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                    Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                    textTransform.SetParent(canvas.transform);                    

                    damageableObject.OnHit(damage, knockback);
                    teksDamagePrefab.GetComponent<TextMeshProUGUI>().text = actualDamage.ToString();

                    if (Random.value < critchance)
                    {                        
                        actualDamage *= critdamage;
                        teksDamagePrefab.GetComponent<TextMeshProUGUI>().text = actualDamage.ToString();
                    } 
                    slimeStats.HealthPoint -= actualDamage;

                    if (slimeStats.GetJumlahSlimeMati() >= JumlahSlime)
                    {
                        HandleEnemyDeath(slimeStats);
                        Debug.Log("Knight jadi musuh sekarang");
                        knight.eventSudahSelesai = true;
                    }
                }
            }        
        }
    }

    private void HandleEnemyDeath(SlimeStats slimeStats)
    {
        slimeStats.animasiSlime.SetBool("isMoving", isMoving);
        pembatasMap.SetActive(false);
        slime.victorySfx.SetActive(true);
        lagu.laguMenang.Play();
        healtBar.SetActive(false);
        papanhealthBar.SetActive(false);   
        StartCoroutine(CekMusikMenang());  
        StartCoroutine(FadeOut(lagu.laguBattle, 1f));
    }

    private IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    private IEnumerator CekMusikMenang()
    {
        yield return new WaitForSeconds(1f);

        while (lagu.laguMenang.isPlaying)
        {
            yield return null;
        }

        lagu.laguDunia.Play();
    }
}
