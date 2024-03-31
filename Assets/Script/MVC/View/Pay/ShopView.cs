using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 商城界面
    /// </summary>
    public class ShopView : PopUpBaseView
    {
        //返回按钮
        private Button _backBtn;
        //关闭按钮（空白处）
        private Button _closeBtn;

        public override void init()
        {
            base.init();

            bindTarget("Prefab/UI/Shop/ShopView", ViewManager.Instance._UIRoot2D);

            _backBtn = _viewObj.Find("Adapter/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);

            //_prevBtn = _viewObj.Find("Adapter/Bg/PrevBtn").GetComponent<Button>();
            //_prevBtn.addListener(clickPrevBtn);

            //_nextBtn = _viewObj.Find("Adapter/Bg/NextBtn").GetComponent<Button>();
            //_nextBtn.addListener(clickNextBtn);

            _closeBtn = _viewObj.Find("CloseBtn").GetComponent<Button>();
            _closeBtn.addListener(clickBackBtn);


            float s1 = 16.0f / 9.0f;
            //实际分辨率比例
            float s2 = Screen.height * 1.0f / Screen.width;
            if (s2 > s1)
            {
                float aspectRatio = s2 / s1;
                _viewObj.Find("Adapter").localScale = new Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }

            layoutShopList();
            updateTextI18N();

            _viewObj.Find("Adapter").GetComponent<TipShowAnimation>().PlayAnimation(null);
        }

        private void updateTextI18N()
        {
            _viewObj.Find("Adapter/Title/Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SHOP);

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                Vector2 titlePos = _viewObj.Find("Adapter/Title/Text").localPosition;
                _viewObj.Find("Adapter/Title/Text").localPosition = new Vector2(titlePos.x, titlePos.y + 8.0f);


                //int btnFontSize = _languageBtn.transform.Find("Text").GetComponent<Text>().fontSize;
                //_languageBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(btnFontSize * 0.9f);
                //Vector2 languageTextPos = _languageBtn.transform.Find("Text").localPosition;
                //_languageBtn.transform.Find("Text").localPosition = new Vector2(languageTextPos.x - 3.0f, languageTextPos.y);

                ////_restoreBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(btnFontSize * 0.8f);
                ////_shareBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(btnFontSize * 0.8f);
                //_evaluateBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(btnFontSize * 0.9f);
                ////_cdkeyBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(btnFontSize * 0.8f);
                //_privacyBtn.transform.Find("Text").GetComponent<Text>().fontSize = (int)(btnFontSize * 0.9f);
                ////int titleFontSize = _viewObj.Find("Mask/Group/Title/Text").GetComponent<Text>().fontSize;
                ////_viewObj.Find("Mask/Group/Title/Text").GetComponent<Text>().fontSize = (int)(titleFontSize * 0.8f);
            }
        }


        private void clickBackBtn()
        {
            _viewObj.Find("Adapter").GetComponent<TipHideAnimation>().PlayAnimation(close);
        }

        private void layoutShopList()
        {
            Transform listRoot = _viewObj.Find("Adapter/Scroll View/Viewport/Content");
            for (int i = 0; i < listRoot.childCount; i++)
            {
                Transform productItem = listRoot.GetChild(i);
                PayInfoData data = DaoManager.Instance._payDao._payInfoList[i];
                productItem.GetComponent<Button>().onClick.AddListener(() =>
                {
                    clickPayBtn(data);
                });

                productItem.Find("PriceLab").GetComponent<Button>().onClick.AddListener(() =>
                {
                    clickPayBtn(data);
                });

                Text nameText = productItem.Find("Name").GetComponent<Text>();
                nameText.text = BusinessManager.Instance._userBiz.getPayInfoNameI18N(data);
                productItem.Find("Desc").GetComponent<Text>().text = "+" + data._gold;
                productItem.Find("PriceLab/Price").GetComponent<Text>().text = "$" + data._price;
                productItem.Find("More").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getPayInfoDescI18N(data);
                Transform stickerTextTrans = productItem.Find("Sticker/Text");
                if (stickerTextTrans != null)
                {
                    if (data._id == PayConstant.ProductId.GOLD_UNLIMIT)
                    {
                        Text stickerText = stickerTextTrans.GetComponent<Text>();
                        stickerText.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_SPECIAL);
                        stickerText.fontSize = 24;
                    }
                    else if (data._id == PayConstant.ProductId.GOLD_MID)
                    {
                        stickerTextTrans.GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_HOT);
                    }
                }

                if (data._id == PayConstant.ProductId.REMOVE_AD)
                {
                    if (EntityManager.Instance._userEntity._isRemovedAD)
                    {
                        //已经购买了去广告
                        productItem.Find("PriceLab").GetComponent<Button>().enabled = false;
                        productItem.Find("PriceLab").GetComponent<Button>().image.color = Color.gray;
                        ShaderManager.Instance.openGrayEffect(productItem.Find("PriceLab"), false);

                        productItem.Find("PriceLab/Price").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PURCHASED);
                        productItem.GetComponent<Button>().enabled = false;
                    }

                    if (!BusinessManager.Instance._userBiz.isChinese())
                    {
                        int nameFontSize = nameText.fontSize;
                        nameText.fontSize = (int)(nameFontSize * 0.9f);
                        nameText.transform.localPosition = new Vector2(nameText.transform.localPosition.x - 20.0f, nameText.transform.localPosition.y);
                        if (EntityManager.Instance._userEntity._isRemovedAD)
                        {
                            productItem.Find("PriceLab/Price").GetComponent<Text>().fontSize = (int)(productItem.Find("PriceLab/Price").GetComponent<Text>().fontSize * 0.9f);
                        }
                    }
                }
            }
        }

        private void clickPayBtn(PayInfoData data)
        {
            BusinessManager.Instance._payBiz.pay(data._id);
        }

        private void updateRemoveAD()
        {
            Transform listRoot = _viewObj.Find("Adapter/Scroll View/Viewport/Content");
            for (int i = 0; i < listRoot.childCount; i++)
            {
                PayInfoData data = DaoManager.Instance._payDao._payInfoList[i];
                Transform productItem = listRoot.GetChild(i);

                if (data._id == PayConstant.ProductId.REMOVE_AD
                  && EntityManager.Instance._userEntity._isRemovedAD)
                {
                    //已经购买了去广告
                    productItem.Find("PriceLab").GetComponent<Button>().enabled = false;
                    productItem.Find("PriceLab/Price").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PURCHASED);
                    productItem.Find("PriceLab").GetComponent<Button>().image.color = Color.gray;
                    productItem.GetComponent<Button>().enabled = false;
                }
            }
        }

        /// <summary>View事件接受</summary>
        /// <param name="e"></param>
        public override void onReceive(ViewEvent e)
        {
            if (e._viewID == ViewConstant.ViewId.SHOP)
            {
                if (e._eventType == ViewEventConstant.EVENT_SHOP_VIEW_UPDATE_REMOVE_AD)
                {
                    updateRemoveAD();
                }
            }
        }

    }
}
