using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{
    public class ADConstant
    {

        //App广告唯一ID
        public static readonly string APP_AD_ID = "ca-app-pub-5342372333654292~1067107572";
        //无任何奖励
        public static readonly string VIDEO_COMMON = "ca-app-pub-5342372333654292/3310127536";
        //主界面领取金币按钮
        public static readonly string VIDEO_MAIN_VIEW_GAIN_GOLD = "ca-app-pub-5342372333654292/9290826924";
        //结算界面点击复活按钮
        public static readonly string VIDEO_RESULT_VIEW_RELIVE = "ca-app-pub-5342372333654292/2397474882";

        /// <summary>
        /// 视频广告位
        /// </summary>
        public enum VideoADPos
        {
            /// <summary>
            /// 主界面领取金币
            /// </summary>
            MAIN_GAIN_GOLD = 1,
            /// <summary>
            /// 重新游戏
            /// </summary>
            RESTART_GAME = 2,
            /// <summary>
            /// 复活
            /// </summary>
            RELIVE = 3,
            /// <summary>
            /// 领取美食
            /// </summary>
            GAIN_ITEM,
        }
    }
}
