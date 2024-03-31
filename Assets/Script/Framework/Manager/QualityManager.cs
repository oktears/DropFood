// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;
// using System.Runtime.InteropServices;
// using System;
//
// namespace Chengzi
// {
//
//
//     /// <summary>
//     /// 分级等级
//     /// </summary>
//     public enum QualityLevel
//     {
//         UNONWN = 0,
//         WORST = 1,
//         LOW = 2,
//         MID = 3,
//         HIGH = 4,
//     }
//
//     /// <summary>
//     /// 质量分级管理器
//     /// </summary>
//     public class QualityManager : Singleton<QualityManager>
//     {
//         private QualityLevel m_currentQuality = QualityLevel.HIGH;
//
//         private List<string> m_suffixStrings = new List<string>();
//         private List<string> m_emptySuffixStrings = new List<string>();
//
//         private readonly string KEY_QUALITY = "KEY_QUALITY";
//
//         //内存2g
//         private const int ANDROID_LOW_MEMORY = 2000;
//         private const int ANDROID_MID_MEMORY = 3000;
//         private const int IOS_LOW_MEMORY = 1000;
//
//         //cpu频率 4核心 2.1ghz
//         private const float ANDROID_LOW_CPU = 2.1f;
//         private const float ANDROID_MID_CPU = 2.5f;
//
//         //设备等级
//         public QualityLevel _deviceLevel;
//         //内存等级
//         public QualityLevel _memoryLevel;
//         //Cpu等级
//         public QualityLevel _cpuLevel;
//         //Gpu等级
//         public QualityLevel _gpuLevel;
//         //分级标准[1,在库里查得cpu和gpu 2.在库里查得cpu 3,根据型号粗略分级 4,未查到]
//         private int _levelType;
//
//         //设备表唯一Id
//         private int _deviceId;
//
//         //是否启用车库镜面反射
//         public bool _isOpenReflect { get; set; }
//
//         //是否开启毛玻璃
//         public bool _isOpenGroundGlass { get; set; }
//
//         //是否开启比赛中滤镜
//         public bool _isOpenFilter { get; set; }
//
//         //是否进行Shader适配
//         public bool _isShaderAdapter { get; set; }
//
//         //是否使用投射
//         public bool _isOpenProjector { get; set; }
//
//         //是否比赛中使用高摸车
//         public static bool s_isRaceUseHMCar;
//         //适配后分辨率
//         public static float s_adapterResolutionWidth;
//         //适配后分辨率
//         public static float s_adapterResolutionHeight;
//
//         public void init()
//         {
//             // genDefaultLevel();
//             //
//             // if (DebugConfig.s_isShowDeviceDialog)
//             //     showDeviceInfo();
//         }
//
//         /// <summary>
//         /// 生成默认分级
//         /// </summary>
//         public void genDefaultLevel()
//         {
//             _isOpenReflect = true;
//             _isOpenGroundGlass = true;
//             s_adapterResolutionWidth = Screen.width;
//             s_adapterResolutionHeight = Screen.height;
//
//             calcCpuLevel();
//             calcMemoryLevel();
//             calcGpuLevel();
//             adapterSpecialDevice();
//
//             //_gpuLevel = QualityLevel.WORST;
//
//             if (_memoryLevel == QualityLevel.WORST
//                 || _cpuLevel == QualityLevel.WORST
//                 || _gpuLevel == QualityLevel.WORST)
//             {
//                 _deviceLevel = QualityLevel.WORST;
//                 _isShaderAdapter = true;
//             }
//
//             if (_gpuLevel == QualityLevel.LOW
//                 || _gpuLevel == QualityLevel.WORST)
//             {
//                 _isOpenGroundGlass = false;
//             }
//
//             //_isRaceUseHMCar = true;
//             //_isOpenProjector = true;
//
//             if (_gpuLevel == QualityLevel.HIGH)
//             {
//                 _isOpenProjector = true;
//             }
//
//             setLightCount();
//             setAntiAliasing();
//             setAnisotropicFiltering();
//             setTextureMipmap();
//             setVSyncCount();
//         }
//
//         /// <summary>
//         /// 适配特殊机型
//         /// </summary>
//         private void adapterSpecialDevice()
//         {
//             //联想ZUK Z2，骁龙820 8996 阉割版 主频1.6
//             if (StringUtil.Contains(PlatformManager.Instance._deviceInfo.getDeviceModel(), "ZUKZ2", StringComparison.OrdinalIgnoreCase))
//             {
//                 _gpuLevel = QualityLevel.LOW;
//                 _cpuLevel = QualityLevel.LOW;
//                 _deviceLevel = QualityLevel.LOW;
//                 _isOpenReflect = false;
//             }
//         }
//
//         /// <summary>
//         /// 计算内存分级
//         /// </summary>
//         public void calcMemoryLevel()
//         {
//             return;
//
//             IDeviceInfo deviceInfo = PlatformManager.Instance._deviceInfo;
// #if UNITY_ANDROID
//
//             if (deviceInfo.getTotalMemory() != 0
//                 && deviceInfo.getTotalMemory() <= ANDROID_LOW_MEMORY)
//             {
//                 //2g ram以下android机型认为是低内存机型
//                 _memoryLevel = QualityLevel.WORST;
//             }
//             else if (deviceInfo.getTotalMemory() != 0
//               && deviceInfo.getTotalMemory() <= ANDROID_MID_MEMORY
//               && deviceInfo.getTotalMemory() > ANDROID_LOW_MEMORY)
//             {
//                 _memoryLevel = QualityLevel.MID;
//             }
//             else
//             {
//                 _memoryLevel = QualityLevel.HIGH;
//             }
// #elif UNITY_IOS
//             if (deviceInfo.getTotalMemory() != 0 && deviceInfo.getTotalMemory() < IOS_LOW_MEMORY)
//             {
//                 _memoryLevel = QualityLevel.LOW;
//             }
//             else if (deviceInfo.getTotalMemory() != 0 && deviceInfo.getTotalMemory() < IOS_LOW_MEMORY / 2)
//             {
//                 _memoryLevel = QualityLevel.WORST;
//             }
//             else
//             {
//                 _memoryLevel = QualityLevel.HIGH;
//             }
// #else
// 	        _memoryLevel = QualityLevel.HIGH;
// #endif
//
//         }
//
//         /// <summary>
//         /// 计算Cpu分级
//         /// </summary>
//         public void calcCpuLevel()
//         {
//             _cpuLevel = QualityLevel.HIGH;
//
//             return;
//
//             IDeviceInfo deviceInfo = PlatformManager.Instance._deviceInfo;
// #if UNITY_ANDROID
//             int cpuNum = deviceInfo.getCpuCoresNum();
//             float cpuFreq = deviceInfo.getCpuCurFreq();
//             if (cpuNum <= 4)
//             {
//                 if (cpuFreq < ANDROID_LOW_CPU)
//                 {
//                     //4核心2.1ghz以下, 低cpu
//                     _cpuLevel = QualityLevel.WORST;
//                 }
//             }
//             else if (cpuNum > 4
//               && cpuNum <= 8)
//             {
//                 if (cpuFreq < ANDROID_LOW_CPU)
//                 {
//                     //4核心2.1ghz以下, 低cpu
//                     _cpuLevel = QualityLevel.MID;
//                 }
//             }
//             else if (cpuNum > 8)
//             {
//                 //4核心2.1ghz以下, 低cpu
//                 _cpuLevel = QualityLevel.HIGH;
//             }
// #else
//             //_cpuLevel = QualityLevel.MID;
// #endif
//         }
//
//         /**
//          *  计算Gpu分级
//          */
//         public void calcGpuLevel()
//         {
//
//             return;
//
//
//             //针对Android平台
//             //1.)检测cpu/gpu信息库，查找设备等级
//             //2.)未查到，根据gpu型号进行粗略分级
//             //3.)获取不到gpu型号，则按内存分级
//
//             IDeviceInfo deviceInfo = PlatformManager.Instance._deviceInfo;
// #if UNITY_ANDROID
//
//             string gpuName = deviceInfo.getGpuModel().Trim().ToLower();
//             string cpuName = deviceInfo.getCpuModel().Trim().ToLower();
//             //是否检测到该类型cpu
//             bool isCheckedGpu = false;
//
//             //1.检索gpu信息库，查找设备等级
//             foreach (DeviceData data in DaoManager.Instance._deviceDao._deviceList)
//             {
//                 string cpu = data._cpu.Trim().ToLower();
//                 string gpu = data._gpu.Trim().ToLower(); ;
//                 if (StringUtil.Contains(gpuName, gpu, StringComparison.OrdinalIgnoreCase)
//                     && StringUtil.Contains(cpuName, cpu, StringComparison.OrdinalIgnoreCase))
//                 {
//                     //按cpu + gpu筛
//                     _gpuLevel = data._level;
//                     _cpuLevel = data._level;
//                     _levelType = 1;
//                     _isOpenFilter = data._isOpenFilter;
//                     _deviceId = data._id;
//                     return;
//                 }
//             }
//
//             //按cpu筛
//             foreach (DeviceData data in DaoManager.Instance._deviceDao._deviceList)
//             {
//                 string cpu = data._cpu.Trim().ToLower();
//                 //Debug.Log(cpu);
//                 if (StringUtil.Contains(cpuName, cpu, StringComparison.OrdinalIgnoreCase))
//                 {
//                     _gpuLevel = data._level;
//                     _cpuLevel = data._level;
//                     _levelType = 2;
//                     _isOpenFilter = data._isOpenFilter;
//                     _deviceId = data._id;
//                     return;
//                 }
//             }
//
//             //2.根据gpu型号进行粗略分级
//             if (StringUtil.Contains(gpuName, "Adreno", StringComparison.OrdinalIgnoreCase))
//             {
//                 int deviceModelID = StringUtil.getIntFromString(gpuName);
//                 if (deviceModelID >= 0)
//                 {
//                     isCheckedGpu = true;
//                     if (deviceModelID <= 320)
//                     {
//                         _gpuLevel = QualityLevel.LOW;
//                     }
//                     else if (deviceModelID <= 420
//                         && deviceModelID > 320)
//                     {
//                         _gpuLevel = QualityLevel.MID;
//                     }
//                     else
//                     {
//                         _gpuLevel = QualityLevel.HIGH;
//                     }
//                 }
//             }
//             else if (StringUtil.Contains(gpuName, "PowerVR SGX".Trim().ToLower(), StringComparison.OrdinalIgnoreCase))
//             {
//                 int deviceModelID = StringUtil.getIntFromString(gpuName);
//                 if (deviceModelID >= 0)
//                 {
//                     isCheckedGpu = true;
//                     if (deviceModelID <= 540)
//                     {
//                         _gpuLevel = QualityLevel.LOW;
//                     }
//                     else
//                     {
//                         _gpuLevel = QualityLevel.MID;
//                     }
//                 }
//             }
//             else if (StringUtil.Contains(gpuName, "Mali".Trim().ToLower(), StringComparison.OrdinalIgnoreCase))
//             {
//                 int deviceModelID = StringUtil.getIntFromString(gpuName);
//                 if (StringUtil.Contains(gpuName, "Mali T".Trim().ToLower(), StringComparison.OrdinalIgnoreCase)
//                       || StringUtil.Contains(gpuName, "Mali-T".Trim().ToLower(), StringComparison.OrdinalIgnoreCase))
//                 {
//                     isCheckedGpu = true;
//                     //T系列
//                     if (deviceModelID >= 0)
//                     {
//                         if (deviceModelID <= 628)
//                         {
//                             _gpuLevel = QualityLevel.LOW;
//                         }
//                         else if (deviceModelID > 628
//                             && deviceModelID <= 760)
//                         {
//                             _gpuLevel = QualityLevel.MID;
//                         }
//                         else
//                         {
//                             _gpuLevel = QualityLevel.HIGH;
//                         }
//                     }
//                 }
//                 else
//                 {
//                     if (deviceModelID >= 0)
//                     {
//                         isCheckedGpu = true;
//                         if (deviceModelID == 300
//                             || deviceModelID == 200
//                             || (deviceModelID == 400))
//                         {
//                             _gpuLevel = QualityLevel.LOW;
//                         }
//                         else
//                         {
//                             _gpuLevel = QualityLevel.MID;
//                         }
//                     }
//                 }
//             }
//
//             //3.未在gpu库里找到该型号，使用内存级别给gpu分级
//             if (isCheckedGpu == false)
//             {
//                 _gpuLevel = _memoryLevel;
//                 _levelType = 4;
//             }
//             else
//             {
//                 _levelType = 3;
//             }
//
//
// #if UNITY_EDITOR
//             _isOpenFilter = true;
// #else
//
// #endif
//
// #elif UNITY_IOS
//
//
//             //iOS低端机列表
//             string[] worstCpuDevice = new string[] {
//                 "iPhone1G",
//                 "iPhone3G",
//                 "iPhone3GS",
//                 "iPhone4",
//                 "VerizoniPhone 4",
//                 "iPhone4S",
//                 "iPodTouch1G",
//                 "iPodTouch2G",
//                 "iPodTouch3G",
//                 "iPodTouch4G",
//                 "iPodTouch5G",
//                 "iPad",
//                 "iPad2(WiFi)",
//                 "iPad2(GSM)",
//                 "iPad2(CDMA)",
//                 "iPad2(32nm)",
//                 "iPadmini(WiFi)",
//                 "iPadmini(GSM)",
//                 "iPadmini(CDMA)"
//             };
//
//             //iOS低端机列表
//             string[] lowCpuDevice = new string[] {
//                 "iPhone5",
//                 "iPhone5C",
//                 "iPad3(WiFi)",
//                 "iPad3(CDMA)",
//                 "iPad3(4G)",
//                 "iPad4(WiFi)",
//                 "iPad4(CDMA)",
//                 "iPadmini3",
//                 "iPadmini2",
//                 "iPadAir",
//             };
//
//             //ios中端机列表
//             string[] midCpuDevice = new string[] {
//                 "iPhone5S",
//                 "iPad4(WiFi)",
//                 "iPad4(CDMA)",
//                 "iPadmini4",
//                 "iPadAir2",
//             };
//
//             //ios高端机列表
//             string[] highCpuDevice = new string[] {
//                 "iPhoneSE",
//                 "iPhone6",
//                 "iPhone6Plus",
//                 "iPhone6s",
//                 "iPhone6sPlus",
//                 "iPhone7",
//                 "iPhone7Plus",
//                 "iPhone8",
//                 "iPhone8plus",
//                 "iPhoneX",
//                 "iPad Pro",
//                 "iPad Pro2"
//             };
//
//             //可以开滤镜的机器
//             string[] lightDevice = new string[] {
//                 "iPhone6s",
//                 "iPhone6sPlus",
//                 "iPhoneSE",
//                 "iPhone7",
//                 "iPhone7Plus",
//                 "iPad Pro",
//                 "iPad Pro2",
//                 "iPhone8",
//                 "iPhone8Plus",
//                 "iPhoneX"
//             };
//
//             string model = deviceInfo.getDeviceModel();
//             model = model.ToLower().Trim();
//
//             //model = "iphone7";
//
//             //比赛中使用高摸车机型
//             for (int i = 0; i < lightDevice.Length; i++)
//             {
//                 string device = lightDevice[i].ToLower().Trim();
//                 if (StringUtil.Contains(device, model, StringComparison.OrdinalIgnoreCase))
//                 {
//                     s_isRaceUseHMCar = true;
//                     break;
//                 }
//             }
//
//             //最差机器
//             for (int i = 0; i < worstCpuDevice.Length; i++)
//             {
//                 string device = worstCpuDevice[i].ToLower().Trim();
//                 if (StringUtil.Contains(device, model, StringComparison.OrdinalIgnoreCase))
//                 {
//                     _gpuLevel = QualityLevel.WORST;
//                     _cpuLevel = QualityLevel.WORST;
//                     return;
//                 }
//             }
//
//             //低端机
//             for (int i = 0; i < lowCpuDevice.Length; i++)
//             {
//                 string device = lowCpuDevice[i].ToLower().Trim();
//                 if (StringUtil.Contains(device, model, StringComparison.OrdinalIgnoreCase))
//                 {
//                     _gpuLevel = QualityLevel.LOW;
//                     _cpuLevel = QualityLevel.LOW;
//                     return;
//                 }
//             }
//
//             //中端机
//             for (int i = 0; i < midCpuDevice.Length; i++)
//             {
//                 string device = midCpuDevice[i].ToLower().Trim();
//                 if (StringUtil.Contains(device, model, StringComparison.OrdinalIgnoreCase))
//                 {
//                     _gpuLevel = QualityLevel.MID;
//                     _cpuLevel = QualityLevel.MID;
//                     return;
//                 }
//             }
//
//             //高端机
//             for (int i = 0; i < highCpuDevice.Length; i++)
//             {
//                 string device = highCpuDevice[i].ToLower().Trim();
//                 if (StringUtil.Contains(device, model, StringComparison.OrdinalIgnoreCase))
//                 {
//                     _gpuLevel = QualityLevel.HIGH;
//                     _cpuLevel = QualityLevel.HIGH;
//                     _isOpenFilter = true;
//                     return;
//                 }
//             }
//
//             //未在列表里找到，应该是iOS最新机型
//             _gpuLevel = QualityLevel.HIGH;
//             _cpuLevel = QualityLevel.HIGH;
//             _isOpenFilter = true;
// #else
//             _gpuLevel = QualityLevel.MID;
// #endif
//
//         }
//
//         /// <summary>
//         /// 设置像素灯数量
//         /// </summary>
//         public void setLightCount()
//         {
//             if (_gpuLevel == QualityLevel.HIGH)
//             {
//                 QualitySettings.pixelLightCount = 6;
//             }
//             else if (_gpuLevel == QualityLevel.MID)
//             {
//                 QualitySettings.pixelLightCount = 3;
//             }
//             else if (_gpuLevel == QualityLevel.LOW)
//             {
//                 QualitySettings.pixelLightCount = 1;
//             }
//             else if (_gpuLevel == QualityLevel.WORST)
//             {
//                 QualitySettings.pixelLightCount = 0;
//             }
//         }
//
//         /// <summary>
//         /// 设置抗锯齿等级
//         /// </summary>
//         public void setAntiAliasing()
//         {
//             if (_gpuLevel == QualityLevel.HIGH)
//             {
//                 QualitySettings.antiAliasing = 3;
//             }
//             else if (_gpuLevel == QualityLevel.MID)
//             {
//                 QualitySettings.antiAliasing = 2;
//             }
//             else
//             {
//                 QualitySettings.antiAliasing = 0;
//             }
//         }
//
//         /// <summary>
//         /// 设置纹理质量
//         /// </summary>
//         public void setAnisotropicFiltering()
//         {
//             if (_gpuLevel == QualityLevel.HIGH)
//             {
//                 QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
//             }
//             else if (_gpuLevel == QualityLevel.MID)
//             {
//                 QualitySettings.anisotropicFiltering = AnisotropicFiltering.Enable;
//             }
//             else if (_gpuLevel == QualityLevel.WORST
//                 || _gpuLevel == QualityLevel.LOW)
//             {
//                 QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;
//             }
//         }
//
//         /// <summary>
//         /// 设置粒子级别
//         /// </summary>
//         /// <param name="obj"></param>
//         public void setParticleLevel(GameObject obj)
//         {
//             ParticleSystem[] psArr = obj.GetComponentsInChildren<ParticleSystem>();
//             if (psArr != null)
//             {
//                 for (int i = 0; i < psArr.Length; i++)
//                 {
//                     if (_gpuLevel == QualityLevel.MID)
//                     {
//                         if (psArr[i].main.maxParticles > 1)
//                         {
//                             psArr[i].startLifetime /= 2;
//                             psArr[i].maxParticles /= 2;
//                         }
//                     }
//                     else if (_gpuLevel == QualityLevel.LOW
//                         || _gpuLevel == QualityLevel.WORST)
//                     {
//                         if (psArr[i].maxParticles > 1)
//                         {
//                             psArr[i].maxParticles /= 4;
//                             psArr[i].startLifetime /= 2;
//                         }
//                     }
//                     if (psArr[i].maxParticles == 0)
//                     {
//                         psArr[i].maxParticles = 1;
//                     }
//                     if (psArr[i].startLifetime < 0.1f)
//                     {
//                         psArr[i].startLifetime = 0.1f;
//                     }
//                 }
//             }
//         }
//
//         /// <summary>
//         /// 设置贴图使用mipmap等级
//         /// </summary>
//         private void setTextureMipmap()
//         {
//             if (_gpuLevel == QualityLevel.HIGH)
//             {
//                 QualitySettings.masterTextureLimit = 0;
//             }
//             else if (_gpuLevel == QualityLevel.MID
//                 || _gpuLevel == QualityLevel.LOW)
//             {
//                 QualitySettings.masterTextureLimit = 1;
//             }
//             else
//             {
//                 QualitySettings.masterTextureLimit = 2;
//             }
//         }
//
//         /// <summary>
//         /// 设置垂直同步数
//         /// </summary>
//         private void setVSyncCount()
//         {
//             if (_gpuLevel == QualityLevel.WORST)
//             {
//                 QualitySettings.vSyncCount = 0; //垂直同步数据[0,1,2]  0为不等待垂直同步 
//                 Application.targetFrameRate = 30;
//                 Time.fixedDeltaTime = 0.03f;
//             }
//         }
//
//         void showDeviceInfo()
//         {
//             //return;n
//
//             string particleLevel = "粒子密度：";
//             string memoryLevel = "内存水平：";
//             string anisotropicFiltering = "各项异性纹理：";
//             string antiAliasing = "抗锯齿：";
//             string vSyncCount = "垂直同步：" + (QualitySettings.vSyncCount == 0 ? "关" : "开");
//             string lightCount = "最大灯光数：" + QualitySettings.pixelLightCount;
//             string openFilter = "相机滤镜：" + (_isOpenFilter ? "开" : "关");
//             string garageReflect = "镜面反射：" + (_isOpenReflect ? "开" : "关");
//             string groundGlass = "毛玻璃：" + (_isOpenGroundGlass ? "开" : "关");
//             string fog = "雾：" + (_gpuLevel == QualityLevel.WORST ? "关" : "开");
//             string shader = "Shader：" + (_gpuLevel == QualityLevel.WORST ? "低" : "高");
//
//             if (_memoryLevel == QualityLevel.HIGH)
//             {
//                 memoryLevel += "【高】";
//             }
//             else if (this._memoryLevel == QualityLevel.MID)
//             {
//                 memoryLevel += "【中】";
//             }
//             else if (this._memoryLevel == QualityLevel.WORST)
//             {
//                 memoryLevel += "【最低】";
//             }
//             else if (this._memoryLevel == QualityLevel.LOW)
//             {
//                 memoryLevel += "【低】";
//             }
//
//             string cpuLevel = "Cpu水平：";
//             if (_cpuLevel == QualityLevel.HIGH)
//             {
//                 cpuLevel += "【高】";
//             }
//             else if (_cpuLevel == QualityLevel.LOW)
//             {
//                 cpuLevel += "【低】";
//             }
//             else if (_cpuLevel == QualityLevel.MID)
//             {
//                 cpuLevel += "【中】";
//             }
//             else if (_cpuLevel == QualityLevel.WORST)
//             {
//                 cpuLevel += "【最低】";
//             }
//
//             string gpuLevel = "Gpu水平：";
//             if (_gpuLevel == QualityLevel.HIGH)
//             {
//                 gpuLevel += "【高】";
//                 particleLevel += "【高】";
//                 anisotropicFiltering += "【强制开启(高)】";
//                 antiAliasing += "3倍";
//             }
//             else if (_gpuLevel == QualityLevel.MID)
//             {
//                 gpuLevel += "【中】";
//                 particleLevel += "【中】";
//                 anisotropicFiltering += "【开启(中)】";
//                 antiAliasing += "2倍";
//             }
//             else if (_gpuLevel == QualityLevel.LOW)
//             {
//                 gpuLevel += "【低】";
//                 particleLevel += "【低】";
//                 anisotropicFiltering += "【关闭(低)】";
//                 antiAliasing += "关闭";
//             }
//             else if (_gpuLevel == QualityLevel.WORST)
//             {
//                 gpuLevel += "【最低】(锁30帧)";
//                 particleLevel += "【低】";
//                 anisotropicFiltering += "【关闭(低)】";
//                 antiAliasing += "关闭";
//             }
//
//             string levelType = "分级方式：";
//             if (_levelType == 1)
//             {
//                 levelType += "【1.数据库中查到cpu+gpu】";
//                 levelType += "id:";
//                 levelType += _deviceId;
//             }
//             else if (_levelType == 2)
//             {
//                 levelType += "【2.数据库中查到cpu】";
//                 levelType += "id:";
//                 levelType += _deviceId;
//             }
//             else if (_levelType == 3)
//             {
//                 levelType += "【3.db中未查到cpu和gpu, 按型号区间划分】";
//             }
//             else if (_levelType == 4)
//             {
//                 levelType += "【4.db中未查到cpu和gpu，按照内存水平分级】";
//             }
//
//             IDeviceInfo deviceInfo = PlatformManager._instance._deviceInfo;
//             string deviceInfoStr =
//                 "设备型号:" + deviceInfo.getDeviceModel() + "\n" +
//                 "设备Id:" + deviceInfo.getDeviceId() + "\n" +
//                 "渠道:" + deviceInfo.getChannelId() + "\n" +
//                 "网络信号:" + deviceInfo.getWifiLevel() + "\n" +
//                 "电池电量:" + deviceInfo.getBatteryLevel() + "\n" +
//                 "Cpu型号:" + deviceInfo.getCpuModel() + "\n" +
//                 "Cpu架构:" + deviceInfo.getCpuArchitectureType() + "\n" +
//                 "Cpu核心数:" + deviceInfo.getCpuCoresNum() + "核\n" +
//                 "Cpu当前频率:" + deviceInfo.getCpuCurFreq() + "mhz\n" +
//                 "Cpu总频率:" + deviceInfo.getCpuTotalFreq() + "mhz\n" +
//                 "Gpu型号:" + deviceInfo.getGpuModel() + "\n" +
//                 "Gpu供应商:" + deviceInfo.getGpuVendor() + "\n" +
//                 "Gpu版本号:" + deviceInfo.getGpuVersion() + "\n" +
//                 "总内存:" + deviceInfo.getTotalMemory() + "m\n" +
//                 "剩余内存:" + deviceInfo.getRemainMemory() + "m\n" +
//                 "Rom总空间:" + deviceInfo.getRomTotalSize() + "m\n" +
//                 "Rom剩余空间:" + deviceInfo.getRomRemainSize() + "m\n" +
//                 "SD卡总空间:" + deviceInfo.getSDCardTotalSize() + "m\n" +
//                 "SD卡剩余空间:" + deviceInfo.getSDCardRemainSize() + "m\n" +
//                 "包名：" + Application.identifier + "\n" +
//                 "App版本号：" + deviceInfo.getAppVersionName() + "\n" +
//                 "Unity版本号：" + Application.unityVersion + "\n" +
//                 "操作系统：" + SystemInfo.operatingSystem + "\n" +
//                 "设备分辨率:" + deviceInfo.getResolution() + "\n" +
//                 "适配分辨率:" + string.Format("{0}x{1}", s_adapterResolutionWidth, s_adapterResolutionHeight) + "\n" +
//                 "Ip地址(wifi):" + deviceInfo.getIpAddressWifi() + "\n" +
//                 "Ip地址(4g):" + deviceInfo.getIpAddress4G() + "\n" +
//                 //"手机号:" + deviceInfo.getPhoneNumber() + "\n" +
//                 "实时光照:" + (s_isRaceUseHMCar ? "开" : "关") + "\n" +
//                 "DPI:" + Screen.dpi + "\n" +
//                 levelType + "\n" +
//                 memoryLevel + "\n" +
//                 cpuLevel + "\n" +
//                 gpuLevel + "\n" +
//                 shader + "\n" +
//                 particleLevel + "\n" +
//                 anisotropicFiltering + "\n" +
//                 antiAliasing + "\n" +
//                 vSyncCount + "\n" +
//                 lightCount + "\n" +
//                 openFilter + "\n";
//             //garageReflect + "\n" +
//             //groundGlass + "\n";
//
//             //Debug.Log(deviceInfoStr);
//             PlatformManager._instance.runOnUIThread().showSimpleDialog(Application.productName, deviceInfoStr, "哦");
//         }
//
//         private Dictionary<string, string> s_shaderDic = new Dictionary<string, string>();
//         private Shader s_replaceShader = null;
//
//         public void setShaderLod(GameObject go)
//         {
//             List<Renderer> renderers = new List<Renderer>();
//             renderers.AddRange(go.GetComponentsInChildren<Renderer>(true));
//
//             for (int i = 0; i < renderers.Count; i++)
//             {
//                 if (renderers[i] == null)
//                 {
//                     continue;
//                 }
//                 MeshRenderer render = new MeshRenderer();
//                 Material[] materials = renderers[i].sharedMaterials;
//                 for (int j = 0; j < materials.Length; j++)
//                 {
//                     if (materials[j] == null || materials[j].shader == null)
//                     {
//                         continue;
//                     }
//
//                     string shaderName = materials[j].shader.name;
//                     if (shaderName.Contains("Lightmap_Fog")
//                         || shaderName.Contains("Unlit_Fog")
//                         || shaderName.Contains("Unlit_Transparent_Tree_Fog")
//                         || shaderName.Contains("Lightmap_Fog_Mask")
//                         || shaderName.Contains("Bumped_Specular_LightMap")
//                         || shaderName.Contains("Bumped_Specular_LightMap_Mask")
//                         || shaderName.Contains("Bumped_Specular_LightMap_Mask2")
//                         || shaderName.Contains("Bumped_Specular_LightMap_Fog"))
//                     {
//                         materials[j].shader.maximumLOD = 99;
//                     }
//                 }
//             }
//         }
//
//         public void screenAdapterForCamera(Camera camera)
//         {
//             camera.fieldOfView = camera.fieldOfView * (16f / 9f / ((float)Screen.width / Screen.height));
//         }
//
//         public float getScreenRatio()
//         {
//             return 16f / 9f / ((float)Screen.width / Screen.height);
//         }
//
//     }
// }