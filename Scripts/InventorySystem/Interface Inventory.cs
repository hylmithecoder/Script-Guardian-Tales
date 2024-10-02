// Interface Inventory System

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceInventory : MonoBehaviour
{
    public float ukuranSlotItem;
    private Inventory inventory;
    private Transform slotItem;
    private Transform itemNya;

    private void Awake() 
    {
        slotItem = transform.Find("SlotItemContainer");    
        itemNya = slotItem.Find("SlotItem");
    }

    // Setup dan Fungsi Menambahka Inventory System
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventory();
    }
    // Kalau Inventory nambah
    private void Inventory_OnItemListChanged(object pengirim, System.EventArgs args)
    {
        RefreshInventory();
    }
    // Mengrefresh Inventory
    private void RefreshInventory()
    {
        foreach (Transform anakan in slotItem)
        {
            if (anakan == itemNya) continue;
            Destroy(anakan.gameObject);
        }
        // Get Item Objek nya Dari player ketika menyentuh item nya dan dipindah kan ke inventory atau UI nya
        int x = 0;
        int y = 0;
        foreach (Item item in inventory.DapatkanListItem())
        {
            RectTransform tranformSlotItem = Instantiate(itemNya, slotItem).GetComponent<RectTransform>();
            tranformSlotItem.gameObject.SetActive(true);
            tranformSlotItem.anchoredPosition = new Vector2 (x * ukuranSlotItem, y * ukuranSlotItem);
            Image image = tranformSlotItem.Find("Pedang").GetComponent<Image>();
            image.sprite = item.GetSprite();
            TextMeshProUGUI stakTeks = tranformSlotItem.Find("Jumlah Stak").GetComponent<TextMeshProUGUI>();
            if (item.jumlah > 1)
            {
                stakTeks.SetText(item.jumlah.ToString());
            } else 
            {
                stakTeks.SetText("0");
            }
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }
}
