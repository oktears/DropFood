using System.IO;
using System.IO.Compression;
using UnityEngine;
using System;
using System.Collections;

namespace Chengzi
{
    public class FileUtils
    {

        public static string GetStreamingAssetsDataPath(string path)
        {
            return Application.streamingAssetsPath + "/" + path;
        }

        public static string GetPersistentAssetsDataPath(string path)
        {
            return Application.persistentDataPath + "/" + path;
        }

        public static string GetApplicationDataPath(string path)
        {
            return Application.dataPath + "/" + path;
        }

        public static string GetAppContentPath()
        {
            string path = string.Empty;
            if (Application.platform == RuntimePlatform.Android)
            {
                path = "jar:file://" + Application.dataPath + "!/assets/";
            }
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                path = Application.dataPath + "/Raw/";
            }
            if (Application.isEditor)
            {
                path = Application.dataPath + "/StreamingAssets/";
            }

            return path;
        }


        public static string GetDataPath()
        {
            string path = string.Empty;
            if (Application.isMobilePlatform)
            { //Android IOS
                return Application.persistentDataPath + "/";
            }
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            { //WindowsPlayer
                return Application.streamingAssetsPath + "/";
            }
            if (Application.isEditor)
            { //WindowEditor
                return Application.dataPath + "/StreamingAssets/";
            }
            return path;
        }

        public static void CreateFile(string path, string name, string info)
        {
            StreamWriter sw = null;
            try
            {
                FileInfo f = new FileInfo(path + name);
                if (!f.Exists)
                {
                    sw = f.CreateText();
                }
                else
                {
                    sw = f.AppendText();
                }
                sw.WriteLine(info);
            }
            catch (IOException ioe)
            {
                Debug.Log("[XTool] CreateFile IO Exception" + ioe.Message);
            }
            catch (Exception ex)
            {
                Debug.Log("[XTool] CreateFile Exception" + ex.Message);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }

        public static void DeleteFile(string path, string name)
        {
            try
            {
                File.Delete(path + name);
            }
            catch (IOException ioe)
            {
                Debug.Log("[XTool] DeleteFile IO Exception");
            }
            catch (Exception ex)
            {
                Debug.Log("[XTool] DeleteFile Exception");
            }
        }


        public static ArrayList LoadFileLinesToList(string path, string name)
        {
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(path + name);
                string line;
                ArrayList arrlist = new ArrayList();
                while ((line = sr.ReadLine()) != null)
                {
                    arrlist.Add(line);
                }
                return arrlist;
            }
            catch (IOException ioe)
            {
                Debug.Log("[XTool] LoadFile IO Exception" + ioe.Message);
            }
            catch (Exception ex)
            {
                Debug.Log("[XTool] LoadFile Exception" + ex.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
            }
            return null;
        }


        /// <summary>
        /// 获取AssetBundle资源存放路径
        /// </summary>
        /// <returns></returns>
        public static string GetAssetBundlePath()
        {
            return GetResVerPath();
        }

        /// <summary>
        /// 获取res.ver存放路径
        /// </summary>
        /// <returns></returns>
        public static string GetResVerPath()
        {

#if UNITY_ANDORID
            return "Res-android";
#elif UNITY_IOS 
            return "Res-ios";
#endif
            return "";
        }

        /// <summary>
        /// 获取Xd资源存放路径
        /// </summary>
        /// <returns></returns>
        public static string GetXDPath()
        {
            return "Config";
        }


        public static byte[] UnZipBytes(byte[] bytes)
        {
            return bytes;
            /*
            if (bytes == null || bytes.Length == 0)
            {
                return new byte[0];
            }
            // iOS使用C#自带的GZipStream，因为64编译后使用第三方库无法正常解压
            // 其他平台使用第三方库ICSharpCode，有些平台（比如windows）库不完整，无法找到GZipStream
            Stream gzs = null;
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                gzs = new GZipStream(new MemoryStream(bytes), CompressionMode.Decompress);
            }
            else
            {
#if UNITY_ANDORID || UNITY_STANDALONE
                gzs = new GZipInputStream(new MemoryStream(bytes));
#endif
            }
            MemoryStream ms = new MemoryStream();
            CopyTo(gzs, ms);
            gzs.Close();

            return ms.ToArray();
            */
        }

        public static byte[] UnZipFile(string fileName)
        {
            byte[] bytes = null;
            bool bSuc = TryGetFileBytes(fileName, out bytes);
            if (bSuc)
            {
                return UnZipBytes(bytes);
            }
            else
            {
                return null;
            }
        }

        public static string GetFileName(string filePath)
        {
            string fileName = filePath;
            string[] dataPaths = { Application.dataPath, Application.persistentDataPath }; //, Application.streamingAssetsPath};
            foreach (string dataPath in dataPaths)
            {
                if (fileName.Contains(dataPath))
                {
                    fileName = fileName.Substring(fileName.IndexOf(dataPath) + dataPath.Length);
                }
            }

            if (fileName.StartsWith("/"))
            {
                fileName = fileName.Substring(1);
            }

            return fileName;
        }

        public static void SaveDataToFile(byte[] data, string filePath)
        {

            //ThreadPool.RunAsync(() =>
            //  {
            //long beginTime = DateTimeUtil.GetTimestampMS();

            try
            {
                FileInfo f = new FileInfo(filePath);
                if (f.Exists)
                {
                    f.Delete();
                }

                string dir = f.Directory.ToString();
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.WriteAllBytes(filePath, data);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }

            //long duringTime = DateTimeUtil.GetTimestampMS() - beginTime;
            //Debug.Log(duringTime);
            //});

        }


        public static bool isExistFile(string fileName)
        {
            string filePath = PathHelper.GetStreamingAssetsPath(fileName);
            bool isExist = File.Exists(filePath);
            if (!isExist)
            {
                // 安卓用WWW去StreamingAssets里读资源
                if (Application.platform == RuntimePlatform.Android)
                {
                    try
                    {
                        WWW www = new WWW(filePath);
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
                            isExist = true;
                        }
                        else
                        {
                            isExist = false;
                        }
                    }
                    catch (Exception e)
                    {
                        isExist = false;
                    }
                }

            }
            //PlatformManager.Instance._mainUIThread.showSimpleDialog("", "filePath=" + filePath + ", isExist=" + isExist, "ok");

            return isExist;
        }

