using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{
    /// <summary>
    /// 收集品网格界面
    /// </summary>
    public class CollectionView : PopUpBaseView
    {
        //返回按钮
        private Button _backBtn;

        //上翻页
        private Button _prevBtn;

        //下翻页
        private Button _nextBtn;

        //网格页
        private Transform _girdViewRoot;

        //总记录数
        private int _rowCount;

        //每页显示的记录数
        private int _pageSize;

        //页数
        private int _pageCount;

        //当前页
        private int _pageNow;

        //关于按钮
        private Button _aboutBtn;

        //金币条
        private Text _gold;

        //页码
        private Text _pageNo;

        private List<int> _collectionList = new List<int>();


        private const float ITEM_ICON_POS_Y = -15.6f;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.COLLECTION_LIST;

            bindTarget("Prefab/UI/Collection/CollectionView", ViewManager.Instance._UIRoot2D);

            _backBtn = _viewObj.Find("Adapter/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);

            _aboutBtn = _viewObj.Find("Adapter/AboutBtn").GetComponent<Button>();
            _aboutBtn.addListener(clickAboutBtn);

            _prevBtn = _viewObj.Find("Bg/PrevBtn").GetComponent<Button>();
            _prevBtn.addListener(clickPrevBtn, AudioManager.SOUND_SFX_BTN_PAGE);

            _nextBtn = _viewObj.Find("Bg/NextBtn").GetComponent<Button>();
            _nextBtn.addListener(clickNextBtn, AudioManager.SOUND_SFX_BTN_PAGE);

            _girdViewRoot = _viewObj.Find("Bg/BoxBg/GridRoot");

            _gold = _viewObj.Find("Adapter/GoldBar/Text").GetComponent<Text>();
            if (EntityManager.Instance._userEntity._goldCount < 10)
            {
                _gold.text = EntityManager.Instance._userEntity._goldCount + "";
            }
            else
            {
                _gold.text = string.Format("{0:0,0}", EntityManager.Instance._userEntity._goldCount);
            }

            _pageNo = _viewObj.Find("Bg/PageNo").GetComponent<Text>();

            float s1 = 16.0f / 9.0f;
            //实际分辨率比例
            float s2 = Screen.height * 1.0f / Screen.width;
            if (s2 > s1)
            {
                float aspectRatio = s2 / s1;
                _viewObj.Find("Bg").localScale = new Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }
            else
            {
                //float aspectRatio = s1 / s2;
                // _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }

            initPageData();
            layoutGridView(updatePageData());
            updatePageBtnState();

            _viewObj.GetComponent<TipShowAnimation>().PlayAnimation(null);
        }

        private void clickAboutBtn()
        {
            ViewManager.Instance.getView(ViewConstant.ViewId.PROFILE);
        }

        private int sortList(int a, int b)
        {
            if (a > b)
            {
                return 1;
            }
            else if (a < b)
            {
                return -1;
            }

            return 0;
        }

        private void initPageData()
        {
            _collectionList.Clear();

            foreach (var item in DaoManager.Instance._gameDao._itemDataDict)
            {
                if (item.Value._itemType != ItemType.GOLD
                    && item.Value._itemType != ItemType.TEST)
                {
                    _collectionList.Add(item.Key);
                }
            }

            _collectionList.Sort();

            _rowCount = _collectionList.Count;
            _pageSize = 9;
            _pageCount = (_rowCount - 1) / _pageSize + 1;
            _pageNow = 1;
        }

        private List<int> updatePageData()
        {
            List<int> dataList = new List<int>();
            int start = (_pageNow - 1) * _pageSize;
            int end = _pageNow * _pageSize;
            end = (end > _rowCount) ? _rowCount : end;
            for (int i = start; i < end; i++)
            {
                int itemId = _collectionList[i];
                dataList.Add(itemId);
            }

            return dataList;
        }

        private void layoutGridView(List<int> dataList)
        {
            hideAllGird();
            for (int i = 0; i < dataList.Count; i++)
            {
                int itemId = dataList[i];
                GameObject go = _girdViewRoot.Find(string.Format("Cell_{0}", i)).gameObject;
                go.SetActive(true);
                Transform item = go.transform.Find("Item");
                Image image = item.GetComponent<Image>();
                item.GetComponent<Button>().onClick.RemoveAllListeners();
                item.GetComponent<Button>().onClick.AddListener(() =>
                {
                    Bundle bundle = new Bundle();
                    bundle.PutInt("ItemId", itemId);
                    AudioManager.Instance.play(AudioManager.SOUND_SFX_BTN_CLICK);
                    ViewManager.Instance.getView(ViewConstant.ViewId.COLLECTION_INFO, bundle);
                });
                ItemData itemData = DaoManager.Instance._gameDao._itemDataDict[itemId];
                item.localScale = new Vector3(itemData._scaleInCollection, itemData._scaleInCollection,
                    itemData._scaleInCollection);
                string imgPath = "Texture/Game/New/F" + itemId;
                image.sprite = Resources.Load<Sprite>(imgPath);
                image.SetNativeSize();
                image.transform.localPosition = new Vector2(image.transform.localPosition.x,
                    ITEM_ICON_POS_Y + itemData._offsetYInCollection);


                bool isUnlock = EntityManager.Instance._userEntity._ownCollection.SingleOrDefault(d => d == itemId) !=
                                0;
                Transform lockImg = go.transform.Find("Lock");
                lockImg.gameObject.SetActive(!isUnlock);
                if (!isUnlock)
                {
                    string lockPath = "lock";
                    // if (itemData._gainType == ItemGainType.AD)
                    // {
                    //     lockPath = "lock_ad";
                    // }
                    // else
                    if (itemData._gainType == ItemGainType.BUY)
                    {
                        lockPath = "lock_gold";
                    }

                    lockImg.GetComponent<Image>().sprite = Resources.Load<Sprite>("Texture/UI/Main/" + lockPath);
                }

                image.color = isUnlock ? Color.white : new Color(150.0f / 255.0f, 150.0f / 255.0f, 150.0f / 255.0f, 1);
            }
        }

        private string getItemSpriteExtNameByType(ItemType type)
        {
            string ret = "";
            switch (type)
            {
                case ItemType.UNKNOWN:
                    break;
                case ItemType.DESSERT:
                    break;
                case ItemType.BLOCK:
                    ret = "_Block";
                    break;
                case ItemType.ELIMINATE:
                    ret = "_Eliminate";
                    break;
                case ItemType.TIME:
                    break;
                case ItemType.ACC_DESSERT:
                    ret = "_Acc";
                    break;
                case ItemType.ROTATE:
                    ret = "_Rotate";
                    break;
                case ItemType.GOLD:
                    ret = "_Gold";
                    break;
                case ItemType.TEST:
                    ret = "_Test";
                    break;
                default:
                    break;
            }

            return ret;
        }

        private void clickBackBtn()
        {
            _viewObj.GetComponent<TipHideAnimation>().PlayAnimation(close);
        }

        private void clickPrevBtn()
        {
            if (_pageNow > 1)
            {
                --_pageNow;
            }

            layoutGridView(updatePageData());
            updatePageBtnState();
        }

        private void clickNextBtn()
        {
            if (_pageNow < _pageCount)
            {
                ++_pageNow;
            }

            layoutGridView(updatePageData());
            updatePageBtnState();
        }

        private void updatePageBtnState()
        {
            _prevBtn.image.color = new Color(1, 1, 1, 1);
            _prevBtn.enabled = true;
            _nextBtn.image.color = new Color(1, 1, 1, 1);
            _nextBtn.enabled = true;

            if (_pageNow == 1)
            {
                _prevBtn.image.color = new Color(1, 1, 1, 0.8f);
                _prevBtn.enabled = false;
            }

            if (_pageNow == _pageCount)
            {
                _nextBtn.image.color = new Color(1, 1, 1, 0.8f);
                _nextBtn.enabled = false;
            }

            _pageNo.text = _pageNow + " / " + _pageCount;
        }

        private void hideAllGird()
        {
            for (int i = 0; i < _girdViewRoot.childCount; i++)
            {
                _girdViewRoot.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void updateGoldText()
        {
            if (EntityManager.Instance._userEntity._goldCount < 10)
            {
                _gold.text = EntityManager.Instance._userEntity._goldCount + "";
            }
            else
            {
                _gold.text = string.Format("{0:0,0}", EntityManager.Instance._userEntity._goldCount);
                _gold.transform.DOScale(new Vector2(1.1f, 1.1f), 0.3f);
                _gold.transform.DOScale(new Vector2(1.0f, 1.0f), 0.3f).SetDelay(0.3f);
            }
        }

        /// <summary>View事件接受</summary>
        /// <param name="e"></param>
        public override void onReceive(ViewEvent e)
        {
            if (e._viewID == ViewConstant.ViewId.COLLECTION_LIST)
            {
                if (e._eventType == ViewEventConstant.EVENT_COLLECTION_VIEW_UPDATE_GOLD)
                {
                    updateGoldText();
                    layoutGridView(updatePageData());
                }
            }
        }
    }
}