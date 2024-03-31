using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 设置界面
    /// </summary>
    public class SettingView : PopUpBaseView
    {
        //关闭按钮（空白处）
        private Button _closeBtn;
        //返回按钮
        private Button _backBtn;
        //语言按钮
        private Button _languageBtn;
        //分享按钮
        private Button _shareBtn;
        //隐私政策按钮
        private Button _privacyBtn;
        //评价按钮
        private Button _evaluateBtn;
        //cdk按钮
        private Button _cdkeyBtn;
        //内购恢复按钮
        private Button _restoreBtn;


        //调试按钮，解锁所有关卡
        private Button _debugUnlockBtn;


        //清理存档按钮
        private Button _resetBtn;

        //秘籍界面
        private GameObject _passcodeView;
        private InputField _passcodeInputField;
        private Button _passcodeOk;

        private int _clickDebugTimes = 0;

        private const int COMMON_BTN_FONT_SIZE = 46;

        private Vector2 _languageTextPos;
        private Vector2 _titlePos;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.SETTING;

            bindTarget("Prefab/UI/Setting/SettingView", ViewManager.Instance._UIRoot2D);

            _closeBtn = _viewObj.Find("CloseBtn").GetComponent<Button>();
            _closeBtn.addListener(clickBackBtn);

            _backBtn = _viewObj.Find("Mask/Group/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);

            _languageBtn = _viewObj.Find("Mask/Group/LanguageBtn").GetComponent<Button>();
            _languageBtn.addListener(clickLanguageBtn);

            _privacyBtn = _viewObj.Find("Mask/Group/PrivacyBtn").GetComponent<Button>();
            _privacyBtn.addListener(clickPrivacyBtn);

            _shareBtn = _viewObj.Find("Mask/Group/ShareBtn").GetComponent<Button>();
            _shareBtn.addListener(clickShareBtn);

            _evaluateBtn = _viewObj.Find("Mask/Group/EvaluateBtn").GetComponent<Button>();
            _evaluateBtn.addListener(clickEvaluateBtn);

            _cdkeyBtn = _viewObj.Find("Mask/Group/CDkeyBtn").GetComponent<Button>();
            _cdkeyBtn.addListener(clickCDKBtn);

            _restoreBtn = _viewObj.Find("Mask/Group/RestoreBtn").GetComponent<Button>();
            _restoreBtn.addListener(clickRestoreBtn);

            _debugUnlockBtn = _viewObj.Find("Mask/Group/Debug_UnlockLevelBtn").GetComponent<Button>();
            _debugUnlockBtn.addListener(clickDebugUnlockBtn);

            _passcodeView = _viewObj.Find("Mask/Group/PasscodeView").gameObject;
            _passcodeView.SetActive(false);
            _passcodeInputField = _passcodeView.transform.Find("InputField").GetComponent<InputField>();
            _passcodeOk = _passcodeView.transform.Find("PasscodeOkBtn").GetComponent<Button>();
            _passcodeOk.addListener(clickPasscodeOkBtn);

            //音乐&音效控制部分
            Button bgmBtn = _viewObj.Find("Mask/Group/Audio/Bgm/BgmBtn").GetComponent<Button>();
            bgmBtn.onClick.AddListener(clickBgmBtn);

            Button sfxBtn = _viewObj.Find("Mask/Group/Audio/Sfx/SfxBtn").GetComponent<Button>();
            sfxBtn.onClick.AddListener(clickSfxBtn);

            _languageTextPos = _languageBtn.transform.Find("Text").localPosition;

            _titlePos = _viewObj.Find("Mask/Group/Title/Text").localPosition;

            // string versionInfo = "Version " + PlatformManager.Instance._deviceInfo.getAppVersionName() + " GooglePlay";
            // _viewObj.Find("Mask/Group/AboutInfo/Version").GetComponent<Text>().text = versionInfo;


            refreshBgmView();
            refreshSfxView();
            updateTextI18N();

            _viewObj.Find("Mask/Group").GetComponent<TipShowAnimation>().PlayAnimation(null);
        }

        private void updateTextI18N()
        {
            _viewObj.Find("Mask/Group/Title/Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SETTING);
            _languageBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_LANGUAGE);
            _restoreBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RESTORE);
            _shareBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SHARE);
            _evaluateBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RATE_US);
            _cdkeyBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CDKEY);
            _privacyBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PRIVACY_POLICY);

            if (BusinessManager.Instance._userBiz.isChinese())
            {
                _languageBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);
                _evaluateBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);
                _privacyBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE);

                //_languageBtn.transform.Find("Text").localPosition = new Vector2(_languageTextPos.x, _languageTextPos.y);
                _viewObj.Find("Mask/Group/Title/Text").localPosition = new Vector2(_titlePos.x, _titlePos.y);
            }
            else
            {
                _languageBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE * 0.9f);
                _evaluateBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE * 0.9f);
                _privacyBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(COMMON_BTN_FONT_SIZE * 0.9f);

                //_languageBtn.transform.Find("Text").localPosition = new Vector2(_languageTextPos.x - 3, _languageTextPos.y);
                _viewObj.Find("Mask/Group/Title/Text").localPosition = new Vector2(_titlePos.x, _titlePos.y + 8);
            }
        }

        private void clickBackBtn()
        {
            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(close);
        }

        private void clickLanguageBtn()
        {
            //切换中英
            ViewManager.Instance.getView(ViewConstant.ViewId.LANGUAGE);
        }

        /// <summary>
        /// 隐私政策
        /// </summary>
        private void clickPrivacyBtn()
        {
            Application.OpenURL("http://orangeh5.cn/wechat_orangeh5/official/privacy-policy.html");
        }

        /// <summary>
        /// 分享按钮
        /// </summary>
        private void clickShareBtn()
        {
            string content = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SHARE_CONTENT);
            string website = "https://play.google.com/store/apps/details?id=com.cc.dropfood";
            string text = content + "\n" + website;
            // PlatformManager.Instance.runOnUIThread().shareText(text);
        }

        /// <summary>
        /// 评价按钮
        /// </summary>
        private void clickEvaluateBtn()
        {
            //TODO 
            string testPackage = "com.cc.dropfood";
            // PlatformManager.Instance.runOnUIThread().openAppstore(testPackage);
        }

        /// <summary>
        /// CDK
        /// </summary>
        private void clickCDKBtn()
        {
            CommonViewManager.Instance.showTip(BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CDKEY_NOT_OPEN));
        }

        /// <summary>
        /// 内购恢复
        /// </summary>
        private void clickRestoreBtn()
        {
            //TODO 
            // IAPManager.Instance.restorePurchases();
        }

        private void clickDebugUnlockBtn()
        {
            _clickDebugTimes++;
            if (_clickDebugTimes >= 10)
            {
                if (!_passcodeView.activeInHierarchy)
                {
                    showPasscodeView(true);
                }
                _clickDebugTimes = 0;
            }
        }

        private void clickPasscodeOkBtn()
        {
            if (_passcodeInputField.text.Equals("2018"))
            {
                ViewEvent updateEvent = new ViewEvent(ViewConstant.ViewId.LEVEL,
                    ViewEventConstant.EVENT_LEVEL_VIEW_REFRESH_LEVEL);
                EventCenter.Instance.send(updateEvent);

                CommonViewManager.Instance.showTip("似乎发生了一些神奇的事情!");
            }
            showPasscodeView(false);
        }

        private void showPasscodeView(bool isShow)
        {
            _passcodeView.SetActive(isShow);
        }

        private void clickBgmBtn()
        {
            EntityManager.Instance._userEntity._isOpenBgm = !EntityManager.Instance._userEntity._isOpenBgm;
            DaoManager.Instance._userDao.saveBgm();
            if (EntityManager.Instance._userEntity._isOpenBgm)
            {
                AudioManager.Instance.playBGM(AudioManager.SOUND_BGM_COMMON);
            }
            else
            {
                AudioManager.Instance.stopBGM();
            }
            refreshBgmView();
        }

        private void refreshBgmView()
        {
            if (EntityManager.Instance._userEntity._isOpenBgm)
            {
                _viewObj.Find("Mask/Group/Audio/Bgm/MoveGroup").localPosition = new Vector3(0, 0, 0);
                ShaderManager.Instance.setUIDefaultMaterial(_viewObj.Find("Mask/Group/Audio/Bgm"), true);
            }
            else
            {
                _viewObj.Find("Mask/Group/Audio/Bgm/MoveGroup").localPosition = new Vector3(-130, 0, 0);
                ShaderManager.Instance.openGrayEffect(_viewObj.Find("Mask/Group/Audio/Bgm"), true);
            }
        }

        private void clickSfxBtn()
        {
            EntityManager.Instance._userEntity._isOpenSfx = !EntityManager.Instance._userEntity._isOpenSfx;
            DaoManager.Instance._userDao.saveSfx();
            refreshSfxView();
        }

        private void refreshSfxView()
        {
            if (EntityManager.Instance._userEntity._isOpenSfx)
            {
                _viewObj.Find("Mask/Group/Audio/Sfx/MoveGroup").localPosition = new Vector3(0, 0, 0);
                ShaderManager.Instance.setUIDefaultMaterial(_viewObj.Find("Mask/Group/Audio/Sfx"), true);
            }
            else
            {
                _viewObj.Find("Mask/Group/Audio/Sfx/MoveGroup").localPosition = new Vector3(-130, 0, 0);
                ShaderManager.Instance.openGrayEffect(_viewObj.Find("Mask/Group/Audio/Sfx"), true);
            }
        }

        private void clickResetBtn()
        {
            //清存档
            BusinessManager.Instance._userBiz.resetGame();
            ViewEvent updateEvent = new ViewEvent(ViewConstant.ViewId.LEVEL,
            ViewEventConstant.EVENT_LEVEL_VIEW_REFRESH_LEVEL);
            EventCenter.Instance.send(updateEvent);
            CommonViewManager.Instance.showTip("游戏存档已清除!");
        }


        /// <summary>View事件接受</summary>
        /// <param name="e"></param>
        public override void onReceive(ViewEvent e)
        {
            if (e._viewID == ViewConstant.ViewId.SETTING)
            {
                if (e._eventType == ViewEventConstant.EVENT_SETTING_VIEW_UPDATE_I18N)
                {
                    updateTextI18N();
                }
            }
        }
    }
}
