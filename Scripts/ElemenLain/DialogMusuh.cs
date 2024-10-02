using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KontrolMusuh : MonoBehaviour, Interactable
{
    // Note Masih tes untuk buat pintu masuk
    [SerializeField] Dialog line;
    public void Interaksi()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(line));
    }

}
