using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Teleport
{
    [SerializeField] string nameOfScene;
    public string Name
    {
        get {return nameOfScene;}
    }
}
