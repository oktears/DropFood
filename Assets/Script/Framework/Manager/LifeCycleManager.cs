
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 生命周期管理器
    /// </summary>
    public class LifeCycleManager : Singleton<LifeCycleManager>
    {

        /// <summary>
        /// 是否已进入主界面
        /// </summary>
        public bool _isEnterMain { get; set; }

        /// <summary> 
        /// 是否在后台
        /// </summary>
        public bool _isInBackgorund { get; private set; }

        /// <summary>
        /// 是否在比赛中
        /// </summary>
        public bool _isRacing { get; set; }

        /// <summary>
        /// 网络断开状态
        /// </summary>
        public bool _isDisconnectNet { get; set; }

        public void init()
        {
            _isDisconnectNet = false;
        }

        public void initStep1()
        {
            _isInBackgorund = false;
        }

        public void initStep2()
        {
            // PlatformManager.Instance.init();
        }

        public void initStep3()
        {
            // QualityManager.Instance.init();
        }

        public void initStep4()
        {

        }

        public void launcheGame()
        {
#if !UNITY_WEBGL
            Application.targetFrameRate = 60;
#endif
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            EntityManager.Instance.init();
            BusinessManager.Instance.init();
            DaoManager.Instance.init();
            AudioManager.Instance.init();
        }

        public void enterMain()
        {

        }

        /// <summary>
        /// 游戏进入后台
        /// </summary>
        public void background()
        {
            this._isInBackgorund = false;
            Application.targetFrameRate = 1;
            if (LifeCycleManager.Instance._isEnterMain)
            {

            }

#if UNITY_IOS
            PlatformManager.Instance._talkingData.onEnd();
#endif
        }

        /// <summary>
        /// 游戏进入前台
        /// </summary>
        public void foreground()
        {
            this._isInBackgorund = true;
            Application.targetFrameRate = 60;
#if UNITY_IOS
            if (LifeCycleManager.Instance._isEnterMain)
            {
                PlatformManager.Instance._talkingData.onStart();
                PlatformManager.Instance._talkingData.initAccount();
            }
#endif

            //PlatformManager.Instance._adSDK.showInterstitial();

        }

        /// <summary>
        /// 游戏切到后台/前台
        /// </summary>
        /// <param name="isPause"></param>
        public void onApplicationPause(bool isPause)
        {
            if (isPause)
            {
                background();
            }
            else
            {
                foreground();
            }
        }

        public void onQuit()
        {
            if (LifeCycleManager.Instance._isEnterMain)
            {
            }
        }


    }
}
