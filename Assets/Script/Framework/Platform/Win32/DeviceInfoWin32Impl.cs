using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 设备信息接口 - Win32平台实现
    /// </summary>
    public class DeviceInfoWin32Impl : IDeviceInfo
    {

        /// 获取主板厂商
        /// </summary>
        /// <returns>设备厂商</returns> 
        public string getDeviceProducer()
        {
            //MachineInfo info = MachineInfo.I(); //获取主机的对象信息

            return "";
        }

        /// <summary>
        /// 获取设备型号
        /// </summary>
        /// <returns>设备型号</returns>
        public string getDeviceModel()
        {
            return SystemInfo.deviceModel;
        }

        /// <summary>
        /// 获取Cpu型号
        /// </summary>
        /// <returns>Cpu型号</returns>
        public string getCpuModel()
        {
            return SystemInfo.processorType;
        }

        /// <summary>
        /// 获取Cpu指令集类型
        /// </summary>
        /// <returns>Cpu指令集类型</returns>
        public string getCpuArchitectureType()
        {
            return "";
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

        /// <summary>
        /// 获取剩余内存
        /// </summary>
        /// <returns></returns>
        public short getRemainMemory()
        {
            return 0;
        }

        /// <summary>
        /// 获取Rom的总空间
        /// </summary>
        /// <returns>Rom的总空间</returns>
        public short getRomTotalSize()
        {
            return 0;
        }

        /// <summary>
        /// 获取剩余的Rom空间
        /// </summary>
        /// <returns>剩余的Rom空间</returns>
        public short getRomRemainSize()
        {
            return 0;
        }

        /// <summary>
        /// 获取SD卡的总空间
        /// </summary>
        /// <returns>SD卡的总空间</returns>
        public short getSDCardTotalSize()
        {
            return 0;
        }

        /// <summary>
        /// 获取SD卡的剩余空间
        /// </summary>
        /// <returns>SD卡的剩余空间</returns>
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
            return Application.version;
        }

        /// <summary>
        /// 获取SDK版本号
        /// </summary>
        /// <returns>SDK版本号</returns>
        public string getSDKVersion()
        {
            return Application.version;
        }

        /// <summary>
        /// 获取设备分辨率
        /// </summary>
        /// <returns></returns>
        public string getResolution()
        {
            return "";
        }

        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns>Mac地址</returns>
        public string getMacAddress()
        {
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
        }

        /// <summary>
        /// 获取网络时间
        /// </summary>
        /// <returns></returns>
        public long getNetworkTime()
        {
            return 0;
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
            return "1";
        }

        /// <summary>
        /// 获取电话号码
        /// </summary>
        /// <returns></returns>
        public string getPhoneNumber()
        {
            return "10010";
        }

        /// <summary>
        /// 获取Ip地址 4g
        /// </summary>
        /// <returns></returns>
        public string getIpAddress4G()
        {
            return "";
        }

        /// <summary>
        /// 获取Ip地址 wifi
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 获取渠道Id
        /// </summary>
        /// <returns></returns>
        public string getChannelId()
        {

            return "2000";
        }

        /// <summary>
        /// 获取版本名
        /// </summary>
        /// <returns></returns>
        public string getAppVersionName()
        {
            return "1.0.0";
        }

        /// <summary>
        /// 获取版本号
        /// </summary>
        /// <returns></returns>
        public string getAppVersionCode()
        {
            return Application.version;
        }

        /// <summary>
        /// 获取实时内存
        /// </summary>
        /// <returns></returns>
        public short getCurMemory()
        {
            return getRemainMemory();
        }

        public int getWifiLevel()
        {
            return 3;
        }

        public float getBatteryLevel()
        {
            return 1;
        }
    }

}

