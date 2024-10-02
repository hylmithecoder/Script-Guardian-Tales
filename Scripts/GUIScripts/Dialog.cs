using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialog
{
    public string name;
    [TextArea(3, 10)]
    [SerializeField] List<string> lines;

    public List<string> Baris
    {
        get {return lines;}
    }
}
