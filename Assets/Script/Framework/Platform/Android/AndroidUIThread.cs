using System.Runtime.InteropServices;
using UnityEngine;

namespace Chengzi
{

#if UNITY_ANDROID

    /// <summary> 
    /// 调用Android 高级UI线程
    /// </summary>
    public class AndroidUIThread : AndroidBridge, IUIThread
    {

#if UNITY_STANDALONE_WIN || UNITY_EDITOR

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(int h, string m, string c, int type);

#endif
        protected override string javaClassName
        {
            get { return "com.chengzi.unitybase.platform.AdvanceUIUtil"; }
        }

        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="isLong">是否为长时间显示</param>
        public void showToast(string content, bool isLong)
        {
#if UNITY_ANDROID
            callStatic("showToast", content, isLong);
#else

#endif
        }

        /// <summary>
        /// 显示简单的对话框
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttonText">按钮上的文字</param>
        public void showSimpleDialog(string title, string content, string buttonText)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            callStatic("showDialog", title, content, buttonText);
#else
            if (Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                AndroidUIThread.MessageBox(0, content, title, 0);
            }
#endif
        }

        /// <summary>
        /// 显示退出对话框
        /// </summary>
        /// <param name="content"></param>
        public void showExitDialog(string content, string title, string exitButtonText, string cancelButtonText)
        {
            callStatic("exitDialog", content, title, exitButtonText, cancelButtonText);
        }

        /// <summary>
        /// 分享文本信息
        /// </summary>
        /// <param name="content"></param>
        public void shareText(string content)
        {
#if UNITY_ANDROID
            callStatic("shareText", content);
#else

#endif
        }

        /// <summary>
        /// 跳转到应用商店
        /// </summary>
        /// <param name="packageName"></param>
        public void openAppstore(string packageName)
        {
#if UNITY_ANDROID
            //init AndroidJavaClass
            AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"); ;
            AndroidJavaClass Intent = new AndroidJavaClass("android.content.Intent");
            AndroidJavaClass Uri = new AndroidJavaClass("android.net.Uri");
            // get currentActivity
            AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject jstr_content = new AndroidJavaObject("java.lang.String", "market://details?id=" + packageName);
            AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", Intent.GetStatic<AndroidJavaObject>("ACTION_VIEW"), Uri.CallStatic<AndroidJavaObject>("parse", jstr_content));
            currentActivity.Call("startActivity", intent);
#endif
        }

    }
#endif
}
