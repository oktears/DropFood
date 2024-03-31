//
// using UnityEngine;
//
// namespace Chengzi
// {
//
//     /// <summary>
//     /// Android 分辨率适配
//     /// </summary>
//     public class AndroidScreenAdapter
//     {
//         private float adapterWidth = 0;
//         private float adapterHeight = 0;
//
//         private static float DESIGN_WIDTH = 750.0f;
//         private static float DESIGN_HEIGHT = 1334.0f;
//
//
//         /// <summary>
//         /// 屏幕适配
//         /// </summary>
//         public void adapter()
//         {
//             //if (Screen.currentResolution.width >= TARGET_WIDTH)
//             //{ 
//             //    Screen.SetResolution(TARGET_WIDTH, TARGET_HEIGHT, true);
//             //}
//
//             float screenWidth = Screen.currentResolution.width;
//             float screenHeight = Screen.currentResolution.height;
//
//             if (QualityManager.Instance._gpuLevel == QualityLevel.HIGH)
//             {
//                 if (screenWidth > 1080.0f)
//                 {
//                     //固定最大屏幕尺寸 1080p
//
//                     //缩放比
//                     float scaleWidth = screenWidth / 1080.0f;
//                     adapterWidth = 1080.0f;
//                     adapterHeight = screenHeight / scaleWidth;
//
//                     if (adapterWidth % 2 == 0)
//                     {
//                         adapterWidth += 1;
//                     }
//                     else
//                     {
//                         adapterWidth -= 1;
//                     }
//
//                     Screen.SetResolution((int)adapterWidth, (int)adapterHeight, true);
//                 }
//             }
//             else
//             {
//                 //最低端机
//                 if (QualityManager.Instance._gpuLevel == QualityLevel.WORST)
//                 {
//                     DESIGN_WIDTH = 480.0f;
//                     DESIGN_HEIGHT = 854.0f;
//                 }
//
//                 //设计分辨率比例
//                 //16:9=1.777
//                 float s1 = DESIGN_WIDTH / DESIGN_HEIGHT;
//                 //实际分辨率比例
//                 float s2 = screenWidth / screenHeight;
//                 float designWidth = DESIGN_WIDTH;
//                 float designHeight = DESIGN_HEIGHT;
//                 if (s1 < s2)
//                 {
//                     //这种基本可以忽略
//                     designWidth = Mathf.FloorToInt(DESIGN_HEIGHT * s2);
//                 }
//                 else if (s1 >= s2)
//                 {
//                     //4:3 16:10 等等
//
//                     //比如 1080 * 1920
//                     //640 / 1080 = 
//                     designHeight = Mathf.FloorToInt(DESIGN_WIDTH / s2);
//                 }
//                 float scale = designWidth / screenWidth;
//                 if (scale < 1.0f)
//                 {
//                     adapterWidth = designWidth;
//                     adapterHeight = designHeight;
//                 }
//                 Screen.SetResolution((int)adapterWidth, (int)adapterHeight, true);
//                 QualityManager.s_adapterResolutionWidth = adapterWidth;
//                 QualityManager.s_adapterResolutionHeight = adapterHeight;
//                 //string text = "真实分辨率:" + screenWidth + "," + screenHeight +
//                 //    "分辨率:" + adapterWidth + "," + adapterHeight + ",s1=" + s1 + ",s2=" + s2 + ",scale=" + scale;
//                 //PlatformManager.Instance.runOnUIThread().showSimpleDialog("", text, "ok");
//             }
//         }
//     }
// }