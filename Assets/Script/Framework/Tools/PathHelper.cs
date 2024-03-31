using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.IO.Compression;


namespace Chengzi
{
    public class PathHelper
    {
        public static String GetFilePath(string path)
        {
            string url = "";
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN
            url = Application.streamingAssetsPath + "/" + path;
#elif UNITY_IPHONE
            url = Application.dataPath +"/Raw/" + path;
#elif UNITY_ANDROID
            url = "jar:file://" + Application.dataPath + "!/assets/" + path;
#endif
            //Debug.Log(url);
            return url;
        }

        /// <summary>
        /// 获取StremingAsset路径，使用www加载时必须调用此接口，开头必须有file://
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetStreamingAssetsPath(string fileName)
        {
            string url = "";
#if UNITY_EDITOR
            url = Application.streamingAssetsPath + "/" + fileName;
#else
            if (Application.platform == RuntimePlatform.Android)
            {
                url = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                url = "file://" + Application.dataPath + "/Raw/" + fileName;
            }
            else
            {
#if UNITY_EDITOR
                url = "file://" + Application.streamingAssetsPath + "/" + fileName;
#else
                url = Application.streamingAssetsPath + "/" + fileName;
#endif
            }
#endif
            return url;
        }

        public static Stream OpenFile(string path)
        {

            Stream ret = null;
            try
            {
#if UNITY_EDITOR
                ret = File.OpenRead(path);
#elif UNITY_ANDROID

           
                WWW www = new WWW(path);
                while (!www.isDone)
                {
                    // wait for www
                }
                byte[] data = new byte[0];
                if (www.bytesDownloaded > 0)
                {
                    data = www.bytes;
                }

                if (data.Length > 0)
                {

                    // 使用WWW读取很慢，直接用File读取耗时15ms的文件WWW大概耗时400ms
                    // 安卓在读取后把文件复制一份到persistentDataPath中
                    ret = new MemoryStream(data);
                }
#else
                ret = File.OpenRead(path);
#endif

            }
            catch (Exception e)
            {
                ret = null;
            }

            return ret;
        }

        public static FileStream OpenWrite(string path)
        {
            FileStream fs = null;
            try
            {
                fs = File.Open(path, FileMode.Create);
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                if (fs != null)
                {
                    fs.Close();
                }
                fs = null;
            }
            return fs;
        }


    }
}

