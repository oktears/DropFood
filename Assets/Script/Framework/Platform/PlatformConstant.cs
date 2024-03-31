 
namespace Chengzi
{

    /// <summary>
    /// Cpu指令集(架构)类型
    /// </summary>
    public enum CpuArchitectureType
    {
        UNKNOWN = 0,
        //ARM9核心
        ARM_V5 = 1,
        //ARM11核心
        ARM_V6 = 2,
        //iPhone，iPhone2，iPhone3G，第一代和第二代iPod Touch
        ARM_V7 = 3,
        //iPhone5, iPhone5C
        ARM_V7S = 4,
        //华为海思K3V5
        ARM_V8 = 6,
        //Pc And Little Android
        X86 = 8,
        //Pc And Little Android
        X86_64 = 9
    }

    /// <summary>
    /// Cpu特性类型
    /// </summary>
    public enum CpuFeatureType
    {
        UNKNOWN = 0,
        //软浮点
        VFP,
        //支持硬浮点
        VFPV3,
        //支持媒体处理引擎
        NEON
    }


    /// <summary>
    /// 数据传输类型
    /// </summary>
    public enum DtType
    {
        UNKNOWN,

        /// <summary>
        /// 退出App
        /// </summary>
        QUIT_APP = 1,
        /// <summary>
        /// 发起支付
        /// </summary>
        PAY = 2,
        /// <summary>
        /// 支付回调
        /// </summary>
        PAY_CALLBACK = 3,
        /// <summary>
        /// 更多游戏
        /// </summary>
        MORE_GAME = 4,
        /// <summary>
        /// 登录
        /// </summary>
        LOGIN = 5,
        /// <summary>
        /// 退出App
        /// </summary>
        QUIT_APP_CALLBACK = 6,
        /// <summary>
        /// 广告显示隐藏
        /// </summary>
        AD = 7,
        /// <summary>
        /// 广告回调
        /// </summary>
        ADCallBack = 8,
        /// <summary>
        /// 分享
        /// </summary>
        SHARED = 10,
        /// <summary>
        /// 分享回调
        /// </summary>
        SHARED_CALLBACK = 11,
        /// <summary>
        /// 登录回调
        /// </summary>
        LOGIN_CALLBACK = 9,

        /// <summary>微信支付</summary>
        WECHAT_PAT = 12,
        /// <summary>阿里支付</summary>
        ALI_PAY = 13,
        /// <summary>线下支付回调</summary>
        OFFLINE_PAYCALLBACK = 14,
        /// <summary>分享图片</summary>
        SHARE_PICTURE = 15,
    }

}
