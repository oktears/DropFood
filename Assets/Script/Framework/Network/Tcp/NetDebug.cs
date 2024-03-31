using System;
using UnityEngine;

namespace Chengzi
{
    public class NetDebug
    {
        public static void LogError(string strLog, string strMask)
        {
            Debug.LogError(strLog);
        }

        public static void Log(string strLog, string strMask)
        {
            Debug.Log(strLog);
        }

        public static void LogHeartBeatTime(string strLog)
        {
            Debug.Log(strLog);
        }
    }
}
