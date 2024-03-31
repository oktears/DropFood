using DG.Tweening;
using Org.BouncyCastle.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{
    /// <summary>
    /// 主界面
    /// </summary>
    public class MainView : FirstBaseView
    {

        //排行按钮
        private Button _rankBtn;
        //排行按钮
        private Button _themeBtn;
        //收藏按钮
        private Button _collectionBtn;

        //设置按钮
        private Button _settingBtn;

        //单机按钮
        private Button _normalBtn;
        //商城按钮
        private Button _shopBtn;
        //继续按钮
        private Button _continueBtn;
        //黑遮罩层
        private Image _balckMask;
        //关于按钮
        //private Button _aboutBtn;
        //关闭按钮
        private Button _closeBtn;
        //金币条
        private Text _gold;
        //分数
        private Text _score;
        //看广告领取金币
        private Button _adBtn;

        //测试
        private Button _addGoldBtn;

        private const int COMMON_BTN_FONT_SIZE = 45;
        private const int NORMAL_BTN_FONT_SIZE = 68;


        //Logo
        private Transform _logo;

        private vp_Timer.Handle _timer = new vp_Timer.Handle();

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.MAIN;
            bindTarget("Prefab/UI/Main/MainView", ViewManager.Instance._UIRoot2D);

            _normalBtn = _viewObj.Find("Group/NormalBtn").GetComponent<Button>();
            _normalBtn.addListener(clickNormalBtn);

            _rankBtn = _viewObj.Find("Group/RankBtn").GetComponent<Button>();
            _rankBtn.addListener(clickRankBtn);

            _collectionBtn = _viewObj.Find("Group/CollectionBtn").GetComponent<Button>();
            _collectionBtn.addListener(clickCollectionBtn);

            _themeBtn = _viewObj.Find("Group/ThemeBtn").GetComponent<Button>();
            _themeBtn.addListener(clickThemeBtn);

            _logo = _viewObj.Find("Group/Logo");

            //_aboutBtn = _viewObj.Find("Adapter/AboutBtn").GetComponent<Button>();
            //_aboutBtn.addListener(clickAboutBtn);
            //_aboutBtn.gameObject.SetActive(false);

            _settingBtn = _viewObj.Find("Group/SettingBtn").GetComponent<Button>();
            _settingBtn.addListener(clickSettingBtn);

            _shopBtn = _viewObj.Find("Group/ShopBtn").GetComponent<Button>();
            _shopBtn.addListener(clickShopBtn);

            _gold = _viewObj.Find("Group/GoldBar/Text").GetComponent<Text>();
            if (EntityManager.Instance._userEntity._goldCount < 10)
            {
                _gold.text = EntityManager.Instance._userEntity._goldCount + "";
            }
            else
            {
                _gold.text = string.Format("{0:0,0}", EntityManager.Instance._userEntity._goldCount);
            }

            _score = _viewObj.Find("Group/FoodBar/Text").GetComponent<Text>();
            _score.text = EntityManager.Instance._userEntity._gameScore.ToString();


            _adBtn = _viewObj.Find("Group/ADBtn").GetComponent<Button>();
            _adBtn.addListener(clickADBtn);

            _addGoldBtn = _viewObj.Find("Group/AddGoldBtn").GetComponent<Button>();
            _addGoldBtn.addListener(clickAddGoldBtn);
            _addGoldBtn.gameObject.SetActive(DebugConfig.s_isDebugMode);

            float s1 = 16.0f / 9.0f;
            //实际分辨率比例
            float s2 = Screen.height * 1.0f / Screen.width;
            if (s2 > s1)
            {
                float aspectRatio = s2 / s1;
                _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }
            else
            {
                //float aspectRatio = s1 / s2;
                // _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }

            if (Launcher.s_levelMode == LevelMode.EDIT)
            {
                clickNormalBtn();
            }

            //if (LifeCycleManager.Instance._isEnterMain)
            //{
            //    clickContinueBtn();
            //}

            updateTextI18N();

            playBgm();
            if (EntityManager.Instance._userEntity._gameScore == 0)
            {
                //第一次游戏不显示广告条
                _score.enabled = false;
            }
            else
            {
                //显示广告条
                // PlatformManager.Instance._adSDK.showBannerAD(true);
            }

            // PlatformManager.Instance._adSDK.preloadVideoAD(ADConstant.VideoADPos.MAIN_GAIN_GOLD);

            // GooglePlayManager.Instance.login();

            //CommonViewManager.Instance.showWaitView(true, "Wating...");
        }

        private void updateTextI18N()
        {
            _normalBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PLAY_GAME);
            _themeBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_THEME);
            _collectionBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CATE);
            _shopBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SHOP);
            _rankBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RANK);

            if (BusinessManager.Instance._userBiz.isChinese())
            {
                _viewObj.Find("Group/Logo/EN").gameObject.SetActive(false);
                _viewObj.Find("Group/Logo/CN").gameObject.SetActive(true);
                _normalBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(NORMAL_BTN_FONT_SIZE);
                _themeBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);
                //_collectionBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);
                //_shopBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);
                //_rankBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);
            }
            else
            {
                _viewObj.Find("Group/Logo/EN").gameObject.SetActive(true);
                _viewObj.Find("Group/Logo/CN").gameObject.SetActive(false);
                _normalBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(NORMAL_BTN_FONT_SIZE * 0.8f);
                _themeBtn.transform.Find("Text").GetComponent<Text>().fontSize = 40;
                //_collectionBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE * 0.8f);
                //_shopBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE * 0.8f);
                //_rankBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE * 0.8f);
            }
        }


        public override void close()
        {
            base.close();
            if (_timer != null)
            {
                _timer.Cancel();
            }
            //AudioManager.Instance.stopDelayBgm();
            //AudioManager.Instance.fadeOutBg(0.8f);
        }

        private void playBgm()
        {
            AudioManager.Instance.fadeInBgm(AudioManager.SOUND_BGM_COMMON, 1.0f);
        }

        private void updateGoldText()
        {
            if (EntityManager.Instance._userEntity._goldCount < 10)
            {
                _gold.text = EntityManager.Instance._userEntity._goldCount + "";

            }
            else
            {
                _gold.text = string.Format("{0:0,0}", EntityManager.Instance._userEntity._goldCount);
                _gold.transform.DOScale(new Vector2(1.1f, 1.1f), 0.3f);
                _gold.transform.DOScale(new Vector2(1.0f, 1.0f), 0.3f).SetDelay(0.3f);
            }
        }

        private void clickNormalBtn()
        {
            if (_timer != null)
            {
                _timer.Cancel();
            }
            replace2Game();
            // PlatformManager.Instance._adSDK.showBannerAD(false);
        }

        private void clickCollectionBtn()
        {
            //点击收藏品按钮
            if (_timer != null)
            {
                _timer.Cancel();
            }
            //AudioManager.Instance.fadeOutBg(0.8f);
            ViewManager.Instance.getView(ViewConstant.ViewId.COLLECTION_LIST);
        }

        /// <summary>
        /// 排行榜
        /// </summary>
        private void clickRankBtn()
        {
            //CommonViewManager.Instance.showTip(BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RANK_NOT_OPEN));
            // GooglePlayManager.Instance.showRank();
        }

        /// <summary>
        /// 主题
        /// </summary>
        private void clickThemeBtn()
        {
            CommonViewManager.Instance.showTip(BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_THEME_NOT_OPEN));
        }

        public void replace2Game()
        {
            BusinessManager.Instance._gameBiz.init();
            SceneManager.Instance.LoadScene(SceneType.GAME, false);
        }

        private void clickSettingBtn()
        {
            ViewManager.Instance.getView(ViewConstant.ViewId.SETTING);
        }

        private void clickShopBtn()
        {
            ViewManager.Instance.getView(ViewConstant.ViewId.SHOP);
        }

        private void clickADBtn()
        {
// #if UNITY_EDITOR
            // PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.MAIN_GAIN_GOLD);
            // (PlatformManager.Instance._adSDK as ADSDKAndroidImpl).onRewardedVideoEvent("onRewarded", "");
// #else
            // PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.MAIN_GAIN_GOLD);
// #endif
        }

        private void clickAddGoldBtn()
        {
            BusinessManager.Instance._userBiz.addGold(10000);
            AudioManager.Instance.play(AudioManager.SOUND_SFX_GAIN_GOLD);
            ViewEvent goldEvent = new ViewEvent(ViewConstant.ViewId.MAIN, ViewEventConstant.EVENT_MAIN_VIEW_AD_GOLD);
            EventCenter.Instance.send(goldEvent);
        }

        private void playBlackMaskAnim()
        {
            _balckMask.DOFade(0.5f, 2.0f);
        }

        private void fadeOutAnimFinish()
        {
            //_aboutBtn.gameObject.SetActive(false);
            _closeBtn.gameObject.SetActive(false);
            _continueBtn.enabled = true;
            _logo.gameObject.SetActive(true);
        }

        /// <summary>View事件接受</summary>
        /// <param name="e"></param>
        public override void onReceive(ViewEvent e)
        {
            if (e._viewID == ViewConstant.ViewId.MAIN)
            {
                if (e._eventType == ViewEventConstant.EVENT_MAIN_VIEW_PLAY_BGM)
                {
                    _timer = new vp_Timer.Handle();
                    vp_Timer.In(0.2f, delegate ()
                    {
                        playBgm();
                    }, _timer);
                }
                else if (e._eventType == ViewEventConstant.EVENT_MAIN_VIEW_AD_GOLD)
                {
                    updateGoldText();
                }
                else if (e._eventType == ViewEventConstant.EVENT_MAIN_VIEW_UPDATE_I18N)
                {
                    updateTextI18N();
                }
            }
        }
    }
}