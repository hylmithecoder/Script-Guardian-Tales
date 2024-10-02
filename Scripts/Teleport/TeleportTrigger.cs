using System;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour, ITeleport
{
    [SerializeField] Teleport teleport;
    // public string Scene;
    private LevelLoader levelLoader;
    private string nameScene;
    // bool isFind = false;

    private void Awake() 
    {
        nameScene = teleport.Name;     
        levelLoader = GameObject.Find("CrossFade").GetComponent<LevelLoader>();    
    }

    public void Teleport()
    {
        nameScene = teleport.Name;
        Debug.Log(nameScene);
        StartCoroutine(levelLoader.LoadLevelWithString(nameScene));
        StartCoroutine(levelLoader.AnimateLoadingText());
    }
}