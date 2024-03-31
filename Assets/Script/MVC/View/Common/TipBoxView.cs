using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

namespace Chengzi
{
    public class TipBoxView
    {
        private RectTransform _viewObj;
        private Text _contentText;

        public TipBoxView()
        {
            init();
        }

        public void init()
        {
            _viewObj = PrefabPool.Instance.getObject("Prefab/UI/Common/TipBoxView").GetComponent<RectTransform>();
            _viewObj.SetParent(CommonViewManager.Instance._tipViewNode);
            _viewObj.localScale = Vector3.one;
            _viewObj.localPosition = Vector3.zero;
            _viewObj.localRotation = Quaternion.identity;
            _contentText = _viewObj.transform.Find("Bg/Content").GetComponent<Text>();
        }

        private void setDelayTime(float delay)
        {
            TweenUtil.fade(_viewObj.gameObject, 0.0f, delay - 1.5f, 1.5f);
            _viewObj.DOLocalMoveY(30.0f, delay);

            CoroutineTool.startCoroutine(hide(), delay);
        }


        public void setContent(string content)
        {
            _contentText.text = content;
        }

        public void stopCoroutine()
        {
            CoroutineTool.stopCoroutine(hide());
        }

        /// <summary> 刷新内容 </summary>
        public void refreshTipContent(string content)
        {
            //CommonViewManager.Instance.SetTipViewIsShow(true);
            _viewObj.gameObject.SetActive(true);
            _contentText.text = content;
            setDelayTime(2);

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                int fontSize = _contentText.fontSize;
                _contentText.fontSize = (int)(fontSize * 0.8f);
            }
        }

        /// <summary>
        /// 刷新内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="delayTime">延迟时间 </param>
        public void refreshTipContent(string content, float delayTime)
        {
            _viewObj.gameObject.SetActive(true);
            _contentText.text = content;
            setDelayTime(delayTime);

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                int fontSize = _contentText.fontSize;
                _contentText.fontSize = (int)(fontSize * 0.8f);
            }
        }

        public IEnumerator hide()
        {
            hideView();
            yield return null;
        }

        public void close()
        {
            if (_viewObj != null)
            {
                GameObject.Destroy(_viewObj.gameObject);
            }
        }

        /// <summary>隐藏</summary>
        public void hideView()
        {
            if (_viewObj != null)
            {
                CommonViewManager.Instance._tipViewList.Remove(this);
                GameObject.Destroy(_viewObj.gameObject);
                _viewObj = null;
                if (CommonViewManager.Instance._tipViewList.Count == 0)
                    CommonViewManager.Instance.setTipViewIsShow(false);
            }
        }


        /// <summary>隐藏</summary>
        public void hideViewAuto()
        {
            if (_viewObj != null)
            {
                GameObject.Destroy(_viewObj.gameObject);
                _viewObj = null;
                if (CommonViewManager.Instance._tipBoxViewAutoHide != null)
                    CommonViewManager.Instance._tipBoxViewAutoHide = null;
                {
                }
                if (CommonViewManager.Instance._tipViewList.Count == 0)
                {
                    CommonViewManager.Instance.setTipViewIsShow(false);
                }
            }
        }
    }
}

