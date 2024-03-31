using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Diagnostics;
using Chengzi;

public class ExceptionHandler : MonoBehaviour
{
    private bool _isQuitWhenException = false;
    private string _logPath;
    private string _bugExePath;

    void Awake()
    {
        //_logPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
        //_bugExePath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
        //UnityEngine.Debug.Log(_bugExePath);
        if (DebugConfig.s_isOpenDebugLog)
        {
#if (UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE) && !UNITY_EDITOR
            Application.logMessageReceived += Handler;
#endif
        }
    }

    void OnDestory()
    {
        Application.logMessageReceived -= Handler;
    }


    void Handler(string logString, string stackTrace, LogType type)
    {

        if (type == LogType.Error || type == LogType.Exception || type == LogType.Assert)
        {
            string logPath = _logPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss") + ".log";

            string log = string.Format("[time]:{0}\r\n [type]:{1}\r\n[exception message]:{2}\r\n[stack trace]:{3}\r\n", DateTime.Now.ToString(), type.ToString(), logString, stackTrace);

            //if (Directory.Exists(_logPath))
            //{
            //    File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
            //    File.AppendAllText(logPath, "[type]:" + type.ToString() + "\r\n");
            //    File.AppendAllText(logPath, "[exception message]:" + logString + "\r\n");
            //    File.AppendAllText(logPath, "[stack trace]:" + stackTrace + "\r\n");
            //}

            // PlatformManager.Instance._mainUIThread.showSimpleDialog("LOG信息", log, "哦");
            //CommonViewManager.Instance.showTip(log, 10);
            //启动bug反馈程序  
            //if (File.Exists(_bugExePath))
            //{
            //    ProcessStartInfo pros = new ProcessStartInfo();
            //    pros.FileName = _bugExePath;
            //    pros.Arguments = "\"" + logPath + "\"";
            //    Process pro = new Process();
            //    pro.StartInfo = pros;
            //    pro.Start();
            //}
            //退出程序，bug反馈程序重启主程序  
            if (_isQuitWhenException)
            {
                Application.Quit();
            }
        }
    }

}
