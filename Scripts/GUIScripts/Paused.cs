using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Paused : MonoBehaviour
{
    [SerializeField] Button keluar;
    [SerializeField] Button kembali;

    private void Start() 
    {
        keluar.onClick.AddListener(() => Application.Quit());    
    }


}
