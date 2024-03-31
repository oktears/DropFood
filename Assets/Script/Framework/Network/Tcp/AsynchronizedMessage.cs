using UnityEngine;
using System.Collections;
namespace Chengzi
{
    /// <summary>
    /// 客户端连接成功或者异常的异步消息
    /// 放入主线程能够处理的消息队列就避免的unity的限制
    /// </summary>
    public class AsynchronizedMessage : AbstractMessage
    {
        protected override void decode0(ByteBuffer byteBuffer)
        {
        }

        protected override void encode0(ByteBuffer byteBuffer)
        {
        }

        public string _asynMsg;
    }

}
