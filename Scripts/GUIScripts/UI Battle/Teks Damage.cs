// This Source Code For Teks Damage if hit enemy, and take hit enemy

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TeksDamage : MonoBehaviour
{
    public float waktuAnimasiKeAtas = 0.5f;
    public float floatSpeed;
    public Vector3 arah = new Vector3 (0, 1, 0);
    public TextMeshProUGUI teksDamage;
    public Color warnaAwal;

    RectTransform rectTransform;

    float timeElapsed = 0.0f;

    void Start() 
    {        
        warnaAwal = teksDamage.color;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
            timeElapsed += Time.deltaTime;

            rectTransform.position += arah * floatSpeed * Time.deltaTime;

            teksDamage.color = new Color(warnaAwal.r, warnaAwal.g, warnaAwal.b, 1 - (timeElapsed / waktuAnimasiKeAtas));

            if (timeElapsed > waktuAnimasiKeAtas)
            {
                Destroy(gameObject);
            }
    }
}
