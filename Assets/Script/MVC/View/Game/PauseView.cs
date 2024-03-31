using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 暂停界面
    /// </summary>
    public class PauseView : PopUpBaseView
    {
        //重新游戏
        private Button _restartBtn;
        //退出游戏
        private Button _exitBtn;
        //关闭
        private Button _continueBtn;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GAME_PAUSE;
            bindTarget("Prefab/UI/Game/PauseView", ViewManager.Instance._UIRoot2D);

            _viewObj.Find("Mask/Group/Title/TitleInfo").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_GAME_PAUSED);

            _restartBtn = _viewObj.Find("Mask/Group/RestartBtn").GetComponent<Button>();
            _restartBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RESTART);
            _restartBtn.addListener(restartGameBtnClick);

            _exitBtn = _viewObj.Find("Mask/Group/ExitBtn").GetComponent<Button>();
            _exitBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_QUIT);
            _exitBtn.addListener(exitGameBtnClick);

            _continueBtn = _viewObj.Find("Mask/Group/ContinueBtn").GetComponent<Button>();
            _continueBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CONTINUE);
            _continueBtn.addListener(clickContinueBtn);

            updateTextI18N();

            _viewObj.Find("Mask/Group").GetComponent<TipShowAnimation>().PlayAnimation(openAnimEnd);
        }

        private void updateTextI18N()
        {
            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                Vector2 titlePos = _viewObj.Find("Mask/Group/Title/TitleInfo").localPosition;
                _viewObj.Find("Mask/Group/Title/TitleInfo").localPosition = new Vector2(titlePos.x, titlePos.y + 4.0f);
            }
        }


        private void openAnimEnd()
        {
            //打开暂停界面播插屏广告
            // PlatformManager.Instance._adSDK.showInterstitial();
        }

        private void continueGame()
        {
            close();
        }

        private void clickContinueBtn()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz._isPause = false;
            AudioManager.Instance.pauseBG(BusinessManager.Instance._gameBiz._isPause);
            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(continueGame);
        }

        private void restartGame()
        {
            // if (!EntityManager.Instance._userEntity._isRemovedAD)
            // {
            //     PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.RESTART_GAME);
            // }

            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
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

        private void exitGame()
        {
            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            hide();

            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();

            AudioManager.Instance.fadeOutBg(1.0f);
            change2MainScene();
        }

        private void exitGameBtnClick()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(exitGame);
        }

        private void change2MainScene()
        {
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
            SceneManager.Instance.LoadScene(SceneType.MAIN, false);
        }

        public void change2GameScene()
        {
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
            SceneManager.Instance.LoadScene(SceneType.GAME, false);
        }

        public override void onReceive(ViewEvent e)
        {
            base.onReceive(e);
        }

        public override void onUpdate()
        {
            base.onUpdate();
        }
    }
}