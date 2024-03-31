using UnityEngine;
using System.Collections;
using System.IO;

namespace Chengzi
{
    public abstract class AbstractMessage : IMessage
    {
        public ushort getMsgCode()
        {
            return _msgCode;
        }
        public void setMsgCode(ushort i)
        {
            this._msgCode = i;
        }
        /// <summary>
        /// Decode
        /// </summary>
        /// <param name="ioBuffer"></param>
        public void decode(ByteBuffer byteBuffer)
        {
            decode0(byteBuffer);
        }
        protected abstract void decode0(ByteBuffer byteBuffer);
        protected virtual void decodeBody(ByteBuffer byteBuffer)
        {
        }

        /// <summary>
        /// Encode
        /// </summary>
        /// <returns></returns>
        public ByteBuffer encode()
        {
            ByteBuffer byteBuffer = new ByteBuffer();
            encode0(byteBuffer);
            return byteBuffer;
        }

        protected abstract void encode0(ByteBuffer byteBuffer);
        protected virtual void encodeBody(ByteBuffer byteBuffer)
        {
        }
        /// <summary>
        /// Update
        /// </summary>
        /// <returns></returns>
        public virtual bool update()
        {
            return false;
        }

        /// <summary>
        /// 是否立即处理该消息
        /// </summary>
        /// <returns></returns>
        public bool immediatelyRun()
        {
            return ((_delayTime <= 0f) && _isImmediatelyRun);
        }

        /// <summary>
        /// 更新延时处理时间
        /// </summary>
        /// <returns></returns>
        public bool updateDelayTime()
        {
            _delayTime -= Time.deltaTime;
            return (_delayTime <= 0f);
        }

        // 立即进行处理
        protected bool _isImmediatelyRun = true;
        // 延时处理时间(毫秒)
        protected float _delayTime;
        // msgCode
        private ushort _msgCode;
    }
}

