using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertZonaBattle : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;

    public void Interaksi()
    {
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog));
    }
    public void QuestKeTrigger()
    {
        
    }
}
