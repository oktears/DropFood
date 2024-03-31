using UnityEngine;
using System.Collections;
using Chengzi;

namespace Chengzi
{
    public class FirstViewManager : BaseViewManager
    {
        public BaseView _curView { get; private set; }

        public BaseView creatView(ViewConstant.ViewId viewId, Bundle bundle)
        {
            closeView();
            switch (viewId)
            { 
                case ViewConstant.ViewId.LOGIN:
                    _curView = new LoginView();
                    break;
                case ViewConstant.ViewId.GAME:
                    _curView = new GameView();
                    break;
                case ViewConstant.ViewId.LAUNCH_LOADING:
                    _curView = new LoadingView();
                    break;
                case ViewConstant.ViewId.MAIN:
                    _curView = new MainView();
                    break;
            }
            if (_curView != null)
            {
                _curView._bundleData = bundle;
                _curView.setViewId(viewId);
                _curView.init();
                _curView.intEvent();
                _curView.finish();
            }
            return _curView;
        }

        public void closeView()
        {
            if (_curView != null)
                _curView.close();
        }
    }
}