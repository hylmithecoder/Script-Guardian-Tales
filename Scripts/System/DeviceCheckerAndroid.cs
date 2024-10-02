using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.Android;

public class AndroidChipsetLogger : MonoBehaviour
{
    private string logFilePath;
    private StringBuilder logBuilder;

    void Awake()
    {
        logBuilder = new StringBuilder();
        logFilePath = Path.Combine(Application.persistentDataPath, "android_chipset_log.txt");
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        LogAndroidChipsetInfo();
        SaveLog();
    }

    void LogAndroidChipsetInfo()
    {
        logBuilder.AppendLine("Android Chipset and Storage Log - " + DateTime.Now.ToString());
        logBuilder.AppendLine("--------------------");

        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass buildClass = new AndroidJavaClass("android.os.Build"))
            {
                logBuilder.AppendLine("Device: " + buildClass.GetStatic<string>("DEVICE"));
                logBuilder.AppendLine("Model: " + buildClass.GetStatic<string>("MODEL"));
                logBuilder.AppendLine("Manufacturer: " + buildClass.GetStatic<string>("MANUFACTURER"));
                logBuilder.AppendLine("Brand: " + buildClass.GetStatic<string>("BRAND"));
                logBuilder.AppendLine("Product: " + buildClass.GetStatic<string>("PRODUCT"));
                logBuilder.AppendLine("Hardware: " + buildClass.GetStatic<string>("HARDWARE"));
                logBuilder.AppendLine("Board: " + buildClass.GetStatic<string>("BOARD"));
                
                try
                {
                    using (AndroidJavaClass buildVersionClass = new AndroidJavaClass("android.os.Build$VERSION"))
                    {
                        string codename = buildVersionClass.GetStatic<string>("CODENAME");
                        logBuilder.AppendLine("Codename: " + codename);
                    }
                }
                catch (Exception e)
                {
                    logBuilder.AppendLine("Codename: Unable to retrieve");
                }
            }

            // Get CPU info
            string cpuInfo = GetCPUInfo();
            logBuilder.AppendLine("CPU Info: " + cpuInfo);

            // Get GPU info
            logBuilder.AppendLine("GPU: " + SystemInfo.graphicsDeviceName);

            // Get more detailed system info
            logBuilder.AppendLine("OS Version: " + SystemInfo.operatingSystem);
            logBuilder.AppendLine("System Memory: " + SystemInfo.systemMemorySize + " MB");
            logBuilder.AppendLine("Processor Count: " + SystemInfo.processorCount);
            logBuilder.AppendLine("Processor Frequency: " + SystemInfo.processorFrequency + " MHz");

            // Get storage info
            string storageInfo = GetStorageInfo();
            logBuilder.AppendLine(storageInfo);
        }
        else
        {
            logBuilder.AppendLine("This is not an Android device.");
        }

        logBuilder.AppendLine("--------------------");
    }

    string GetCPUInfo()
    {
        try
        {
            using (AndroidJavaObject processBuilder = new AndroidJavaObject("java.lang.ProcessBuilder"))
            {
                AndroidJavaObject command = new AndroidJavaObject("java.util.ArrayList");
                command.Call<bool>("add", "/system/bin/cat");
                command.Call<bool>("add", "/proc/cpuinfo");
                
                processBuilder.Call<AndroidJavaObject>("command", command);
                AndroidJavaObject process = processBuilder.Call<AndroidJavaObject>("start");
                AndroidJavaObject inputStream = process.Call<AndroidJavaObject>("getInputStream");
                using (AndroidJavaObject bufferedReader = new AndroidJavaObject("java.io.BufferedReader", new AndroidJavaObject("java.io.InputStreamReader", inputStream)))
                {
                    StringBuilder sb = new StringBuilder();
                    string line;
                    while ((line = bufferedReader.Call<string>("readLine")) != null)
                    {
                        if (line.Contains("Hardware") || line.Contains("model name"))
                        {
                            sb.AppendLine(line);
                        }
                    }
                    return sb.ToString();
                }
            }
        }
        catch (Exception e)
        {
            return "Failed to get CPU info: " + e.Message;
        }
    }

    string GetStorageInfo()
    {
        try
        {
            using (AndroidJavaClass environmentClass = new AndroidJavaClass("android.os.Environment"))
            using (AndroidJavaObject dataDirectory = environmentClass.CallStatic<AndroidJavaObject>("getDataDirectory"))
            using (AndroidJavaObject statFs = new AndroidJavaObject("android.os.StatFs", dataDirectory.Call<string>("getPath")))
            {
                long blockSize = statFs.Call<long>("getBlockSizeLong");
                long totalBlocks = statFs.Call<long>("getBlockCountLong");
                long availableBlocks = statFs.Call<long>("getAvailableBlocksLong");

                long totalSize = totalBlocks * blockSize;
                long availableSize = availableBlocks * blockSize;

                return string.Format("Storage Info:\n" +
                                     "Total Storage: {0:F2} GB\n" +
                                     "Available Storage: {1:F2} GB\n" +
                                     "Used Storage: {2:F2} GB",
                                     totalSize / (1024f * 1024f * 1024f),
                                     availableSize / (1024f * 1024f * 1024f),
                                     (totalSize - availableSize) / (1024f * 1024f * 1024f));
            }
        }
        catch (Exception e)
        {
            return "Failed to get storage info: " + e.Message;
        }
    }

    void SaveLog()
    {
        try
        {
            File.WriteAllText(logFilePath, logBuilder.ToString());
            Debug.Log("Android chipset and storage log saved to: " + logFilePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to save Android chipset and storage log: " + e.Message);
        }
    }

    public void LogCustomInfo(string info)
    {
        logBuilder.AppendLine(DateTime.Now.ToString() + ": " + info);
        SaveLog();
    }
}