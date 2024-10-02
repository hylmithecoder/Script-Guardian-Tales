using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interface_Source : MonoBehaviour
{
    public float jarakItem;
    public Color warna;
    private Stamina inventory;
    private Transform itemContainer;
    private Transform itemSlot;

    private void Awake() 
    {
        itemContainer = transform.Find("SlotContainer");
        itemSlot = itemContainer.Find("SlotItem");     
        
    }

    public void SetInventory(Stamina inventory)
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
        int x = 0;
        int y = 0;
        foreach (ItemTerpenting item in inventory.GetListCurrency())
        {
            foreach (Transform child in itemContainer)
            {
                if (child == itemSlot) continue;
                Destroy(child.gameObject);
            }

            RectTransform tranformSlotItem = Instantiate(itemSlot, itemContainer).GetComponent<RectTransform>();
            tranformSlotItem.gameObject.SetActive(true);
            tranformSlotItem.anchoredPosition = new Vector2 (x * jarakItem, y * jarakItem);

            Image image = tranformSlotItem.Find("GambarNya").GetComponent<Image>();
            image.sprite = item.Gambar();

            TextMeshProUGUI stakTeks = tranformSlotItem.Find("Stamina Sekarang").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI maksStamina = tranformSlotItem.Find("Batas Stamina").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI slash = tranformSlotItem.Find("Slash").GetComponent<TextMeshProUGUI>();
            slash.SetText("/");
            maksStamina.SetText(item.maxStamina.ToString());
            if (item.jumlah > item.maxStamina)
            {
                stakTeks.color = warna; // Ubah warna jika jumlah melebihi maxStamina
            }
            else
            {
                stakTeks.color = Color.white; // Pastikan warna kembali normal jika tidak melebihi maxStamina
            }

            stakTeks.SetText(item.jumlah.ToString());

            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

    public void UpdateInventory(Stamina inventory)
    {
        this.inventory = inventory;
        RefreshInventory();
    }
}
