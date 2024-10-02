using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float kecepatanGerak;

    public float Sprint;

    private bool Bergerak;

    private bool IniSprint;

    private Vector2 input;

    public LayerMask Interaktif;

    private Animator animasi;

    public LayerMask ObjekKerasLayer;

    private void Awake() 
    {
        animasi = GetComponent<Animator>();    
    }

    private void Update()
    {
        if (!Bergerak)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            Debug.Log("Ini Adalah Input X "+ input.x+" "+input.y);
            // Debug.Log("Ini adalah input y "+ input.x+" "+input.y);

            if (input.x != 0) input.y = 0;
            
            if (input != Vector2.zero)
            {
                animasi.SetFloat("moveX", input.x);
                animasi.SetFloat("moveY", input.y);

                var PosisiTarget = transform.position;
                PosisiTarget.x += input.x;
                PosisiTarget.y += input.y;

                if (IniBerjalan(PosisiTarget))
                StartCoroutine(Gerak(PosisiTarget));
            }
        }
        if (Bergerak) 
        {
            float currentSpeed = IniSprint ? Sprint : kecepatanGerak;
            Vector3 PosisiTarget = transform.position + new Vector3(input.x, input.y) * currentSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, PosisiTarget) <= Mathf.Epsilon)
            { 
                IniSprint = false;
            }
        }
        animasi.SetBool("isMoving", Bergerak);
        if (Input.GetKeyDown(KeyCode.Z))
            Interaksi();
    }
    IEnumerator Gerak(Vector3 PosisiTarget)
    {
        Bergerak = true;

        while ((PosisiTarget - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, PosisiTarget, kecepatanGerak * Time.deltaTime);
            yield return null;
        }
        transform.position = PosisiTarget;

        Bergerak = false;
    }
    private void FixedUpdate()
    {
        IniSprint = Input.GetKey(KeyCode.LeftShift);
    }
    private bool IniBerjalan(Vector3 PosisiTarget)
    {
        if(Physics2D.OverlapCircle(PosisiTarget, 0.2f, ObjekKerasLayer | Interaktif) != null)
        {
            return false;
        }

        return true;
    }

    void Interaksi()
    {
        var ArahMuka = new Vector3(animasi.GetFloat("moveX"), animasi.GetFloat("moveY"));
        var PosisiInteraksi = transform.position + ArahMuka;

        Debug.DrawLine(transform.position, PosisiInteraksi, Color.red, 1f);

        var collider = Physics2D.OverlapCircle(PosisiInteraksi, 0.2f, Interaktif);
        if(collider != null)
        {
            collider.GetComponent<Interactable>()?.Interaksi();
        }
    }
}
