#if !JUST_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;

namespace Race.Editor
{
    public class PublishMenu
    {
        private static void directoryCopy(string sourceDirectory, string targetDirectory)
        {
            sourceDirectory = new DirectoryInfo(sourceDirectory).FullName;
            targetDirectory = new DirectoryInfo(targetDirectory).FullName;
            if (!Directory.Exists(sourceDirectory))
            {
                return;
            }
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }
            DirectoryInfo sourceInfo = new DirectoryInfo(sourceDirectory);
            FileInfo[] fileInfo = sourceInfo.GetFiles();
            foreach (FileInfo fiTemp in fileInfo)
            {
                File.Copy(sourceDirectory + "\\" + fiTemp.Name, targetDirectory + "\\" + fiTemp.Name, true);
            }
            DirectoryInfo[] diInfo = sourceInfo.GetDirectories();
            foreach (DirectoryInfo diTemp in diInfo)
            {
                string sourcePath = diTemp.FullName;
                string targetPath = diTemp.FullName.Replace(sourceDirectory, targetDirectory);
                Directory.CreateDirectory(targetPath);
                directoryCopy(sourcePath, targetPath);
            }
        }

        [MenuItem("Publish/Build dll &b")]
        public static void Build_dll()
        {
            if (!Application.isEditor) return;
            IniUtil iu = IniUtil.loadIni(Directory.GetCurrentDirectory() + "\\Assets\\Editor\\BuildConfig.ini");
            // 编译器路径
            string compilerPath = iu.read("Build", "compilerPath");
            // 输出路径 这个根据各自的本地路径自行调整
            string outputPath = iu.read("Build", "outputPath");

            // DLL 路径，一共四个
            string dlls = "\"{1}\\UnityExtensions\\Unity\\GUISystem\\UnityEngine.UI.dll\";" +
                "\"{1}\\Managed\\UnityEngine.dll\";" +
                "\"{1}\\Managed\\UnityEditor.dll\";" +
                "\"{4}\\Plugins\\DOTween\\DOTween.dll\";" +
                "\"{4}\\Plugins\\DOTween\\DOTween43.dll\";" +
                "\"{4}\\Plugins\\DOTween\\DOTween46.dll\";" +
                "\"{4}\\Plugins\\DOTween\\DOTween50.dll\";" +
                "\"{1}\\Mono\\lib\\mono\\2.0\\ICSharpCode.SharpZipLib.dll\" ";


            string macro = "\"JUST_EDITOR;";
            for (int i = 0; i < EditorUserBuildSettings.activeScriptCompilationDefines.Length; i++)
            {
                string str = EditorUserBuildSettings.activeScriptCompilationDefines[i];
                if (i != EditorUserBuildSettings.activeScriptCompilationDefines.Length - 1)
                {
                    macro = macro + str + ";";
                }
                else
                {
                    macro = macro + str + "\"";
                }
            }


            string nguiCompCmd = "/define:{0} " +
                "/r:" + dlls +
                "/target:library " +
                "/out:{2}\\ngui.dll " +
                "{3} " +
                "/recurse:{4}\\External\\*.cs " +
                "/unsafe " +
                "/warn:0";

            string vsBuildCommand = "/define:{0} " +
                "/r:" + dlls +
                //"/r:\"{2}\\ngui.dll\" " +
                "/target:library " +
                "/out:{2}\\DLL\\race.dll " +
                "{3} " +
                //"/recurse:{4}\\External\\*.cs " +
                //"/recurse:{4}\\DLL\\UI\\*.cs " +
                //"/recurse:{4}\\DLL\\Network\\*.cs " +
                //"/recurse:{4}\\DLL\\Tools\\*.cs " +
                "/recurse:{4}\\Script\\*.cs " +
                "/recurse:{4}\\Editor\\*.cs " +
                "/recurse:{4}\\MonoScript\\WPEdit.cs " +
                "/recurse:{4}\\MonoScript\\ItemEdit.cs " +
                "/recurse:{4}\\MonoScript\\UI\\OnEventTrigger.cs " +
                "/recurse:{4}\\MonoScript\\UI\\Text\\*.cs " +
                //"/recurse:{4}\\MonoScript\\GhostDataForDesgin.cs " +
                "/unsafe " +
                "/warn:0";

            string cmd = string.Format(
            vsBuildCommand,
            macro, EditorApplication.applicationContentsPath, outputPath, "", Application.dataPath);

            string ngui = string.Format(
            nguiCompCmd,
            macro, EditorApplication.applicationContentsPath, outputPath, "", Application.dataPath);

            //ProcessUtil.shell(VSCompilerPath, ngui);

            ProcessUtil.shell(compilerPath, cmd);
            string[] folders = { "MonoScript", "T4M", "T4MOBJ" };
            foreach (string s in folders)
            {
                string o = outputPath + s;
                string t = Application.dataPath + "/" + s;
                if (Directory.Exists(o))
                {
                    Directory.Delete(o, true);
                }
                directoryCopy(t, o);
            }


        }
    }
}
#endif
