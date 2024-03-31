
using System.Runtime.InteropServices;
using UnityEngine;

namespace Chengzi
{
#if UNITY_IPHONE || UNITY_STANDALONE_OSX

    /// <summary>
    /// IOS高级UI
    /// </summary>
    public class IOSUIThread : IUIThread
    { 

        [DllImport("__Internal")]
        private static extern void showDialog(string title, string content, string buttonText);

        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="isLong">是否为长时间显示</param>
        public void showToast(string content, bool isLong)
        {
#if UNITY_IPHONE

#else

#endif
        }

        /// <summary>
        /// 显示简单的对话框
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttonText">按钮上的文字</param>w 
        public void showSimpleDialog(string title, string content, string buttonText)
        {
#if UNITY_IPHONE
            if (!Application.isEditor)
            {
                showDialog(title, content, buttonText);
            }
#else
            Debug.Log("");
#endif
        }


        public void showExitDialog(string content, string title, string exitButtonText, string cancelButtonText)
        {
        }
    }


#endif
}
