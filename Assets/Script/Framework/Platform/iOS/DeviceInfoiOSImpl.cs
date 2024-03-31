using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;


namespace Chengzi
{

#if UNITY_IPHONE || UNITY_STANDALONE_OSX

    /// 设备信息接口 - iOS平台实现
    public class DeviceInfoiOSImpl : IDeviceInfo
    {

        [DllImport("__Internal")]
        private static extern string getCpuName();
        [DllImport("__Internal")]
        private static extern short getRemainMemorySize();
        [DllImport("__Internal")]
        private static extern short getDiskRemainSize();
        [DllImport("__Internal")]
        private static extern short getDiskTotalSize();
        [DllImport("__Internal")]
        private static extern string getSystemVersionCode();
        [DllImport("__Internal")]
        private static extern string getAppVersion();

        public string getDeviceProducer()
        {
            return "Apple";
        }

        private string getAppleDeviceName()
        {
            //iPhone
            if (SystemInfo.deviceModel.Equals("iPhone1,1")) return "iPhone1G";
            if (SystemInfo.deviceModel.Equals("iPhone1,2")) return "iPhone3G";
            if (SystemInfo.deviceModel.Equals("iPhone2,1")) return "iPhone3GS";
            if (SystemInfo.deviceModel.Equals("iPhone3,1")) return "iPhone4";
            if (SystemInfo.deviceModel.Equals("iPhone3,2")) return "iPhone4";
            if (SystemInfo.deviceModel.Equals("iPhone3,3")) return "iPhone4";
            if (SystemInfo.deviceModel.Equals("iPhone4,1")) return "iPhone4S";
            if (SystemInfo.deviceModel.Equals("iPhone5,1")) return "iPhone5";
            if (SystemInfo.deviceModel.Equals("iPhone5,2")) return "iPhone5";
            if (SystemInfo.deviceModel.Equals("iPhone5,3")) return "iPhone5C";
            if (SystemInfo.deviceModel.Equals("iPhone5,4")) return "iPhone5C";
            if (SystemInfo.deviceModel.Equals("iPhone6,1")) return "iPhone5S";
            if (SystemInfo.deviceModel.Equals("iPhone6,2")) return "iPhone5S";
            if (SystemInfo.deviceModel.Equals("iPhone7,1")) return "iPhone6Plus";
            if (SystemInfo.deviceModel.Equals("iPhone7,2")) return "iPhone6";
            if (SystemInfo.deviceModel.Equals("iPhone8,1")) return "iPhone6s";
            if (SystemInfo.deviceModel.Equals("iPhone8,2")) return "iPhone6sPlus";
            if (SystemInfo.deviceModel.Equals("iPhone8,4")) return "iPhoneSE";
            if (SystemInfo.deviceModel.Equals("iPhone9,1")) return "iPhone7";
            if (SystemInfo.deviceModel.Equals("iPhone9,2")) return "iPhone7Plus";
            if (SystemInfo.deviceModel.Equals("iPhone9,3")) return "iPhone7";
            if (SystemInfo.deviceModel.Equals("iPhone9,4")) return "iPhone7Plus";
            if (SystemInfo.deviceModel.Equals("iPhone10,1")) return "iPhone8";
            if (SystemInfo.deviceModel.Equals("iPhone10,4")) return "iPhone8P";
            if (SystemInfo.deviceModel.Equals("iPhone10,2")) return "iPhone8Plus";
            if (SystemInfo.deviceModel.Equals("iPhone10,5")) return "iPhone8Plus";
            if (SystemInfo.deviceModel.Equals("iPhone10,3")) return "iPhoneX";
            if (SystemInfo.deviceModel.Equals("iPhone10,6")) return "iPhoneX";

            //iPod
            if (SystemInfo.deviceModel.Equals("iPod1,1")) return "iPodTouch1G";
            if (SystemInfo.deviceModel.Equals("iPod2,1")) return "iPodTouch2G";
            if (SystemInfo.deviceModel.Equals("iPod3,1")) return "iPodTouch3G";
            if (SystemInfo.deviceModel.Equals("iPod4,1")) return "iPodTouch4G";
            if (SystemInfo.deviceModel.Equals("iPod5,1")) return "iPodTouch5G";

            //iPad
            if (SystemInfo.deviceModel.Equals("iPad1,1")) return "iPad";
            if (SystemInfo.deviceModel.Equals("iPad1,2")) return "iPad";
            if (SystemInfo.deviceModel.Equals("iPad2,1")) return "iPad2";
            if (SystemInfo.deviceModel.Equals("iPad2,2")) return "iPad2";
            if (SystemInfo.deviceModel.Equals("iPad2,3")) return "iPad2";
            if (SystemInfo.deviceModel.Equals("iPad2,4")) return "iPad2";
            if (SystemInfo.deviceModel.Equals("iPad2,5")) return "iPadmini";
            if (SystemInfo.deviceModel.Equals("iPad2,6")) return "iPadmini";
            if (SystemInfo.deviceModel.Equals("iPad2,7")) return "iPadmini";

            if (SystemInfo.deviceModel.Equals("iPad3,1")) return "iPad3";
            if (SystemInfo.deviceModel.Equals("iPad3,2")) return "iPad3";
            if (SystemInfo.deviceModel.Equals("iPad3,3")) return "iPad3";
            if (SystemInfo.deviceModel.Equals("iPad3,4")) return "iPad4";
            if (SystemInfo.deviceModel.Equals("iPad3,5")) return "iPad4";
            if (SystemInfo.deviceModel.Equals("iPad3,6")) return "iPad4";

            if (SystemInfo.deviceModel.Equals("iPad4,1")) return "iPadAir";
            if (SystemInfo.deviceModel.Equals("iPad4,2")) return "iPadAir";
            if (SystemInfo.deviceModel.Equals("iPad4,3")) return "iPadAir";

            if (SystemInfo.deviceModel.Equals("iPad5,3")) return "iPadAir2";
            if (SystemInfo.deviceModel.Equals("iPad5,4")) return "iPadAir2";

            if (SystemInfo.deviceModel.Equals("i386")) return "Simulator";
            if (SystemInfo.deviceModel.Equals("x86_64")) return "Simulator";

            if (SystemInfo.deviceModel.Equals("iPad4,4")
                || SystemInfo.deviceModel.Equals("iPad4,5")
                || SystemInfo.deviceModel.Equals("iPad4,6")) return "iPadmini2";

            if (SystemInfo.deviceModel.Equals("iPad4,7")
                || SystemInfo.deviceModel.Equals("iPad4,8")
                || SystemInfo.deviceModel.Equals("iPad4,9")) return "iPadmini3";

            if (SystemInfo.deviceModel.Equals("iPad5,1")
                || SystemInfo.deviceModel.Equals("iPad5,2")) return "iPadmini4";

            if (SystemInfo.deviceModel.Equals("iPad6,7")
                || SystemInfo.deviceModel.Equals("iPad6,8")
                || SystemInfo.deviceModel.Equals("iPad6,3")
                || SystemInfo.deviceModel.Equals("iPad6,4")) return "iPad Pro";

            return "Unknown";
        }

