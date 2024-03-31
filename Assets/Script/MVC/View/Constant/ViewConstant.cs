
namespace Chengzi
{

    /// <summary>
    /// 界面相关常量
    /// </summary>
    public class ViewConstant
    {
        /// <summary>
        /// 界面Id
        /// 一级界面为：100开始 范围[100,200)
        /// 弹出界面为：200开始 范围[200,300)  不阻塞
        /// 弹出界面为：300开始 范围[300,400)  阻塞
        /// 引导界面为：400开始 范围[400,500)  暂时没有
        /// </summary>
        public enum ViewId
        {
            /// <summary>
            /// 登录界面
            /// </summary>
            LOGIN = 100,

            /// <summary>
            /// 游戏界面
            /// </summary>
            GAME = 101,

            /// <summary>
            /// 主界面
            /// </summary>
            MAIN = 102,

            /// <summary>
            /// 启动加载界面
            /// </summary>
            LAUNCH_LOADING = 104,

            /// <summary>
            /// 游戏结算界面
            /// </summary>
            GAME_RESULT = 300,

            /// <summary>
            /// 游戏暂停界面
            /// </summary>
            GAME_PAUSE = 301,

            /// <summary>
            /// 游戏失败界面
            /// </summary>
            GAME_FAILURE = 302,

            /// <summary>
            /// 设置界面
            /// </summary>
            SETTING = 303,

            /// <summary>
            /// 说明界面
            /// </summary>
            PROFILE = 304,

            /// <summary>
            /// 制作人界面
            /// </summary>
            PRODUCER = 305,

            /// <summary>
            /// 收集品列表界面
            /// </summary>
            COLLECTION_LIST = 306,

            /// <summary>
            /// 收藏品详情界面
            /// </summary>
            COLLECTION_INFO = 307,

            /// <summary>
            /// 选关界面
            /// </summary>
            LEVEL = 308,

            /// <summary>
            /// 游戏加载界面
            /// </summary>
            GAME_LOADING = 309,

            /// <summary>
            /// 感谢试玩界面
            /// </summary>
            THANKS = 310,

            /// <summary>
            /// 引导界面
            /// </summary>
            GUIDE = 311,

            /// <summary>
            /// 获得物品界面
            /// </summary>
            GAIN_ITEM = 312,

            /// <summary>
            /// 商城界面
            /// </summary>
            SHOP = 313,

            /// <summary>
            /// 语言界面
            /// </summary>
            LANGUAGE = 314,
        }
    }
}
