// Ini Healtbar nya

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Text namaMusuh;
    public Text healthPoint;
    public Text maxhealthPoint;
    public Gradient gradiasi;
    public Image isi;

    // Max HP dari musuh atau character
    public void SetMaxHP(float Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
        isi.color = gradiasi.Evaluate(1f);
    }

    // HP dari musuh atau character
    public void SetHP(float Health, string tagMusuh = null)
    {
        slider.value = Health;        
        isi.color = gradiasi.Evaluate(slider.normalizedValue);

        // Nama Musuh dan HP
        if (!string.IsNullOrEmpty(tagMusuh))
        {
            namaMusuh.text = tagMusuh;
            healthPoint.text = Health.ToString();
            maxhealthPoint.text = slider.maxValue.ToString();
        }
    }

}
