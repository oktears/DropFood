/*
http://www.cgsoso.com/forum-211-1.html

CG鎼滄悳 Unity3d 姣忔棩Unity3d鎻掍欢鍏嶈垂鏇存柊 鏇存湁VIP璧勬簮锛�

CGSOSO 涓绘墦娓告垙寮�彂锛屽奖瑙嗚璁＄瓑CG璧勬簮绱犳潗銆�

鎻掍欢濡傝嫢鍟嗙敤锛岃鍔″繀瀹樼綉璐拱锛�

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BestHTTP.Logger
{
    /// <summary>
    /// A basic logger implementation to be able to log intelligently additional informations about the plugin's internal mechanism.
    /// </summary>
    public class DefaultLogger : BestHTTP.Logger.ILogger
    {
        public Loglevels Level { get; set; }
        public string FormatVerbose { get; set; }
        public string FormatInfo { get; set; }
        public string FormatWarn { get; set; }
        public string FormatErr { get; set; }
        public string FormatEx { get; set; }

        public DefaultLogger()
        {
            FormatVerbose = "I [{0}]: {1}";
            FormatInfo = "I [{0}]: {1}";
            FormatWarn = "W [{0}]: {1}";
            FormatErr = "Err [{0}]: {1}";
            FormatEx = "Ex [{0}]: {1} - Message: {2}  StackTrace: {3}";

            Level = UnityEngine.Debug.isDebugBuild ? Loglevels.Warning : Loglevels.Error;
        }

        public void Verbose(string division, string verb)
        {
            if (Level <= Loglevels.All)
            {
                try
                {
                    UnityEngine.Debug.Log(string.Format(FormatVerbose, division, verb));
                }
                catch
                { }
            }
        }

        public void Information(string division, string info)
        {
            if (Level <= Loglevels.Information)
            {
                try
                {
                    UnityEngine.Debug.Log(string.Format(FormatInfo, division, info));
                }
                catch
                { }
            }
        }

        public void Warning(string division, string warn)
        {
            if (Level <= Loglevels.Warning)
            {
                try
                {
                    UnityEngine.Debug.LogWarning(string.Format(FormatWarn, division, warn));
                }
                catch
                { }
            }
        }

        public void Error(string division, string err)
        {
            if (Level <= Loglevels.Error)
            {
                try
                {
                    UnityEngine.Debug.LogError(string.Format(FormatErr, division, err));
                }
                catch
                { }
            }
        }

        public void Exception(string division, string msg, Exception ex)
        {
            if (Level <= Loglevels.Exception)
            {
                try
                {
                    string exceptionMessage = string.Empty;
                    if (ex == null)
                        exceptionMessage = "null";
                    else
                    {
                        StringBuilder sb = new StringBuilder();

                        Exception exception = ex;
                        int counter = 1;
                        while (exception != null)
                        {
                            sb.AppendFormat("{0}: {1} {2}", counter++.ToString(), ex.Message, ex.StackTrace);

                            exception = exception.InnerException;

                            if (exception != null)
                                sb.AppendLine();
                        }

                        exceptionMessage = sb.ToString();
                    }

                    UnityEngine.Debug.LogError(string.Format(FormatEx,
                                                                division,
                                                                msg,
                                                                exceptionMessage,
                                                                ex != null ? ex.StackTrace : "null"));
                }
                catch
                { }
            }
        }
    }
}