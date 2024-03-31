using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 支付逻辑
    /// </summary>
    public class PayBiz
    {

        /// <summary>
        /// 当前支付商品Id
        /// </summary>
        public PayConstant.ProductId _productId { get; private set; }

        /// <summary>
        /// 当前渠道是否可下单
        /// </summary>
        public bool _isCanOrder { get; set; }


        ///// <summary>
        ///// 获取金币数量
        ///// </summary>
        ///// <param name="productId"></param>
        ///// <returns></returns>
        //public int getGoldOrModNum(PayConstant.ProductId productId)
        //{
        //    string prop = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == productId)._propInfo;
        //    return XDParseUtil.parseIntArray(prop)[1];
        //}

        /// <summary>
        /// 发起支付
        /// </summary>
        /// <param name="productId"></param>
        public void pay(PayConstant.ProductId productId)
        {
            _productId = productId;


// #if UNITY_EDITOR
            paySuccess(productId);
// #else
//             IAPManager.Instance.pay(productId);
// #endif

            //PayDto dto = new PayDto();
            //dto._dtType = (int)DtType.PAY;
            ////dto
            //AndroidPayInfoData data = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == productId);
            //dto._cmccMiGuId = data._cmccMiGuId;
            //dto._cmccMMId = data._cmccMMId;
            //dto._desc = data._desc;
            //dto._id = (byte)data._id;
            //dto._lenovoId = data._lenovoId;
            //dto._name = data._name;
            //dto._price = data._price;
            //dto._sansungId = data._sansungId;
            //dto._telecomId = data._telecomId;
            //dto._unicomId = data._unicomId;
            //dto._xiaomiId = data._xiaomiId;
            //dto._name = data._name;
            //dto._orderId = orderId;
            //PlatformManager.Instance._sdkBridge.pay(dto);


        }

        /// <summary>
        /// 支付成功，发放道具
        /// </summary>
        /// <param name="productId"></param>
        public void paySuccess(PayConstant.ProductId productId)
        {
            if (productId == PayConstant.ProductId.REMOVE_AD)
            {
                //去广告
                // PlatformManager.Instance._adSDK.showBannerAD(false);
                EntityManager.Instance._userEntity._isRemovedAD = true;
                DaoManager.Instance._userDao.saveRemovedAD();

                ViewEvent viewEventShopView = new ViewEvent(ViewConstant.ViewId.SHOP, ViewEventConstant.EVENT_SHOP_VIEW_UPDATE_REMOVE_AD);
                EventCenter.Instance.send(viewEventShopView);
            }
            else
            {
                //发金币
                PayInfoData payInfoData = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == productId);
                BusinessManager.Instance._userBiz.addGold(payInfoData._gold);
                AudioManager.Instance.play(AudioManager.SOUND_SFX_GAIN_GOLD);

                //传送消息到主界面
                ViewEvent viewEventMainView = new ViewEvent(ViewConstant.ViewId.MAIN, ViewEventConstant.EVENT_MAIN_VIEW_AD_GOLD);
                EventCenter.Instance.send(viewEventMainView);
            }

            //弹出获得物品界面
            Bundle bundle = new Bundle();
            GainItemData itemData = new GainItemData();
            itemData._type = GainItemType.BUY_PRODUCT;
            itemData._proudctId = productId;
            bundle.PutObject("GainItemData", itemData);
            ViewManager.Instance.getView(ViewConstant.ViewId.GAIN_ITEM, bundle);

        }

    }
}
