
namespace Chengzi
{

    /// <summary>
    /// 消息请求数据基类
    /// </summary>
    public class BaseMessageRequestProtocolData
    {

        /// <summary>
        /// 最大系统消息Id
        /// </summary>
        public int MaxSystemMessageId { get; set; }

        /// <summary>
        /// 最大玩家消息Id
        /// </summary>
        public long MaxPlayerMessageId { get; set; }

    }

    /// <summary>
    /// 消息下行数据基类
    /// </summary>
    public class BaseMessageReponseProtocolData
    {

        /// <summary>
        /// 消息Id
        /// </summary>
        public long MessageId { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public byte MessageType { get; set; }
        
        /// <summary>
        /// 系统时间
        /// </summary>
        public long SystemTime { get; set; }

    }

}
