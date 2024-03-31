
namespace Chengzi
{

    /// <summary>
    /// 登录上行协议数据
    /// </summary>
    public class LoginRequestProtocolData : BaseRequestProtocolData
    {
        /// <summary>
        /// OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// OpenKey
        /// </summary>
        public string OpenKey { get; set; }

        /// <summary>
        /// 支付token
        /// </summary>
        public string PayToken { get; set; }

        /// <summary>
        /// 刷新token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Pf
        /// </summary>
        public string Pf { get; set; }

        /// <summary>
        /// PfKey
        /// </summary>
        public string PfKey { get; set; }

        /// <summary>
        /// 客户端版本号
        /// </summary>
        public string ClientVersion { get; set; }

        /// <summary>
        /// 对应平台的操作系统版本号
        /// </summary>
        public string SystemVersion { get; set; }

        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceModel { get; set; }

        /// <summary>
        /// 手机运营商
        /// </summary>
        public string TelComOper { get; set; }

        /// <summary>
        /// 网络类型 4G/3G/2G/WIFI
        /// </summary>
        public string NetworkType { get; set; }

        /// <summary>
        /// 屏幕分辨率宽度
        /// </summary>
        public int ResolutionWidth { get; set; }

        /// <summary>
        /// 屏幕分辨率高度
        /// </summary>
        public int ResolutionHeight { get; set; }

        /// <summary>
        /// 像素密度
        /// </summary>
        public float PixelDensity { get; set; } 

        /// <summary>
        /// Cpu信息 类型-频率-核心
        /// </summary>
        public string CpuInfo { get; set; }

        /// <summary>
        /// 内存大小，单位M
        /// </summary>
        public int MemorySize { get; set; }

        /// <summary>
        /// Gpu信息
        /// </summary>
        public string GpuInfo { get; set; }

        /// <summary>
        /// OpenGL版本信息
        /// </summary>
        public string OpenGLVersion { get; set; }

        /// <summary>
        /// 设备Id
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 注册渠道
        /// </summary>
        public string RegisterChannel { get; set; }

        /// <summary>
        /// 安装渠道
        /// </summary>
        public string InstallChannel { get; set; }

        /// <summary>
        /// Idfa
        /// </summary>
        public string Idfa { get; set; }

        /// <summary>
        /// accessToken失效时间
        /// </summary>
        public long OpenkeyExpire { get; set; }
    }

    /// <summary>
    /// 登录下行协议数据
    /// </summary>
    public class LoginResponseProtocolData : BaseResponseProtocolData
    {
        /// <summary>
        /// 玩家UID
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 玩家SID
        /// </summary>
        public string ServerId { get; set; }
    }
}
