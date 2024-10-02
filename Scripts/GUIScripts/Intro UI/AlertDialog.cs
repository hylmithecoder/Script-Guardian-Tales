// Alert Dialog using Unity plugin with .aar file yang di build with Bahasa Java
using System;
using UnityEngine;

public class AlertDialog : MonoBehaviour 
{
    AndroidJavaClass unityClass;
    AndroidJavaObject unityActivity;
    AndroidJavaObject pluginInstance;

    private void Awake() 
    {
        InitialPlugin("com.hylmi.unitylibrary.PluginsUnity");
        BuatAlert();    
    }

    void Start() 
    {
        ShowAlert();
        if (pluginInstance != null)
        {
            Debug.Log("Plugin Berhasil Di Load");
        }
        if (unityActivity != null)
        {
            Debug.Log("Aktivitas berhasil diakses");
        }
    }

    void InitialPlugin(string pluginName)
    {
        try
        {
            unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            
            pluginInstance = new AndroidJavaObject(pluginName);
            if (pluginInstance != null)
            {
                pluginInstance.CallStatic("receiveUnityActivity", unityActivity);
                Debug.Log("Plugin Berhasil Di Load");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Gagal menginisialisasi plugin: " + ex.Message+"\n"+ex.StackTrace);
        }
    }

    void BuatAlert()
    {
        if (pluginInstance != null)
        {
            pluginInstance.Call("CreateAlert");
        }
        else
        {
            Debug.LogError("Plugin instance is null. BuatAlert tidak bisa dijalankan.\nUntuk Computer bisa diabaikan");
        }
    }
// TODO : buat dia muncul pas awal buka game buka pakai tombol
    public void ShowAlert()
    {
        if (pluginInstance != null)
        {
            pluginInstance.Call("TunjukkanDialog");
        }
        else
        {
            Debug.LogError("Plugin instance is null. ShowAlert tidak bisa dijalankan.");
        }
    }
}
