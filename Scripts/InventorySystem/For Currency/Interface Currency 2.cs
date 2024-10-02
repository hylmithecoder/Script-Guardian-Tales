using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interface_Source2 : MonoBehaviour
{
    public float jarakItem;
    private Diamond inventory;
    private Transform itemContainer;
    private Transform itemSlot;

    private void Awake() 
    {
        itemContainer = transform.Find("SlotContainer");
        itemSlot = itemContainer.Find("SlotItem");    
    }

    public void SetInventory(Diamond inventory)
    {
        this.inventory = inventory;
        inventory.Event += ItemBerubah;
        RefreshInventory();
    }

    private void ItemBerubah(object sender, System.EventArgs args)
    {
        RefreshInventory();
    }

    private void RefreshInventory()
    {       
        // foreach (Transform Child in itemContainer)
        // {
        //     Destroy(Child.gameObject);
        // }
        int x = 0;
        int y = 0;
        foreach (ItemTerpenting item in inventory.GetListCurrency())
        {
            RectTransform tranformSlotItem = Instantiate(itemSlot, itemContainer).GetComponent<RectTransform>();
            tranformSlotItem.gameObject.SetActive(true);
            tranformSlotItem.anchoredPosition = new Vector2 (x * jarakItem, y * jarakItem);

            Image image = tranformSlotItem.Find("GambarNya").GetComponent<Image>();
            image.sprite = item.Gambar();

            TextMeshProUGUI stakTeks = tranformSlotItem.Find("Teks").GetComponent<TextMeshProUGUI>();            
            
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
