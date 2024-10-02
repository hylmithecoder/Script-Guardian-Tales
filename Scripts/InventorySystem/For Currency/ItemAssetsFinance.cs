using System;
using UnityEngine;

public class ItemAssetsFinance : MonoBehaviour
{
    public static ItemAssetsFinance Instance {get; private set;}
    private void Awake() 
    {
        Instance = this;
    }
    
    public Sprite diamondSprite;
    public Sprite staminaSprite;
}