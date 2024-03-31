using System;

namespace Chengzi
{
    public class NetReceiveResult
    {
        public ushort m_usMsgCode;
        // ProtocolMessage
        public byte[] m_Data;
        // AsynchronizedMessage
        public string m_AsynMsg;

        public NetReceiveResult(ushort t, byte[] d)
        {
            this.m_usMsgCode = t;
            this.m_Data = d;
        }

        public NetReceiveResult(ushort t, string s)
        {
            this.m_usMsgCode = t;
            this.m_AsynMsg = s;
        }
    }

}
