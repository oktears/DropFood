// using admob;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using UnityEngine;
//
// namespace Chengzi
// {
//     public class ADSDKAndroidImpl : IADSDK
//     {
//
//         private string _bannerID = "ca-app-pub-5342372333654292/7813313477";
//         private string _interstitialID = "ca-app-pub-5342372333654292/9290826924";
//         private string _videoID = "";
//
//
//         public override void initAD()
//         {
//             initAdmob();
//         }
//
//         private void initAdmob()
//         {
//             AdProperties adProperties = new AdProperties();
//             adProperties.isTesting(false);
//             adProperties.isAppMuted(false);
//             adProperties.isUnderAgeOfConsent(false);
//             adProperties.appVolume(100);
//             adProperties.maxAdContentRating(AdProperties.maxAdContentRating_G);
//             //string[] keywords = { "diagram", "league", "brambling" };
//             //adProperties.keyworks(keywords);
//
//             Admob ad = Admob.Instance();
//             ad.bannerEventHandler += onBannerEvent;
//             ad.interstitialEventHandler += onInterstitialEvent;
//             ad.rewardedVideoEventHandler += onRewardedVideoEvent;
//             ad.nativeBannerEventHandler += onNativeBannerEvent;
//             ad.initSDK(adProperties);//reqired,adProperties can been null
//
//             Admob.Instance().loadRewardedVideo(ADConstant.VIDEO_MAIN_VIEW_GAIN_GOLD);
//             Admob.Instance().loadInterstitial(_interstitialID);
//         }
//
//         public override void showVideoAD(ADConstant.VideoADPos videoId, int itemId)
//         {
//             showVideoAD(videoId);
//             _itemId = itemId;
//         }
//
//         public override void showVideoAD(ADConstant.VideoADPos videoADPos)
//         {
//
//             string videoId = "";
//             switch (videoADPos)
//             {
//                 case ADConstant.VideoADPos.MAIN_GAIN_GOLD:
//                     videoId = ADConstant.VIDEO_MAIN_VIEW_GAIN_GOLD;
//                     break;
//                 case ADConstant.VideoADPos.RESTART_GAME:
//                     videoId = ADConstant.VIDEO_COMMON;
//                     break;
//                 case ADConstant.VideoADPos.RELIVE:
//                     videoId = ADConstant.VIDEO_RESULT_VIEW_RELIVE;
//                     break;
//                 default:
//                     videoId = ADConstant.VIDEO_COMMON;
//                     break;
//             }
//
//             if (Admob.Instance().isRewardedVideoReady())
//             {
//                 Admob.Instance().showRewardedVideo();
//                 //1秒后加载新广告
//                 vp_Timer.In(1.0f, new vp_Timer.Callback(() =>
//                 {
//                     preloadVideoAD(videoADPos);
//                 }));
//             }
//             else
//             {
//                 preloadVideoAD(videoADPos);
//             }
//
//             _lastVideoPos = videoADPos;
//             _videoID = videoId;
//         }
//
//         public override void preloadVideoAD(ADConstant.VideoADPos videoADPos)
//         {
//
//             string videoId = "";
//             switch (videoADPos)
//             {
//                 case ADConstant.VideoADPos.MAIN_GAIN_GOLD:
//                     videoId = ADConstant.VIDEO_MAIN_VIEW_GAIN_GOLD;
//                     break;
//                 case ADConstant.VideoADPos.RESTART_GAME:
//                     videoId = ADConstant.VIDEO_COMMON;
//                     break;
//                 case ADConstant.VideoADPos.RELIVE:
//                     videoId = ADConstant.VIDEO_RESULT_VIEW_RELIVE;
//                     break;
//                 default:
//                     videoId = ADConstant.VIDEO_COMMON;
//                     break;
//             }
//
//             Admob.Instance().loadRewardedVideo(videoId);
//             _lastVideoPos = videoADPos;
//             _videoID = videoId;
//         }
//
//         public override void showBannerAD(bool isShow)
//         {
//             if (EntityManager.Instance._userEntity._isRemovedAD)
//             {
//                 return;
//             }
//
//             if (isShow)
//             {
//                 Admob.Instance().showBannerRelative(_bannerID, AdSize.BANNER, AdPosition.BOTTOM_CENTER);
//             }
//             else
//             {
//                 Admob.Instance().removeBanner();
//             }
//         }
//
//         public override void showInterstitial()
//         {
//             if (EntityManager.Instance._userEntity._isRemovedAD)
//             {
//                 return;
//             }
//
//             if (Admob.Instance().isInterstitialReady())
//             {
//                 Admob.Instance().showInterstitial();
//             }
//             else
//             {
//                 Admob.Instance().loadInterstitial(_interstitialID);
//             }
//         }
//
//         void onInterstitialEvent(string eventName, string msg)
//         {
//             Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
//             if (eventName == AdmobEvent.onAdLoaded)
//             {
//             }
//         }
//
//         void onBannerEvent(string eventName, string msg)
//         {
//             Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
//         }
//
//         public void onRewardedVideoEvent(string eventName, string msg)
//         {
//             ThreadPool.Instance.runOnRenderThread(() =>
//             {
//
//                 Debug.Log("handler onRewardedVideoEvent---" + eventName + "  rewarded: " + msg);
//                 if (eventName == AdmobEvent.onAdFailedToLoad)
//                 {
//                     //preloadVideoAD(_lastVideoPos);
//                     Debug.Log("视频广告加载失败");
//                     PlatformManager.Instance.runOnUIThread().showToast("广告加载失败！", true);
//                 }
//                 else if (eventName == AdmobEvent.onRewarded)
//                 {
//                     if (_lastVideoPos == ADConstant.VideoADPos.RESTART_GAME)
//                     {
//                         //重新游戏
//                     }
//                     else if (_lastVideoPos == ADConstant.VideoADPos.MAIN_GAIN_GOLD)
//                     {
//                         //获取金币
//                         Bundle bundle = new Bundle();
//                         GainItemData data = new GainItemData();
//                         data._type = GainItemType.AD_GOLD;
//                         data._gold = 188;
//                         bundle.PutObject("GainItemData", data);
//                         ViewManager.Instance.getView(ViewConstant.ViewId.GAIN_ITEM, bundle);
//                     }
//                     else if (_lastVideoPos == ADConstant.VideoADPos.RELIVE)
//                     {
//                         //复活
//                         ViewEvent reliveEntvt = new ViewEvent(ViewConstant.ViewId.GAME_RESULT, ViewEventConstant.EVENT_GAME_RESULT_VIEW_RELIVE);
//                         EventCenter.Instance.send(reliveEntvt);
//                     }
//                     else if (_lastVideoPos == ADConstant.VideoADPos.GAIN_ITEM)
//                     {
//                         //获取美食
//                         Bundle bundle = new Bundle();
//                         GainItemData data = new GainItemData();
//                         data._type = GainItemType.ITEM;
//                         data._itemId = _itemId;
//                         bundle.PutObject("GainItemData", data);
//                         ViewManager.Instance.getView(ViewConstant.ViewId.GAIN_ITEM, bundle);
//                         BusinessManager.Instance._userBiz.addCollection(_itemId);
//                         DaoManager.Instance._userDao.saveCollection();
//
//                         //更新收藏品界面金币条
//                         ViewEvent goldEvent2 = new ViewEvent(ViewConstant.ViewId.COLLECTION_LIST, ViewEventConstant.EVENT_COLLECTION_VIEW_UPDATE_GOLD);
//                         EventCenter.Instance.send(goldEvent2);
//
//                         //更新收藏品详情页按钮
//                         ViewEvent goldEvent3 = new ViewEvent(ViewConstant.ViewId.COLLECTION_INFO, ViewEventConstant.EVENT_COLLECTION_INFO_VIEW_UPDATE_BUTTON);
//                         EventCenter.Instance.send(goldEvent3);
//                     }
//
//                     //发放奖励
//                     //PlatformManager.Instance.runOnUIThread().showToast("msg=" + msg, true);
//                 }
//                 else if (eventName == AdmobEvent.onAdClosed)
//                 {
//                     //预加载下一个视频广告
//                     preloadVideoAD(_lastVideoPos);
//                     Debug.Log("关闭视频广告，加载下一个");
//                 }
//             });
//
//         }
//
//         void onNativeBannerEvent(string eventName, string msg)
//         {
//             Debug.Log("handler onAdmobNativeBannerEvent---" + eventName + "   " + msg);
//         }
//
//
//     }
// }
