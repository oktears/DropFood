using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    public class WaitingView : LoopBaseObject
    {
        private RectTransform _viewObj;
        private Text _tip;
        private Transform _waitImg;
        private bool _isPlayAm;
        private int _addValue = 360;

        public WaitingView()
        {
            init();
        }

        public void init()
        {
            _viewObj = PrefabPool.Instance.getObject("Prefab/UI/Common/WaitingView").GetComponent<RectTransform>();
            _tip = _viewObj.Find("Text").GetComponent<Text>();
            _waitImg = _viewObj.Find("load");

            _viewObj.setParent(CommonViewManager.Instance._loadingNode);
            _viewObj.localScale = Vector3.one;
            _viewObj.localPosition = Vector3.zero;
            _viewObj.localRotation = Quaternion.identity;
        }

        public override void onUpdate()
        {
            base.onUpdate();
            if (_isPlayAm)
            {
                _addValue += 5;
                if (_addValue >= 720)
                {
                    _addValue = 360;
                }
                _waitImg.localRotation = Quaternion.Euler(_waitImg.localRotation.x,
                    _waitImg.localRotation.y,
                    (_waitImg.localRotation.z - _addValue) % 360);
            }
            else
            {

            }
        }

        public void playAm(bool isShow)
        {
            _isPlayAm = isShow;
        }

        public void showTip(string str)
        {
            _tip.text = str;
        }
    }
}

