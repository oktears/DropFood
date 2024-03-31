
using DG.Tweening;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    public enum GainItemType
    {
        /// <summary>
        /// 看广告领的金币
        /// </summary>
        AD_GOLD,
        /// <summary>
        /// 道具
        /// </summary>
        ITEM,
        /// <summary>
        /// 内购
        /// </summary>
        BUY_PRODUCT,

    }

    public class GainItemData
    {
        public GainItemType _type;
        public int _itemId;
        public int _gold;
        public PayConstant.ProductId _proudctId;
    }


    /// <summary>
    /// 领取金币界面
    /// </summary>
    public class GainItemView : PopUpBaseView
    {
        //关闭按钮（空白处）
        private Button _closeBtn;
        private Button _exitBtn;

        GainItemData _data;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GAIN_ITEM;
            bindTarget("Prefab/UI/Main/GainItemView", ViewManager.Instance._UIRoot2D);

            _data = _bundleData.GetObject("GainItemData") as GainItemData;

            _exitBtn = _viewObj.Find("Mask/Group/ExitBtn").GetComponent<Button>();
            _exitBtn.addListener(closeBtnClick);

            _closeBtn = _viewObj.Find("CloseBtn").GetComponent<Button>();
            _closeBtn.addListener(closeBtnClick);

            Text title = _viewObj.Find("Mask/Group/Title/TitleInfo").GetComponent<Text>();
            Transform layoutFood = _viewObj.Find("Mask/Group/LayoutFood");
            layoutFood.gameObject.SetActive(false);
            Transform layoutGoldPack = _viewObj.Find("Mask/Group/LayoutGoldPack");
            layoutGoldPack.gameObject.SetActive(false);
            Transform layoutRemoveAD = _viewObj.Find("Mask/Group/LayoutRemoveAD");
            layoutRemoveAD.gameObject.SetActive(false);


            if (_data._type == GainItemType.AD_GOLD)
            {
                //领取免费金币
                layoutGoldPack.gameObject.SetActive(true);
                _exitBtn.GetComponentInChildren<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CLAIM);

                Text name = layoutGoldPack.Find("Name").GetComponent<Text>();
                Text desc = layoutGoldPack.Find("Desc").GetComponent<Text>();
                Image icon = layoutGoldPack.Find("Item/Icon").GetComponent<Image>();
                name.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_VIDEO_AWARDS);
                desc.text = " +" + _data._gold;
                icon.sprite = Resources.Load<Sprite>("Texture/Game/New/F82");
                icon.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                icon.SetNativeSize();
                title.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CLAIM_AWARDS);

                if (!BusinessManager.Instance._userBiz.isChinese())
                {
                    int titleFontSize = title.fontSize;
                    title.fontSize = (int)(titleFontSize * 0.9f);
                    title.transform.localPosition = new Vector2(title.transform.localPosition.x, title.transform.localPosition.y + 3.0f);

                    int nameFontSize = name.fontSize;
                    name.fontSize = (int)(nameFontSize * 0.9f);
                }

            }
            else if (_data._type == GainItemType.ITEM)
            {
                //美食
                layoutFood.gameObject.SetActive(true);
                _exitBtn.GetComponentInChildren<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CLOSE);

                Text desc = layoutFood.Find("Name").GetComponent<Text>();
                Image icon = layoutFood.Find("Item/Icon").GetComponent<Image>();

                ItemData itemData = DaoManager.Instance._gameDao._itemDataDict[_data._itemId];
                desc.text = BusinessManager.Instance._userBiz.getItemNameI18N(itemData);

                icon.sprite = Resources.Load<Sprite>("Texture/Game/New/F" + _data._itemId);
                icon.SetNativeSize();
                icon.transform.localScale = new Vector3(itemData._scaleInCollection, itemData._scaleInCollection, 1);
                title.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CATE_COLLECTED);

                if (!BusinessManager.Instance._userBiz.isChinese())
                {
                    int titleFontSize = title.fontSize;
                    title.fontSize = (int)(titleFontSize * 0.9f);
                    title.transform.localPosition = new Vector2(title.transform.localPosition.x, title.transform.localPosition.y + 3.0f);

                    int nameFontSize = desc.fontSize;
                    desc.fontSize = (int)(nameFontSize * 0.8f);
                }

            }
            else if (_data._type == GainItemType.BUY_PRODUCT)
            {
                //内购
                _exitBtn.GetComponentInChildren<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CLOSE);
                title.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PURCHASE_SUCCEEDED);

                if (_data._proudctId == PayConstant.ProductId.REMOVE_AD)
                {
                    //去广告

                    Text desc = layoutRemoveAD.Find("Desc").GetComponent<Text>();
                    Text name = layoutRemoveAD.Find("Name").GetComponent<Text>();

                    layoutRemoveAD.gameObject.SetActive(true);
                    name.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_REMOVE_ADS);
                    layoutRemoveAD.Find("Desc").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_REMOVE_ADS_DESC);

                    if (!BusinessManager.Instance._userBiz.isChinese())
                    {
                        title.fontSize = 40;
                        desc.fontSize = 40;
                    }
                }
                else
                {
                    //金币包
                    layoutGoldPack.gameObject.SetActive(true);

                    Text desc = layoutGoldPack.Find("Desc").GetComponent<Text>();
                    Text name = layoutGoldPack.Find("Name").GetComponent<Text>();
                    Image icon = layoutGoldPack.Find("Item/Icon").GetComponent<Image>();
                    //金币礼包
                    PayInfoData payInfo = DaoManager.Instance._payDao._payInfoList.SingleOrDefault(d => d._id == _data._proudctId);
                    desc.text = " +" + payInfo._gold;
                    name.text = BusinessManager.Instance._userBiz.getPayInfoNameI18N(payInfo);

                    string sprName = "";
                    switch (_data._proudctId)
                    {
                        case PayConstant.ProductId.GOLD_SMALL:
                            sprName = "Texture/Game/New/F82";
                            icon.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                            break;
                        case PayConstant.ProductId.GOLD_MID:
                            sprName = "Texture/Game/New/F83";
                            icon.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
                            break;
                        case PayConstant.ProductId.GOLD_BIG:
                            sprName = "Texture/UI/Main/GoldBigPack";
                            icon.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
                            break;
                        case PayConstant.ProductId.GOLD_HUGE:
                            sprName = "Texture/Game/New/F84";
                            icon.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
                            break;
                        case PayConstant.ProductId.GOLD_UNLIMIT:
                            sprName = "Texture/UI/Main/GoldHugePack";
                            icon.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                            break;
                        default:
                            break;
                    }
                    icon.sprite = Resources.Load<Sprite>(sprName);
                    icon.SetNativeSize();

                    if (!BusinessManager.Instance._userBiz.isChinese())
                    {
                        title.fontSize = 40;
                        int nameFontSize = name.fontSize;
                        name.fontSize = (int)(nameFontSize * 0.8f);
                    }
                }




            }


            _viewObj.Find("Mask/Group").GetComponent<TipShowAnimation>().PlayAnimation(playOpenAnimEnd);
            AudioManager.Instance.play(AudioManager.SOUND_GAIN_ITEM);
        }

        private void playOpenAnimEnd()
        {

        }

        private void closeBtnClick()
        {

            if (_data._type == GainItemType.AD_GOLD)
            {
                AudioManager.Instance.play(AudioManager.SOUND_SFX_GAIN_GOLD);
                BusinessManager.Instance._userBiz.addGold(_data._gold);
                ViewEvent goldEvent = new ViewEvent(ViewConstant.ViewId.MAIN, ViewEventConstant.EVENT_MAIN_VIEW_AD_GOLD);
                EventCenter.Instance.send(goldEvent);
            }

            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(close);
        }

    }
}
