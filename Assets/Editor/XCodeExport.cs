using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.Collections;
using System.IO;

public class XCodeExport : Editor
{
    [PostProcessBuild]
    static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
    {
        ModifyProj(pathToBuildProject);
        SetPlist(pathToBuildProject);
    }

    public static void ModifyProj(string pathToBuildProject)
    {
        string _projPath = PBXProject.GetPBXProjectPath(pathToBuildProject);
        PBXProject _pbxProj = new PBXProject();

        _pbxProj.ReadFromString(File.ReadAllText(_projPath));
        string _targetGuid = _pbxProj.TargetGuidByName("Unity-iPhone");

        //*******************************添加framework*******************************//
        _pbxProj.AddFrameworkToProject(_targetGuid, "StoreKit.framework", true);

        //*******************************添加tbd*******************************//
        _pbxProj.AddFileToBuild(_targetGuid, _pbxProj.AddFile("usr/lib/libz.tbd", "Frameworks/libz.tbd", PBXSourceTree.Sdk));
        _pbxProj.AddFileToBuild(_targetGuid, _pbxProj.AddFile("usr/lib/libc++.tbd", "Frameworks/libc++.tbd", PBXSourceTree.Sdk));

        //*******************************设置buildsetting*******************************//
        //_pbxProj.SetBuildProperty(_targetGuid, "CODE_SIGN_IDENTITY", code_sign_identity);

        //关闭字节码压缩
        _pbxProj.SetBuildProperty(_targetGuid, "ENABLE_BITCODE", "NO");

        //开启推送服务
        _pbxProj.AddCapability(_targetGuid, PBXCapabilityType.PushNotifications);

        File.WriteAllText(_projPath, _pbxProj.WriteToString());
    }  

    static void SetPlist(string pathToBuildProject)
    {
        string _plistPath = pathToBuildProject + "/Info.plist";
        PlistDocument _plist = new PlistDocument();

        _plist.ReadFromString(File.ReadAllText(_plistPath));
        PlistElementDict _rootDic = _plist.root;

        _rootDic.SetString("NSAllowsArbitraryLoads", "YES");
        _rootDic.SetString("NSPhotoLibraryUsageDescription", "我们需要打开您的相册");

        File.WriteAllText(_plistPath, _plist.WriteToString());
    }


}

