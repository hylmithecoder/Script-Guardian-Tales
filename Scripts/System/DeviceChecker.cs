// Checking Device

using UnityEngine;
using System;
using System.IO;
using System.Text;

public class DeviceLogger : MonoBehaviour
{
    private string logFilePath;
    private StringBuilder logBuilder;

    void Awake()
    {
        logBuilder = new StringBuilder();
        logFilePath = Path.Combine(Application.persistentDataPath, "device_log.txt");
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        LogDeviceInfo();
        SaveLog();
    }

    void LogDeviceInfo()
    {
        logBuilder.AppendLine("Device Log - " + DateTime.Now.ToString());
        logBuilder.AppendLine("--------------------");
        logBuilder.AppendLine("Device Model: " + SystemInfo.deviceModel);
        logBuilder.AppendLine("Device Name: " + SystemInfo.deviceName);
        logBuilder.AppendLine("Device Type: " + SystemInfo.deviceType);
        logBuilder.AppendLine("Operating System: " + SystemInfo.operatingSystem);
        logBuilder.AppendLine("System Memory Size: " + SystemInfo.systemMemorySize + " MB");
        logBuilder.AppendLine("Processor Type: " + SystemInfo.processorType);
        logBuilder.AppendLine("Processor Frequency: " + SystemInfo.processorFrequency + " MHz");
        logBuilder.AppendLine("Processor Count: " + SystemInfo.processorCount);
        logBuilder.AppendLine("Battery Level: "+ SystemInfo.batteryLevel + "%");
        logBuilder.AppendLine("Graphics Device Name: " + SystemInfo.graphicsDeviceName);
        logBuilder.AppendLine("Graphics Memory Size: " + SystemInfo.graphicsMemorySize + " MB");
        logBuilder.AppendLine("Graphics API: " + SystemInfo.graphicsDeviceType);
        logBuilder.AppendLine("Screen Resolution: " + Screen.currentResolution.width + "x" + Screen.currentResolution.height);
        logBuilder.AppendLine("--------------------");
    }

    void SaveLog()
    {
        try
        {
            File.WriteAllText(logFilePath, logBuilder.ToString());
            Debug.Log("Device log saved to: " + logFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save device log: " + e.Message);
        }
    }

    public void LogCustomInfo(string info)
    {
        logBuilder.AppendLine(DateTime.Now.ToString() + ": " + info);
        SaveLog();
    }
}