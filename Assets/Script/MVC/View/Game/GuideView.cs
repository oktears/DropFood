
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 引导界面
    /// </summary>
    public class GuideView : PopUpBaseView
    {

        public Button _restartBtn;
        public Button _exitBtn;
        public Button _continueBtn;

        private Transform _leftMask;
        private Transform _rightMask;

        private Image _guideImg;

        private Text _content;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GUIDE;
            bindTarget("Prefab/UI/Game/GuideView", ViewManager.Instance._UIRoot2D);

            float triggerPosX = _bundleData.GetFloat("triggerPosX");
            _leftMask = _viewObj.Find("LeftMask");
            _rightMask = _viewObj.Find("RightMask");
            if (triggerPosX == 0)
            {
                _leftMask.localPosition = new Vector2(0, _leftMask.localPosition.y);
                _rightMask.gameObject.SetActive(false);
            }
            else
            {

                float offset = 445.0f;
                float s1 = 16.0f / 9.0f;
                //实际分辨率比例 高：宽
                float s2 = Screen.height * 1.0f / Screen.width;
                if (s2 < s1)
                {
                    //ipad适配
                    float w1 = CameraAdapter.DESIGN_WIDTH;
                    //实际分辨率比例
                    float w2 = Screen.width * 1.0f / Screen.height * CameraAdapter.DESIGN_HEIGHT;
                    float ratio = w2 / w1;
                    offset = offset * ratio;
                }

                _leftMask.localPosition = new Vector2(triggerPosX - offset, _leftMask.localPosition.y);
                _rightMask.localPosition = new Vector2(triggerPosX + offset, _rightMask.localPosition.y);
            }

            _content = _viewObj.Find("Dialog/Content").GetComponent<Text>();

            //string text = "";
            //if (BusinessManager.Instance._gameBiz._guideStep == 1)
            //{
            //    text = BusinessManager.Instance._userBiz.getTextI18N(3);
            //}
            //else if (BusinessManager.Instance._gameBiz._guideStep == 2)
            //{
            //    text = BusinessManager.Instance._userBiz.getTextI18N(4);
            //}
            //else if (BusinessManager.Instance._gameBiz._guideStep == 3)
            //{
            //    text = BusinessManager.Instance._userBiz.getTextI18N(5);
            //}
            //else if (BusinessManager.Instance._gameBiz._guideStep == 4)
            //{
            //    text = BusinessManager.Instance._userBiz.getTextI18N(6);
            //}
            updateGuideText();
            //updateGuideImg();


            _viewObj.Find("ClickArea").GetComponent<Button>().onClick.AddListener(clickScreen);
        }

        private void updateGuideImg()
        {
            string path = "Texture/UI/Guide/guide_bg0" + BusinessManager.Instance._gameBiz._guideStep;
            _guideImg = _viewObj.Find("Dialog").GetComponent<Image>();
            _guideImg.sprite = Resources.Load<Sprite>(path);
            _guideImg.SetNativeSize();
        }

        private void updateGuideText()
        {
            _content.text = BusinessManager.Instance._userBiz.getTextI18N(BusinessManager.Instance._gameBiz._guideStep);
        }

        private void clickScreen()
        {

            if (BusinessManager.Instance._gameBiz._guideStep == 3)
            {
                BusinessManager.Instance._gameBiz._guideStep = 4;
                updateGuideText();
                return;
            }
            else if (BusinessManager.Instance._gameBiz._guideStep == 4)
            {
                BusinessManager.Instance._gameBiz._guideStep = 5;
            }
            else if (BusinessManager.Instance._gameBiz._guideStep == 5)
            {
                BusinessManager.Instance._gameBiz._guideStep = 6;
            }
            else if (BusinessManager.Instance._gameBiz._guideStep == 6)
            {
                BusinessManager.Instance._gameBiz._guideStep = 7;
                updateGuideText();
                return;
            }
            else if (BusinessManager.Instance._gameBiz._guideStep == 7)
            {
                BusinessManager.Instance._gameBiz._guideStep = 8;
                updateGuideText();
                return;
            }
            else if (BusinessManager.Instance._gameBiz._guideStep == 8)
            {
                BusinessManager.Instance._userBiz.updateGuide();
            }
            //else if (BusinessManager.Instance._gameBiz._guideStep == 9)
            //{
            //    BusinessManager.Instance._userBiz.updateGuide();
            //}
            BusinessManager.Instance._gameBiz._isGuiding = false;
            NotificationCenter.getInstance().notify(Event.EVENT_GUIDE_TOUCH, 0);
            this.close();
        }

        public override void onReceive(ViewEvent e)
        {
            base.onReceive(e);
        }

        public override void onUpdate()
        {
            base.onUpdate();
        }
    }
}
