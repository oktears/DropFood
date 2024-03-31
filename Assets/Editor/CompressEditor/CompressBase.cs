using UnityEngine;
using System.IO;

using UnityEditor;

namespace ETEditor
{
    public abstract class CompressBase
    {

        //for example
        //  assetPath = Assets/Fbx
        //  systemPath = E:/Project/Sample/Assets/Fbx
        //  dataPath = E:/Project/Sample/Assets

        public static bool s_isShowHint = true;

        public static string EXCLUDE_PATH = "NoCompress";

        protected static string GetSystemPathByAssetPath(string assetPath)
        {
            return Application.dataPath + assetPath.Replace("Assets", "");
        }

        protected static string GetAssetPathBySystemPath(string systemPath)
        {
            string assetPath = systemPath.Replace(Application.dataPath, "");
            assetPath = assetPath.Replace("\\", "/");
            return "Assets" + assetPath;
        }

        protected static bool IsFileNameEndWith(string filename, string s)
        {
            int len1 = s.Length;
            int len2 = filename.Length;
            int index = len2 - len1;
            if (index < 0)
                return false;
            return filename.Substring(index, len1).ToLower() == s.ToLower();
        }

        // protected StreamWriter _writer;

        protected abstract string GetLogFile();

        public void Do()
        {
            Object[] selections = Selection.GetFiltered(typeof(Object), SelectionMode.Assets);
            if (selections.Length == 0)
                return;
            string assetPath = AssetDatabase.GetAssetPath(selections[0]);
            string systemPath = GetSystemPathByAssetPath(assetPath);
            if (!Directory.Exists(systemPath))
            {
                EditorUtility.DisplayDialog("Error", "You should select a directiory!", "OK");
                return;
            }

            // FileStream fs = new FileStream(GetLogFile(), FileMode.Create, FileAccess.Write);
            // _writer = new StreamWriter(fs);

            this.DoByRootPath(systemPath);

            // _writer.Flush();
            // _writer.Close();
            // fs.Close();

            if (s_isShowHint)
                EditorUtility.DisplayDialog("Hint", "All files is done", "OK");
        }

        protected abstract bool IsSelectedFile(string file);

        protected virtual bool IsExcludedPath(string path)
        {
            string pathLow = path.ToLower();
            if (EXCLUDE_PATH != string.Empty && pathLow.Contains(EXCLUDE_PATH.ToLower()))
                return true;
            if (pathLow.Contains("resources") && pathLow.Contains("audio"))
                return true;
            return false;
        }

        protected void DoByRootPath(string rootPath)
        {
            if (IsExcludedPath(rootPath))
            {
                return;
            }

            string[] files = Directory.GetFiles(rootPath);
            foreach (string file in files)
            {
                if (!IsSelectedFile(file))
                {
                    continue;
                }

                DoByFileName(file);
            }

            string[] dirs = Directory.GetDirectories(rootPath);
            foreach (string dir in dirs)
            {
                if (IsExcludedPath(dir))
                {
                    continue;
                }

                this.DoByRootPath(dir);
            }
        }

        protected abstract void DoByFileName(string file);

    }
}