using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace Chengzi
{

#if UNITY_ANDROID || UNITY_STANDALONE_WIN

    /// <summary>
    /// 设备信息接口 - Android平台实现
    /// </summary>
    public class DeviceInfoAndroidImpl : AndroidBridge, IDeviceInfo
    {

        /// <summary>
        /// 调用Java类名
        /// </summary>
        protected override string javaClassName
        {
            get { return "com.chengzi.unitybase.platform.PlatformManager"; }
        }

        /// 获取设备厂商
        /// </summary>
        /// <returns>设备厂商</returns>
        public string getDeviceProducer()
        {
            return callStatic<string>("getDeviceProducer");
        }

        /// <summary>
        /// 获取设备型号
        /// </summary>
        /// <returns>设备型号</returns>
        public string getDeviceModel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getDeviceModel");
#else
            return SystemInfo.deviceModel;
#endif
        }

        /// <summary>
        /// 获取Cpu型号
        /// </summary>
        /// <returns>Cpu型号</returns>
        public string getCpuModel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getCpuModel");
#else
            return SystemInfo.processorType;
#endif
        }

        /// <summary>
        /// 获取Cpu指令集类型
        /// </summary>
        /// <returns>Cpu指令集类型</returns>
        public string getCpuArchitectureType()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string type = callStatic<string>("getCpuArchitectureType");
            return type;
#else
            return "";
#endif
        }

        /// <summary>
        /// 获取Cpu特性
        /// </summary>
        /// <returns>Cpu特性</returns>
        public CpuFeatureType getCpuFeature()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            CpuFeatureType type = (CpuFeatureType)callStatic<int>("getCpuFeature");
            return type;
#else
            return CpuFeatureType.NEON;
#endif
        }

        /// <summary>
        /// 获取Cpu核心数
        /// </summary>
        /// <returns>Cpu核心数</returns>
        public int getCpuCoresNum()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<int>("getCpuCoresNum");
#else
            return SystemInfo.processorCount;
#endif
        }

        /// <summary>
        /// 获取Cpu最大频率
        /// </summary>
        /// <returns>最大频率</returns>
        public float getCpuMaxFreq()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<float>("getCpuMaxFreq");
#else
            return SystemInfo.processorFrequency;
#endif
        }

        /// <summary>
        /// 获取Cpu最小频率
        /// </summary>
        /// <returns>最小频率</returns>
        public float getCpuMinFreq()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<float>("getCpuMinFreq");
#else
            return SystemInfo.processorFrequency;
#endif
        }

        /// <summary>
        /// 获取Cpu当前频率
        /// </summary>
        /// <returns>Cpu当前频率</returns>
        public float getCpuCurFreq()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<float>("getCpuCurFreq");
#else
            return SystemInfo.processorFrequency;
#endif
        }

        /// <summary>
        /// 获取Cpu总频率
        /// </summary>
        /// <returns>Cpu总频率</returns>
        public float getCpuTotalFreq()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<float>("getCpuTotalFreq");
#else
            return SystemInfo.processorFrequency * getCpuCoresNum();
#endif
        }

        /// <summary>
        /// 获取总的物理内存
        /// </summary>
        /// <returns>总物理内存</returns>
        public short getTotalMemory()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getTotalMemory");
#else
            return Convert.ToInt16(SystemInfo.systemMemorySize);
#endif
        }

        /// <summary>
        /// 获取剩余内存
        /// </summary>
        /// <returns></returns>
        public short getRemainMemory()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getRemainMemory");
#else
            return 0;
#endif
        }

        /// <summary>
        /// 获取Rom的总空间
        /// </summary>
        /// <returns>Rom的总空间</returns>
        public short getRomTotalSize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getRomTotalSize");
#else
            return 0;
#endif
        }

        /// <summary>
        /// 获取剩余的Rom空间
        /// </summary>
        /// <returns>剩余的Rom空间</returns>
        public short getRomRemainSize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getRomRemainSize");
#else
            return 0;
#endif
        }

        /// <summary>
        /// 获取SD卡的总空间
        /// </summary>
        /// <returns>SD卡的总空间</returns>
        public short getSDCardTotalSize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getSDCardTotalSize");
#else
            return 0;
#endif
        }

        /// <summary>
        /// 获取SD卡的剩余空间
        /// </summary>
        /// <returns>SD卡的剩余空间</returns>
        public short getSDCardRemainSize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getSDCardRemainSize");
#else
            return 0;
