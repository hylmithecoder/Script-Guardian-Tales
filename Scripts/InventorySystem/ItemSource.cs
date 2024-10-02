// Ini Source Code untuk item manager nya buat class baru untuk menambah item untuk kegunaan berbeda 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType 
    { 
        Sword, 
        Pedang_OP,
        Purple_Coin,
        Normal_Coin
    }
    public enum Currency
    {
        Stamina,
        Diamond
    }
    public ItemType itemType;
    public Currency currency;
    public int jumlah;

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:        return ItemAssets.Instance.pedangTutorialSprite;
            case ItemType.Purple_Coin:  return ItemAssets.Instance.purpleCoinSprite;
            case ItemType.Normal_Coin:  return ItemAssets.Instance.normalCoin;
            case ItemType.Pedang_OP:    return ItemAssets.Instance.pedangOp;
        }        
    }

    // Buat Item Bisa Di Stak
    public bool isStackable()
    {
        switch (itemType)
        {
            default:
            case ItemType.Sword:
            case ItemType.Pedang_OP:
            case ItemType.Purple_Coin:
            case ItemType.Normal_Coin:
            return true;

            // Tambahkan item yang tidak bisa di stak
            // return false;
        }
    }
}
// stamina and diamond
public class ItemTerpenting
{
    public enum Jenis
    {
        Diamond,
        Stamina
    }

    public Jenis jenis;

    public int jumlah;
    public int maxStamina = 70;

    public Sprite Gambar()
    {
        switch (jenis)
        {
            default: 
            case Jenis.Diamond: return ItemAssetsFinance.Instance.diamondSprite;
            case Jenis.Stamina: return ItemAssetsFinance.Instance.staminaSprite;
        }
    }

    public bool isStackable()
    {
        switch (jenis)
        {
            default:
            case Jenis.Diamond:
            case Jenis.Stamina:
            return true;

            // Tambahkan item yang tidak bisa di stak
            // return false;
        }
    }

    public void Save()
    {
        SaveSystem.Save(this);
    }

    public void Load()
    {
        StaminaData staminaData = SaveSystem.Load();

        jumlah = staminaData.amount;
        maxStamina = staminaData.maxStamina;
    }
}
