using UnityEngine;
using System.Collections;
using Microsoft.Win32;

/// <summary>
/// 注册表工具类
/// </summary>
public class RegeditUtil
{

    /// <summary>
    /// 打开注册表
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static RegistryKey openRegedit(string name)
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(name, true);
        return key;
    }

    /// <summary>
    /// 删除注册表项
    /// </summary>
    /// <param name="path"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static bool deleteRegedit(string path, string item)
    {
        RegistryKey delKey = Registry.CurrentUser.OpenSubKey(path, true);
        if (delKey != null)
        {
            delKey.DeleteSubKeyTree(item);
            delKey.Close();
            return true;
        }
        return false;
    }

    /// <summary>
    /// 是否存在指定的项
    /// </summary>
    /// <param name="RegBoot"></param>
    /// <param name="ItemName"></param>
    /// <returns></returns>
    public static bool isExistItem(RegistryKey RegBoot, string ItemName)
    {
        if (ItemName.IndexOf("\\") <= -1)
        {
            string[] subkeyNames;
            subkeyNames = RegBoot.GetValueNames();
            foreach (string ikeyName in subkeyNames)
            {
                if (ikeyName == ItemName)
                {
                    return true;
                }
            }
            return false;
        }
        else
        {
            string[] strkeyNames = ItemName.Split('\\');
            RegistryKey _newsubRegKey = RegBoot.OpenSubKey(strkeyNames[0]);
            string _newRegKeyName = "";
            int i;
            for (i = 1; i < strkeyNames.Length; i++)
            {
                _newRegKeyName = _newRegKeyName + strkeyNames[i];
                if (i != strkeyNames.Length - 1)
                {
                    _newRegKeyName = _newRegKeyName + "\\";
                }
            }
            return isExistItem(_newsubRegKey, _newRegKeyName);
        }
    }

    /// <summary>
    /// 是否存在指定的key
    /// </summary>
    /// <param name="RegBoot"></param>
    /// <param name="RegKeyName"></param>
    /// <returns></returns>
    public static bool isExistKey(RegistryKey RegBoot, string RegKeyName)
    {

        string[] subkeyNames;
        subkeyNames = RegBoot.GetValueNames();
        foreach (string keyName in subkeyNames)
        {

            if (keyName == RegKeyName)  //判断键值的名称
            {
                return true;
            }
        }
        return false;
    }

}
