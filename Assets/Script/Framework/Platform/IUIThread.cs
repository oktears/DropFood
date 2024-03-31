using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 高级UI线程
    /// </summary>
    public interface IUIThread
    {


        /// <summary>
        /// 分享文本信息
        /// </summary>
        /// <param name="content"></param>
        void shareText(string content);

        /// <summary>
        /// 显示提示
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="isLong">是否为长时间显示</param>
        void showToast(string content, bool isLong);

        /// <summary>
        /// 显示简单的对话框
        /// </summary>
        /// <param name="content">文字内容</param>
        /// <param name="title">标题</param>
        /// <param name="buttonText">按钮上的文字</param>
        void showSimpleDialog(string title, string content, string buttonText);


        /// <summary>
        /// 显示退出对话框
        /// </summary>
        /// <param name="content"></param>
        void showExitDialog(string content, string title, string exitButtonText, string cancelButtonText);


        /// <summary>
        /// 跳转到应用商店
        /// </summary>
        /// <param name="packageName"></param>
        void openAppstore(string packageName);

    }
}
