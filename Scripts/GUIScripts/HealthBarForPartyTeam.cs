// Ini Healtbar nya

using UnityEngine;
using UnityEngine.UI;

public class HealthBarForPartyTeam : MonoBehaviour
{
    public Slider slider;
    public Text maxhealthPoint;
    public Text currentHealthPoint;
    public Gradient gradiasi;
    public Image isi;

    // Max HP dari musuh atau character
    private void Start() 
    {
        currentHealthPoint = maxhealthPoint;    
    }
    public void SetMaxHP(float Health) 
    {
        slider.maxValue = Health;
        slider.value = Health;
        isi.color = gradiasi.Evaluate(1f);
    }

    // HP dari musuh atau character
    public void SetHP(float Health) 
    {
        slider.value = Health;        
        isi.color = gradiasi.Evaluate(slider.normalizedValue);
        currentHealthPoint.text = Health.ToString();
    }

}