        /// <summary>
        /// 获取设备型号
        /// </summary>
        /// <returns>设备型号</returns>
        public string getDeviceModel()
        {
            return getAppleDeviceName();
        }

        /// <summary>
        /// 获取Cpu型号
        /// </summary>
        /// <returns>Cpu型号</returns>
        public string getCpuModel()
        {
#if UNITY_EDITOR
            return SystemInfo.processorType;
#else
            return getCpuName();
#endif
        }

        /// <summary>
        /// 获取Cpu指令集类型
        /// </summary>
        /// <returns>Cpu指令集类型</returns>
        public string getCpuArchitectureType()
        {
            return SystemInfo.processorType;
        }

        /// <summary>
        /// 获取Cpu特性
        /// </summary>
        /// <returns>Cpu特性</returns>
        public CpuFeatureType getCpuFeature()
        {
            return CpuFeatureType.NEON;
        }

        /// <summary>
        /// 获取Cpu核心数
        /// </summary>
        /// <returns>Cpu核心数</returns>
        public int getCpuCoresNum()
        {
            return SystemInfo.processorCount;
        }

        /// <summary>
        /// 获取Cpu最大频率
        /// </summary>
        /// <returns>最大频率</returns>
        public float getCpuMaxFreq()
        {
            return SystemInfo.processorFrequency;
        }

