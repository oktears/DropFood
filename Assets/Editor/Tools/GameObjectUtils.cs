using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectUtils
{

    public static List<GameNode> GetAllChild(Transform o)
    {
        if (o == null)
        {
            Debug.LogError("指定的对象为空");
            return null;
        }
        string basePath = string.Empty;
        Transform to = o.parent;
        while (to != null)
        {
            basePath = to.name + '/' + basePath;
            to = to.parent;
        }


        List<GameNode> ret = new List<GameNode>();
        findAllChild(o, null, basePath, ref ret);
        //ret.Sort();
        return ret;
    }

    private static void findAllChild(Transform o, string path, string basePath, ref List<GameNode> list)
    {
        if (list == null)
        {
            list = new List<GameNode>();
        }

        if (string.IsNullOrEmpty(basePath))
        {
            basePath = "";
        }
        if (basePath.EndsWith("/") || basePath.EndsWith("\\"))
        {
            basePath = basePath.Remove(basePath.Length - 1);
        }

        if (string.IsNullOrEmpty(path))
        {
            path = o.name;
        }
        else
        {
            path = path + "/" + o.name;
        }


        GameNode node = new GameNode();
        node.transform = o;
        node.obj = o.gameObject;
        node.FullPath = basePath + '/' + path;
        node.RelPath = path;
        node.name = o.name;
        list.Add(node);

        //		UIRect tmp = null;
        //		if( (tmp = o.GetComponent<UIRect>()) != null)
        //		{
        //			buffer = buffer + string.Format(format,o.name,path,tmp.GetType() );
        //		}
        int count = o.childCount;
        for (int i = 0; i < count; i++)
        {
            findAllChild(o.GetChild(i), path, basePath, ref list);
        }
    }
}

