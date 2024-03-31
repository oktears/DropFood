
namespace Chengzi
{
    public class ViewEventConstant
    {

        /// <summary>
        /// 主界面播放背景音乐
        /// </summary>
        public const int EVENT_MAIN_VIEW_PLAY_BGM = (int)(ViewConstant.ViewId.MAIN) * 100 + 1;

        /// <summary>
        /// 主界面看完广告发金币
        /// </summary>
        public const int EVENT_MAIN_VIEW_AD_GOLD = (int)(ViewConstant.ViewId.MAIN) * 100 + 2;

        /// <summary>
        /// 主界面刷新I18N
        /// </summary>
        public const int EVENT_MAIN_VIEW_UPDATE_I18N = (int)(ViewConstant.ViewId.MAIN) * 100 + 3;

        /// <summary>
        /// 刷新游戏界面分数
        /// </summary>
        public const int EVENT_GAME_VIEW_UPDATE_SCORE = (int)(ViewConstant.ViewId.GAME) * 100 + 1;

        /// <summary>
        /// 复活后刷新游戏界面
        /// </summary>
        public const int EVENT_GAME_VIEW_RELIVE = (int)(ViewConstant.ViewId.GAME) * 100 + 2;

        /// <summary>
        /// 刷新游戏界面Mp
        /// </summary>
        public const int EVENT_GAME_VIEW_UPDATE_MP = (int)(ViewConstant.ViewId.GAME) * 100 + 3;

        /// <summary>
        /// 隐藏黑遮罩
        /// </summary>
        public const int EVENT_GAME_VIEW_HIDE_BLACK_LAYER = (int)(ViewConstant.ViewId.GAME) * 100 + 4;

        /// <summary>
        /// 结束隐藏UI
        /// </summary>
        public const int EVENT_GAME_VIEW_HIDE_UI = (int)(ViewConstant.ViewId.GAME) * 100 + 5;

        /// <summary>
        /// 显示黑遮罩
        /// </summary>
        public const int EVENT_GAME_VIEW_SHOW_BLACK_LAYER = (int)(ViewConstant.ViewId.GAME) * 100 + 6;

        /// <summary>
        /// 更新游戏分数
        /// </summary>
        public const int EVENT_GAME_VIEW_UPDATE_SCORE_AND_GOLD = (int)(ViewConstant.ViewId.GAME) * 100 + 7;

        /// <summary>
        /// 切换下一个道具
        /// </summary>
        public const int EVENT_GAME_VIEW_CHANGE_NEXT_ITEM = (int)(ViewConstant.ViewId.GAME) * 100 + 8;

        /// <summary>
        ///  游戏结束，UI飞出
        /// </summary>
        public const int EVENT_GAME_VIEW_GAME_OVER = (int)(ViewConstant.ViewId.GAME) * 100 + 9;

        /// <summary>
        /// 关闭暂停界面，暂停按钮飞入
        /// </summary>
        public const int EVENT_GAME_VIEW_PAUSE_FLY_IN = (int)(ViewConstant.ViewId.GAME) * 100 + 10;

        /// <summary>
        /// 获得新收藏的美食
        /// </summary>
        public const int EVENT_GAME_VIEW_NEW_COLLECT = (int)(ViewConstant.ViewId.GAME) * 100 + 11;

        /// <summary>
        /// 游戏中发现奖励金币
        /// </summary>
        public const int EVENT_GAME_VIEW_BONUS_COLD = (int)(ViewConstant.ViewId.GAME) * 100 + 12;

        /// <summary>
        /// 游戏中破记录了
        /// </summary>
        public const int EVENT_GAME_VIEW_NEW_RECORD = (int)(ViewConstant.ViewId.GAME) * 100 + 13;

        /// <summary>
        /// 刷新关卡
        /// </summary>
        public const int EVENT_LEVEL_VIEW_REFRESH_LEVEL = (int)(ViewConstant.ViewId.LEVEL) * 100 + 1;

        /// <summary>
        /// 游戏加载界面，加载完成消息
        /// </summary>
        public const int EVENT_GAME_LOADING_VIEW_LOAD_FINISH = (int)(ViewConstant.ViewId.GAME_LOADING) * 100 + 1;

        /// <summary>
        /// 游戏结算界面，切换场景
        /// </summary>
        public const int EVENT_GAME_RESULT_VIEW_CHANGE_SCENE = (int)(ViewConstant.ViewId.GAME_RESULT) * 100 + 1;

        /// <summary>
        /// 游戏结算界面，复活
        /// </summary>
        public const int EVENT_GAME_RESULT_VIEW_RELIVE = (int)(ViewConstant.ViewId.GAME_RESULT) * 100 + 2;

        /// <summary>
        /// 收藏品界面，刷新金币条
        /// </summary>
        public const int EVENT_COLLECTION_VIEW_UPDATE_GOLD = (int)(ViewConstant.ViewId.COLLECTION_LIST) * 100 + 1;

        /// <summary>
        /// 收藏品详情界面，刷新底部按钮
        /// </summary>
        public const int EVENT_COLLECTION_INFO_VIEW_UPDATE_BUTTON = (int)(ViewConstant.ViewId.COLLECTION_INFO) * 100 + 1;

        /// <summary>
        /// 商城界面，刷新去广告列表项
        /// </summary>
        public const int EVENT_SHOP_VIEW_UPDATE_REMOVE_AD = (int)(ViewConstant.ViewId.SHOP) * 100 + 1;

        /// <summary>
        /// 设置界面刷新I18N
        /// </summary>
        public const int EVENT_SETTING_VIEW_UPDATE_I18N = (int)(ViewConstant.ViewId.MAIN) * 100 + 1;
    }
}