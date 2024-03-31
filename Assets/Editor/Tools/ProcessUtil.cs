using UnityEngine;
using System.Diagnostics;
using System.Text;

/// <summary>
/// 进程操作工具类
/// </summary>
public class ProcessUtil {

    /// <summary>
    /// 执行进程命令
    /// </summary>
    /// <param name="command">命令脚本路径</param>
    /// <param name="argument">参数列表，逗号分割</param>
    public static void processCommand(string command, string args) {
        ProcessStartInfo start = new ProcessStartInfo(command);
        start.Arguments = args;
        start.CreateNoWindow = false;
        start.ErrorDialog = true;
        start.UseShellExecute = true;

        if (start.UseShellExecute) {
            start.RedirectStandardOutput = false;
            start.RedirectStandardError = false;
            start.RedirectStandardInput = false;
        } else {
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.RedirectStandardInput = true;
            start.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
            start.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
        }

        Process p = Process.Start(start);

        p.WaitForExit();
        p.Close(); 
    }

    public static void shell(string appName ,string command)
    {
        ProcessStartInfo start = new ProcessStartInfo();
        start.FileName = appName;
        start.Arguments = command;
        start.CreateNoWindow = true;
        start.ErrorDialog = true;
        start.UseShellExecute = false;
        start.RedirectStandardOutput = true;
        start.RedirectStandardError = true;
        start.RedirectStandardInput = true;

        Process p = new Process();
        StringBuilder sb = new StringBuilder();
        p.OutputDataReceived += (sender, data) => {
            if (data.Data != null && data.Data.Trim().Length > 0)
                sb.Append(data.Data).Append("\n");

        };

        p.StartInfo = start;
        p.Start();
        p.BeginOutputReadLine();
        p.WaitForExit();
        
        if (p.ExitCode != 0)
        {
            UnityEngine.Debug.ClearDeveloperConsole();
            UnityEngine.Debug.LogError(sb.ToString());
        }
        else
        {
            UnityEngine.Debug.ClearDeveloperConsole();
            UnityEngine.Debug.Log(sb.ToString());
        }

        p.Close();
        
    }
}
