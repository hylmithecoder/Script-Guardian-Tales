// Transisi Pindah Scene 

using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour 
{
    public GameObject levelLoader;
    public Animator transisiScene;
    public Text loadingTeks;
    public float waktuTransisi;

    private void Start() 
    {
        levelLoader.SetActive(false);
    }

    private void Update() 
    {
        // Harus pakai ini supaya game object dialog yang di initiate tidak muncul di game object cross fade pada levelloader
        // float time = 2;
        // time -= Time.deltaTime;
        // if (time <= 0)
        // {
        //     levelLoader.SetActive(true);
        //     if (levelLoader != null)
        //     {
        //         Debug.Log("Level loader object");
        //     }
        //     float timeLittle = 0.5f;
        //     timeLittle -= Time.deltaTime;
        //     if (timeLittle <= 0)
        //     {
        //         levelLoader.SetActive(false);
        //     }
        // }            
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        levelLoader.SetActive(true);
        transisiScene.SetTrigger("Start");
        yield return new WaitForSeconds(waktuTransisi);
        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator LoadLevelWithString(string levelName)
    {
        levelLoader.SetActive(true);
        transisiScene.SetTrigger("Start");
        yield return new WaitForSeconds(waktuTransisi);
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator AnimateLoadingText()
    {
        string baseText = "Loading";
        int dotCount = 0;

        while (true)
        {
            dotCount = (dotCount + 1) % 4; // cycle between 0 to 3
            loadingTeks.text = baseText + new string('.', dotCount);
            yield return new WaitForSeconds(0.5f); // adjust for desired speed
        }
    }
}