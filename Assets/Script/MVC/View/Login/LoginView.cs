using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chengzi
{
    public class LoginView : FirstBaseView
    {
        public Button _loginBtn;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.LOGIN;
            bindTarget("Prefab/UI/Login/LoginView", ViewManager.Instance._UIRoot2D);
            _loginBtn = _viewObj.Find("Bg/LoginBtn").GetComponent<Button>();
            _loginBtn.addListener(loginBtnOnClick);
            Debug.Log("初始化完毕");
            NotificationCenter.getInstance().postMessage(Event.UI_EVENT_LOGIN, "欢迎登陆");
        }

        public void loginBtnOnClick()
        {
            BusinessManager.Instance._gameBiz.init();
            SceneManager.Instance.LoadScene(SceneType.GAME, false);
        }
    }

}
