
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 感谢界面
    /// </summary>
    public class ThanksView : PopUpBaseView
    {

        public Button _exitBtn;

        private bool _isPlayFadeIn = false;

        private CanvasGroup _canvasGroup;
        private Image _blackLayer;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.THANKS;
            bindTarget("Prefab/UI/Game/ThanksView", ViewManager.Instance._UIRoot2D);

            _canvasGroup = _viewObj.Find("Bg").GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;

            _exitBtn = _viewObj.Find("Bg/ExitBtn").GetComponent<Button>();
            _exitBtn.addListener(exitGameBtnClick);

            _blackLayer = _viewObj.Find("Bg/BlackLayer").GetComponent<Image>();

            playFadeInAnim();
        }

        private void playFadeInAnim()
        {
            _isPlayFadeIn = true;
        }

        private void exitGame()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1.0f;
            // BusinessManager.Instance._gameBiz.reset();
            AudioManager.Instance.stopByExit();
            hide();

            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_SHOW_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
            changeScene();
        }

        private void exitGameBtnClick()
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
