
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 收集品网格界面
    /// </summary>
    public class CollectionInfoView : PopUpBaseView
    {
        //返回按钮
        private Button _backBtn;
        ////上翻页
        //private Button _prevBtn;
        ////下翻页
        //private Button _nextBtn;
        //关闭按钮（空白处）
        private Button _closeBtn;

        //道具名
        private Text _name;
        //一句话描述
        private Text _desc;
        //icon
        private Image _icon;
        //奖励金币
        private Text _rewardGold;
        //星
        private List<Image> _starsImgList;
        //类型
        private Text _type;

        //看广告
        private Button _adBtn;
        //金币购买
        private Button _buyBtn;
        //关卡内解锁
        private Button _stageUnlockBtn;

        //价格
        private Text _price;

        private int _itemId;
        //private List<byte> _collectionList = new List<byte>();
        //private readonly string markList = "(\\！|\\？|\\，|\\。|\\《|\\》|\\）|\\：|\\“|\\‘|\\、|\\；|\\+|\\-)";
        ////单行个数
        //private readonly int singleLineCount = 11;

        //private Text _pageNo;

        public override void init()
        {
            base.init();

            bindTarget("Prefab/UI/Collection/CollectionInfoView", ViewManager.Instance._UIRoot2D);

            _itemId = _bundleData.GetInt("ItemId");
            ItemData data = DaoManager.Instance._gameDao._itemDataDict[_itemId];

            _backBtn = _viewObj.Find("Adapter/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);

            _name = _viewObj.Find("Adapter/Title/Text").GetComponent<Text>();


            //_prevBtn = _viewObj.Find("Adapter/Bg/PrevBtn").GetComponent<Button>();
            //_prevBtn.addListener(clickPrevBtn);

            //_nextBtn = _viewObj.Find("Adapter/Bg/NextBtn").GetComponent<Button>();
            //_nextBtn.addListener(clickNextBtn);

            _closeBtn = _viewObj.Find("CloseBtn").GetComponent<Button>();
            _closeBtn.addListener(clickBackBtn);


            //_pageNo = _viewObj.Find("Adapter/Bg/PageNo").GetComponent<Text>();


            _adBtn = _viewObj.Find("Adapter/ADBtn").GetComponent<Button>();
            _adBtn.addListener(clickADBtn);

            _stageUnlockBtn = _viewObj.Find("Adapter/StageUnlockBtn").GetComponent<Button>();
            _stageUnlockBtn.addListener(clickStageUnlockBtn);

            _buyBtn = _viewObj.Find("Adapter/BuyBtn").GetComponent<Button>();
            _buyBtn.addListener(clickBuyBtn);

            _price = _buyBtn.transform.Find("Price").GetComponent<Text>();
            _price.text = string.Format("{0:0,0}", data._price);

            float s1 = 16.0f / 9.0f;
            //实际分辨率比例
            float s2 = Screen.height * 1.0f / Screen.width;
            if (s2 > s1)
            {
                float aspectRatio = s2 / s1;
                _viewObj.Find("Adapter").localScale = new Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }

            //initData();
            //updatePageNo();
            //updatePageBtnState();
            updateLayoutI18N();
            updateTextI18N();
            updateInfo();
            updateButton();

            _viewObj.Find("Adapter").GetComponent<TipShowAnimation>().PlayAnimation(null);

        }

        private void updateLayoutI18N()
        {
            Transform layoutCN = _viewObj.Find("Adapter/CNLayout");
            Transform layoutEN = _viewObj.Find("Adapter/ENLayout");
            Transform useLayout = null;
            if (BusinessManager.Instance._userBiz.isChinese())
            {
                layoutCN.gameObject.SetActive(true);
                layoutEN.gameObject.SetActive(false);
                useLayout = layoutCN;
            }
            else
            {
                layoutCN.gameObject.SetActive(false);
                layoutEN.gameObject.SetActive(true);
                useLayout = layoutEN;
            }

            useLayout.gameObject.SetActive(false);
            useLayout.gameObject.SetActive(true);
            _type = useLayout.Find("SpecialLab/Text").GetComponent<Text>();
            _desc = useLayout.Find("Desc").GetComponent<Text>();
            _icon = useLayout.Find("Item/Icon").GetComponent<Image>();
            _rewardGold = useLayout.Find("DropLab/Text").GetComponent<Text>();
            _starsImgList = useLayout.Find("DifficultyLab/Stars").GetComponentsInChildren<Image>().ToList();
            useLayout.Find("DropLab/Title").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_BONUS);
            useLayout.Find("DifficultyLab/Title").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_DIFFICULTY);
            useLayout.Find("SpecialLab/Title").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CATEGORY);


        }

        private void updateTextI18N()
        {
            _buyBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PURCHASE);
            _stageUnlockBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_COLLECT_IN_GAME);
            _adBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_FREE_CLAIM);
            _viewObj.Find("Adapter/Gained/Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_COLLECTED);
            //_evaluateBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_RATE_US);
            //_cdkeyBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_CDKEY);
            //_privacyBtn.transform.Find("Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_PRIVACY_POLICY);
        }

        //private void initData()
        //{
        //    _collectionList.Clear();
        //    for (int i = 0; i < EntityManager.Instance._userEntity._ownCollection.Count; i++)
        //    {
        //        _collectionList.Add(EntityManager.Instance._userEntity._ownCollection[i]);
        //    }

        //    //测试用，所有道具都展示
        //    //foreach (var item in DaoManager.Instance._gameDao._itemDataDict)
        //    //{
        //    //    _collectionList.Add(item.Value._itemId);
        //    //}

        //    _collectionList.Sort();
        //}

        //private void updatePageNo()
        //{
        //    string pageNo = "{0}";
        //    string curNo = string.Format("{0:D2}", getCurIndex() + 1);
        //    string totalNo = string.Format("{0:D2}", _collectionList.Count);
        //    //Debug.Log("curNo = " + curNo);
        //    pageNo = string.Format(pageNo, curNo);
        //    _pageNo.text = pageNo;
        //}

        private void updateButton()
        {
            ItemData data = DaoManager.Instance._gameDao._itemDataDict[_itemId];
            bool isOwn = BusinessManager.Instance._userBiz.isOwnCollection(data._itemId);
            if (isOwn)
            {
                //已拥有
                _viewObj.Find("Adapter/Gained").gameObject.SetActive(true);
                _viewObj.Find("Adapter/BuyBtn").gameObject.SetActive(false);
                _viewObj.Find("Adapter/ADBtn").gameObject.SetActive(false);
                _viewObj.Find("Adapter/StageUnlockBtn").gameObject.SetActive(false);
            }
            else
            {
                switch (data._gainType)
                {
                    case ItemGainType.AD:
                    case ItemGainType.STAGE:
                        _viewObj.Find("Adapter/Gained").gameObject.SetActive(false);
                        _viewObj.Find("Adapter/BuyBtn").gameObject.SetActive(false);
                        _viewObj.Find("Adapter/ADBtn").gameObject.SetActive(false);
                        _viewObj.Find("Adapter/StageUnlockBtn").gameObject.SetActive(true);
                        break;
                        // _viewObj.Find("Adapter/Gained").gameObject.SetActive(false);
                        // _viewObj.Find("Adapter/BuyBtn").gameObject.SetActive(false);
                        // _viewObj.Find("Adapter/ADBtn").gameObject.SetActive(true);
                        // _viewObj.Find("Adapter/StageUnlockBtn").gameObject.SetActive(false);
                        // break;
                    case ItemGainType.BUY:
                        _viewObj.Find("Adapter/Gained").gameObject.SetActive(false);
                        _viewObj.Find("Adapter/BuyBtn").gameObject.SetActive(true);
                        _viewObj.Find("Adapter/ADBtn").gameObject.SetActive(false);
                        _viewObj.Find("Adapter/StageUnlockBtn").gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }

        private void updateInfo()
        {
            ItemData data = DaoManager.Instance._gameDao._itemDataDict[_itemId];
            _name.text = BusinessManager.Instance._userBiz.getItemNameI18N(data);

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                if (_name.text.Length <= 18)
                {
                    //18字以内，字号50
                    _name.fontSize = 50;
                }
                else
                {
                    _name.fontSize = 42;
                }
            }

            _desc.text = BusinessManager.Instance._userBiz.getItemDescI18N(data);
            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                if (_desc.text.Length <= 24)
                {
                    _desc.fontSize = 50;
                }
                else if (_desc.text.Length > 24)
                {
                    _desc.fontSize = 42;
                }
            }

            _rewardGold.text = "+" + data._rewardGold;
            for (int i = 0; i < _starsImgList.Count; i++)
            {
                if (i + 1 <= data._star)
                {
                    _starsImgList[i].sprite = Resources.Load<Sprite>("Texture/UI/Main/YellowStar");
                }
                else
                {
                    _starsImgList[i].sprite = Resources.Load<Sprite>("Texture/UI/Main/GreyStar");
                }
            }
            _type.text = getTypeStr(data._itemType);

            string imgPath = "Texture/Game/New/F" + _itemId;
            Debug.Log("imgPath = " + imgPath);
            _icon.sprite = Resources.Load<Sprite>(imgPath);
            _icon.SetNativeSize();
            _icon.transform.parent.localScale = new Vector3(data._scaleInCollection, data._scaleInCollection, 1);
            _icon.transform.localPosition = new Vector2(_icon.transform.localPosition.x, _icon.transform.localPosition.y + data._offsetYInCollection);
        }

        private string getTypeStr(ItemType type)
        {
            string str = "";
            switch (type)
            {
                case ItemType.UNKNOWN:
                    break;
                case ItemType.DESSERT:
                    str = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_NORMAL);
                    break;
                case ItemType.BLOCK:
                    str = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_BALANCE);
                    break;
                case ItemType.ELIMINATE:
                    str = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_ELIMINATION);
                    break;
                case ItemType.TIME:
                    str = "加时";
                    break;
                case ItemType.ACC_DESSERT:
                    str = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_FAST);
                    break;
                case ItemType.ROTATE:
                    str = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_ROTATION);
                    break;
                case ItemType.GOLD:
                    str = "金币";
                    break;
                case ItemType.TEST:
                    str = "测试";
                    break;
                default:
                    break;
            }
            return str;
        }

        //public string formatLineText(Text textGUI, string text)
        //{
        //    var settings = textGUI.GetGenerationSettings(textGUI.rectTransform.rect.size);
        //    textGUI.cachedTextGenerator.Populate(text, settings);
        //    StringBuilder textStr = new StringBuilder(text);
        //    IList<UILineInfo> lineList = textGUI.cachedTextGenerator.lines;
        //    int changeIndex = -1;
        //    for (int i = 1; i < lineList.Count; i++)
        //    {
        //        bool isMark = Regex.IsMatch(text[lineList[i].startCharIdx].ToString(), markList);
        //        if (isMark)
        //        {
        //            changeIndex = lineList[i].startCharIdx - 1;
        //            string str = text.Substring(lineList[i - 1].startCharIdx, lineList[i].startCharIdx);
        //            MatchCollection richStrMatch = Regex.Matches(str, ".(</color>|<color=#\\w{6}>|" + markList + ")+$");
        //            if (richStrMatch.Count > 0)
        //            {
        //                string richStr = richStrMatch[0].ToString();
        //                int length = richStr.Length;
        //                changeIndex = lineList[i].startCharIdx - length;
        //                break;
        //            }
        //        }
        //    }
        //    if (changeIndex >= 0)
        //    {
        //        if (!textStr[changeIndex].Equals('\n'))
        //        {
        //            textStr.Insert(changeIndex, '\n');
        //        }
        //    }
        //    return textStr.ToString();
        //}

        //private int getCurIndex()
        //{
        //    int curIdx = 0;
        //    for (int i = 0; i < _collectionList.Count; i++)
        //    {
        //        byte itemId = _collectionList[i];
        //        if (itemId == _itemId)
        //        {
        //            curIdx = i;
        //            break;
        //        }
        //    }
        //    return curIdx;
        //}

        //private bool hasPrev()
        //{
        //    bool ret = false;
        //    int curIdx = getCurIndex();
        //    if (curIdx != 0)
        //    {
        //        ret = true;
        //    }
        //    return ret;
        //}

        //private bool hasNext()
        //{
        //    bool ret = false;
        //    int curIdx = getCurIndex();
        //    if (curIdx != _collectionList.Count - 1)
        //    {
        //        ret = true;
        //    }
        //    return ret;
        //}

        //private byte getPrevItemId()
        //{
        //    byte itemId = _itemId;
        //    int curIdx = getCurIndex();
        //    if (hasPrev())
        //    {
        //        itemId = _collectionList[curIdx - 1];
        //    }
        //    return itemId;
        //}

        //private byte getNextItemId()
        //{
        //    byte itemId = _itemId;
        //    int curIdx = getCurIndex();
        //    if (hasNext())
        //    {
        //        itemId = _collectionList[curIdx + 1];
        //    }
        //    return itemId;
        //}

        //private void clickPrevBtn()
        //{
        //    _itemId = getPrevItemId();
        //    updatePageNo();
        //    updateInfo();
        //    updatePageBtnState();
        //}

        //private void clickNextBtn()
        //{
        //    _itemId = getNextItemId();
        //    updatePageNo();
        //    updateInfo();
        //    updatePageBtnState();
        //}

        //private void updatePageBtnState()
        //{
        //    _prevBtn.image.color = new Color(1, 1, 1, 1);
        //    _prevBtn.enabled = true;
        //    _nextBtn.image.color = new Color(1, 1, 1, 1);
        //    _nextBtn.enabled = true;

        //    if (!hasPrev())
        //    {
        //        _prevBtn.image.color = new Color(1, 1, 1, 0.8f);
        //        _prevBtn.enabled = false;
        //    }

        //    if (!hasNext())
        //    {
        //        _nextBtn.image.color = new Color(1, 1, 1, 0.8f);
        //        _nextBtn.enabled = false;
        //    }
        //}

        private void clickBackBtn()
        {
            _viewObj.Find("Adapter").GetComponent<TipHideAnimation>().PlayAnimation(close);
        }

        private void clickBuyBtn()
        {
            ItemData data = DaoManager.Instance._gameDao._itemDataDict[_itemId];
            if (EntityManager.Instance._userEntity._goldCount >= data._price)
            {
                BusinessManager.Instance._userBiz.addCollection(_itemId);
                DaoManager.Instance._userDao.saveCollection();
                BusinessManager.Instance._userBiz.addGold(data._price * -1);

                updateButton();

                //更新主界面金币条
                ViewEvent goldEvent = new ViewEvent(ViewConstant.ViewId.MAIN, ViewEventConstant.EVENT_MAIN_VIEW_AD_GOLD);
                EventCenter.Instance.send(goldEvent);

                //更新收藏品界面金币条
                ViewEvent goldEvent2 = new ViewEvent(ViewConstant.ViewId.COLLECTION_LIST, ViewEventConstant.EVENT_COLLECTION_VIEW_UPDATE_GOLD);
                EventCenter.Instance.send(goldEvent2);

                //弹出获得物品界面
                Bundle bundle = new Bundle();
                GainItemData itemData = new GainItemData();
                itemData._type = GainItemType.ITEM;
                itemData._itemId = _itemId;
                bundle.PutObject("GainItemData", itemData);
                ViewManager.Instance.getView(ViewConstant.ViewId.GAIN_ITEM, bundle);
            }
            else
            {
                CommonViewManager.Instance.showTip(BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_LACK_OF_COINS));
            }
        }

        private void clickADBtn()
        {
// #if UNITY_EDITOR
            // PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.GAIN_ITEM, _itemId);
            // (PlatformManager.Instance._adSDK as ADSDKAndroidImpl).onRewardedVideoEvent("onRewarded", "");
// #else
            // PlatformManager.Instance._adSDK.showVideoAD(ADConstant.VideoADPos.GAIN_ITEM, _itemId);
// #endif
        }

        private void clickStageUnlockBtn()
        {
            CommonViewManager.Instance.showTip(BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_TIP_COLLECT_IN_GAME));
        }

        /// <summary>View事件接受</summary>
        /// <param name="e"></param>
        public override void onReceive(ViewEvent e)
        {
            if (e._viewID == ViewConstant.ViewId.COLLECTION_INFO)
            {
                if (e._eventType == ViewEventConstant.EVENT_COLLECTION_INFO_VIEW_UPDATE_BUTTON)
                {
                    updateButton();
                }
            }
        }
    }
}