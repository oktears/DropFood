using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    /// <summary>事件通知接口，需要发送事件的类需实现此接口</summary>
    public interface IEventNotice
    {
        /// <summary>注册通知监听</summary>
        void addEventListener(IEvent listener);
        
    }
}
