using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class LogUtil
{
    private const int MaxLogFile = 6;

#if UNITY_EDITOR
    private const int MaxLogCount = 500000;
#else
    private const int MaxLogCount = 8000;
#endif

    private static bool isInited = false;
    private static bool isCacheMode = false;

    private static List<string> cachedLogList = new List<string>();

    private static int currentFileIndex = 0;
    private static int currentLogCount = 0;

    private static string currentLogPath = Application.persistentDataPath + "/logfile0.txt";

    // Can be invoked in a MonoBehaviour Awake()
    public static void Init()
    {
        if (isInited)
            return;

        currentFileIndex = PlayerPrefs.GetInt("LogFileIndex", 0);
        currentLogCount = PlayerPrefs.GetInt("LogCount", 0);

        currentLogPath = Application.persistentDataPath + "/logfile" + currentFileIndex + ".txt";

        GetLogFilePath();
        isInited = true;
        isCacheMode = true;
    }

    // Can be invoked in a MonoBehaviour Update() and OnDestroy()
    public static void Update()
    {
        WriteCachedLogsIntoFile();
    }

    // Application.logMessageReceived += LogUtil.OnLog;
    public static void OnLog(string condition, string stackTrace, LogType type)
    {
        if (!isInited)
        {
            Init();
        }

        StringBuilder sbLog = new StringBuilder();

        sbLog.Append(Util.Now.ToDateTime().ToString("yyyy/MM/dd HH:mm:ss.fff"));
        sbLog.Append("\t");
        sbLog.Append(type.ToString());
        sbLog.Append(": ");
        sbLog.Append(condition);
        sbLog.Append("\n");

        switch (type)
        {
            case LogType.Log:
                break;
            case LogType.Warning:
                break;
            case LogType.Error:
            case LogType.Exception:
                sbLog.Append(stackTrace);
                sbLog.Append("\n");
                break;
            default:
                break;
        }

        if (isCacheMode)
        {
            cachedLogList.Add(sbLog.ToString());
        }
        else
        {
            using (StreamWriter sw = new StreamWriter(GetLogFilePath(), true, Encoding.UTF8))
            {
                sw.Write(sbLog.ToString());
                sw.Close();
            }
        }
    }

    private static string GetLogFilePath()
    {
        int fileIndex = currentFileIndex;
        if (++currentLogCount >= MaxLogCount)
        {
            fileIndex = ++fileIndex % MaxLogFile;
        }

        if (currentLogCount % 100 == 1)
        {
            PlayerPrefs.SetInt("LogCount", currentLogCount);
        }

        if (fileIndex != currentFileIndex)
        {
            currentFileIndex = fileIndex;
            currentLogCount = 1;
            PlayerPrefs.SetInt("LogFileIndex", currentFileIndex);

            currentLogPath = Application.persistentDataPath + "/logfile" + currentFileIndex + ".txt";

            if (File.Exists(currentLogPath))
                File.Delete(currentLogPath);
        }

        return currentLogPath;
    }

    private static void WriteCachedLogsIntoFile(bool doWrite = true)
    {
        if (cachedLogList.Count == 0)
            return;

        using (StreamWriter sw = new StreamWriter(GetLogFilePath(), true, Encoding.UTF8))
        {
            for (int i = 0; i < cachedLogList.Count; i++)
            {
                sw.Write(cachedLogList[i]);
            }

            sw.Close();
        }

        cachedLogList.Clear();
    }

}
