
namespace Chengzi
{
    /// <summary> 
    /// 事件定义 定义原则如下
    /// 游戏内事件全部使用相邻的序号依次往下走
    /// 触发频率越高的事件越靠前
    /// 各个事件分组通过前缀分割，事件ID隔离(EVENT,POST,UI,WP)
    /// 事件触发的接口用 s,o,e注释（s:onStart,o:onEvent,e:onExit)
    /// 注释需说明携带的参数类型
    /// 所有系统事件全部重新封装成本事件结构，统一传递
    /// </summary>
    public class Event
    {
        /*
        EVENT和WP对应IEvent接口
        POST和UI对应IEventNotice接口
        */
        public const int UI_EVENT_LOGIN = 1;


        /// <summary>
        /// 碰撞触发器(s,e),参数为碰撞体，植物顶端大
        /// </summary>
        public const int EVENT_COLLISION_TRIGGER_BRANCH_HEAD_BIG = 3;
        /// <summary>
        /// 碰撞检测(s,o,e),参数为碰撞信息，植物顶端大
        /// </summary>
        public const int EVENT_COLLISION_DETECT_BRANCH_HEAD_BIG = 4;

        /// <summary>
        /// 碰撞触发器(s,e),参数为碰撞体，植物顶端小
        /// </summary>
        public const int EVENT_COLLISION_TRIGGER_BRANCH_HEAD_SMALL = 5;
        /// <summary>
        /// 碰撞检测(s,o,e),参数为碰撞信息，植物顶端小
        /// </summary>
        public const int EVENT_COLLISION_DETECT_BRANCH_HEAD_SMALL = 6;

        //测试枝头手感
        public const int EVENT_TEST_ADD_SPEED = 1001;
        public const int EVENT_TEST_SUB_SPEED = 1002;
        public const int EVENT_TEST_ADD_DIR_RATE = 1003;
        public const int EVENT_TEST_SUB_DIR_RATE = 1004;

        /// <summary>
        /// 时间倒流
        /// </summary>
        public const int EVENT_BRANCH_TIME_REVERSE = 1005;

        /// <summary>
        /// 方向键
        /// </summary>
        public const int EVENT_CLICK_DIR_INPUT = 1006;

        /// <summary>
        /// 切换操作方式
        /// </summary>
        public const int EVENT_CHANGE_CTRL = 1007;

        /// <summary>
        /// 复活
        /// </summary>
        public const int EVENT_RELIVE = 1008;

        /// <summary>
        /// 开关滤镜
        /// </summary>
        public const int EVENT_MODIFY_FILTER = 1009;

        /// <summary>
        /// 显示场景黑层
        /// </summary>
        public const int EVENT_SHOW_BLACK_LAYER = 1010;

        /// <summary>
        /// 触摸引导层
        /// </summary>
        public const int EVENT_GUIDE_TOUCH = 1011;

        /// <summary>
        /// 游戏结束
        /// </summary>
        public const int EVENT_FINISH_GAME = 1012;
    }

}
