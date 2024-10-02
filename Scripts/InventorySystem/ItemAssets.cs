using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    // private Inventory inventory;
    
    private void Awake() 
    {
        Instance = this;            
    }


    // Varibel Manager Sprite Item
    public Transform pfItemWorld;
    public TextMeshProUGUI stak;
    public Sprite pedangTutorialSprite;
    public Sprite purpleCoinSprite;
    public Sprite pedangOp;
    public Sprite normalCoin;
}
