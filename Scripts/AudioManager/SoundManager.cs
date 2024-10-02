// ini untuk yang ngatur SFX sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource laguDunia;
    public AudioSource laguBattle;
    public AudioSource laguMenang;
    public AudioSource gameOverBgm;
    
    public IEnumerator EfekHilang(AudioSource lagu, float fade)
    {
        float start = lagu.volume;

        while (lagu.volume > 0)
        {
            lagu.volume -= start * Time.deltaTime / fade;
            yield return null;
        }

        lagu.Stop();
        lagu.volume = start;
    }

    public void Start() 
    {
        if (laguBattle != null)
        {
            laguBattle.Stop();
        }

        if (laguDunia != null)
        {
            laguDunia.Play();
        }
    }

    public IEnumerator FadeOutEffect(AudioSource lagu, float fadeTime)
    {
        float startVolume = lagu.volume;

        while (lagu.volume > 0)
        {
            lagu.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        lagu.Stop();
        lagu.volume = startVolume; // Reset volume for next use
    }
}
