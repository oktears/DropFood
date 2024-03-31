using System.Collections.Generic;

namespace Chengzi
{
    /// <summary>
    /// 弹出页面管理类
    /// </summary>
    public class PopUpViewManager : BaseViewManager
    {
        /// <summary>弹出页面列表</summary>
        public List<PopUpBaseView> _popUp2DViewList { get; private set; }

        public override void init()
        {
            _popUp2DViewList = new List<PopUpBaseView>();
        }

        public BaseView creatView(ViewConstant.ViewId viewId, Bundle bundle)
        {
            PopUpBaseView _curView = null;
            switch (viewId)
            {
                case ViewConstant.ViewId.GAME_RESULT:
                    _curView = new ResultView();
                    break;
                case ViewConstant.ViewId.GAME_PAUSE:
                    _curView = new PauseView();
                    break;
                case ViewConstant.ViewId.GAME_FAILURE:
                    _curView = new FailureView();
                    break;
                case ViewConstant.ViewId.SETTING:
                    _curView = new SettingView();
                    break;
                case ViewConstant.ViewId.LANGUAGE:
                    _curView = new LanguageView();
                    break;
                case ViewConstant.ViewId.PRODUCER:
                    _curView = new ProducerView();
                    break;
                case ViewConstant.ViewId.PROFILE:
                    _curView = new ProfileView();
                    break;
                case ViewConstant.ViewId.COLLECTION_LIST:
                    _curView = new CollectionView();
                    break;
                case ViewConstant.ViewId.COLLECTION_INFO:
                    _curView = new CollectionInfoView();
                    break;
                case ViewConstant.ViewId.GAME_LOADING:
                    _curView = new GameLoadingView();
                    break;
                case ViewConstant.ViewId.THANKS:
                    _curView = new ThanksView();
                    break;
                case ViewConstant.ViewId.GUIDE:
                    _curView = new GuideView();
                    break;
                case ViewConstant.ViewId.GAIN_ITEM:
                    _curView = new GainItemView();
                    break;
                case ViewConstant.ViewId.SHOP:
                    _curView = new ShopView();
                    break;
            }
            if (_curView != null)
            {
                addPopView(_curView);
                _curView._bundleData = bundle;
                _curView.setViewId(viewId);
                _curView.init();
                _curView.intEvent();
                _curView.finish();
            }
            return _curView;
        }

        /// <summary>关闭弹出界面</summary>
        public void removeView(PopUpBaseView popUpView)
        {
            for (int i = 0; i < _popUp2DViewList.Count; i++)
            {
                if (_popUp2DViewList[i] == popUpView)
                {
                    //_popUp2DViewList[i].close();
                    _popUp2DViewList.Remove(popUpView);
                    break;
                }
            }
        }

        ///// <summary>关闭弹出界面</summary>
        //public void closeView(PopUpBaseView popUpView)
        //{
        //    for (int i = _popUp2DViewList.Count - 1; i >= 0; i++)
        //    {
        //        if (_popUp2DViewList[i] == popUpView)
        //        {
        //            popUpView.close();
        //            //_popUp2DViewList.Remove(popUpView);
        //            break;
        //        }
        //    }
        //    _popUp2DViewList.Remove(popUpView);
        //}

        /// <summary>关闭弹出界面</summary>
        public void closeView(ViewConstant.ViewId popUpView)
        {
            for (int i = _popUp2DViewList.Count - 1; i >= 0; i++)
            {
                if (_popUp2DViewList[i]._viewID == popUpView)
                {
                    _popUp2DViewList[i].close();
                    //_popUp2DViewList.Remove(_popUp2DViewList[i]);
                    break;
                }
            }
        }

        /// <summary>刷新深度排序</summary>
        private void refreshIndex()
        {
            for (int i = _popUp2DViewList.Count - 1; i > 0; i--)
            {
                _popUp2DViewList[i]._viewObj.SetSiblingIndex(_popUp2DViewList.Count - 1);
            }
        }

        /// <summary>
        /// 提升渲染层级到最顶层
        /// </summary>
        /// <param name="view"></param>
        public void topRenderOrder(PopUpBaseView view)
        {
            for (int i = 0; i < _popUp2DViewList.Count; i++)
            {
                if (view == _popUp2DViewList[i])
                {
                    view._viewObj.SetAsLastSibling();
                }
            }
        }

        /// <summary>添加view</summary>
        private void addPopView(PopUpBaseView view)
        {
            do
            {
                if (view == null) break;
                _popUp2DViewList.Add(view);
            } while (false);
        }

        public PopUpBaseView getViewById(ViewConstant.ViewId viewId)
        {
            for (int i = 0; i < _popUp2DViewList.Count; i++)
            {
                if (_popUp2DViewList[i]._viewID == viewId)
                {
                    return _popUp2DViewList[i];
                }
            }
            return null;
        }

        public void closeAll()
        {
            for (int i = _popUp2DViewList.Count - 1; i >= 0; i--)
            {
                _popUp2DViewList[i].close();
            }
            _popUp2DViewList.Clear();
        }
    }
}