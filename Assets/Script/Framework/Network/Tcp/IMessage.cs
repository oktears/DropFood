using UnityEngine;
using System.Collections;

namespace Chengzi
{
    public interface IMessage
    {

        /**
         * 获取消息id
         * @return
         */
        ushort getMsgCode();

        /**
         * 设置消息id
         * @param i
         */
        void setMsgCode(ushort i);

        /**
         * 解码
         * @param data
         */
        void decode(ByteBuffer ioBuffer);

        /**
         * 编码
         * @return
         */
        ByteBuffer encode();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool update();
    }
}

