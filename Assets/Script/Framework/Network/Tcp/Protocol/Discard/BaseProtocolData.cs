

namespace Chengzi
{

    /// <summary>
    /// 协议数据基类
    /// </summary>
    public class BaseProtocolData
    {

        /// <summary>
        /// 当前系统时间(毫秒)
        /// </summary>
        public long CurSystemeTimeMs { get; set; }

        /// <summary>
        /// Socket会话Id
        /// </summary>
        public long SocketSessionId { get; set; }
    }

    /// <summary>
    /// 上行协议数据基类
    /// </summary>
    public class BaseRequestProtocolData : BaseProtocolData
    {

        
    }

    /// <summary>
    /// 下行协议数据基类
    /// </summary>
    public class BaseResponseProtocolData : BaseProtocolData
    {

    }
}
