using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Chengzi
{
    public class CommonViewManager : Singleton<CommonViewManager>
    {

        public RectTransform _UIRoot;

        public RectTransform _loadingNode;
        public RectTransform _tipViewNode;
        public RectTransform _popViewNode;

        private CanvasGroup _lodingGroup;
        private CanvasGroup _tipViewGroup;
        private CanvasGroup _popViewgroup;

        private Image _loadingNodeImage;
        private Image _tipNodeImage;
        private Image _popNodeImage;

        private WaitingView _loadingView;
        public PopBoxView _popBoxView;

        public List<TipBoxView> _tipViewList;

        public TipBoxView _tipBoxView;

        public TipBoxView _tipBoxViewAutoHide;

        public void init()
        {
            _UIRoot = ViewManager.Instance._UIRoot2D.Find("Common").GetComponent<RectTransform>();

            //_UIRoot.localScale = Vector3.one;
            //_UIRoot.localPosition = Vector3.zero;
            //_UIRoot.localRotation = Quaternion.identity;
            //GameObject.DontDestroyOnLoad(_UIRoot);
            //_UIRoot.transform.SetAsLastSibling();

            _loadingNode = _UIRoot.Find("LoadingNode").GetComponent<RectTransform>();
            _tipViewNode = _UIRoot.Find("TipViewNode").GetComponent<RectTransform>();
            _popViewNode = _UIRoot.Find("PopViewNode").GetComponent<RectTransform>();

            _loadingNodeImage = _loadingNode.GetComponent<Image>();
            _tipNodeImage = _tipViewNode.GetComponent<Image>();
            _popNodeImage = _popViewNode.GetComponent<Image>();

            _loadingNode.gameObject.SetActive(false);
            _tipViewNode.gameObject.SetActive(false);
            _popViewNode.gameObject.SetActive(false);

            // 默认按钮都是阻塞的
            _lodingGroup = _loadingNode.GetComponent<CanvasGroup>();
            _lodingGroup.interactable = true;
            _lodingGroup.blocksRaycasts = true;

            _tipViewGroup = _tipViewNode.GetComponent<CanvasGroup>();
            _tipViewGroup.interactable = true;
            _tipViewGroup.blocksRaycasts = true;

            _popViewgroup = _popViewNode.GetComponent<CanvasGroup>();
            _popViewgroup.interactable = true;
            _popViewgroup.blocksRaycasts = true;

            isShow(true);

            _tipViewList = new List<TipBoxView>();

        }

        /// <summary>根节点是否显示 </summary>
        public void isShow(bool isShow)
        {
            _UIRoot.gameObject.SetActive(isShow);
            _UIRoot.gameObject.SetActive(isShow);
        }

        ///// <summary>设置loading界面显示与隐藏 </summary>
        //public void setLoadingViewIsShow(bool isShow)
        //{
        //    _loadingNode.gameObject.SetActive(isShow);
        //}

        /// <summary>设置PopView显示与隐藏</summary>
        public void setPopViewIsShow(bool isShow)
        {
            _popViewNode.gameObject.SetActive(isShow);
        }

        /// <summary>设置TipView 显示与隐藏 </summary>
        public void setTipViewIsShow(bool isShow)
        {
            _tipViewNode.gameObject.SetActive(isShow);
        }

        public void createTipView(string content)
        {
            setTipViewIsShow(true);
            if (_tipBoxView == null)
            {
                _tipBoxView = new TipBoxView();
            }
            _tipBoxView.setContent(content);
        }

        public void createTipViewAutoHide(string content, bool isBlockTouch, float delayTime)
        {

            if (_tipBoxViewAutoHide != null)
            {
                _tipBoxViewAutoHide.hideViewAuto();
            }
            setTipViewIsShow(true);
            _tipBoxViewAutoHide = new TipBoxView();
            _tipBoxViewAutoHide.refreshTipContent(content, delayTime);
            setTipViewTouch(isBlockTouch);
        }

        public void closeTipView()
        {
            if (_tipBoxView != null)
            {
                _tipBoxView.close();
                _tipBoxView = null;
            }
            setTipViewIsShow(false);
        }

        public void closeTipViewa()
        {
            if (_tipBoxView != null)
            {
                _tipBoxView.close();
                _tipBoxView = null;
            }
            setTipViewIsShow(false);
        }

        /// <summary>
        /// 设置背景图片的透明度
        /// </summary>
        /// <param name="value"> 0——255 </param>
        public void setCommonLodingViewOpacity(int value)
        {
            _loadingNodeImage.color = new Vector4(0, 0, 0, value);
        }

        public void setCommonTipViewOpacity(int value)
        {
            _tipNodeImage.color = new Vector4(0, 0, 0, value);
        }

        public void setCommonPopViewOpactity(int value)
        {
            _popNodeImage.color = new Vector4(0, 0, 0, value);
        }

        /// <summary>
        /// 设置是否可以继续交互
        /// </summary>
        /// <param name="isTouch"> 能点穿为fase  </param>
        public void setLoadingViewTouch(bool isTouch)
        {
            _lodingGroup.interactable = isTouch;
            _lodingGroup.blocksRaycasts = isTouch;
        }

        public void setTipViewTouch(bool isTouch)
        {
            _tipViewGroup.interactable = isTouch;
            _tipViewGroup.blocksRaycasts = isTouch;
        }
        public void setPopViewTouch(bool isTouch)
        {
            _popViewgroup.interactable = isTouch;
            _popViewgroup.blocksRaycasts = isTouch;
        }

        /// <summary>
        /// 显示Tip 默认隐藏时间为2s
        /// </summary>
        /// <param name="content"></param>
        public void showTip(string content)
        {
            showTip(content, false);
        }

        /// <summary>
        /// 显示Tip 默认隐藏时间为2s
        /// </summary>
        /// <param name="content"></param>
        public void showTip(string content, bool isBlockTouch)
        {
            setTipViewIsShow(true);
            TipBoxView view = new TipBoxView();
            view.refreshTipContent(content);
            _tipViewList.Add(view);
            setTipViewTouch(isBlockTouch);
        }

        public void showTip(string content, bool isBlockTouch, float delayTime)
        {
            setTipViewIsShow(true);
            TipBoxView view = new TipBoxView();

            view.refreshTipContent(content, delayTime);
            _tipViewList.Add(view);
            setTipViewTouch(isBlockTouch);
        }

        /// <summary>
        /// 显示Tip 默认隐藏时间为2s
        /// </summary>
        /// <param name="content"></param>
        /// <param name="delayTime">延迟时间</param>
        public void showTip(string content, float delayTime)
        {
            setTipViewIsShow(true);
            TipBoxView view = new TipBoxView();

            view.refreshTipContent(content, delayTime);
            _tipViewList.Add(view);
            setTipViewTouch(true);
        }

        public void showWaitView(bool isShow)
        {
            //D.c("通用界面waiting:" + isShow);
            if (_loadingView == null)
            {
                _loadingView = new WaitingView();
            }
            _loadingNode.gameObject.SetActive(isShow);
            _loadingView.playAm(isShow);
        }


        public void showWaitView(bool isShow, string s)
        {
            //D.c("通用界面waiting:" + isShow);
            if (_loadingView == null)
            {
                _loadingView = new WaitingView();
            }
            _loadingNode.gameObject.SetActive(isShow);
            _loadingView.playAm(isShow);
            if (isShow == false)
            {
                _loadingView.showTip("");

            }
            else
            {
                _loadingView.showTip(s);
            }
        }

        public void showPopBoxView(string content)
        {
            _popBoxView = PopBoxView.create(content, PopBoxView.DialogType.SUER_OR_CANCEL);
            setPopViewIsShow(true);
        }


        public void closePopBoxView()
        {
            _popBoxView.close();
            _popBoxView = null;
        }




    }
}
