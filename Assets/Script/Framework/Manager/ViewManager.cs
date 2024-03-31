using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Chengzi
{
    /// <summary>
    /// UI界面管理器
    /// 一级界面：100~199
    /// 弹出界面：200~299（不阻塞）
    /// 弹出界面：300~399（阻塞）
    /// </summary>
    public class ViewManager : Singleton<ViewManager>
    {
        public const string DELAY_TIME = "delayTime";

        public ViewConstant.ViewId _viewId;

        /// <summary>一级界面管理器</summary> 
        public FirstViewManager _firstViewManager { get; private set; }

        /// <summary>弹出界面管理器</summary>
        public PopUpViewManager _popUpViewManager { get; private set; }

        /// <summary>2DUI跟节点</summary>
        public RectTransform _UIRoot2D { get; private set; }

        /// <summary>3DUI根节点</summary>
        public RectTransform _UIRoot3D { get; private set; }

        /// <summary>3D相机</summary>
        public Camera _UI3DCamera { get; private set; }

        /// <summary>2D相机</summary>
        public Camera _UI2DCamera { get; private set; }

        public Transform _2DFIRST;
        public Transform _2DPOP;
        public Transform _2DMIDDLE;
        public Transform _2DMask;

        public Transform _3DFIRST;
        public Transform _3DPOP;
        public Transform _3DMIDDLE;

        public bool _isAni { get; private set; }

        private Transform _fps;

        public List<ViewConstant.ViewId> _viewIdMap = new List<ViewConstant.ViewId>();

        //////////////////新手引导


        public void init()
        {
            if (_firstViewManager == null)
            {
                _firstViewManager = new FirstViewManager();
                _firstViewManager.init();
            }
            if (_popUpViewManager == null)
            {
                _popUpViewManager = new PopUpViewManager();
                _popUpViewManager.init();
            }

            ViewManager.Instance.onStart();
        }

        bool isInit = false;

        /// <summary>
        /// 切换场景时调用
        /// </summary>
        public void onStart()
        {
            //_UIRoot2D = Object.Instantiate(Resources.Load("UIPrefab/Root/UIRoot2D") as GameObject).GetComponent<RectTransform>();
            if (isInit) return;

            GameObject o = null;
            do
            {
                //o = GameObject.Find("UIRoot2D");
                o = PrefabPool.Instance.getObject("Prefab/UI/Root/UIRoot2D");
                if (o == null) break;
                _UIRoot2D = o.GetComponent<RectTransform>();
                Object.DontDestroyOnLoad(_UIRoot2D);
                if (_UIRoot2D == null || _UIRoot2D.Find("Camera") == null) break;
                _UI2DCamera = _UIRoot2D.Find("Camera").GetComponent<Camera>();

                _2DFIRST = _UIRoot2D.Find("First");
                _2DPOP = _UIRoot2D.Find("Pop");
                _2DMIDDLE = _UIRoot2D.Find("Middle");
                _2DMask = _UIRoot2D.Find("Mask");
                _fps = _UIRoot2D.Find("FPS");
                showFps(false);
                CommonViewManager.Instance.init();

            } while (false);

            do
            {
                //o = GameObject.Find("UIRoot3D");
                o = PrefabPool.Instance.getObject("Prefab/UI/Root/UIRoot3D");
                if (o == null && o.GetComponent<RectTransform>() == null) break;
                _UIRoot3D = o.GetComponent<RectTransform>();
                Object.DontDestroyOnLoad(_UIRoot3D);

                if (_UIRoot3D == null || _UIRoot3D.Find("Camera") == null) break;
                _UI3DCamera = _UIRoot3D.Find("Camera").GetComponent<Camera>();

                _3DFIRST = _UIRoot3D.Find("First");
                _3DPOP = _UIRoot3D.Find("Pop");
                _3DMIDDLE = _UIRoot3D.Find("Middle");

                //CommonViewManager.Instance.init();
            } while (false);

            isInit = true;

            //相机起始坐标调整，只针对比16：9长的屏幕
            float aspectRatio = Screen.height * 1.0f / Screen.width;
            if (aspectRatio < 1.6f)
            {
                _UIRoot2D.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
        }

        public void getView()
        {
            getView(_viewId);
        }

        /// <summary>创建一个View</summary>
        public void getView(ViewConstant.ViewId viewId)
        {
            getView(viewId, null);
        }

        /// <summary>创建一个View</summary>
        public void getView(ViewConstant.ViewId viewId, Bundle bundleData)
        {
            createView(viewId, bundleData);
            _viewIdMap.Add(viewId);
            if (_viewIdMap.Count == 10) _viewIdMap.RemoveAt(0);
        }

        /// <summary>生成View</summary>
        private void createView(ViewConstant.ViewId ViewId, Bundle bundle)
        {

            if (bundle != null && bundle.ContainsKey(DELAY_TIME))
            {
                _2DMask.gameObject.SetActive(true);
                CoroutineTool.startCoroutine(changeViewEnumerator(ViewId, bundle));
            }
            else
            {
                changeView(ViewId, bundle);
            }
        }

        public void changeView(ViewConstant.ViewId ViewId, Bundle bundle)
        {
            int a = ViewId.GetHashCode();
            BaseView baseView = null;
            _2DMask.gameObject.SetActive(false);
            if (a >= 100 && a < 200)
            {
                baseView = _firstViewManager.creatView(ViewId, bundle);
            }
            else if (a >= 200 && a < 1000)
            {
                baseView = _popUpViewManager.creatView(ViewId, bundle);
            }
        }

        public IEnumerator changeViewEnumerator(ViewConstant.ViewId ViewId, Bundle bundle)
        {
            float delayTime = bundle.GetFloat("delayTime");
            if (delayTime == 0)
            {
                delayTime = 0.5f;
            }
            _isAni = true;
            yield return new WaitForSeconds(delayTime);
            _isAni = false;
            changeView(ViewId, bundle);
        }

        public bool hasView(ViewConstant.ViewId ViewId)
        {
            bool ret = false;

            do
            {
                if (_firstViewManager._curView._viewID == ViewId) ret = true;
                if (ret) break;

                for (int i = 0; i < _popUpViewManager._popUp2DViewList.Count; i++)
                {
                    if (_popUpViewManager._popUp2DViewList[i]._viewID == ViewId)
                    {
                        ret = true;
                        break;
                    }
                }

            } while (false);

            return ret;
        }

        /// <summary>刷新所有界面</summary>
        public void refreshAllView()
        {
            do
            {
                if (_firstViewManager == null) break;
                if (_popUpViewManager == null) break;
                if (_popUpViewManager._popUp2DViewList == null) break;

                _firstViewManager._curView.refreshView();

                for (int i = 0; i < _popUpViewManager._popUp2DViewList.Count; i++)
                {
                    _popUpViewManager._popUp2DViewList[i].refreshView();
                }
            } while (false);
        }

        public void showFps(bool isShow)
        {
            _fps.gameObject.SetActive(isShow);
        }
    }
}