using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 通用上行协议数据
    /// </summary>
    public class CommonRequestProtocolData : BaseRequestProtocolData
    {
        /// <summary>
        /// 头协议版本
        /// </summary>
        public byte HeaderProtocolVersion { get; set; }

        /// <summary>
        /// Body协议版本
        /// </summary>
        public short BodyProtocolVersion { get; set; }

        /// <summary>
        /// 玩家Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 服务器Id
        /// </summary>
        public String ServerId { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public byte PlatformType { get; set; }

        /// <summary>
        /// 序列码，判断预留重发(请求Id)
        /// </summary>
        public int RequestId { get; set; }
    }

    /// <summary>
    /// 通用下行协议数据
    /// </summary>
    public class CommonResponseProtocolData : BaseResponseProtocolData
    {

        /// <summary>
        /// 通用获得物品
        /// </summary>
        public string GainParams { get; set; }

        /// <summary>
        /// 通用属性变化
        /// </summary>
        public string PropertyChange { get; set; }

        /// <summary>
        /// 通用刷新标记位
        /// </summary>
        public string Refresh { get; set; }
    }
}
