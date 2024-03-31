
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 失败界面
    /// </summary>
    public class FailureView : PopUpBaseView
    {

        private Button _restartBtn;
        private Button _resurgenceBtn;
        private Button _exitBtn;

        private CanvasGroup _canvasGroup;

        private bool _isPlayFadeIn = false;

        private Image _blackLayer;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GAME_FAILURE;
            bindTarget("Prefab/UI/Game/FailureView", ViewManager.Instance._UIRoot2D);

            _canvasGroup = _viewObj.GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;

            _restartBtn = _viewObj.Find("Bg/RestartBtn").GetComponent<Button>();
            _restartBtn.addListener(restartGameBtnClick);

            _resurgenceBtn = _viewObj.Find("Bg/ResurgenceBtn").GetComponent<Button>();
            _resurgenceBtn.addListener(resurgenceBtnBtnClick);

            _exitBtn = _viewObj.Find("Bg/ExitBtn").GetComponent<Button>();
            _exitBtn.addListener(exitBtnClick);

            _blackLayer = _viewObj.Find("BlackLayer").GetComponent<Image>();

            playFadeInAnim();
        }

        private void playFadeInAnim()
        {
            _isPlayFadeIn = true;
        }

        private void exitGame()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();
            AudioManager.Instance.stopByExit();
            hide();

            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            changeScene();
        }

        private void exitBtnClick()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            _blackLayer.raycastTarget = true;
            _blackLayer.DOFade(1.0f, 3.0f).onComplete = exitGame;
        }

        private void changeScene()
        {
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
            SceneManager.Instance.LoadScene(SceneType.MAIN, false);
        }

        private void restartGame()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            BusinessManager.Instance._gameBiz.reset();
            //SceneManager.Instance.LoadScene(SceneType.GAME, false);
            AudioManager.Instance.stopByExit();
            hide();

            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            change2GameScene();
        }

        private void restartGameBtnClick()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1;
            _blackLayer.raycastTarget = true;
            _blackLayer.DOFade(1.0f, 3.0f).onComplete = restartGame;
        }

        private void change2GameScene()
        {
            NotificationCenter.getInstance().notify(Event.EVENT_SHOW_BLACK_LAYER, 0);
            SceneManager.Instance.LoadScene(SceneType.GAME, false);
        }

        private void resurgenceBtnBtnClick()
        {
            //满血复活
            BusinessManager.Instance._gameBiz._isPause = false;
            ViewEvent updateEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                    ViewEventConstant.EVENT_GAME_VIEW_RELIVE);
            EventCenter.Instance.send(updateEvent);
            NotificationCenter.getInstance().notify(Event.EVENT_RELIVE, 0);
            close();
        }

        public override void onReceive(ViewEvent e)
        {
            base.onReceive(e);
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (_isPlayFadeIn)
            {
                if (_canvasGroup.alpha < 1)
                {
                    _canvasGroup.alpha += 3.0f / 60.0f;
                }

                if (_canvasGroup.alpha >= 1)
                {
                    _isPlayFadeIn = false;
                }
            }
        }
    }
}
