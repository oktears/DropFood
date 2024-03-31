
namespace Chengzi
{

    /// <summary>
    /// 玩家账号数据
    /// </summary>
    public class PlayerAccountData 
    {

        /// <summary>
        /// sid
        /// </summary>
        public string SID { get; set; }

        /// <summary>
        /// Http会话Id
        /// </summary>
        public long HttpSessionId { get; set; }

        /// <summary>
        /// Socket会话Id
        /// </summary>
        public long SocketSessionId { get; set; }

        /// <summary>
        /// PVP大厅S会话Id
        /// </summary>
        public long PvPRoomSessionId { get; set; }

        /// <summary>

        /// <summary>
        /// 当前经验
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// 当前体力
        /// </summary>
        public int Energy { get; set; }

        /// <summary>
        /// 上次体力恢复时间
        /// </summary>
        public long LastEnergyTime { get; set; }

        /// <summary>
        /// 体力恢复的速度(秒/点)
        /// </summary>
        public short EnergyFrequency { get; set; }

        /// <summary>
        /// 最大体力上限
        /// </summary>
        public short MaxEnergy { get; set; }


    }
}
