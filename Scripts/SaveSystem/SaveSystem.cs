// Skrip ini buat save game nya dan bisa encode atau enkripsi dan juga bisa decode atau dekripsi
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem 
{
    // fungsi untuk save untuk button jadi belum bisa autosave ya
    public static void SavePlayer (statprototype player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData playerData = new PlayerData(player);

        formatter.Serialize(stream, playerData);
        stream.Close();
    }
    // save system for inventory system tpi belum worth!!!
    public static void Save (ItemTerpenting item)
    {
        Debug.Log("Berhasil menyimpan data");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/item.file";
        FileStream stream = new FileStream(path, FileMode.Create);

        StaminaData itemData = new StaminaData(item);

        formatter.Serialize(stream, itemData);
        stream.Close();
    }

    public static PlayerData LoadPlayer ()
    {
        string path = Application.persistentDataPath + "/player.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log("Player Berhasil di Load");
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close(); 

            return data;
        }
        else 
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }


    public static StaminaData Load ()
    {
        string path = Application.persistentDataPath + "/item.file";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Debug.Log("Data Berhasil di Load");
            StaminaData data = formatter.Deserialize(stream) as StaminaData;
            stream.Close(); 

            return data;
        }
        else 
        {
            Debug.LogError("Save File not found in " + path);
            return null;
        }
    }
}