using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KontrolNPC : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog questDialog;
    public int targetVarQuest;
    private AiMember variabelQuest;
    private AiMember knight;
    private AiMember wizard;
    private Transform Self;
    const string wizardObject = "NPC Wizard";

    private void Start()
    {
        variabelQuest = GameObject.Find("Knight").GetComponent<AiMember>();      
        knight = GameObject.Find("Knight").GetComponent<AiMember>();
        wizard = GameObject.Find(wizardObject).GetComponent<AiMember>();
    }
    public void Interaksi()
    {
        //  Implementasi variabel nya
        if (variabelQuest.GetCountVariabelQuest() >= targetVarQuest)
        {
            DialogManager.Instance.StartDialog(questDialog, Self);
            Debug.Log("Kondisi dimana variabel quest nya sudah lebih atau sama dengan 2 ");
            knight.eventSudahSelesai = true;
            wizard.eventSudahSelesai = true;
        }
        else 
        {
            DialogManager.Instance.StartDialog(dialog, Self);;
        }
        

    }
}
