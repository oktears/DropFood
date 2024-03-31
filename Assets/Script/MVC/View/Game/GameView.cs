
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

namespace Chengzi
{

    /// <summary>
    /// 游戏界面
    /// </summary>
    public class GameView : FirstBaseView
    {
        //主菜单按钮
        private Button _homeBtn;
        ////退出按钮
        //private Button _exitBtn;
        //重新开始关卡按钮
        private Button _restartBtn;

        private Transform _bg;

        ////游戏时间
        //private Text _gameTime;
        //游戏分数
        private Text _score;
        ////下一个道具
        //private Text _nextItem;

        //黑蒙版
        private Image _blackLayer;

        //传送带的位置
        public Transform _conveyor { get; private set; }

        //加金币
        private Text _addGoldText;
        private Image _addGoldIcon;
        //金币
        private Text _goldText;

        //新收藏提示
        public Transform _newCollectTip;
        //获得金币提示
        public Transform _bonusGoldTip;
        //新记录提示
        public Transform _newRecord;
        //是否显示了新记录
        private bool _isShowNewRecord = false;
        //破纪录的延迟特效计时器
        private vp_Timer.Handle _recordTimer;


        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GAME;
            bindTarget("Prefab/UI/Game/GameView", ViewManager.Instance._UIRoot2D);

            _bg = _viewObj.Find("Bg");
            _homeBtn = _viewObj.Find("Bg/HomeBtn").GetComponent<Button>();
            _homeBtn.addListener(pauseBtnClick);
            //_pauseBtn.gameObject.SetActive(DebugConfig.s_isDebugMode);

            //_exitBtn = _viewObj.Find("Bg/ExitBtn").GetComponent<Button>();
            //_exitBtn.addListener(exitBtnClick);

            _restartBtn = _viewObj.Find("Bg/RestartBtn").GetComponent<Button>();
            _restartBtn.addListener(restartBtnClick);

            //_gameTime = _viewObj.Find("Bg/GameTime").GetComponent<Text>();

            _score = _viewObj.Find("Bg/Score").GetComponent<Text>();
            _score.text = "0";

            _conveyor = _viewObj.Find("Bg/Conveyor").transform;

            //_nextItem = _viewObj.Find("Bg/NextItem").GetComponent<Text>();

            _addGoldText = _viewObj.Find("Bg/AddGold").GetComponent<Text>();
            _addGoldText.gameObject.SetActive(false);
            _addGoldIcon = _addGoldText.gameObject.GetComponentInChildren<Image>();

            _goldText = _viewObj.Find("Bg/GoldBar/Text").GetComponent<Text>();
            if (EntityManager.Instance._userEntity._goldCount + BusinessManager.Instance._gameBiz._gainGold < 10)
            {
                _goldText.text = BusinessManager.Instance._gameBiz._gainGold + EntityManager.Instance._userEntity._goldCount + "";
            }
            else
            {
                _goldText.text = string.Format("{0:0,0}", BusinessManager.Instance._gameBiz._gainGold + EntityManager.Instance._userEntity._goldCount);
            }


            _newCollectTip = _viewObj.Find("Bg/NewCollectTip");
            _newCollectTip.gameObject.SetActive(false);

            _bonusGoldTip = _viewObj.Find("Bg/BonusGoldTip");
            _bonusGoldTip.gameObject.SetActive(false);

            _newRecord = _viewObj.Find("Bg/NewRecord");
            _newRecord.gameObject.SetActive(false);
            //updateNextItem();
            //_gameTime.enabled = DebugConfig.s_isDebugMode;

            _score.transform.parent.localPosition = new Vector2(_score.transform.parent.localPosition.x, _score.transform.parent.localPosition.y + 400);
            reliveFlyAnim();
        }

        /// <summary>
        /// 播放新记录动画
        /// </summary>
        private void playNewRecordAnim()
        {
            if (BusinessManager.Instance._gameBiz._score <= EntityManager.Instance._userEntity._gameScore)
            {
                return;
            }

            if (_isShowNewRecord)
            {
                return;
            }

            _isShowNewRecord = true;

            if (EntityManager.Instance._userEntity._isCheckGuide)
            {
                //引导关，第一次玩不显示破记录
                return;
            }

            //播放破记录特效
            _recordTimer = new vp_Timer.Handle();
            vp_Timer.In(1.0f, delegate ()
            {
                _newRecord.gameObject.SetActive(true);
                _recordTimer.Cancel();
            }, _recordTimer);


            Text tipText = _newRecord.Find("Text").GetComponent<Text>();
            tipText.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RECORD_BREAKING);

            Vector2 lastPos = tipText.transform.localPosition;
            CanvasGroup cg = tipText.GetComponent<CanvasGroup>();
            cg.alpha = 0.0f;
            cg.DOFade(1.0f, 0.3f);
            tipText.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            tipText.transform.DOScale(1.0f, 0.3f);

