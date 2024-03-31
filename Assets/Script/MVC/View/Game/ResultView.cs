using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{
    /// <summary>
    /// 关卡结算界面
    /// </summary>
    public class ResultView : PopUpBaseView
    {
        private Button _restartBtn;
        private Button _exitBtn;
        private Button _reliveBtn;
        private Button _shareBtn;
        private Button _evaluateBtn;


        //private CanvasGroup _canvasGroup;
        //private bool _isPlayFadeIn = false;

        //private Image _blackLayer;

        private Text _score;
        private Text _gameTime;
        private Text _historyScore;
        private Text _gold;

        public Transform _newScoreEffect;
        private vp_Timer.Handle _timer = new vp_Timer.Handle();

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GAME_RESULT;
            bindTarget("Prefab/UI/Game/ResultView", ViewManager.Instance._UIRoot2D);

            //_canvasGroup = _viewObj.Find("Bg").GetComponent<CanvasGroup>();
            //_canvasGroup.alpha = 0;

            _viewObj.Find("Mask/Group/Title/TitleInfo").GetComponent<Text>().text =
                BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_GAME_OVER);

            _restartBtn = _viewObj.Find("Mask/Group/RestartBtn").GetComponent<Button>();
            _restartBtn.addListener(restartGameBtnClick);

            _exitBtn = _viewObj.Find("Mask/Group/ExitBtn").GetComponent<Button>();
            _exitBtn.addListener(clickExitGameBtn);

            _reliveBtn = _viewObj.Find("Mask/Group/ReliveBtn").GetComponent<Button>();
            _reliveBtn.transform.Find("Text").GetComponent<Text>().text =
                BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_REVIVE);
            _reliveBtn.addListener(clickReliveBtn);

            _shareBtn = _viewObj.Find("Mask/Group/ShareBtn").GetComponent<Button>();
            _shareBtn.addListener(clickShareBtn);
            _shareBtn.transform.Find("Text").GetComponent<Text>().text =
                BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SHARE);

            if (!DebugConfig.s_isUnlimitRelive)
            {
                _reliveBtn.gameObject.SetActive(BusinessManager.Instance._gameBiz._reliveTimes > 0);
                _shareBtn.gameObject.SetActive(BusinessManager.Instance._gameBiz._reliveTimes <= 0);
            }
            else
            {
                _shareBtn.gameObject.SetActive(false);
                _reliveBtn.gameObject.SetActive(true);
            }


            _evaluateBtn = _viewObj.Find("Mask/Group/EvaluateBtn").GetComponent<Button>();
            _evaluateBtn.addListener(clickEvaluateBtn);


            //offsetMin： 对应Left、Bottom
            //offsetMax： 对应Right、Top

            if (EntityManager.Instance._userEntity._isRemovedAD)
            {
                //已经去广告的玩家，复活按钮不再显示广告图标
                _reliveBtn.transform.Find("Icon").gameObject.SetActive(false);
                RectTransform rect = _reliveBtn.transform.Find("Text").GetComponent<RectTransform>();
                rect.offsetMin = new Vector2(0, rect.offsetMin.y);
                rect.offsetMax = new Vector2(0, rect.offsetMax.y);
            }

            //_blackLayer = _viewObj.Find("Bg/BlackLayer").GetComponent<Image>();

            _score = _viewObj.Find("Mask/Group/Score").GetComponent<Text>();
            _score.text = BusinessManager.Instance._gameBiz._score + "";
            _viewObj.Find("Mask/Group/ScoreLabel").GetComponent<Text>().text =
                BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_GAME_SCORE);

            //_gameTime = _viewObj.Find("Mask/Bg/GameTime").GetComponent<Text>();
            //_gameTime.text = DateTimeUtil.getSecondToFormatTime(BusinessManager.Instance._gameBiz._eclipseTime);

            _gold = _viewObj.Find("Mask/Group/Gold").GetComponent<Text>();
            _gold.text = "+" + BusinessManager.Instance._gameBiz._gainGold;

            _historyScore = _viewObj.Find("Mask/Group/BestScore").GetComponent<Text>();
            _historyScore.text = EntityManager.Instance._userEntity._gameScore.ToString();
            _viewObj.Find("Mask/Group/BestLabel").GetComponent<Text>().text =
                BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_HIGHEST_SCORE);


            _newScoreEffect = _viewObj.Find("Mask/Group/NewScoreEffect");

            updateTextI18N();

            if (!BusinessManager.Instance._gameBiz._isNewRecord)
            {
                //没破记录
                _viewObj.Find("Mask/Group/New").gameObject.SetActive(false);
            }
            else
            {
                //破记录
                _timer = new vp_Timer.Handle();
                vp_Timer.In(1.0f, delegate()
                {
                    _newScoreEffect.gameObject.SetActive(true);
                    _timer.Cancel();
                }, _timer);


                _viewObj.Find("Mask/Group/New").gameObject.SetActive(true);
            }

            hide();

            BusinessManager.Instance._gameBiz._cameraMoveTimes++;

            XTool.DelayCall(2.0f + BusinessManager.Instance._gameBiz._cameraMoveTimes * 0.1f, showView);

            // PlatformManager.Instance._adSDK.preloadVideoAD(ADConstant.VideoADPos.RELIVE);


            //AudioManager.Instance.playBG(AudioManager.SOUND_BGM_GAME_1_1);
            //playFadeInAnim();
        }


        private void updateTextI18N()
        {
            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                Vector2 titlePos = _viewObj.Find("Mask/Group/Title/TitleInfo").localPosition;
                _viewObj.Find("Mask/Group/Title/TitleInfo").localPosition = new Vector2(titlePos.x, titlePos.y + 4.0f);

                _reliveBtn.transform.Find("Text").GetComponent<Text>().fontSize = 40;
            }
        }

        private void showView()
        {
            show();
            _viewObj.Find("Mask/Group").GetComponent<TipShowAnimation>().PlayAnimation(null);
            AudioManager.Instance.play(AudioManager.SOUND_SFX_RESULT);

            if (BusinessManager.Instance._gameBiz._isNewRecord)
            {
                //播放破记录特效
                _timer = new vp_Timer.Handle();
                vp_Timer.In(1.0f, delegate()
                {
                    _newScoreEffect.gameObject.SetActive(true);
                    _timer.Cancel();
                }, _timer);
            }
        }

        //private void playFadeInAnim()
        //{
        //    _isPlayFadeIn = true;
        //}

        private void restartGame()
        {
            // if (!EntityManager.Instance._userEntity._isRemovedAD)
            // {
            //     PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.RESTART_GAME);
            // }

            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            hide();
            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();
            AudioManager.Instance.fadeInBgm(AudioManager.SOUND_BGM_COMMON, 1.0f);

            change2GameScene();
        }

        private void restartGameBtnClick()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(restartGame);
            //_blackLayer.raycastTarget = true;
            //_blackLayer.DOFade(1.0f, 3.0f).onComplete = restartGame;
        }

        private void change2GameScene()
        {
            SceneManager.Instance.LoadScene(SceneType.GAME, false);
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
        }

        private void exitGame()
        {
            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            hide();

            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();

            AudioManager.Instance.fadeOutBg(1.0f);
            change2MainScene();
        }

        private void exitGameFromCollection()
        {
            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            hide();

            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();

            BusinessManager.Instance._gameBiz._isBackHomeInCollectionView = true;

            AudioManager.Instance.fadeOutBg(1.0f);
            change2MainScene();
        }

        /// <summary>
        /// 点击分享
        /// </summary>
        public void clickShareBtn()
        {
            string content = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SHARE_CONTENT);
            string website = "https://play.google.com/store/apps/details?id=com.cc.dropfood";
            string text = content + "\n" + website;
            // PlatformManager.Instance.runOnUIThread().shareText(text);
        }

        /// <summary>
        /// 点击评分
        /// </summary>
        public void clickEvaluateBtn()
        {
            //TODO 接完SDK之后再做
            string testPackage = "com.cc.dropfood";
            // PlatformManager.Instance.runOnUIThread().openAppstore(testPackage);
        }

        private void clickReliveBtn()
        {
            //复活
// #if UNITY_EDITOR
            //BusinessManager.Instance._userBiz.addGold(200);
            //updateGoldText();
            //ViewManager.Instance.getView(ViewConstant.ViewId.GAIN_GOLD);
            // if (!EntityManager.Instance._userEntity._isRemovedAD)
            // {
            //     PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.RELIVE);
            //     (PlatformManager.Instance._adSDK as ADSDKAndroidImpl).onRewardedVideoEvent("onRewarded", "");
            // }
            // else
            // {
            ViewEvent reliveEntvt = new ViewEvent(ViewConstant.ViewId.GAME_RESULT,
                ViewEventConstant.EVENT_GAME_RESULT_VIEW_RELIVE);
            EventCenter.Instance.send(reliveEntvt);
            // }

// #else
//             //购买了去广告的玩家免费复活1次
//             if (!EntityManager.Instance._userEntity._isRemovedAD)
//             {
//                 PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.RELIVE);
//             } else {
//                 ViewEvent reliveEntvt =
//  new ViewEvent(ViewConstant.ViewId.GAME_RESULT, ViewEventConstant.EVENT_GAME_RESULT_VIEW_RELIVE);
//                 EventCenter.Instance.send(reliveEntvt);
//             }
//
// #endif
        }


        private void relive()
        {
            BusinessManager.Instance._gameBiz._reliveTimes--;
            BusinessManager.Instance._gameBiz._isPause = false;
            BusinessManager.Instance._gameBiz._isGameOver = false;
            BusinessManager.Instance._gameBiz._score = BusinessManager.Instance._gameBiz._recordScore;
            ViewEvent updateEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                ViewEventConstant.EVENT_GAME_VIEW_UPDATE_SCORE);
            EventCenter.Instance.send(updateEvent);
            NotificationCenter.getInstance().notify(Event.EVENT_RELIVE, 0);
            close();

            AudioManager.Instance.fadeInBgm(AudioManager.SOUND_BGM_COMMON, 1.0f);
        }

        private void clickExitGameBtn()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(exitGame);

            //_blackLayer.raycastTarget = true;
            //_blackLayer.DOFade(1.0f, 3.0f).onComplete = exitGame;
        }

        private void change2MainScene()
        {
            SceneManager.Instance.LoadScene(SceneType.MAIN, false);
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
        }

        public override void onReceive(ViewEvent e)
        {
            base.onReceive(e);
            switch (e._eventType)
            {
                case ViewEventConstant.EVENT_GAME_RESULT_VIEW_CHANGE_SCENE:
                    exitGameFromCollection();
                    break;
                case ViewEventConstant.EVENT_GAME_RESULT_VIEW_RELIVE:
                    relive();
                    break;
                default:
                    break;
            }
        }

        public override void onUpdate()
        {
            base.onUpdate();
            //if (_isPlayFadeIn)
            //{
            //    if (_canvasGroup.alpha < 1)
            //    {
            //        _canvasGroup.alpha += 3.0f / 60.0f;
            //    }

            //    if (_canvasGroup.alpha >= 1)
            //    {
            //        _isPlayFadeIn = false;
            //    }
            //}
        }
    }
}