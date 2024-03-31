using UnityEngine;
using System.Collections;
using System.IO;
using ProtoBuf;
using Newtonsoft.Json;

namespace Chengzi
{
    public enum NetTipType
    {
        //通用Tip
        Common = 0,
        //没有提示
        NoTip = 1,
        //通用Tip
        Login = 2,
    }

    /// <summary>
    /// 客户端与服务器消息
    /// </summary>
    public abstract class ProtocolMessage : AbstractMessage
    {
        public NetTipType type = NetTipType.NoTip;

        protected override void decode0(ByteBuffer byteBuffer)
        {
            decodeBody(byteBuffer);
        }

        protected override void encode0(ByteBuffer byteBuffer)
        {
            // 写入保留4byte
            byteBuffer.writeInt(0);
            // 写入MsgCode
            byteBuffer.writeUShort((ushort)getMsgCode());
            if (getMsgCode() != MsgCode.C2S_MSG_HEART.GetHashCode() && getMsgCode() != MsgCode.S2C_MSG_HEART.GetHashCode())
            {
                //Debug.Log("发送的协议号：" + (ushort)getMsgCode());
            }
            // 写入具体消息数据
            encodeBody(byteBuffer);
            // 重新修改数据长度
            //EncodeDecodeStream.PutMessageLength(byteBuffer);
        }

        public T deserialize<T>(byte[] byteArray)
        {
            MemoryStream stream = new MemoryStream(byteArray);
            T ret = Serializer.Deserialize<T>(stream);
            return ret;
        }

        public byte[] getByte<T>(T t)
        {
            MemoryStream s = new MemoryStream();
            ProtoBuf.Serializer.Serialize(s, t);
            return s.ToArray();
        }

    }

}
