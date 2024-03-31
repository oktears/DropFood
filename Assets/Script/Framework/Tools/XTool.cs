// songrui modify 2016/3/8

using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.IO;

namespace Chengzi
{

    public class XTool : MonoSingleton<XTool>
    {


        #region Coroutine
        public delegate void DelayCallDelegate();

        //DelayCall
        public static void DelayCall(float delay, DelayCallDelegate call)
        {
            Instance._DelayCall(delay, call);
        }

        private void _DelayCall(float delay, DelayCallDelegate call)
        {
            StartCoroutine(_CoroutineDelayCall(delay, call));
        }

        private IEnumerator _CoroutineDelayCall(float delay, DelayCallDelegate call)
        {
            yield return new WaitForSeconds(delay);
            if (call != null)
            {
                call();
            }
        }

        //DelayCallEndOfFrame
        public static void DelayCallEndOfFrame(DelayCallDelegate call)
        {
            Instance._DelayCallEndOfFrame(call);
        }

        private void _DelayCallEndOfFrame(DelayCallDelegate call)
        {
            StartCoroutine(_CoroutineDelayCallEndOfFrame(call));
        }

        private IEnumerator _CoroutineDelayCallEndOfFrame(DelayCallDelegate call)
        {
            yield return new WaitForEndOfFrame();
            if (call != null)
            {
                call();
            }
        }
        #endregion



        #region string tool
        public static string GetNameInPath(string path)
        {
            int index = path.LastIndexOf("/");
            if (index < 0)
            {
                return path;
            }
            else if (index + 1 < path.Length)
            {
                return path.Substring(index + 1);
            }
            else
            {
                return "";
            }
        }

        public static string GetTransformPath(Transform trans)
        {
            StringBuilder sb = new StringBuilder();
            while (trans != null)
            {
                if (sb.Length != 0)
                {
                    sb.Insert(0, "/");
                }
                sb.Insert(0, trans.name);
                trans = trans.parent;
            }
            return sb.ToString();
        }

        public static int GetIntFromString(string str)
        {
            int index = 0;
            int ret = -1;
            int intIndex = -1;
            int charIndex = 0;
            int charStartIndex = -1;
            bool isInIntString = false;

            while (true)
            {
                if (charIndex >= str.Length)
                {
                    if (isInIntString)
                    {
                        intIndex++;
                        if (intIndex == index)
                        {
                            ret = int.Parse(str.Substring(charStartIndex, charIndex - charStartIndex));
                        }
                    }

                    break;
                }

                if (!isInIntString)
                {
                    if (char.IsDigit(str[charIndex]))
                    {
                        isInIntString = true;
                        charStartIndex = charIndex;
                    }
                    charIndex++;
                }
                else
                {
                    if (char.IsDigit(str[charIndex]))
                    {
                        charIndex++;
                    }
                    else
                    {
                        intIndex++;
                        if (intIndex == index)
                        {
                            ret = int.Parse(str.Substring(charStartIndex, charIndex - charStartIndex));
                            break;
                        }
                        else
                        {
                            isInIntString = false;
                            charStartIndex = -1;
                        }
                    }
                }
            }

            return ret;
        }
        #endregion


        #region resource copy from StreamAssets to PersistentAssets
        public void CopyStreamingAssetsToPersistentAssets(string path, string name)
        {
            StartCoroutine(_CopyStreamingAssetsToPersistentAssets(path, name));
        }

        IEnumerator _CopyStreamingAssetsToPersistentAssets(string path, string name)
        {
            string src = FileUtils.GetStreamingAssetsDataPath(path) + name;
            // resource has no dir,but lua has dir
            string des = FileUtils.GetPersistentAssetsDataPath(path) + name;

            WWW www = new WWW(src);
            yield return www;
            if (string.IsNullOrEmpty(www.error))
            {
                FileStream fsDes = null;
                try
                {
                    if (File.Exists(des))
                    {
                        File.Delete(des);
                    }
                    fsDes = File.Create(des);
                    fsDes.Write(www.bytes, 0, www.bytes.Length);
                }
                catch (IOException ioe)
                {
                    Debug.Log("[XTool] CopyStreamingAssetsToPersistentAssets IO Error" + ioe.Message);
                }
                finally
                {
                    if (fsDes != null)
                    {
                        fsDes.Flush();
                        fsDes.Close();
                    }
                    www.Dispose();
                }
            }
            else
            {
                Debug.Log("[XTool] CopyStreamingAssetsToPersistentAssets WWW Error" + www.error);
            }
        }
        #endregion

    }
}