            tipText.transform.DOBlendableLocalMoveBy(new Vector2(0, 70), 1.5f).SetDelay(0.3f);
            tipText.DOFade(0, 0.3f).SetDelay(1.8f).onComplete = () =>
            {
                _newRecord.gameObject.SetActive(false);
                tipText.transform.localPosition = lastPos;
            };

        }

        /// <summary>
        /// 播放发现金币奖励动画
        /// </summary>
        private void playBonusGoldAnim(int itemId)
        {
            _bonusGoldTip.gameObject.SetActive(true);
            string imgPath = "Texture/Game/New/F" + itemId;
            _bonusGoldTip.Find("AnimRoot/Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
            _bonusGoldTip.Find("AnimRoot/Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_LUCKY_YOU);
            _bonusGoldTip.Find("AnimRoot/Name").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_FOUND)
                + BusinessManager.Instance._userBiz.getItemNameI18N(DaoManager.Instance._gameDao._itemDataDict[itemId]);

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                int fontSize = _bonusGoldTip.Find("AnimRoot/Name").GetComponent<Text>().fontSize;
                _bonusGoldTip.Find("AnimRoot/Name").GetComponent<Text>().fontSize = (int)(fontSize * 0.8);
            }

            Transform animRoot = _bonusGoldTip.Find("AnimRoot");
            Vector2 lastPos = _bonusGoldTip.localPosition;
            CanvasGroup cg = _bonusGoldTip.GetComponent<CanvasGroup>();
            cg.alpha = 0.0f;

            animRoot.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            animRoot.DOScale(1.0f, 0.5f);
            cg.DOFade(1.0f, 0.5f);
            _bonusGoldTip.transform.DOBlendableLocalMoveBy(new Vector2(0, 50), 1.5f).SetDelay(0.5f);
            cg.DOFade(0, 0.5f).SetDelay(1.5f).onComplete = () =>
            {
                _bonusGoldTip.gameObject.SetActive(false);
                _bonusGoldTip.localPosition = lastPos;
            };
        }

        /// <summary>
        /// 播放新收藏动画
        /// </summary>
        private void playNewCollectAnim(int itemId)
        {
            _newCollectTip.gameObject.SetActive(true);
            string imgPath = "Texture/Game/New/F" + itemId;
            _newCollectTip.Find("AnimRoot/Icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
            _newCollectTip.Find("AnimRoot/Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_NEW_COLLECTED);
            _newCollectTip.Find("AnimRoot/Name").GetComponent<Text>().text = "★" + BusinessManager.Instance._userBiz.getItemNameI18N(DaoManager.Instance._gameDao._itemDataDict[itemId]);

            Transform animRoot = _newCollectTip.Find("AnimRoot");
            Vector2 lastPos = _newCollectTip.localPosition;
            CanvasGroup cg = _newCollectTip.GetComponent<CanvasGroup>();
            cg.alpha = 0.0f;

            animRoot.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            animRoot.DOScale(1.0f, 0.5f);
            cg.DOFade(1.0f, 0.5f);
            _newCollectTip.transform.DOBlendableLocalMoveBy(new Vector2(0, 50), 1.5f).SetDelay(0.5f);
            cg.DOFade(0, 0.5f).SetDelay(1.5f).onComplete = () =>
            {
                _newCollectTip.gameObject.SetActive(false);
                _newCollectTip.localPosition = lastPos;
            };
        }


        /// <summary>
        /// 播放增加金币动画
        /// </summary>
        /// <param name="itemPos"></param>
        /// <param name="gold"></param>
        /// <param name="offsetY"></param>
        private void playAddGoldAnim(Vector2 itemPos, int gold, float offsetY)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(itemPos);
            Vector2 uiPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_bg.GetComponent<RectTransform>(),
                screenPos,
                ViewManager.Instance._UI2DCamera,
                out uiPos);

            _addGoldText.text = "+" + gold;
            uiPos.Set(uiPos.x, uiPos.y + offsetY);
            _addGoldText.rectTransform.anchoredPosition = uiPos;
            _addGoldText.gameObject.SetActive(true);
            _addGoldText.color = new Color(1, 1, 1, 1);
            _addGoldIcon.color = new Color(1, 1, 1, 1);
            _addGoldText.transform.DOBlendableLocalMoveBy(new Vector2(30, 50), 1.2f);
            _addGoldText.DOFade(0, 1.2f).onComplete = playAddScoreAnimFinish;
            _addGoldIcon.DOFade(0, 1.2f);
        }

        private void playAddScoreAnimFinish()
        {
            _addGoldText.gameObject.SetActive(false);
        }

        //public void updateGameTime(float time)
        //{
        //    _gameTime.text = "时间：" + time.ToString();
        //}

        private void updateScore(float score)
        {
            _score.text = score.ToString();
        }

        private void updateScore()
        {
            _score.text = BusinessManager.Instance._gameBiz._score.ToString();
        }

        private void updateGold()
        {
            if (EntityManager.Instance._userEntity._goldCount + BusinessManager.Instance._gameBiz._gainGold < 10)
            {
                _goldText.text = BusinessManager.Instance._gameBiz._gainGold + EntityManager.Instance._userEntity._goldCount + "";
            }
            else
            {
                _goldText.text = string.Format("{0:0,0}", BusinessManager.Instance._gameBiz._gainGold + EntityManager.Instance._userEntity._goldCount);
            }

            _goldText.transform.DOScale(new Vector2(1.1f, 1.1f), 0.3f);
            _goldText.transform.DOScale(new Vector2(1.0f, 1.0f), 0.3f).SetDelay(0.3f);
        }

        //private void updateNextItem()
        //{
        //    byte nextItemId = BusinessManager.Instance._gameBiz._nextItemId;
        //    if (nextItemId != 0)
        //    {
        //        string itemName = DaoManager.Instance._gameDao._itemDataDict[nextItemId]._itemName;
        //        _nextItem.text = "下一个：" + itemName;
        //    }
        //}

        public override void onFixedUpdate()
        {
            base.onFixedUpdate();
        }

        private void pauseBtnClick()
        {
            BusinessManager.Instance._gameBiz._isPause = true;
            AudioManager.Instance.pauseBG(BusinessManager.Instance._gameBiz._isPause);
            ViewManager.Instance.getView(ViewConstant.ViewId.GAME_PAUSE);
        }

        private void exitBtnClick()
        {
            ////SceneManager.Instance.LoadScene(SceneType.LOGINSCENE, false);
            //BusinessManager.Instance._gameBiz._timeScale = 1;
            //BusinessManager.Instance._gameBiz._isPause = true;
            ////AudioManager.Instance.pauseBG(true);
            //ViewManager.Instance.getView(ViewConstant.ViewId.GAME_PAUSE);

            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz._isPause = false;
            BusinessManager.Instance._gameBiz.reset();
            //AudioManager.Instance.fadeOutBg(2.0f);
            hide();

            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            change2MainScene();
        }

        private void change2MainScene()
        {
            SceneManager.Instance.LoadScene(SceneType.MAIN, false);
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
        }

        private void restartBtnClick()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();
            SceneManager.Instance.LoadScene(SceneType.GAME, false);

            // if (!EntityManager.Instance._userEntity._isRemovedAD)
            // {
                // PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.RESTART_GAME);
            // }
        }

        private void gameOverFlyAnim()
        {
            _score.transform.parent.DOBlendableLocalMoveBy(new Vector3(0, 400), 2.0f);
        }

        private void reliveFlyAnim()
        {
            _score.transform.parent.DOBlendableLocalMoveBy(new Vector3(0, -400), 2.0f);
        }

        //private void pauseFlyIn()
        //{
        //    _homeBtn.transform.DOBlendableLocalMoveBy(new Vector3(0, -200), 0.5f);
        //}

        //private void pauseFlyOut()
        //{
        //    BusinessManager.Instance._gameBiz._isPause = true;
        //    //TODO: SHOW PASEU VIEW
        //    //_homeBtn.transform.DOBlendableLocalMoveBy(new Vector3(0, 200), 0.5f).OnComplete(() =>
        //    //{
        //    //    pauseBtnClick();
        //    //});
        //}

        public override void onReceive(ViewEvent e)
        {
            base.onReceive(e);
            switch (e._eventType)
            {
                case ViewEventConstant.EVENT_GAME_VIEW_HIDE_BLACK_LAYER:
                    {
                        //_blackLayer.gameObject.SetActive(false);
                        _blackLayer.DOFade(0.0f, 3.0f);
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER:
                    {
                        //_blackLayer.gameObject.SetActive(false);
                        _blackLayer.DOFade(1.0f, 3.0f);
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_HIDE_UI:
                    {
                        _bg.gameObject.SetActive(false);
                        //_blackLayer.DOFade(0.8f, 2.0f);
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_UPDATE_SCORE:
                    {
                        updateScore();
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_UPDATE_SCORE_AND_GOLD:
                    {
                        Vector2 itemPos = (Vector2)e._bundleData.GetObject("itemPos");
                        int gold = e._bundleData.GetInt("gold");
                        float offsetY = e._bundleData.GetFloat("offsetY");
                        updateScore();
                        updateGold();
                        playAddGoldAnim(itemPos, gold, offsetY);
                        playNewRecordAnim();
                    }
                    break;
                //case ViewEventConstant.EVENT_GAME_VIEW_CHANGE_NEXT_ITEM:
                //    {
                //        updateNextItem();
                //    }
                //    break;
                case ViewEventConstant.EVENT_GAME_VIEW_GAME_OVER:
                    {
                        gameOverFlyAnim();
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_RELIVE:
                    {
                        reliveFlyAnim();
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_NEW_COLLECT:
                    {
                        int itemId = e._bundleData.GetInt("ItemId");
                        playNewCollectAnim(itemId);
                    }
                    break;
                case ViewEventConstant.EVENT_GAME_VIEW_BONUS_COLD:
                    {
                        int itemId = e._bundleData.GetInt("ItemId");
                        playBonusGoldAnim(itemId);
                    }
                    break;
                //case ViewEventConstant.EVENT_GAME_VIEW_NEW_RECORD:
                //    {
                //        playNewRecordAnim();
                //    }
                //    break;
                default:
                    break;
            }
        }

    }

}
