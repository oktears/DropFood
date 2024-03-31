using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// SDK交互接口
    /// </summary>
    public interface ISDKBridge
    {
        void initSDK();

        /// <summary>
        /// 退出App
        /// </summary>
        void quitApp();

        /// <summary>
        /// 获取发送的消息字符串
        /// </summary>
        /// <param name="dto"></param>
        string getMsg(BaseDto dto);


    }
}
