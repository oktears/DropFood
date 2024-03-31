
namespace Chengzi
{

    /// <summary>
    /// 系统消息下行数据
    /// </summary>
    public class SystemMessageResponseProtocolData : BaseMessageReponseProtocolData
    {

        /// <summary>
        /// 显示名称
        /// </summary>
        public string ShowName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 奖励字符串
        /// </summary>
        public string Gain { get; set; }

        /// <summary>   
        /// 处理状态
        /// </summary>
        public byte State { get; set; }
    }
}
