using System.Runtime.InteropServices;
using UnityEngine;

namespace Chengzi
{

#if UNITY_WEBGL

    /// <summary>
    /// 调用Win32 高级UI线程
    /// </summary>
    public class Html5UIThread : IUIThread
    {

        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="isLong">是否为长时间显示</param>
        public void showToast(string content, bool isLong)
        {

        }

        /// <summary>
        /// 显示简单的对话框
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttonText">按钮上的文字</param>
        public void showSimpleDialog(string title, string content, string buttonText)
        {
        }

        /// <summary>
        /// 显示退出对话框
        /// </summary>
        /// <param name="content"></param>
        public void showExitDialog(string content, string title, string exitButtonText, string cancelButtonText)
        {

        }

    }

#endif
}
