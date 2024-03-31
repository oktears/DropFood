// using System;
// using UnityEngine;
//
// namespace Chengzi
// {
//
//     /// <summary>
//     /// 跨平台管理器
//     /// </summary>
//     public class PlatformManager : Singleton<PlatformManager>, IDisposable
//     {
//         public void Dispose()
//         {
//             GC.Collect();
//         }
//
//         //attribute
//
//         public IDeviceInfo _deviceInfo { get; set; }
//
//         public ITalkingData _talkingData = new ITalkingData();
//
//         public IUIThread _mainUIThread { get; set; }
//
//         public ISDKBridge _sdkBridge { get; set; }
//
//         public IADSDK _adSDK { get; set; }
//
//
// #if UNITY_ANDROID
//         private AndroidContext _androidContext;
//         private AndroidScreenAdapter _androidScreenAdapter;
// #endif
//
//         //包是否被破解
//         public bool _isCracked { get; set; }
//
//         public IUIThread runOnUIThread()
//         {
//             return this._mainUIThread;
//         }
//
//         ~PlatformManager()
//         {
//             GC.Collect();
//             GC.SuppressFinalize(this);
//         }
//
//         //init
//
//         public void init()
//         {
//
// #if UNITY_STANDALONE_WIN
//
//             this._deviceInfo = new DeviceInfoWin32Impl();
//             this._mainUIThread = new Win32UIThread();
//             this._sdkBridge = new SDKBridgeAndroidImpl();
//    
// #elif UNITY_ANDROID
//             // this._deviceInfo = new DeviceInfoAndroidImpl();
//             // this._mainUIThread = new AndroidUIThread();
//             // this._androidContext = new AndroidContext();
//             //this._androidContext.initPlatformContext();
//             //this._androidContext.checkCrack();
//             //this._androidScreenAdapter = new AndroidScreenAdapter();
//             // this._sdkBridge = new SDKBridgeAndroidImpl();
//             //this._androidScreenAdapter.adapter();
//
//             // _adSDK = new ADSDKAndroidImpl();
//             // _adSDK.initAD();
//
//             // GooglePlayManager.Instance.init();
//
//             //this._sdkBridge.login();
//
// #elif UNITY_IPHONE || UNITY_EDITOR_OSX
//             this._deviceInfo = new DeviceInfoiOSImpl();
//             this._mainUIThread = new IOSUIThread();
//             this._sdkBridge = new SDKBridgeiOSImpl();
// #elif UNITY_WEBGL
//             this._deviceInfo = new DeviceInfoHtml5Impl();
//             this._mainUIThread = new Html5UIThread();
//             this._sdkBridge = new SDKBridgeHtml5Impl();
// #endif
//
//             //this._sdkBridge.login();
//             // this._sdkBridge.initSDK();
//             // IAPManager.Instance.init();
//
//             //this._talkingData = new ITalkingData();
//             //this._talkingData.onStart();
//             //this._talkingData.initAccount();
//
//             //_mainUIThread.showToast("welcome 2 game ~", true);
//         }
//
//         //biz logic
//
//         /// <summary>
//         /// 是否为移动平台[Android, iOS, WP]
//         /// </summary>
//         public bool IsMobilePlatform()
//         {
//             return Application.platform == RuntimePlatform.Android
//                 || Application.platform == RuntimePlatform.IPhonePlayer
//                 || Application.platform == RuntimePlatform.WP8Player;
//         }
//
//         /// <summary>
//         /// 屏幕适配
//         /// </summary>
//         public void screenAdapter()
//         {
// #if UNITY_ANDROID
//             _androidScreenAdapter.adapter();
// #endif
//         }
//
//         /// <summary>
//         /// 当前运行环境是否为开发环境
//         /// </summary>
//         /// <returns></returns>
//         public bool IsDevelopmentMode()
//         {
//             return !IsMobilePlatform();
//         }
//
//     }
// }