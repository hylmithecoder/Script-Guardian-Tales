// Ini Skill nya

using System.Collections;
using UnityEngine;
using TMPro;

public class DrownedSkillButton : MonoBehaviour
{
    [SerializeField] GameObject healthBar;
    [SerializeField] GameObject papanHealthBar;
    public GameObject cooldownUi;
    public GameObject teksDamagePrefab;
    public GameObject peringatanCooldown;
    public SFXManager sfx;
    public TextMeshProUGUI cooldownTeks;    
    public Collider2D hitboxTornado;
    public StatPedangOP statPedangOP;
    public Vector3 faceRight = new Vector3(0.79f, -0.079f, 0);
    public Vector3 faceLeft = new Vector3(-0.79f, -0.079f, 0);

    public float cooldown;
    public float statSkill;
    private float baseDamage;
    private float skillDamage;
    private float critchance;
    private float critdamage;
    private Animator tornado;
    public GameObject self;
    private int countEnemy = 3;
    private AiMember knight;
    private bool isCooldown = false; // Track cooldown state

    private void Awake() 
    {
        knight = GameObject.Find("Knight").GetComponent<AiMember>();  
    }
    void Start()
    {
        sfx = GetComponent<SFXManager>();
        tornado = GetComponent<Animator>();
        if (self != null)
        {
            self.SetActive(false);
        }
        if (hitboxTornado == null)
        {
            Debug.LogWarning("Hitbox tornado not set on " + gameObject.name);
        }
        baseDamage = statPedangOP.damage;
        skillDamage = baseDamage * statSkill;
        critchance = statPedangOP.critchance;
        critdamage = statPedangOP.critdamage;
    }

    public void AttackHadapKanan()
    {
        if (!isCooldown) // Cek apakah lagi cooldown
        {            
            transform.localPosition = faceRight;

            self.SetActive(true);
            tornado.SetTrigger("Attack");
            hitboxTornado.enabled = true;
            Invoke("StopAttack", 0.5f);
        }
        else 
        {
            Debug.Log("Skill Cooldown : " + cooldownTeks.text + " Detik.");
            peringatanCooldown.SetActive(true);
            Invoke("PeringatanOff", 0.5f);
        }
    }

    public void AttackHadapKiri()
    {
        if (!isCooldown) // Cek apakah lagi cooldown 
        {
            transform.localPosition = faceLeft;

            self.SetActive(true);
            tornado.SetTrigger("Attack");
            hitboxTornado.enabled = true;
            Invoke("StopAttack", 0.5f);
        }
        else 
        {
            Debug.Log("Skill Cooldown : " + cooldownTeks.text + " Detik.");
            peringatanCooldown.SetActive(true);
            Invoke("PeringatanOff", 0.5f);
        }
    }

    void StopAttack()
    {
        hitboxTornado.enabled = false;
    }

    public void PeringatanOff()
    {
        peringatanCooldown.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D musuhnya)
    {
        IDamageable damageable = musuhnya.GetComponent<IDamageable>();
        if (damageable != null && musuhnya.tag == "Slime")
        {
            SlimeStats slimeStats = musuhnya.GetComponent<SlimeStats>();
            if (slimeStats != null)
            {
                healthBar.SetActive(true);
                papanHealthBar.SetActive(true);

                RectTransform textTransform = Instantiate(teksDamagePrefab).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);

                float actualDamage = skillDamage;
                teksDamagePrefab.GetComponent<TextMeshProUGUI>().text = actualDamage.ToString();

                if (Random.value < critchance)
                {
                    actualDamage *= critdamage;
                }
                slimeStats.HealthPoint -= actualDamage;

                if (slimeStats.GetJumlahSlimeMati() >= countEnemy)
                {
                    slimeStats.animasiSlime.SetBool("isMoving", false);
                    
                }
            }
        }
    }
    // cooldown
    public IEnumerator HitungCooldown()
    {
        isCooldown = true;
        cooldownUi.SetActive(true);

        float remainingCooldown = cooldown;

        while (remainingCooldown > 0)
        {
            cooldownTeks.text = remainingCooldown.ToString("F0"); // Update text with 1 decimal place
            yield return new WaitForSeconds(0.1f); // Update every 0.1 second
            remainingCooldown -= 0.1f;
            remainingCooldown = Mathf.Max(remainingCooldown, 0);
        }

        cooldownTeks.text = "0"; // Ensure it ends at 0
        yield return new WaitForSeconds(0.001f);
        hitboxTornado.enabled = false;
        cooldownUi.SetActive(false);
        isCooldown = false;
    }
}