#endif
        }

        /// <summary>
        /// 获取Gpu型号
        /// </summary>
        /// <returns>Gpu型号</returns>
        public string getGpuModel()
        {
            return SystemInfo.graphicsDeviceName;
        }

        /// <summary>
        /// 获取Gpu供应商 
        /// </summary>
        /// <returns></returns>
        public string getGpuVendor()
        {
            return SystemInfo.graphicsDeviceVendor;
        }

        /// <summary>
        /// Gpu版本号
        /// </summary>
        /// <returns></returns>
        public string getGpuVersion()
        {
            return SystemInfo.graphicsDeviceVersion;
        }

        /// <summary>
        /// 获取显存大小
        /// </summary>
        /// <returns></returns>
        public long getGpuMemorySize()
        {
            return SystemInfo.graphicsMemorySize;
        }

        /// <summary>
        /// 获取当前网络链接状况
        /// </summary>
        /// <returns></returns>
        public NetworkReachability getNetworkState()
        {
            return Application.internetReachability;
        }

        /// <summary>
        /// 获取系统版本号
        /// </summary>
        /// <returns>系统版本号</returns>
        public string getSystemVersion()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getSystemVersion");
#else
            return Application.version;
#endif
        }

        /// <summary>
        /// 获取SDK版本号
        /// </summary>
        /// <returns>SDK版本号</returns>
        public string getSDKVersion()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getSDKVersion");
#else
            return Application.version;
#endif
        }

        /// <summary>
        /// 获取设备分辨率
        /// </summary>
        /// <returns></returns>
        public string getResolution()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getResolution");
#else
            return "";
#endif
        }

        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns>Mac地址</returns>
        public string getMacAddress()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getMacAddress");
#else
            string macAddress = "";
            NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adaper in nis)
            {
                if (adaper.Description == "en0")
                {
                    macAddress = adaper.GetPhysicalAddress().ToString();
                    break;
                }
                else
                {
                    macAddress = adaper.GetPhysicalAddress().ToString();
                    if (macAddress != "")
                    {
                        break;
                    };
                }
            }
            return macAddress;
#endif
        }

        /// <summary>
        /// 获取网络时间
        /// </summary>
        /// <returns></returns>
        public long getNetworkTime()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<long>("getNetworkTime");
#else
            return 0;
#endif
        }

        /// <summary>
        /// 获取设备唯一ID
        /// </summary>
        /// <returns></returns>
        public string getDeviceId()
        {
            string id = SystemInfo.deviceUniqueIdentifier;
            if (id == "" || id == string.Empty)
            {
                id = "0";
            }
            return id;
        }

        /// <summary>
        /// 获取运营商类型
        /// </summary>
        /// <returns></returns>
        public string getOperatorType()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getOperateType");
#else
            return "1";
#endif
        }

        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <returns></returns>
        public string getPhoneNumber()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string num = callStatic<string>("getPhoneNumber");
            if (num != "")
            {
                return num;
            }
            else
            {
                return "0";
            }
#else
            return "10010";
#endif
        }

        /// <summary>
        /// 获取Ip地址 4g
        /// </summary>
        /// <returns></returns>
        public string getIpAddress4G()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getSimCardIpAddress");
#else
            return "";
#endif
        }

        /// <summary>
        /// 获取Ip地址 wifi
        /// </summary>
        /// <returns></returns>
        public string getIpAddressWifi()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getWifiIpAddress");
#else
            string userIp = "";
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces(); ;
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.Supports(NetworkInterfaceComponent.IPv4))
                {
                    UnicastIPAddressInformationCollection uniCast = adapter.GetIPProperties().UnicastAddresses;
                    if (uniCast.Count > 0)
                    {
                        foreach (UnicastIPAddressInformation uni in uniCast)
                        {
                            //得到IPv4的地址。 AddressFamily.InterNetwork指的是IPv4
                            if (uni.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                userIp = uni.Address.ToString();
                            }
                        }
                    }
                }
            }
            return userIp;
#endif
        }

        /// <summary>
        /// 获取渠道Id
        /// </summary>
        /// <returns></returns>
        public string getChannelId()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            string num = callStatic<string>("getChannelId");
            if (num != "")
            {
                return num;
            }
            else
            {
                return "2000";
            }
#else
            return "2000";
#endif
        }

        /// <summary>
        /// 获取版本名
        /// </summary>
        /// <returns></returns>
        public string getAppVersionName()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getAppVersionName");
#else
            //return "1987.03.31";
            return "1.0.36";
#endif
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public string getAppVersionCode()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<string>("getAppVersionCode");
#else
            return Application.version;
#endif
        }

        /// <summary>
        /// 获取实时内存
        /// </summary>
        /// <returns></returns>
        public short getCurMemory()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<short>("getCurMemory");
#else
            return getRemainMemory();
#endif
        }

        public int getWifiLevel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<int>("getWifiLevel");
#else
            return 3;
#endif
        }

        public float getBatteryLevel()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return callStatic<float>("getBatteryLevel");
#else
            return 1;
#endif
        }
    }

#endif

}

