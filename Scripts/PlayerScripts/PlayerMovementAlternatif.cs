using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAlternatif : MonoBehaviour
{
    public float walkSpeed;  // Default walking speed
    public float sprintSpeed; // Speed when holding shift
    private BasicAttack basicAttack;
    private PlayerMovementAndroid asAttack;
    private SFXManager sfx;
    private bool isMoving;
    private bool isSprinting; 
    // private bool IniInteraktif;
    public LayerMask Interaktif;// Flag for sprint state
    private Vector2 input;
    private Animator animasikarakter;

    public LayerMask ObjekKerasLayer;

    private void Awake()
    {
        animasikarakter = GetComponent<Animator>();
        basicAttack = GetComponent<BasicAttack>();
        asAttack = GetComponent<PlayerMovementAndroid>();
        //animasi1 = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            // Improved debug logging (optional)
            // if (Input.anyKey)
            // {
            //     Debug.Log("Input X: " + input.x + ", Input Y: " + input.y);
            // }

            if (input.x != 0) input.y = 0; // Restrict diagonal movement (optional)

            if (input != Vector2.zero)
            {
                isMoving = true;

                animasikarakter.SetFloat("moveX", input.x);
                animasikarakter.SetFloat("moveY", input.y);
            }
        }
        animasikarakter.SetBool("isMoving", isMoving);
        // Handle movement and sprinting logic
        if (isMoving)
        {
            float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;
            Vector3 targetPosition = transform.position + new Vector3(input.x, input.y) * currentSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

            // Check for stopping conditions (optional)
            if (Vector3.Distance(transform.position, targetPosition) <= Mathf.Epsilon)
            {
                isMoving = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            sfx = GetComponent<SFXManager>();
            sfx.swordSfxObject.SetActive(true);
            sfx.basicAttackSound.Play();
        }
        
        animasikarakter.SetBool("isMoving",isSprinting);

        if (Input.GetKeyDown(KeyCode.Q))
            Interaksi();
    }

    void FixedUpdate() // Use FixedUpdate for physics-related actions (optional)
    {
        // Detect shift key being pressed/released for sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift);        
    }
    void Interaksi()
    {
        var ArahMuka = new Vector3(animasikarakter.GetFloat("moveX"), animasikarakter.GetFloat("moveY"));
        var PosisiInteraksi = transform.position + ArahMuka;

        Debug.DrawLine(transform.position, PosisiInteraksi, Color.blue, 1f);

        var collider = Physics2D.OverlapCircle(PosisiInteraksi, 0.2f, Interaktif);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interaksi();
        }
    }
}
