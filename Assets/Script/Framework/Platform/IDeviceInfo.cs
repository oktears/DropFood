using UnityEngine;
using System.Collections;

namespace Chengzi
{

    /// <summary>
    /// 设备信息接口
    /// </summary>
    public interface IDeviceInfo
    {

        /// <summary>
        /// 获取设备唯一Id
        /// </summary>
        /// <returns></returns>
        string getDeviceId();

        /// <summary>
        /// 获取设备厂商
        /// </summary>
        /// <returns>设备厂商</returns>
        string getDeviceProducer();

        /// <summary>
        /// 获取设备型号 (如： ipad mini2, xiaomi redmi note2) 
        /// </summary>
        /// <returns>设备型号</returns>
        string getDeviceModel();

        /// <summary>
        /// 获取Cpu型号
        /// </summary>
        /// <returns>Cpu型号</returns>
        string getCpuModel();

        /// <summary>
        /// 获取Cpu指令集类型
        /// </summary>
        /// <returns>Cpu指令集类型</returns>
        string getCpuArchitectureType();
         
        /// <summary>
        /// 获取Cpu特性
        /// </summary>
        /// <returns>Cpu特性</returns>
        CpuFeatureType getCpuFeature(); 

        /// <summary>
        /// 获取Cpu核心数
        /// </summary>
        /// <returns>Cpu核心数</returns>
        int getCpuCoresNum();

        /// <summary>
        /// 获取Cpu最大频率 khz
        /// </summary>
        /// <returns>最大频率</returns>
        float getCpuMaxFreq();

        /// <summary>
        /// 获取Cpu最小频率 khz
        /// </summary>
        /// <returns>最小频率</returns>
        float getCpuMinFreq();

        /// <summary>
        /// 获取Cpu当前频率 khz
        /// </summary>
        /// <returns>Cpu当前频率</returns>
        float getCpuCurFreq();

        /// <summary>
        /// 获取Cpu总频率 khz
        /// </summary>
        /// <returns>Cpu总频率</returns>
        float getCpuTotalFreq();

        /// <summary>
        /// 获取总的物理内存 mb
        /// </summary>
        /// <returns>总物理内存</returns>
        short getTotalMemory();

        /// <summary>
        /// 获取剩余内存 mb
        /// </summary>
        /// <returns>剩余内存</returns>
        short getRemainMemory();

        /// <summary>
        /// 获取Rom的总空间 mb
        /// </summary>
        /// <returns>Rom的总空间</returns>
        short getRomTotalSize();

        /// <summary>
        /// 获取当前网络链接状况
        /// </summary>
        /// <returns>无网络，wifi，数据</returns>
        NetworkReachability getNetworkState();

        /// <summary>
        /// 获取剩余的Rom空间 mb
        /// </summary>
        /// <returns>剩余的Rom空间</returns>
        short getRomRemainSize();

        /// <summary>
        /// 获取SD卡的总空间 mb
        /// </summary>
        /// <returns>SD卡的总空间</returns>
        short getSDCardTotalSize();

        /// <summary>
        /// 获取SD卡的剩余空间 mb
        /// </summary>
        /// <returns>SD卡的剩余空间</returns>
        short getSDCardRemainSize();

        /// <summary>
        /// 获取Gpu型号
        /// </summary>
        /// <returns>Gpu型号</returns>
        string getGpuModel();

        /// <summary>
        /// 获取Gpu供应商
        /// </summary>
        /// <returns>Gpu供应商</returns>
        string getGpuVendor();

        /// <summary>
        /// Gpu版本号
        /// </summary>
        /// <returns>Gpu版本号</returns>
        string getGpuVersion();

        /// <summary>
        /// 获取显存大小
        /// </summary>
        /// <returns></returns>
        long getGpuMemorySize();

        /// <summary>
        /// 获取系统版本号
        /// </summary>
        /// <returns>系统版本号</returns>
        string getSystemVersion();

        /// <summary>
        /// 获取SDK版本号
        /// </summary>
        /// <returns>SDK版本号</returns>
        string getSDKVersion();

        /// <summary>
        /// 获取设备分辨率
        /// </summary>
        /// <returns>设备分辨率</returns>
        string getResolution();

        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns>Mac地址</returns>
        string getMacAddress();

        /// <summary>
        /// 获取手机运营商类型
        /// </summary>
        /// <returns></returns>
        string getOperatorType();

        /// <summary>
        /// 获取手机号
        /// </summary>
        /// <returns></returns>
        string getPhoneNumber();

        /// <summary>
        /// 获取4G Ip地址
        /// </summary>
        /// <returns></returns>
        string getIpAddress4G();

        /// <summary>
        /// 获取Wifi Ip地址
        /// </summary>
        /// <returns></returns>
        string getIpAddressWifi();

        /// <summary>
        /// 获取网络时间
        /// </summary>
        /// <returns></returns>
        long getNetworkTime();

        /// <summary>
        /// 获取渠道Id
        /// </summary>
        /// <returns></returns>
        string getChannelId();

        /// <summary>
        /// 获取App版本名
        /// </summary>
        /// <returns></returns>
        string getAppVersionName();

        /// <summary>
        /// 获取App版本号
        /// </summary>
        /// <returns></returns>
        string getAppVersionCode();

        /// <summary>
        /// 获取当前内存（实时）
        /// </summary>
        /// <returns></returns>
        short getCurMemory();

        /// <summary>
        /// 获取Wifi信号等级
        /// </summary>
        /// <returns></returns>
        int getWifiLevel();

        /// <summary>
        /// 获取电池电量百分比
        /// </summary>
        /// <returns></returns>
        float getBatteryLevel();
    }

}