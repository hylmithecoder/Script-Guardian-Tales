// // Ai Musuh

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SlimeNgejar : MonoBehaviour
// {
//     public GameObject Character;
//     public GameObject pembatasMap;
//     public float kecepatanNgejar;
//     public float jangkauanPendeteksi;
//     public AudioSource laguBattle;
//     public AudioSource laguDunia;
//     public Animator animasiJalan;
//     private bool isNgejar = false;
//     private float distance;
//     private static bool isBattleAudioPlaying = false;
//     private Vector3 posisiawal;

//     // Reference to the SlimeStats script
//     public SlimeStats slimeStats;

//     private void Start()
//     {
//         if (laguBattle != null)
//         {
//             laguBattle.Stop();
//         }
//         posisiawal = transform.position; // Store the original position of the slime
//         slimeStats = GetComponent<SlimeStats>(); // Get the SlimeStats component
//     }

//     public void Update()
//     {
//         if (slimeStats.HealthPoint <= 0)
//         {
//             return; // Stop updating if the slime is dead
//         }

//         distance = Vector2.Distance(transform.position, Character.transform.position);
//         Vector2 arah = Character.transform.position - transform.position;
//         arah.Normalize();
//         // float sudut = Mathf.Atan2(arah.y, arah.x);

//         // Fungsi Trigger Ngejar
//         if (distance < jangkauanPendeteksi)
//         {
//             if (!isNgejar)
//             {
//                 isNgejar = true;
//                 pembatasMap.SetActive(true);
//                 // untuk play lagu battle dan stop lagu dunia
//                 if (!isBattleAudioPlaying && laguBattle != null)
//                 {
//                     laguBattle.Play();
//                     StartCoroutine(EfekHilangAudio(laguDunia, 1f));
//                     isBattleAudioPlaying = true;
//                 }
//             }

//             // ini fungsi untuk ngejar player...
//             transform.position = Vector2.MoveTowards(this.transform.position, Character.transform.position, kecepatanNgejar * Time.deltaTime);
//             // transform.rotation = Quaternion.Euler(Vector3.forward * sudut);

//             if (arah != Vector2.zero)
//             {
//                 animasiJalan.SetBool("isMoving", true);
//                 animasiJalan.SetFloat("moveX", arah.x);
//                 animasiJalan.SetFloat("moveY", arah.y);
//             }
//         }
//         else
//         {
//             if (isNgejar)
//             {
//                 Die();
//                 isNgejar = false;
//                 pembatasMap.SetActive(false);
//                 // untuk stop lagu battle dan play lagu dunia
//                 if (laguBattle != null)
//                 {
//                     StartCoroutine(EfekHilangAudio(laguBattle, 1f));
//                     isBattleAudioPlaying = false;
//                     if (!laguDunia.isPlaying)
//                     {
//                         laguDunia.Play();
//                     }
//                 }
//             }

//             // Fungsi Untuk kembali ketempat semula
//             transform.position = Vector2.MoveTowards(this.transform.position, posisiawal, kecepatanNgejar * Time.deltaTime);
//             if (Vector2.Distance(transform.position, posisiawal) < 0.1f)
//             {
//                 animasiJalan.SetBool("isMoving", false);
//                 animasiJalan.SetFloat("moveX", 0);
//                 animasiJalan.SetFloat("moveY", 0);
//             }
//             else
//             {
//                 Vector2 arahBalik = posisiawal - transform.position;
//                 arahBalik.Normalize();
//                 animasiJalan.SetBool("isMoving", true);
//                 animasiJalan.SetFloat("moveX", arahBalik.x);
//                 animasiJalan.SetFloat("moveY", arahBalik.y);
//             }
//         }

//         // Untuk Monitoring aktifkan ini
//         // if (arah != Vector2.zero)
//         // {
//         //     Debug.Log("Musuh Ngejar Player");
//         //     Debug.Log("kordinat musuh X " + arah.x + ",Y " + arah.y);
//         // }
//     }

//     private void Die()
//     {
//         // Stop the battle music
//         if (laguBattle != null && isBattleAudioPlaying)
//         {
//             laguBattle.Stop();
//             isBattleAudioPlaying = false;
//         }

//         // Play the world music if not playing
//         if (laguDunia != null && !laguDunia.isPlaying)
//         {
//             laguDunia.Play();
//         }

//         // Trigger the defeated animation in SlimeStats
//         slimeStats.Kalah();
//     }

//     private IEnumerator EfekHilangAudio(AudioSource lagu, float waktuhilang)
//     {
//         float startVolume = lagu.volume;

//         while (lagu.volume > 0)
//         {
//             lagu.volume -= startVolume * Time.deltaTime / waktuhilang;
//             yield return null;
//         }

//         lagu.Stop();
//         lagu.volume = startVolume;
//     }
// }
