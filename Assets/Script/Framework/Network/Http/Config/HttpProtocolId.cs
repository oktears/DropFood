using System;
using System.Collections.Generic;

namespace Chengzi
{
    public class HttpProtocolId
    {
        private const string INSIDE_URL = "http://192.168.124.221:8080/ChallengeCarServer/";
        private const string EXTERNAL_URL = "http://cszb.752g.com:5054/ChallengeCarServer/";

        //起始URL用于请求 http和tcp的地址
        public static string START_URL = "http://192.168.124.225/api/flashapi.php";
        //http地址
        public static string debug_Url = "http://192.168.124.225/api/flashapi.php";
        //tcp地址
        public static string game_Url = "http://cogshanxiyan.752g.com:5054/api/flashapi.php";
        //tcp端口号
        public static int socket_Port = 0;

        public static int urlMode;

        public static string getURL(ProtocolId pid)
        {
            if (pid == ProtocolId.QUERY_CHANNEL)
            {
                return pid.GetDescribe();
            }
            else
            {
                return getURL() + pid.GetDescribe();
            }
        }

        public static string getURL()
        {
            string ret = INSIDE_URL;
            switch (urlMode)
            {
                case 1:
                    ret = EXTERNAL_URL;
                    break;
            }
            return ret;
        }
    }

    public enum ProtocolId
    {
        /// <summary>登录</summary>
        [EnumDescription("DailyLogin")]
        LOGIN = 101,
        /// <summary>检查重复名字</summary>
        [EnumDescription("PlayersNickName")]
        PLAYERS_NICK_NAME = 102,
        /// <summary>获取天梯赛玩家数据</summary>
        [EnumDescription("ladderTopList")]
        LADDER_TOP = 103,
        /// <summary>玩家建档</summary>
        [EnumDescription("PlayersArchive")]
        PLAYERS_ARCHIVE = 104,
        /// <summary>天梯结算</summary>
        [EnumDescription("ladderClearing")]
        RANK_SETTLE = 105,
        /// <summary>天梯对手匹配</summary>
        [EnumDescription("ladderSelect")]
        MATH_OPPONENT = 106,
        /// <summary>下订单协议</summary>
        [EnumDescription("rechargeOrder")]
        PAY_ORDER = 107,
        /// <summary>赛季奖励查询协议</summary>
        [EnumDescription("RewardCheck")]
        AWARD_CHECK = 108,
        /// <summary>确认领取赛季奖励</summary>
        [EnumDescription("rewardConfirm")]
        AWARD_CONFIRM = 109,
        /// <summary>赛季奖励查询所有人的(同时下行玩家积分)</summary>
        [EnumDescription("seasonConfig")]
        AWARD_SEASON = 110,
        /// <summary>玩家日志协议</summary>
        [EnumDescription("userLog")]
        USERLOG = 111,
        /// <summary>赛季结算玩家领奖情况</summary>
        [EnumDescription("seasonResult")]
        SEASON_RESULTS = 112,
        /// <summary>赛季奖励查询所有人的(同时下行玩家积分)</summary>
        [EnumDescription("seasonConfig")]
        AWARD_SEASON_BG = 113,


        /// <summary>查询渠道否需要下单</summary>
        [EnumDescription("http://mobilegame.752g.com/jxscconfig.php")]
        QUERY_CHANNEL = 150,

        ///////////////////////
        //心跳协议
        /// <summary>跑马灯</summary>
        [EnumDescription("noticeMsg")]
        MARQUEE = 201,
    }

    /// <summary>
    /// 枚举注释的自定义属性类
    /// </summary>
    public class EnumDescriptionAttribute : Attribute
    {
        private string m_strDescription;
        public EnumDescriptionAttribute(string strPrinterName)
        {
            m_strDescription = strPrinterName;
        }

        public string Description
        {
            get { return m_strDescription; }
        }
    }

    
}