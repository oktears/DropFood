using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MenusEditor : Editor
{
    [MenuItem("Tools/json2tps")]
    public static void json2tps()
    {
        // Json2tpsheet.convertData();
    }

    //#if !JUST_EDITOR
    [MenuItem("GameObject/Export/Button", false, 20)]
    public static void getButton()
    {
        FindObject.Instance.getPath<Button>();
    }

    [MenuItem("GameObject/Export/Image", false, 20)]
    public static bool getImage()
    {
        FindObject.Instance.getPath<Image>();
        return true;
    }

    [MenuItem("GameObject/Export/Text", false, 20)]
    public static bool getLabel()
    {
        FindObject.Instance.getPath<Text>();
        return true;
    }

    [MenuItem("GameObject/Export/Path", false, 20)]
    public static bool getPath()
    {
        FindObject.Instance.getPath();
        return true;
    }

    [MenuItem("Tools/删除游戏存档")]
    public static void DeleteRecord()
    {
#if UNITY_5 || UNITY_4
        string path = "Software";
        if (!RegeditUtil.deleteRegedit(path, Application.companyName))
        {
            Debug.Log("无游戏存档!");
        }
        else
        {
            Debug.Log("存档删除成功!");
        }
        //FileUtil.DeleteFileOrDirectory(Application.streamingAssetsPath + "/carImg");
#else
        PlayerPrefs.DeleteAll();
#endif
    }

    //#endif
}