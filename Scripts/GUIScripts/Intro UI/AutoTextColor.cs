// Note ini masih Belum worth

using UnityEngine;
using TMPro;

public class AutoTextColor : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // The TextMeshPro component to change color
    public Camera mainCamera;             // The camera to determine background color

    // Threshold for determining if a color is "light" or "dark"
    public float brightnessThreshold = 0.5f; 

    private void Start()
    {
        if (textComponent == null)
        {
            textComponent = GetComponent<TextMeshProUGUI>();
        }

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    private void Update()
    {
        Color backgroundColor = GetBackgroundColor();
        SetTextColorBasedOnBackground(backgroundColor);
    }

    private Color GetBackgroundColor()
    {
        // Assuming the background color is the color seen by the main camera
        // This method can be adjusted if you're detecting specific colors from objects
        return mainCamera.backgroundColor;
    }

    private void SetTextColorBasedOnBackground(Color backgroundColor)
    {
        // Calculate brightness using the perceived luminance formula
        float brightness = backgroundColor.r * 0.299f + backgroundColor.g * 0.587f + backgroundColor.b * 0.114f;

        // Change text color based on brightness
        if (brightness > brightnessThreshold)
        {
            textComponent.color = Color.black; // Set text color to black for light backgrounds
        }
        else
        {
            textComponent.color = Color.white; // Set text color to white for dark backgrounds
        }
    }
}
