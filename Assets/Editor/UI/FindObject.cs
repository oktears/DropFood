using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class FindObject : Editor
{
    private static FindObject instance;

    public static FindObject Instance
    {
        get
        {
            if (instance == null) instance = new FindObject();
            return instance;
        }
    }

    public void getPath<T>() where T : Object
    {
        do
        {
            if (Selection.activeGameObject == null) break;

            T[] imgls = (Selection.activeGameObject as GameObject).GetComponentsInChildren<T>();
            string path = string.Empty;
            for (int i = 0; i < imgls.Length; i++)
            {
                path += getName((imgls[i] as Component).transform);
                path += "\r\n";
            }
            Debug.Log(path);
            GUIUtility.systemCopyBuffer = path;
        } while (false);
    }

    public void getPath()
    {
        do
        {
            if (Selection.activeGameObject == null) break;

            //T[] imgls = (Selection.activeGameObject as GameObject).GetComponentsInChildren<T>();
            string path = string.Empty;
            path = getName(Selection.activeGameObject.transform);
            Debug.Log(path);
            GUIUtility.systemCopyBuffer = path;
        } while (false);
    }

    public string getName(Transform a)
    {
        string ret = string.Empty;
        if (a != null)
        {
            Transform parent = a.transform.parent;
            if (parent != null) ret = getName(a.transform.parent) + "/";
            ret += a.name;
        }
        return ret;
    }
}
