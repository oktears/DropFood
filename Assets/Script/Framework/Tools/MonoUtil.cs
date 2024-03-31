using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public class MonoUtil
    {
        public static GameObject findChildGameObject(GameObject parent, string childName)
        {
            if (parent.name == childName)
            {
                return parent;
            }
            if (parent.transform.childCount < 1)
            {
                return null;
            }
            GameObject obj = null;
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                GameObject go = parent.transform.GetChild(i).gameObject;
                obj = findChildGameObject(go, childName);
                if (obj != null)
                {
                    break;
                }
            }
            return obj;
        }
    }
}
