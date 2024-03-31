//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;

//namespace GrowII
//{
//    public class PayView : PopUpBaseView
//    {

//        public Button _aliPayBtn;
//        public Button _wechatPayBtn;
//        public Button _closeBtn;

//        public int _orderId;

//        public PayView()
//        {
//        }

//        public override void init()
//        {
//            base.init();
//            _viewID = ViewConstant.ViewId.PAY_VIEW;
//            bindTarget("UI/Prefab/Pay/PayView", ViewManager.Instance._UIRoot2D);
//            _closeBtn = _viewObj.Find("Bg/Image/CloseBtn").GetComponent<Button>();
//            _aliPayBtn = _viewObj.Find("Bg/Image/AliPay").GetComponent<Button>();
//            _wechatPayBtn = _viewObj.Find("Bg/Image/WechatPay").GetComponent<Button>();
//            _closeBtn.onClick.AddListener(closeBtnOnClick);
//            _aliPayBtn.onClick.AddListener(aliPayBtnOnClick);
//            _wechatPayBtn.onClick.AddListener(wechatBtnOnClick);
//        }

//        public void aliPayBtnOnClick()
//        {
//            _orderId = _bundleData.GetInt("ORDERID");
//            Debug.Log(string.Format("支付宝支付:{0}", _orderId));
//            Bundle bundle = new Bundle();
//            bundle.Putstring("DEMO", "支付宝支付");
//        }

//        public void wechatBtnOnClick()
//        {
//            _orderId = _bundleData.GetInt("ORDERID");
//            Debug.Log(string.Format("微信支付:{0}", _orderId));
//            Bundle bundle = new Bundle();
//            bundle.Putstring("DEMO", "微信支付");
//        }

//        public void closeBtnOnClick()
//        {
//            close();
//        }

//        public override void close()
//        {
//            base.close();
//        }

//        public override void onReceive(ViewEvent e)
//        {
//            base.onReceive(e);
//        }
//    }
//}

