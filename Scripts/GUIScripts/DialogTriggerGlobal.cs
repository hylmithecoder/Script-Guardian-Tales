using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTriggerForGlobal : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    private Transform self;

    void Start()
    {
        self = GetComponent<Transform>();
    }
    public void Interaksi()
    {
        DialogManager.Instance.StartDialog(dialog, self);
    }
}