        private static IEnumerator TryGetFileBytesWebGL(string filePath, Action<byte[]> callback)
        {

            // 处理文件名，清除Application.dataPath和'/'
            string fileName = GetFileName(filePath);
            string tmpPath = "";

            tmpPath = PathHelper.GetStreamingAssetsPath(fileName);
            Debug.Log("tempPath=" + tmpPath);

            WWW www = new WWW(tmpPath);
            yield return www;

            if (www.bytes.Length > 0)
            {
                callback(www.bytes);
                Debug.Log("www is finish and success, bytes=" + www.bytes);
            }
            else
            {
                Debug.Log("www is finish and fail");
            }
        }

        public static bool TryGetFileBytes(string filePath, out byte[] bytes)
        {

            // 处理文件名，清除Application.dataPath和'/'
            string fileName = GetFileName(filePath);

            string tmpPath = "";
            bool bExists = false;

            // 1.先去下载目录找(Application.persistentDataPath)
            if (false)
            {
                tmpPath = Path.Combine(Application.persistentDataPath, fileName);
                bExists = File.Exists(tmpPath);
            }

            // 2.去Assets里找
            if (!bExists)
            {
                tmpPath = Path.Combine(Application.dataPath, fileName);
                bExists = File.Exists(tmpPath);
            }

            //Debug.Log("tempPath4=" + tmpPath);

            // 3.去StreamingAssets里找 PC和iOS在此就可以直接读文件了，安卓还需要下一步接着用WWW
            if (!bExists)
            {
                tmpPath = Path.Combine(Application.streamingAssetsPath, fileName);
                bExists = File.Exists(tmpPath);
            }
            //Debug.Log("tempPath4=" + tmpPath);

#if UNITY_ANDROID || UNITY_IOS

            // 4.ios和安卓数据读取
            if (!bExists)
            {
                tmpPath = Path.Combine(Application.persistentDataPath, fileName);
                bExists = File.Exists(tmpPath);
            }
            //Debug.Log("tempPath4=" + tmpPath);
#endif

            // 找到了，直接文件读取数据走人
            if (bExists)
            {
                bytes = File.ReadAllBytes(tmpPath);
                //			Debug.Log("[FileHelper] TryGetFileBytes Suc : " + tmpPath + " length:" + bytes.Length);
                //			          + " cost time:" + (XTool.GetTimestampMS() - startTimestamp) + "ms");
                return true;
            }

            // Android和WebGL用WWW去StreamingAssets里读资源
            if (Application.platform == RuntimePlatform.Android
                || Application.platform == RuntimePlatform.WebGLPlayer)
            {
                tmpPath = PathHelper.GetStreamingAssetsPath(fileName);
                //Debug.Log("tempPath4=" + tmpPath);
                try
                {
                    WWW www = new WWW(tmpPath);
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
                        bytes = data;
                        //              Debug.Log("[FileHelper] TryGetFileBytes Suc : " + tmpPath + " length:" + bytes.Length 
                        //                        + " cost time:" + (XTool.GetTimestampMS() - startTimestamp) + "ms");

                        // 使用WWW读取很慢，直接用File读取耗时15ms的文件WWW大概耗时400ms
                        // 

                        if (Application.platform == RuntimePlatform.Android && true/*!CommonService.PlatformManager.IsUpdateClosed()*/)
                        {
                            try
                            {
                                SaveDataToFile(data, Path.Combine(Application.persistentDataPath, fileName));
                            }
                            catch (Exception e)
                            {
                                Debug.LogError(e.Message);
                            }
                        }
                        //Debug.Log("www is finish");
                        return true;
                    }
                    else
                    {
                        //              Debug.LogError("[FileHelper] TryGetFileBytes Error : data length == 0");
                        bytes = new byte[0];
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError("[FileHelper] TryGetFileBytes Error : " + e.Message);
                    bytes = new byte[0];
                    return false;
                }

                //      Debug.Log("[FileHelper] TryGetFileBytes Done : " + filePath);
            }

            bytes = new byte[0];
            return false;
        }

        public static void getFileBytes(string fileName, Action<byte[]> callback)
        {
            XTool.Instance.StartCoroutine(TryGetFileBytesWebGL(fileName, callback));
        }

        public static byte[] XUnZipXD(string path)
        {
            byte[] bytes = null;
            bool bSuc = false;
            try
            {
                bSuc = TryGetFileBytes(path, out bytes);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }

            if (!bSuc)
            {
                //XTool.XLog("文件读取失败: " + path);
                return null;
            }

            return bytes;

            byte[] _bt = null;
            _bt = UnZipBytes(bytes);
            return _bt;
        }

        private static byte[] buffer = new byte[1024 * 16];
        public static void CopyTo(Stream src, Stream dest)
        {
            int cnt;
            while ((cnt = src.Read(buffer, 0, buffer.Length)) != 0)
            {
                dest.Write(buffer, 0, cnt);
            }
        }

        public static void ClearDirectory(string dir)
        {
            try
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
