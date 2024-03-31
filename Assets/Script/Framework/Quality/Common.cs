using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{
    public class Common
    {
        //Android
        private static int HIGH_MEMORY_ANDROID_TAG = 1400;
        private static int LOW_MEMORY_ANDROID_TAG = 1000;

        //IOS
        private static int HIGH_MEMORY_IOS_TAG = 1000;
        private static int LOW_MEMORY_IOS_TAG = 512;
        //GPU
        private static int LOW_GPU_TAG = 100;


        /** 帧率适配参数    60fps:1   30fps:2 */
        public static float FPSFactor = 1.0f;
        /** 加速度适配参数 FPSFactor^2 */
        public static float AccelerateFactor = 1.0f;
        /** 目标帧率 */
        public static int frameRate = 60;
        public static int frameInterval = 17;

        /** UI设计的最小高宽比(最矮) */
        public static float uiHeightWidthRatioMin = 960f / 640f;
        /** UI设计的最大高宽比(最高) */
        public static float uiHeightWidthRatioMax = 1136f / 640f;

        /** 策划设计关卡数据时使用的像素宽度 */
        public static float designScreenWidth = 640f;
        /** 策划设计关卡数据时使用的像素高度 */
        public static float designScreenHeight = 1136f;
        /** 策划设计关卡数据时的高宽比 */
        public static float designHeightWidthRatio = 1136f / 640f;

        /** 游戏内使用的像素宽度 */
        public static float screenWidth;
        /** 游戏内使用的像素高度 */
        public static float screenHeight;
        /** 游戏内使用的高宽比 */
        public static float screenHeightWidthRatio;

        public static float screenWidthFactor = 1;
        public static float screenHeightFactor = 1;
        public static float screenLengthFactor = 1;

        public static float uiScreenWidth;
        public static float uiScreenHeight;
        public static float uiHeightWidthRatio;



        public static float realScreenWidth
        {
            get { return Screen.width; }
        }

        public static float realScreenHeight
        {
            get { return Screen.height; }
        }

        public static float realDPI
        {
            get
            {
                return Screen.dpi;
            }
        }

        public static float realHeightWidthRatio
        {
            get { return realScreenHeight / realScreenWidth; }
        }

        // 音乐音效开关
        //private static bool bMusicPlay;
        //public static bool IsMusicPlay
        //{
        //    get
        //    {
        //        return bMusicPlay;
        //    }
        //    set
        //    {
        //        bMusicPlay = value;
        //    }
        //}

        //private static bool bSoundPlay;
        //public static bool IsSoundPlay
        //{
        //    get
        //    {
        //        return bSoundPlay;
        //    }
        //    set
        //    {
        //        bSoundPlay = value;
        //    }
        //}



        public static bool isHighMemory = true;
        public static bool isLowMemory = false;
        public static bool isLowCpu = false;

        public static void CheckScreenSize()
        {
            // 比iPhone4还要矮,加黑边,适配按iPhone4屏幕计算
            if (realHeightWidthRatio <= uiHeightWidthRatioMin)
            {
                screenHeight = realScreenHeight;
                screenWidth = screenHeight / uiHeightWidthRatioMin;
            }
            else
            {
                screenHeight = realScreenHeight;
                screenWidth = realScreenWidth;
            }
            screenHeightWidthRatio = screenHeight / screenWidth;
            uiHeightWidthRatio = screenHeightWidthRatio;

            uiScreenWidth = 640f;
            uiScreenHeight = uiScreenWidth * uiHeightWidthRatio;

            screenWidthFactor = screenWidth / designScreenWidth;
            screenHeightFactor = screenHeight / designScreenHeight;
            screenLengthFactor = Mathf.Sqrt(screenHeightFactor * screenHeightFactor + screenWidthFactor * screenWidthFactor);

            Debug.Log("[Common] real size: (" + realScreenWidth + " , " + realScreenHeight + ")\n"
                      + " screen size: (" + screenWidth + " , " + screenHeight + ")  ratio:" + screenHeightWidthRatio + "\n"
                      + " factor: (" + screenWidthFactor + " , " + screenHeightFactor + ")\n"
                      + " ui size: (" + uiScreenWidth + " , " + uiScreenHeight + ")"
                      );
        }

        public static void CheckDeviceInfo()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            isLowMemory = false;
            isLowCpu = false;

            float totalMemory = SystemInfo.systemMemorySize;
            string deviceInfo = SystemInfo.deviceModel;

            // 设定FPS
            if (Application.platform == RuntimePlatform.Android)
            {
                isLowMemory = (totalMemory > 0 && totalMemory <= LOW_MEMORY_ANDROID_TAG);
                isHighMemory = (totalMemory > 0 && totalMemory >= HIGH_MEMORY_ANDROID_TAG);
                //				s_b9100 = deviceInfo.Contains(GT9100);

                /// 安卓30/60FPS判定：
                /// Andreno系列： Andreno320及以上机型跑60帧
                /// PowerVR系列： SGX540以上机型跑60帧
                /// NVIDIA Tegra系列： Unknown
                /// Mali系列： Unknown
                /// Unknown：内存大于1GB且显存(估值)大于100MB

                bool bChecked = false;
                string gpuName = SystemInfo.graphicsDeviceName;
                if (gpuName.Contains("Adreno")) //骁龙
                {
                    int deviceModelID = XTool.GetIntFromString(gpuName);
                    if (deviceModelID >= 0)
                    {
                        bChecked = true;
                        isLowCpu = deviceModelID < 320;
                    }
                    Debug.Log("[Common] check Adreno GPU:" + gpuName + " modelID:" + deviceModelID + " isLow:" + isLowCpu);
                }
                else if (gpuName.Contains("PowerVR SGX"))
                {
                    int deviceModelID = XTool.GetIntFromString(gpuName);
                    if (deviceModelID >= 0)
                    {
                        bChecked = true;
                        isLowCpu = deviceModelID <= 540;
                    }
                    Debug.Log("[Common] check PowerVR GPU:" + gpuName + " modelID:" + deviceModelID + " isLow:" + isLowCpu);
                }
                else if (gpuName.Contains("Mali"))
                {
                    int deviceModelID = XTool.GetIntFromString(gpuName);
                    if (deviceModelID >= 0)
                    {
                        bChecked = true;
                        isLowCpu = deviceModelID == 300 || deviceModelID == 200 || (deviceModelID == 400 && isLowMemory); // Note2是Mali400,可以跑60帧
                    }
                    Debug.Log("[Common] check Mali GPU:" + gpuName + " modelID:" + deviceModelID + " isLow:" + isLowCpu);
                }

                if (!bChecked)
                {
                    isLowCpu = SystemInfo.graphicsMemorySize > 0 && SystemInfo.graphicsMemorySize <= LOW_GPU_TAG;
                }

            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                /// iOS 30/60FPS判定：
                /// iPhone4s及以下机型、iTouch所有机型、iPad2及以下机型设定跑30帧
                isLowMemory = (totalMemory > 0 && totalMemory <= LOW_MEMORY_IOS_TAG);
                isHighMemory = (totalMemory > 0 && totalMemory >= HIGH_MEMORY_IOS_TAG);
                string[] lowCpuDevice = new string[] { "iPhone1,", "iPhone2,", "iPhone3,", "iPhone4,", "iPod3,", "iPod4,", "iPod5", "iPad1,", "iPad2," };
                for (int i = 0; i < lowCpuDevice.Length; i++)
                {
                    if (deviceInfo.StartsWith(lowCpuDevice[i]))
                    {
                        isLowCpu = true;
                        break;
                    }
                }
            }

            if (isLowMemory)
            {
                isLowCpu = true;
            }
            //			isLowCpu = true;
            //			isLowMemory = true;

            if (isLowCpu)
            {
                frameRate = 30;
                frameInterval = 33;
                FPSFactor = 2;
                //渲染与显示设备的刷新率同步
                QualitySettings.vSyncCount = 2; //垂直同步数据[0,1,2]  0为不等待垂直同步 
            }
            else
            {
                frameRate = 60;
                frameInterval = 17;
                FPSFactor = 1;
                QualitySettings.vSyncCount = 1;
            }
            AccelerateFactor = FPSFactor * FPSFactor;

            // Android开了vSync，targetFrameRate不起作用，需要设置vSyncCount
            Application.targetFrameRate = frameRate;

            //// 质量分级
            //QualityManager.LoadQualityCfg();

            //if (Application.platform == RuntimePlatform.Android)
            //{
            //    if (isLowCpu)
            //    {
            //        QualityManager.topQuality = QualityLevel.NORMAL;
            //    }
            //    else
            //    {
            //        QualityManager.topQuality = QualityLevel.HIGH;
            //    }
            //}
            //else if (Application.platform == RuntimePlatform.IPhonePlayer)
            //{
            //    if (isLowCpu)
            //    {
            //        QualityManager.topQuality = QualityLevel.NORMAL;
            //    }
            //    else
            //    {
            //        QualityManager.topQuality = QualityLevel.HIGH;
            //    }
            //}
            //else
            //{
            //    QualityManager.topQuality = QualityLevel.HIGH;
            //}

            //Debug.Log("[Common] LoadQualityCfg:" + QualityManager.currentQuality + " topQuality:" + QualityManager.topQuality);

            //if (QualityManager.currentQuality == QualityLevel.LOW || QualityManager.currentQuality > QualityManager.topQuality)
            //{
            //    QualityManager.currentQuality = QualityManager.topQuality;
            //}

            //Debug.Log("[Common] Device Info:"
            //          + "\ndeviceInfo:" + deviceInfo + "  processorType:" + SystemInfo.processorType + "  processorCount:" + SystemInfo.processorCount + "  memory:" + totalMemory
            //          + "\nGPU:" + SystemInfo.graphicsDeviceName + "  GPU memory:" + SystemInfo.graphicsMemorySize
            //          + "\nisLowMemory:" + isLowMemory + "  isHighMemory:" + isHighMemory + "  isLowCpu:" + isLowCpu + "  frameRate:" + frameRate + "  quality:" + QualityManager.currentQuality);

        }
    }
}
