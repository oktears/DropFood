// songrui modify 2016/3/8
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Chengzi
{

    public class Utilities
    {

        #region AssetBundle
        public static GameObject GetPrefab(string resPath)
        {
            return GetAsset<GameObject>(resPath);
        }

        /**
		 
         */
        public static T GetAsset<T>(string resPath) where T : Object
        {
            Object asset = null;
            string filePath = resPath + ".assetbundle";
            if (!File.Exists(filePath))  //加载更新资源assetbundle
            {
                asset = Resources.Load(resPath);
            }
            else
            {
                AssetBundle assetbundle = AssetBundle.LoadFromFile(filePath);
                if (assetbundle != null)
                {
                    string mainAssetFileName = resPath.ToLower();
                    string[] assetNames = assetbundle.GetAllAssetNames();
                    if (assetNames.Length <= 1)
                    {
                        asset = assetbundle.mainAsset;
                    }
                    else
                    {
                        for (int i = 0; i < assetNames.Length; i++)
                        {
                            if (assetNames[i].Contains(mainAssetFileName))
                            {
                                asset = assetbundle.LoadAsset(assetNames[i]);
                                if (asset.GetType() == typeof(T))
                                {
                                    break;
                                }
                                else
                                {
                                    asset = null;
                                }
                            }
                        }
                    }
                    //asset memory mirror
                    assetbundle.Unload(false);
                }
            }
            if (asset == null)
            {
                Debug.LogError("[Utilities] Cann't get asset:" + resPath);
            }
            return asset as T;
        }


        #endregion


        public static void SetLayer(GameObject obj, int layer, bool bChild)
        {
            obj.layer = layer;

            if (bChild)
            {
                Transform trans = obj.transform;
                int count = trans.childCount;
                for (int i = 0; i < count; i++)
                    SetLayer(trans.GetChild(i).gameObject, layer, true);

            }
        }
    }
}

