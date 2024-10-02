// Control This From GameController Object In Unity Editor

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource basicAttackSound;
    public AudioSource buttonSound;
    public AudioSource runSound;
    public AudioSource hitSound;
    public AudioSource skillSound;
    public GameObject runObjekSound;
    public GameObject swordSfxObject;
    public GameObject buttonSfxObject;
    public GameObject tornadoSfxObject;
    public GameObject hitSfxObject;
    // public GameObject sfxOn;
    // public GameObject sfxButtonObject;

    public void buttonSfx()
    {
        buttonSfxObject.SetActive(true);
        buttonSound.Play();
    }

    private void Start() {
        buttonSfxObject.SetActive(true);
    }
}
