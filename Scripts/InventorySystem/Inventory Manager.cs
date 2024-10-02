// Inventory Systemnya 
// Buat class baru untuk inventory manager baru

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// database inventory
public class Inventory
{
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;    

    public Inventory() 
    {
        itemList = new List<Item>();
        AddItem(new Item { itemType = Item.ItemType.Sword, jumlah = 1});
        AddItem(new Item { itemType = Item.ItemType.Purple_Coin, jumlah = 1});   
    }

    public void AddItem(Item item)
    {
        // Implementasi Item Supaya Bisa Di stack
        if (item.isStackable())
        {
            bool itemSudahDiInventory = false;
            foreach (Item itemInventory in itemList)
            {
                if (itemInventory.itemType == item.itemType)
                {
                    // Debug.Log("Barang udah ada, jadi Di stack "+ itemInventory.itemType + " Sekarang Ada " + itemInventory.jumlah);
                    itemInventory.jumlah += item.jumlah;
                    itemSudahDiInventory = true;
                }
            }
                if (!itemSudahDiInventory)
                {
                    itemList.Add(item);
                }
        } 
        else 
        {
            itemList.Add(item);
        }
        
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> DapatkanListItem()
    {
        return itemList;
    }
}
// class stamina
public class Stamina
{
    public event EventHandler Event;
    public List<ItemTerpenting> itemList;

    public Stamina()
    {
        itemList = new List<ItemTerpenting>();
        AddItem(new ItemTerpenting {jenis = ItemTerpenting.Jenis.Stamina, jumlah = 0});        
    }
    
    public void AddItem(ItemTerpenting item)
    {
        if (item.isStackable())
        {
            bool itemReady = false;
            foreach (ItemTerpenting itemInventory in itemList)
            {
                if (itemInventory.jenis == ItemTerpenting.Jenis.Stamina)
                {                    
                    // Debug.Log(itemInventory.jenis+" mu ada "+itemInventory.jumlah);
                    itemInventory.jumlah += item.jumlah;
                    itemReady = true;
                }
            }
            if (!itemReady)
            {
                itemList.Add(item);
            }
        } 
        else 
        {
            itemList.Add(item);
        }
        Event?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemTerpenting> GetListCurrency()
    {
        return itemList;
    }
    public bool HasEnoughStaminaItem()
    {
        foreach (ItemTerpenting item in itemList)
        {
            if (item.jenis == ItemTerpenting.Jenis.Stamina && item.jumlah > 0)
            {
                return true;
            }
        }
        return false;
    }

// Fungsi untuk nambah stamina dengan klik
    public void AddStamina()
    {
        foreach (ItemTerpenting item in itemList)
        {
            if (item.jenis == ItemTerpenting.Jenis.Stamina)
            {                
                item.jumlah += 10;
                int itemSekarang = item.jumlah;
                Debug.Log("Aku ruok lah "+item.jenis+" mu ada "+itemSekarang);
            }
        }
    }

    public void UseStaminaForMainStory()
    {
        foreach (ItemTerpenting item in itemList)
        {
            if (item.jenis == ItemTerpenting.Jenis.Stamina)
            {
                item.jumlah -= 15;
                int jumlahSekarang = item.jumlah;
                Debug.Log("Kamu Menggunakan "+item.jenis+" mu sekarang ada "+jumlahSekarang);
            }
        }
    }
}
// for class Diamond
public class Diamond
{
    public event EventHandler Event;
    public List<ItemTerpenting> itemList;
    public Diamond()
    {
        itemList = new List<ItemTerpenting>();
        AddItem(new ItemTerpenting {jenis = ItemTerpenting.Jenis.Diamond, jumlah = 100});
    }
    public void AddItem(ItemTerpenting item)
    {
        if (item.isStackable())
        {
            bool itemReady = false;
            foreach (ItemTerpenting itemInventory in itemList)
            {
                if (itemInventory.jenis == ItemTerpenting.Jenis.Diamond)
                {
                    Debug.Log(itemInventory.jenis+" mu ada "+itemInventory.jumlah);
                    itemInventory.jumlah += item.jumlah;
                    itemReady = true;
                }
            }
            if (!itemReady)
            {
                itemList.Add(item);
            }
        } 
        else 
        {
            itemList.Add(item);
        }
        Event?.Invoke(this, EventArgs.Empty);
    }

    public List<ItemTerpenting> GetListCurrency()
    {
        return itemList;
    }
}
