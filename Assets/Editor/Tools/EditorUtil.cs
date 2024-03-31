
using UnityEditor;
using UnityEngine;

public static class EditorUtil
{

    public static void savePrefab(GameObject obj, string localPath)
    {
        // Debug.Log("savePrefab :" + localPath);
        PrefabUtility.CreatePrefab(localPath, obj, ReplacePrefabOptions.ConnectToPrefab);
    }

}

