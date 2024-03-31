
using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 启动加载界面
    /// </summary>
    public class LoadingView : FirstBaseView
    {
        public Button _loginBtn;

        private CanvasGroup _logo;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.LAUNCH_LOADING;
            bindTarget("Prefab/UI/Launch/LoadingView", ViewManager.Instance._UIRoot2D);

            //float s1 = 16.0f / 9.0f;
            ////实际分辨率比例
            //float s2 = Screen.height * 1.0f / Screen.width;
            //if (s2 > s1)
            //{
            //    float aspectRatio = s2 / s1;
            //    _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            //}

            _logo = _viewObj.Find("Bg/Logo").GetComponent<CanvasGroup>();
            _logo.alpha = 0;

            load();
        }

        private void load()
        {
            LifeCycleManager.Instance.launcheGame();
            LifeCycleManager.Instance.initStep1();
            LifeCycleManager.Instance.initStep2();
            LifeCycleManager.Instance.initStep3();
            vp_Timer.In(0.1f, playLogoAnim);
            //loadFinish();
        }

        /// <summary>
        /// 播放Logo动画
        /// </summary>
        private void playLogoAnim()
        {
            var seq = DOTween.Sequence();
            seq.Append(_logo.DOFade(1.0f, 1.5f)).SetDelay(0.5f).Append(_logo.DOFade(0f, 1.5f)).AppendCallback(() =>
            {
                SceneManager.Instance.LoadScene(SceneType.MAIN);
            });
        }


        private void loadFinish()
        {
            SceneManager.Instance.LoadScene(SceneType.MAIN);
        }
    }
}
