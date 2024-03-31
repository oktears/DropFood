using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// <summary>
/// Ini文件读写
/// </summary>
public class IniUtil
{

    //调用C++内核库API
    [System.Runtime.InteropServices.DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [System.Runtime.InteropServices.DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);

    private static string _filePath;

    public IniUtil(string path)
    {
        _filePath = path;
    }

    public static IniUtil loadIni(string path)
    {
        IniUtil iu = new IniUtil(path);
        return iu;
    }

    public void write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value, _filePath);
    }

    public string read(string section, string key)
    {
        System.Text.StringBuilder temp = new System.Text.StringBuilder(255);
        GetPrivateProfileString(section, key, "", temp, 255, _filePath);
        return temp.ToString();
    }

}

