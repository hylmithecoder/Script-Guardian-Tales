using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialScene : MonoBehaviour
{
    public float prologTime;
    public PlayableDirector prologScene;
    public LevelLoader levelLoader;
    private string input;
    private TMP_InputField inputNama;
    
    private void Start() 
    {
        StartCoroutine(levelLoader.AnimateLoadingText());    
    }
    void Update()
    {
        // bisa dijadikan referensi waktu berkurang
        prologTime -= Time.deltaTime;
        Debug.Log("Detik tersisa "+prologTime+" Detik");
        if (prologTime <= 0)
        {
            StartCoroutine(levelLoader.LoadLevelWithString("MainScene"));
        }
    }

    public void ReadInputString(string s)
    {
        input = s;
        Debug.Log(s);
    }
}
