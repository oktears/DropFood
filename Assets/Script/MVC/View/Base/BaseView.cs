using UnityEngine;
using Chengzi;
using System.Collections;

namespace Chengzi
{
    public class BaseView : LoopBaseObject
    {
        /// <summary>界面Id</summary>
        public ViewConstant.ViewId _viewID { get; protected set; }

        /// <summary>根物体</summary>
        public RectTransform _viewObj { get; protected set; }

        /// <summary>
        /// 绑定界面数据
        /// </summary>
        public Bundle _bundleData { get; set; }

        ///实例化物体和绑定父物体
        protected virtual void bindTarget(string path, RectTransform parent)
        {
            _viewObj = PrefabPool.Instance.getObject(path).GetComponent<RectTransform>();

            if (false)
            {
                _viewObj.setParent(parent.Find("Middle"));
            }
            else if ((int)_viewID < 200)
            {
                _viewObj.setParent(parent.Find("First"));
            }
            else if (((int)_viewID >= 200 && (int)_viewID < 400))
            {
                _viewObj.setParent(parent.Find("Pop"));
            }
            else
            {
                _viewObj.setParent(parent);
            }

        }

        protected virtual void bindTarget(string path)
        {
            bindTarget(path, ViewManager.Instance._UIRoot2D);
        }

        /// <summary>初始化</summary>
        public virtual void init()
        {

        }

        public void setViewId(ViewConstant.ViewId viewId)
        {
            _viewID = viewId;
        }

        /// <summary>初始化事件</summary>
        public virtual void intEvent()
        {
            regEvent();
        }

        /// <summary>完成初始化之后的操作</summary>
        public virtual void finish()
        {

        }

        /// <summary>View事件接受</summary>
        /// <param name="e"></param>
        public virtual void onReceive(ViewEvent e)
        {

        }

        public virtual void regEvent()
        {
            EventCenter.Instance.regEvent(_viewID, this.onReceive);
        }

        public virtual void unregEvent()
        {
            EventCenter.Instance.unregEvent(_viewID, this.onReceive);
        }

        /// <summary>退出界面</summary>
        public virtual void close()
        {
            if (_viewObj != null)
            {
                Object.Destroy(_viewObj.gameObject);
            }
            recycle();
            CoroutineTool.startCoroutine(delay());
        }

        /// <summary>刷新当前视图</summary>
        public virtual void refreshView()
        {

        }

        /// <summary>显示界面</summary>
        public virtual void show()
        {
            if (!_viewObj.gameObject.activeSelf)
            {
                _viewObj.gameObject.SetActive(true);
            }
        }

        /// <summary>隐藏界面</summary>
        public virtual void hide()
        {
            if (_viewObj.gameObject.activeSelf)
            {
                _viewObj.gameObject.SetActive(false);
            }
        }

        public virtual IEnumerator delay()
        {
            yield return new WaitForEndOfFrame();
            unregEvent();
        }

    }
}
