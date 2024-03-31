
namespace Chengzi
{

    /// <summary>
    /// 玩家消息下行数据
    /// </summary>
    public class PlayerMessageResponseProtocolData : BaseMessageReponseProtocolData
    {

        /// <summary>
        /// 玩家ID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 玩家昵名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Params { get; set; }

    }

}
