using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

namespace ETEditor
{
    /// <summary>
    /// Unity编辑器扩展：图片压缩工具
    /// 会将文件夹下的所有jpg/png文件全部压缩为ASTC 6X6格式
    /// </summary>
    public class CompressAssetsTexture: CompressBase
    {
        protected override string GetLogFile()
        {
            return Application.dataPath + "/LogCompressTexture.txt";
        }

        protected override bool IsSelectedFile(string file)
        {
            if (file.EndsWith(".jpg"))
                return true;
            if (file.EndsWith(".JPG"))
                return true;
            if (file.EndsWith(".png"))
                return true;
            if (file.EndsWith(".PNG"))
                return true;
            if (file.EndsWith(".tga"))
                return true;
            if (file.EndsWith(".TGA"))
                return true;
            return false;
        }

        protected override void DoByFileName(string file)
        {
            string assetPath = GetAssetPathBySystemPath(file);

            string whiteListFilePath = "Assets/Editor/CompressEditor/TextureWhiteList.txt";
            string text = File.ReadAllText(whiteListFilePath);

            string[] textureNamesArr = text.Split(',');
            bool isContains = textureNamesArr.Contains(assetPath);
            if (isContains)
            {
                //检测到在不压缩白名单列表中，无法进行压缩
                return;
            }

            TextureImporter textureImporter = AssetImporter.GetAtPath(assetPath) as TextureImporter;

            if (textureImporter == null)
            {
                Debug.LogError("Error to load : " + file);
                return;
            }

            // if (textureImporter.isReadable)
            // {
            //     textureImporter.isReadable = false;
            //     Debug.Log("TextureImporter.isReadable = true");
            //     bChanged = true;
            // }

            textureImporter.maxTextureSize = 2048;
            if (textureImporter.DoesSourceTextureHaveAlpha())
            {
                textureImporter.SetPlatformTextureSettings("iPhone", textureImporter.maxTextureSize, TextureImporterFormat.ASTC_6x6);
                textureImporter.SetPlatformTextureSettings("Android", textureImporter.maxTextureSize, TextureImporterFormat.ASTC_6x6);
            }
            else
            {
                textureImporter.SetPlatformTextureSettings("iPhone", textureImporter.maxTextureSize, TextureImporterFormat.ASTC_6x6);
                textureImporter.SetPlatformTextureSettings("Android", textureImporter.maxTextureSize, TextureImporterFormat.ASTC_6x6);
            }

            Debug.Log("图片：" + file + " 设置压缩格式为 ASTC_6X6");
            textureImporter.SaveAndReimport();
        }

        int GetMaxSize(int maxSize)
        {
            if (maxSize <= 16)
                maxSize = 16;
            else if (maxSize <= 32)
                maxSize = 32;
            else if (maxSize <= 64)
                maxSize = 64;
            else if (maxSize <= 128)
                maxSize = 128;
            else if (maxSize <= 256)
                maxSize = 256;
            else if (maxSize <= 512)
                maxSize = 512;
            else if (maxSize <= 1024)
                maxSize = 1024;
            else if (maxSize <= 2048)
                maxSize = 2048;

            if (maxSize > 2048)
                maxSize = 2048;

            return maxSize;
        }

        bool IsValidSize(int size)
        {
            if (size == 16 || size == 32 || size == 64 || size == 128 || size == 256 || size == 512 || size == 1024 || size == 2048)
                return true;

            return false;
        }

        [MenuItem("Assets/CompressTools/CompressTexture")]
        static void CompressTexture()
        {
            s_isShowHint = true;
            CompressAssetsTexture action = new CompressAssetsTexture();
            action.Do();
            
        }

        [MenuItem("Assets/CompressTools/图片加入[不压缩白名单]")]
        static void AddTextureToNoCompressWhiteList()
        {
            Object[] selections = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (selections.Length == 0)
                return;

            string whiteListFilePath = "Assets/Editor/CompressEditor/TextureWhiteList.txt";
            string text = File.ReadAllText(whiteListFilePath);

            string texturePath = AssetDatabase.GetAssetPath(selections[0]);
            string[] textureNamesArr = text.Split(',');
            bool isContains = textureNamesArr.Contains(texturePath);
            if (!isContains)
            {
                text = new StringBuilder(text).Append(",").Append(texturePath).ToString();
                File.WriteAllText(whiteListFilePath, text);
                Debug.Log("图片：" + texturePath + ", 加入了[不压缩白名单]。");

                textureNamesArr = text.Split(',');
                Debug.Log("************************[不压缩图片白名单列表-BEGIN]************************");
                foreach (var filePath in textureNamesArr)
                {
                    if (!filePath.Equals(string.Empty) && !filePath.Equals(""))
                    {
                        Debug.Log(filePath);
                    }
                }

                Debug.Log("************************[不压缩图片白名单列表-END]************************");
            }
        }

        [MenuItem("Assets/CompressTools/图片移除[不压缩白名单]")]
        static void DeleteTextureToNoCompressWhiteList()
        {
            Object[] selections = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
            if (selections.Length == 0)
                return;

            string whiteListFilePath = "Assets/Editor/CompressEditor/TextureWhiteList.txt";
            string text = File.ReadAllText(whiteListFilePath);

            string texturePath = AssetDatabase.GetAssetPath(selections[0]);
            string[] textureNamesArr = text.Split(',');
            bool isContains = textureNamesArr.Contains(texturePath);
            if (isContains)
            {
                text = text.Replace(texturePath, "");
                File.WriteAllText(whiteListFilePath, text);
                Debug.Log("图片：" + texturePath + ", 已经从[不压缩白名单]中移除了。");

                textureNamesArr = text.Split(',');
                Debug.Log("************************[不压缩图片白名单列表-BEGIN]************************");
                foreach (var filePath in textureNamesArr)
                {
                    if (!filePath.Equals(string.Empty) && !filePath.Equals(""))
                    {
                        Debug.Log(filePath);
                    }
                }

                Debug.Log("************************[不压缩图片白名单列表-END]************************");
            }
        }
    }
}