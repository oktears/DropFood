using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 用户常量
    /// </summary>
    public class UserConstant
    {

        /// <summary>
        /// 道具类型
        /// </summary>
        public enum PropType
        {

            UNKNOWN = 0,
            /// <summary>
            /// 金币
            /// </summary>
            GOLD = 1,
            /// <summary>
            /// 改装点
            /// </summary>
            MOD = 2,
            /// <summary>
            /// 车碎片
            /// </summary>
            CLIP = 3,
            /// <summary>
            /// 抽取券
            /// </summary>
            EXTRACT_SCROLL = 4,
            /// <summary>
            /// 轮胎兑换券2星
            /// </summary>
            WHEEL_SCROLL = 5,
            ///// <summary>
            ///// 轮胎兑换券3星
            ///// </summary>
            //WHEEL_SCROLL_3 = 6,
            /// <summary>
            /// GTR
            /// </summary>
            CAR_GTR = 7,
            /// <summary>
            /// 兰博基尼
            /// </summary>
            CAR_LBG = 8,
            /// <summary>
            /// 宾利
            /// </summary>
            CAR_BNFLN = 9,
            /// <summary>
            /// 大众
            /// </summary>
            CAR_DZ = 10,
            /// <summary>
            /// 布加迪
            /// </summary>
            CAR_BJD = 11,
            /// <summary>
            /// 孙红雷
            /// </summary>
            ROLE_SHL = 12,
            /// <summary>
            /// 张艺兴
            /// </summary>
            ROLE_ZYX = 13,
            /// <summary>
            /// 车辆随机兑换券
            /// </summary>
            CAR_SCROLL = 14,
            /// <summary>
            /// 周卡
            /// </summary>
            WEEK_CARD = 15,
            /// <summary>
            /// 月卡
            /// </summary>
            MONTH_CARD = 16,
            /// <summary>
            /// 罗志祥
            /// </summary>
            ROLE_LZX = 17,
            /// <summary>
            /// AE86
            /// </summary>
            CAR_AE86 = 18,
            /// <summary>
            /// 宝马
            /// </summary>
            CAR_BWM = 19,
            /// <summary>
            /// 红鬃烈马
            /// </summary>
            CAR_F40 = 20,
            /// <summary>
            /// 特殊道具（幽灵）
            /// </summary>
            ITEM_GHOST = 21,
            /// <summary>
            /// 特殊道具（狂暴）
            /// </summary>
            ITEM_RAGE = 22,
            /// <summary>
            /// 特殊道具（霸道）
            /// </summary>
            ITEM_BOSSY = 23,
            /// <summary>
            /// 京东购物卡
            /// </summary>
            JD_CARD_500 = -1,
            JD_CARD_300 = -2,
            JD_CARD_200 = -3,
            JD_CARD_100 = -4,
            /// <summary>
            /// 50元电话卡
            /// </summary>
            TEL_CHARGE_50 = -5,
            /// <summary>
            /// 女主播
            /// </summary>
            ROLE_GIRL = 24,

            /// <summary>
            /// 乔治巴顿
            /// </summary>
            CAR_QZBD = 25,

            /// <summary>
            /// 钻石
            /// </summary>
            DIAMOND = 26,
        }

    }
}