        /// <summary>
        /// 获取Cpu最小频率
        /// </summary>
        /// <returns>最小频率</returns>
        public float getCpuMinFreq()
        {
            return SystemInfo.processorFrequency;
        }

        /// <summary>
        /// 获取Cpu当前频率
        /// </summary>
        /// <returns>Cpu当前频率</returns>
        public float getCpuCurFreq()
        {
            return SystemInfo.processorFrequency;
        }

        /// <summary>
        /// 获取Cpu总频率
        /// </summary>
        /// <returns>Cpu总频率</returns>
        public float getCpuTotalFreq()
        {
            return SystemInfo.processorFrequency * getCpuCoresNum();
        }

        /// <summary>
        /// 获取总的物理内存
        /// </summary>
        /// <returns>总物理内存</returns>
        public short getTotalMemory()
        {
            return Convert.ToInt16(SystemInfo.systemMemorySize);
        }

        public short getRemainMemory()
        {
#if UNITY_EDITOR
            return 0;
#else
            return getRemainMemorySize();
#endif
        }

        public short getRomTotalSize()
        {
#if UNITY_EDITOR
            return 0;
#else
            return getDiskTotalSize();
#endif

        }

        public short getRomRemainSize()
        {
#if UNITY_EDITOR
            return 0;
#else
            return getDiskRemainSize();
#endif

        }

        public short getSDCardTotalSize()
        {
            return 0;
        }

        public short getSDCardRemainSize()
        {
            return 0;
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
        /// 获取系统版本号
        /// </summary>
        /// <returns>系统版本号</returns>
        public string getSystemVersion()
        {
#if UNITY_EDITOR
            return "";
#else
            return getSystemVersionCode();
#endif
        }

        public string getResolution()
        {
            return string.Format("{0}x{1}", Screen.currentResolution.width, Screen.currentResolution.height);
        }

        public string getMacAddress()
        {
            string physicalAddress = "";
            NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adaper in nice)
            {
                if (adaper.Description == "en0")
                {
                    physicalAddress = adaper.GetPhysicalAddress().ToString();
                    break;
                }
                else
                {
                    physicalAddress = adaper.GetPhysicalAddress().ToString();
                    if (physicalAddress != "")
                    {
                        break;
                    };
                }
            }
            return physicalAddress;
        }

        public string getOperatorType()
        {
            return "4";
        }

        public string getPhoneNumber()
        {
            return "";
        }

        public string getIpAddress4G()
        {
            return getIpAddressWifi();
        }

        public string getIpAddressWifi()
        {
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
        }

        public long getNetworkTime()
        {
            return 0;
        }

        public string getChannelId()
        {
            return "3001";
        }

        public string getAppVersionName()
        {
#if !UNITY_EDITOR
            return getAppVersion();
#else
            return "v1.0.1";
#endif
        }

        public string getAppVersionCode()
        {
            return getAppVersionName();
        }

        public short getCurMemory()
        {
            return getRemainMemory();
        }

        public string getDeviceId()
        {
            //return "8920FF10-9ABD-48E6-94E4-69DF05C895EA";
            return SystemInfo.deviceUniqueIdentifier;
        }

        public string getSDKVersion()
        {
            return "";
        }

        public int getWifiLevel()
        {
            return 0;
        }

        public float getBatteryLevel()
        {
            return 0;
        }

        public NetworkReachability getNetworkState()
        {
            return NetworkReachability.NotReachable;
        }
    }

#endif


}

