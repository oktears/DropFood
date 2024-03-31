using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chengzi
{

    public class ItemManager
    {

        //游戏控制器
        public GameControllerBase _ctrl { get; private set; }
        //道具根节点
        private Transform _itemRoot;
        //当前下落甜点
        private ItemBase _curItem;
        //甜点列表
        public List<ItemBase> _itemList = new List<ItemBase>();
        //游戏时间
        private float _gameTime;
        //生成道具的时间间隔
        private float _genItemInterval = 0.02f;
        //是否正在生成甜点
        private bool _isGeningItem;
        //生成数量
        private int _genCounter;
        //前10个固定顺序
        //private static int[] s_itemGenSeq = new int[] { 43, 49, 43, 49, 49, 43, 12, 5, 3, 11 };
        private static int[] s_itemGenSeq = new int[] { 1, 2, 12, 1, 5, 14, 12, 43, 6, 27 };
        //引导模式下前10固定顺序
        private static int[] s_itemGenSeqGuideMode = new int[] { 1, 2, 12, 1, 5, 14, 12, 43, 6, 27 };
        //无限掉落一个道具的测试
        private bool _isUnlimitTest = false;
        private int _testItemId = 83;


        //落下数量
        private int _dropCounter = 0;
        //是否计算下落数量
        private bool _isCalcDropCount;

        //test
        //private byte[] itemGenSeq = new byte[] { 16, 10, 10, 16, 3, 4, 13, 14, 15, 16 };
        //private byte[] itemGenSeq = new byte[] { 1, 16, 1, 16, 1, 16, 1, 16, 1, 16 };
        //private byte[] itemGenSeq = new byte[] { 10, 16, 10, 16, 14, 16, 14, 16, 10, 14 };
        //private byte[] itemGenSeq = new byte[] { 1, 2, 13, 1, 13, 1, 14, 14, 14, 14 };

        //下一次生成板糖的编号
        private int _nextBlockNo = 15;
        //传送带
        private Transform _conveyor;
        //盘子
        private List<Plate> _plateList = new List<Plate>();
        //第三次引导的物品
        private ItemBase _lastGuideItem;

        //披萨
        private const int ITEM_BLOCK_NORMAL = 12;
        ////消除
        //private const int ITEM_ELIMINATE = 14;
        ////加速
        //private const int ITEM_ACC_DESSERT = 10;

        private long _startTime;

        vp_Timer.Handle _timer = new vp_Timer.Handle();

        private Dictionary<int, RecordItemData> _recordDataDict = new Dictionary<int, RecordItemData>();
        public int _lastDropInstanceId;

        private float _recordInterval = 0;

        public void init(Transform root, GameControllerBase ctrl)
        {
            _itemRoot = root.Find("ItemRoot");

            _ctrl = ctrl;
            _conveyor = root.Find("MapRoot/Conveyor");
            regEvent();
        }

        public void destory()
        {
            unregEvent();
            if (_timer != null)
            {
                _timer.Cancel();
            }
        }

        public void regEvent()
        {
            NotificationCenter.getInstance().regNotify(Event.EVENT_GUIDE_TOUCH, triggerGuide);
            NotificationCenter.getInstance().regNotify(Event.EVENT_RELIVE, relive);
        }

        public void unregEvent()
        {
            NotificationCenter.getInstance().unregNotify(Event.EVENT_GUIDE_TOUCH, triggerGuide);
            NotificationCenter.getInstance().unregNotify(Event.EVENT_RELIVE, relive);
        }

        public bool triggerGuide(int e, object o)
        {
            touchDown();
            return false;
        }

        public bool relive(int e, object o)
        {
            recoverItemState();

            playFlyInAnim();

            if (_curItem != null)
            {
                _itemList.Remove(_curItem);
                _curItem.destory();
            }
            destoryPlate();

            return false;
        }

        private void playReadyGo()
        {
            AudioManager.Instance.play(AudioManager.SOUND_SFX_READY_GO);
        }

        /// <summary>
        /// 开始游戏
        /// </summary>
        public void startGame()
        {
            _conveyor.localPosition = new Vector2(_conveyor.localPosition.x, _conveyor.localPosition.y + 500);
            _conveyor.DOBlendableLocalMoveBy(new Vector2(0, 550), 2.0f);

            playReadyGo();
            calcNextItemId();

            _timer = new vp_Timer.Handle();
            vp_Timer.In(2.1f, delegate ()
            {
                genItem();
                _timer.Cancel();
            }, _timer);

        }

        public void playFlyInAnim()
        {
            _conveyor.DOBlendableLocalMoveBy(new Vector2(0, -550), 2.0f);

            ViewEvent ve = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_RELIVE);
            EventCenter.Instance.send(ve);
        }

        /// <summary>
        /// 更新传送带位置
        /// </summary>
        private void updateConveryor()
        {
            Transform conveyor = (ViewManager.Instance._firstViewManager._curView as GameView)._conveyor;
            Vector3 screenPos = ViewManager.Instance._UI2DCamera.WorldToScreenPoint(conveyor.position);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Vector3 targetObjPos = _conveyor.parent.InverseTransformPoint(worldPos);
            _conveyor.transform.localPosition = new Vector3(_conveyor.transform.localPosition.x, targetObjPos.y, _conveyor.transform.localPosition.z);
            if (_curItem != null)
            {
                if (_curItem._state == ItemState.INIT
                    || _curItem._state == ItemState.READY
                    || _curItem._state == ItemState.OUT_SCREEN)
                {
                    _curItem.transform.localPosition = new Vector3(_curItem.transform.localPosition.x,
                        targetObjPos.y + _curItem._posOffsetY,
                        _curItem.transform.localPosition.z);
                }
            }
            updatePlateY(_conveyor.transform.localPosition.y + 25);
        }

        /// <summary>
        /// 更新摄像机Y坐标
        /// </summary>
        /// <returns></returns>
        public void updateCamera()
        {
            float topItemPosY = 0;
            ItemBase item = getTopItem();
            if (item != null)
            {
                topItemPosY = item.transform.localPosition.y;
            }
            float conveyorPosY = _conveyor.transform.localPosition.y;
            if (topItemPosY > 0 && topItemPosY + 450 > conveyorPosY)
            {
                _ctrl._cameraController.moveByCameraPosY(350);
                AudioManager.Instance.play(AudioManager.SOUND_SFX_CAMERA_MOVE_UP);
                ++BusinessManager.Instance._gameBiz._cameraMoveTimes;
            }
        }

        /// <summary>
        /// 获取随机道具Id
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public int getRandomItemId(int count)
        {
            ItemSeqData itemSeqData = null;

            if (count > 10 && count <= 30)
            {
                itemSeqData = DaoManager.Instance._gameDao._itemSeqDataDict[1];
            }
            else if (count > 30 && count <= 100)
            {
                itemSeqData = DaoManager.Instance._gameDao._itemSeqDataDict[2];
            }
            else if (count > 100)
            {
                itemSeqData = DaoManager.Instance._gameDao._itemSeqDataDict[3];
            }
            else
            {
                itemSeqData = DaoManager.Instance._gameDao._itemSeqDataDict[3];
            }

            int idx = RandomUtil.calcProbability(itemSeqData._itemPropArr);
            //Debug.Log("~~~~~~~~~~~idx=" + itemSeqData._itemPropArr);
            int i = 0;
            int[] itemArr = null;
            foreach (var item in itemSeqData._itemPropDict)
            {
                if (idx == i)
                {
                    itemArr = item.Key;
                    break;
                }
                i++;
            }

            idx = RandomUtil.getRandomInteger(0, itemArr.Length);
            int itemId = itemArr[idx];
            return itemId;
        }

        /// <summary>
        /// 计算下一个生成的道具Id
        /// </summary>
        private void calcNextItemId()
        {
            if (BusinessManager.Instance._gameBiz._nextItemId != 0)
                return;

            //Random Gen Obj on Fix Position
            ++_genCounter;
            //从第7个开始，统计下落数量
            if (_genCounter == 8)
            {
                _isCalcDropCount = true;
            }

            int itemId = 1;
            if (_genCounter <= 10)
            {
                //前10个固定
                if (EntityManager.Instance._userEntity._isCheckGuide)
                {
                    itemId = s_itemGenSeqGuideMode[_genCounter - 1];
                }
                else
                {
                    itemId = s_itemGenSeq[_genCounter - 1];
                }
            }
            else
            {
                if (_dropCounter > 5)
                {
                    //板糖
                    //_nextBlockNo = _genCounter + RandomUtil.getRandomInteger(5, 8);
                    itemId = ITEM_BLOCK_NORMAL;
                    _dropCounter = 0;
                }
                else
                {
                    //从第11个起，是根据分数随机，而不是根据生成的数量
                    itemId = (byte)getRandomItemId(BusinessManager.Instance._gameBiz._score);

                    ItemData itemData = DaoManager.Instance._gameDao._itemDataDict[itemId];
                    if (itemData._gainType == ItemGainType.BUY)
                    {
                        //必须先购买后才能出现
                        if (!BusinessManager.Instance._userBiz.isOwnCollection(itemId))
                        {
                            calcNextItemId();
                            return;
                        }
                    }

                }
            }

            //if (itemId == BusinessManager.Instance._gameBiz._nextItemId)
            ////&& (itemId == ITEM_BLOCK
            ////|| itemId == ITEM_ACC_DESSERT
            ////|| itemId == ITEM_ELIMINATE))
            //{
            //    //特殊类型，与上一个ID相同，重新生成道具Id
            //    calcNextItemId();
            //}
            //else
            //{
            BusinessManager.Instance._gameBiz._nextItemId = itemId;
            if (_isUnlimitTest)
            {
                BusinessManager.Instance._gameBiz._nextItemId = _testItemId;
            }
            //    ViewEvent ve = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_CHANGE_NEXT_ITEM);
            //    EventCenter.Instance.send(ve);
            //}

            //BusinessManager.Instance._gameBiz._nextItemId = 1;
        }

        /// <summary>
        /// 生成道具
        /// </summary>
        public void genItem()
        {

            if (BusinessManager.Instance._gameBiz._nextItemId == 0)
            {
                BusinessManager.Instance._gameBiz._nextItemId = 1;
            }

            ItemData itemData = DaoManager.Instance._gameDao._itemDataDict[BusinessManager.Instance._gameBiz._nextItemId];
            GameObject obj = PrefabPool.Instance.getObject("Prefab/Game/" + itemData._prefab);
            Vector3 scale = obj.transform.localScale;
            obj.transform.setParent(_itemRoot);
            obj.transform.localScale = scale;

            ItemBase item = null;
            if (itemData._itemType == ItemType.DESSERT
                || itemData._itemType == ItemType.TEST)
            {
                item = obj.GetComponent<ItemDessert>();
            }
            else if (itemData._itemType == ItemType.ELIMINATE)
            {
                item = obj.GetComponent<ItemEliminate>();
            }
            else if (itemData._itemType == ItemType.BLOCK)
            {
                item = obj.GetComponent<ItemBlock>();
            }
            else if (itemData._itemType == ItemType.TIME)
            {
                item = obj.GetComponent<ItemTime>();
            }
            else if (itemData._itemType == ItemType.ACC_DESSERT)
            {
                item = obj.GetComponent<ItemAccDessert>();
            }
            else if (itemData._itemType == ItemType.GOLD)
            {
                item = obj.GetComponent<ItemGold>();
            }
            else if (itemData._itemType == ItemType.ROTATE)
            {
                item = obj.GetComponent<ItemRotate>();
            }
            item.init(this, _genCounter);
            item._gold = itemData._rewardGold;
            item._data = itemData;
            _curItem = item;
            _itemList.Add(item);

            BusinessManager.Instance._gameBiz._nextItemId = 0;

            GameObject plateObj = PrefabPool.Instance.getObject("Prefab/Game/Plate");
            plateObj.transform.setParent(_itemRoot);
            Plate plate = plateObj.GetComponent<Plate>();
            plate.updatePosX(item.transform.localPosition.x);
            _plateList.Add(plate);
            checkAddMap();
        }

        /// <summary>
        /// 落下道具
        /// </summary>
        private void dropItem()
        {
            //Drop Obj
            if (_curItem != null)
            {
                long endTime = DateTimeUtil.GetTimestampMS();
                //Debug.Log("playSfxTime = " + endTime);
                //Debug.Log("inteverlTime = " + (endTime - _startTime));

                AudioManager.Instance.play(AudioManager.SOUND_SFX_ITEM_DROP_START);
                //AudioManager.Instance.play(AudioManager.SOUND_SFX_BTN_CLICK);
                _curItem.onDropBegin();
                if (_isCalcDropCount)
                {
                    _dropCounter++;
                }
            }
        }

        /// <summary> 
        /// 是否可以下落 
        /// </summary>
        /// <returns></returns>
        private bool isCanDrop()
        {
            bool isCanDrop = false;
            if (_curItem != null)
            {
                if (_curItem._state == ItemState.READY)
                {
                    isCanDrop = true;
                }
            }
            return isCanDrop;
        }

        /// <summary>
        /// 获取最顶部的寿司
        //  / </summary>
        /// <returns></returns>
        private ItemBase getTopItem()
        {
            float maxPosY = -9999;
            ItemBase topItem = null;
            for (int i = 0; i < _itemList.Count; i++)
            {
                ItemBase item = _itemList[i];
                if (item != null
                    //&& sushi._state == ItemState.DROP_END
                    && item._isCollisionTrigger
                    && item._type != ItemType.ELIMINATE)
                {
                    float curPosY = item.transform.localPosition.y;
                    if (curPosY > maxPosY)
                    {
                        maxPosY = curPosY;
                        topItem = item;
                    }
                }
            }
            return topItem;
        }

        //private void playItemAnim()
        //{
        //    for (int i = 0; i < _itemList.Count; i++)
        //    {
        //        ItemBase item = _itemList[i];
        //        if (item._type == ItemType.BLOCK)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            item.playAnim();
        //        }
        //    }
        //}

        /// <summary>
        /// 触屏
        /// </summary>
        private void touchDown()
        {
            if (isCanDrop())
            {
                dropItem();
            }
        }

        /// <summary>
        /// 更新甜点
        /// </summary>
        /// <param name="dt"></param>
        private void updateDessert(float dt)
        {
            for (int i = 0; i < _itemList.Count; i++)
            {
                _itemList[i].fixedUpdate(dt);
            }
        }

        /// <summary>
        /// 游戏结束时柱子和UI飞出动画
        /// </summary>
        public void gameOverFlyAnim()
        {
            ViewEvent ve = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_GAME_OVER);
            EventCenter.Instance.send(ve);
            _conveyor.DOBlendableLocalMoveBy(new Vector2(0, 550), 2.0f);
            if (_curItem != null)
            {
                _itemList.Remove(_curItem);
                _curItem.destory();
            }
            destoryPlate();
        }

        /// <summary>
        /// 失败时散落效果
        /// </summary>
        public void scatteringAnim()
        {
            for (int i = 0; i < _itemList.Count; i++)
            {
                ItemBase item = _itemList[i];
                item._rigidbody.sharedMaterial.friction = 0.01f;
                item._rigidbody.sharedMaterial.bounciness = 0;
                if (item._state == ItemState.DROP_END
                    || item._state == ItemState.DROP_DURATION)
                {
                    item._rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    item._rigidbody.freezeRotation = false;
                    item._rigidbody.constraints = RigidbodyConstraints2D.None;
                    item._rigidbody.mass = 20;
                    float r = Random.Range(0.0f, 1.0f);
                    if (r > 0.5f)
                    {
                        item._rigidbody.AddForce(new Vector2(-500, -30));
                    }
                    else
                    {
                        item._rigidbody.AddForce(new Vector2(500, -200));
                    }
                }

            }
        }

        /// <summary>
        /// 检测生成下一个道具
        /// </summary>
        public void checkGenItem()
        {
            if (_isGeningItem && _curItem._state != ItemState.INIT)
            {
                return;
            }

            calcNextItemId();

            if (BusinessManager.Instance._gameBiz._score <= 10)
            {
                _genItemInterval = 3.0f;
            }
            else if (BusinessManager.Instance._gameBiz._score > 10
                && BusinessManager.Instance._gameBiz._score <= 30)
            {
                _genItemInterval = 2.5f;
            }
            else if (BusinessManager.Instance._gameBiz._score > 30
               && BusinessManager.Instance._gameBiz._score <= 50)
            {
                _genItemInterval = 2.0f;
            }
            else if (BusinessManager.Instance._gameBiz._score > 50
               && BusinessManager.Instance._gameBiz._score <= 100)
            {
                _genItemInterval = 1.0f;
            }
            else if (BusinessManager.Instance._gameBiz._score > 100)
            {
                _genItemInterval = 0.1f;
            }

            if (_isUnlimitTest)
            {
                _genItemInterval = 0.02f;
            }

            _timer = new vp_Timer.Handle();
            vp_Timer.In(_genItemInterval, delegate ()
            {
                genItem();
                _timer.Cancel();
                _isGeningItem = false;
            }, _timer);

            _isGeningItem = true;
        }

        /// <summary>
        /// 更新盘子Y坐标
        /// </summary>
        /// <param name="y"></param>
        private void updatePlateY(float y)
        {
            for (int i = 0; i < _plateList.Count; i++)
            {
                Plate plate = _plateList[i];
                if (plate._state == PlateState.NORMAL)
                {
                    plate.updatePosY(y);
                }
            }
        }

        /// <summary>
        /// 更新盘子
        /// </summary>
        /// <param name="dt"></param>
        private void updatePlate(float dt)
        {
            for (int i = 0; i < _plateList.Count; i++)
            {
                _plateList[i].fixedUpdate(dt);
            }
        }

        /// <summary>
        /// 销毁盘子
        /// </summary>
        private void destoryPlate()
        {
            for (int i = 0; i < _plateList.Count; i++)
            {
                _plateList[i].destory();
            }
            _plateList.Clear();
        }

        /// <summary>
        /// 检测引导
        /// </summary>
        private void checkGuide()
        {

            if (EntityManager.Instance._userEntity._isCheckGuide
                && _curItem != null)
            {
                switch (_genCounter)
                {
                    case 1:
                        if (_curItem._state == ItemState.READY
                            && BusinessManager.Instance._gameBiz._guideStep == 0)
                        {
                            if (_curItem.transform.localPosition.x < 180)
                            {
                                BusinessManager.Instance._gameBiz._guideStep = 1;
                                BusinessManager.Instance._gameBiz._isGuiding = true;
                                Bundle bundleData = new Bundle();
                                bundleData.PutFloat("triggerPosX", 180);
                                ViewManager.Instance.getView(ViewConstant.ViewId.GUIDE, bundleData);
                            }
                        }
                        break;
                    case 2:
                        if (_curItem._state == ItemState.READY
                            && BusinessManager.Instance._gameBiz._guideStep == 1)
                        {
                            if (_curItem.transform.localPosition.x < -200)
                            {
                                BusinessManager.Instance._gameBiz._guideStep = 2;
                                BusinessManager.Instance._gameBiz._isGuiding = true;
                                Bundle bundleData = new Bundle();
                                bundleData.PutFloat("triggerPosX", -200);
                                ViewManager.Instance.getView(ViewConstant.ViewId.GUIDE, bundleData);
                                _lastGuideItem = _curItem;
                            }
                        }
                        break;
                    case 3:
                        if (_lastGuideItem._state == ItemState.DROP_END
                           && BusinessManager.Instance._gameBiz._guideStep == 2)
                        {
                            BusinessManager.Instance._gameBiz._guideStep = 3;
                            BusinessManager.Instance._gameBiz._isGuiding = true;
                            Bundle bundleData = new Bundle();
                            bundleData.PutFloat("triggerPosX", -200);
                            ViewManager.Instance.getView(ViewConstant.ViewId.GUIDE, bundleData);
                        }
                        else if (_curItem._state == ItemState.READY
                           && BusinessManager.Instance._gameBiz._guideStep == 5)
                        {
                            if (_curItem.transform.localPosition.x < 110)
                            {
                                BusinessManager.Instance._gameBiz._isGuiding = true;
                                Bundle bundleData = new Bundle();
                                bundleData.PutFloat("triggerPosX", 110);
                                ViewManager.Instance.getView(ViewConstant.ViewId.GUIDE, bundleData);
                                _lastGuideItem = _curItem;
                            }
                        }
                        break;
                    case 4:
                        if (_lastGuideItem._state == ItemState.DROP_END
                            && BusinessManager.Instance._gameBiz._guideStep == 6)
                        {
                            BusinessManager.Instance._gameBiz._isGuiding = true;
                            Bundle bundleData = new Bundle();
                            bundleData.PutFloat("triggerPosX", 110);
                            ViewManager.Instance.getView(ViewConstant.ViewId.GUIDE, bundleData);
                        }

                        //if (_curItem._state == ItemState.READY
                        //    && BusinessManager.Instance._gameBiz._guideStep == 6)
                        //{
                        //    if (_curItem.transform.localPosition.x < 110)
                        //    {
                        //        BusinessManager.Instance._gameBiz._guideStep = 7;
                        //        BusinessManager.Instance._gameBiz._isGuiding = true;
                        //        Bundle bundleData = new Bundle();
                        //        bundleData.PutFloat("triggerPosX", 110);
                        //        ViewManager.Instance.getView(ViewConstant.ViewId.GUIDE, bundleData);
                        //    }
                        //}
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 检测增加地图
        /// </summary>
        private void checkAddMap()
        {
            //_ctrl._sceneLoader.addBg();

            if (_itemList.Count >= 100 && _itemList.Count % 100 == 0)
            {
                //每100个元素，一次增加15张地图
                _ctrl._sceneLoader.addBg();
            }
        }

        /// <summary>
        /// 板糖下落后改变场上所有道具的摩擦力和弹力
        /// </summary>
        public void updatePhysicRatioByBlock()
        {
            for (int i = 0; i < _itemList.Count; i++)
            {
                ItemBase item = _itemList[i];
                if (item._type == ItemType.ACC_DESSERT
                    || item._type == ItemType.TEST
                    || item._type == ItemType.TIME
                    || item._type == ItemType.DESSERT
                    || item._type == ItemType.BLOCK
                    || item._type == ItemType.ROTATE)
                {
                    item.updatePhysicRatio();
                }
            }
        }

        /// <summary>
        /// 记录道具的状态
        /// </summary>
        public void recordItemState()
        {
            for (int i = 0; i < _itemList.Count; i++)
            {
                ItemBase item = _itemList[i];
                if (item._state == ItemState.DROP_END)
                {
                    if (_recordDataDict.ContainsKey(item.gameObject.GetInstanceID()))
                    {
                        _recordDataDict[item.gameObject.GetInstanceID()]._pos = item.gameObject.transform.position;
                        _recordDataDict[item.gameObject.GetInstanceID()]._rotation = item.gameObject.transform.rotation;
                    }
                    else
                    {
                        RecordItemData data = new RecordItemData();
                        data._pos = item.gameObject.transform.position;
                        data._rotation = item.gameObject.transform.rotation;
                        _recordDataDict.Add(item.gameObject.GetInstanceID(), data);
                    }
                    //Debug.Log("记录道具状态······");
                }
            }
            if (_curItem != null)
            {
                BusinessManager.Instance._gameBiz._recordNextItemId = _curItem._data._itemId;
            }
            BusinessManager.Instance._gameBiz._recordDropCount = _dropCounter;
            BusinessManager.Instance._gameBiz._recordScore = BusinessManager.Instance._gameBiz._score;
            BusinessManager.Instance._gameBiz._recordMainCameraY = _ctrl._cameraController._mainCameraObj.transform.localPosition.y;
            BusinessManager.Instance._gameBiz._recordBgCameraY = _ctrl._cameraController._bgCameraObj.transform.localPosition.y;

        }

        /// <summary>
        /// 检测记录点
        /// </summary>
        /// <param name="dt"></param>
        private void checkRecord(float dt)
        {
            _recordInterval += dt;
            for (int i = 0; i < _itemList.Count; i++)
            {
                ItemBase item = _itemList[i];
                if (item._state == ItemState.DROP_END)
                {
                    if (item._rigidbody.velocity.y > 0.0001f
                        || item._rigidbody.velocity.x > 0.0001f)
                    {
                        return;
                    }
                }
                //else if (item._state == ItemState.DROP_DURATION)
                //{
                //    return;
                //}
            }

            if (_recordInterval > 2.0f)
            {
                _recordInterval = 0;
                recordItemState();
            }
        }
        /// <summary>
        /// 恢复道具状态
        /// </summary>
        public void recoverItemState()
        {
            for (int i = _itemList.Count - 1; i >= 0; i--)
            {
                ItemBase item = _itemList[i];

                if (_lastDropInstanceId == item.gameObject.GetInstanceID())
                {
                    _itemList.Remove(item);
                    if (item == _curItem)
                    {
                        _curItem = null;
                    }
                    Object.Destroy(item.gameObject);
                }
                else
                {
                    if (_recordDataDict.ContainsKey(item.gameObject.GetInstanceID()))
                    {
                        //记录之后又掉到桌子上的 移除掉
                        //if (item._state == ItemState.DROP_GROUND
                        //    || item._state == ItemState.DESTORY)
                        //{
                        //    _itemList.Remove(item);
                        //    if (item == _curItem)
                        //    {
                        //        _curItem = null;
                        //    }
                        //    Object.Destroy(item.gameObject);
                        //}
                        //else
                        //{

                        item.gameObject.transform.position = _recordDataDict[item.gameObject.GetInstanceID()]._pos;
                        item.gameObject.transform.rotation = _recordDataDict[item.gameObject.GetInstanceID()]._rotation;
                        item.onDropEnd();
                        item.setDefaultPhysic();

                        //if (item is ItemBlock)
                        //{
                        //    ItemBlock block = (item as ItemBlock);
                        //    block._rigidbody.bodyType = RigidbodyType2D.Kinematic;
                        //    block._rigidbody.freezeRotation = true;
                        //    block._rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                        //}
                        //else
                        //{
                        //暂时冻结刚体，防止恢复时相互作用力弹开
                        item.freezeRigidbody();
                        //}
                        //}
                    }
                    else
                    {
                        _itemList.Remove(item);
                        if (item == _curItem)
                        {
                            _curItem = null;
                        }
                        Object.Destroy(item.gameObject);
                    }
                }
            }

            BusinessManager.Instance._gameBiz._score = BusinessManager.Instance._gameBiz._recordScore;
            BusinessManager.Instance._gameBiz._nextItemId = BusinessManager.Instance._gameBiz._recordNextItemId;
            _dropCounter = BusinessManager.Instance._gameBiz._recordDropCount;
            _ctrl._cameraController.moveToCameraPosY(BusinessManager.Instance._gameBiz._recordMainCameraY, BusinessManager.Instance._gameBiz._recordBgCameraY);

            _timer = new vp_Timer.Handle();
            vp_Timer.In(2.5f, delegate ()
            {
                foreach (var item in _itemList)
                {
                    if (!(item is ItemBlock))
                    {
                        //item.unfreezeRigidbody();
                    }
                }

                genItem();
                _timer.Cancel();
                _isGeningItem = false;
            }, _timer);

        }

        /// <summary>
        /// 检测销毁道具
        /// </summary>
        private void checkDestory()
        {
            for (int i = _itemList.Count - 1; i >= 0; i--)
            {
                ItemBase item = _itemList[i];
                if (item._state == ItemState.OUT_SCREEN)
                {
                    _itemList.Remove(item);
                    item.destory();
                }
            }

            for (int i = _plateList.Count - 1; i >= 0; i--)
            {
                Plate plate = _plateList[i];
                if (plate._state == PlateState.OUT_SCREEN)
                {
                    _plateList.Remove(plate);
                    plate.destory();
                }
            }
        }

        public void onGUI(float dt)
        {

            if (EntityManager.Instance._userEntity._isCheckGuide)
            {
                return;
            }

            if (UnityEngine.Event.current != null)
            {
                //if (EventSystem.current.IsPointerOverGameObject())
                //{
                //    return;
                //}

                if (UnityEngine.Event.current.type == EventType.MouseDown)
                {
                    //_startTime = DateTimeUtil.GetTimestampMS();
                    //Debug.Log("click screen time = " + _startTime);
                    if (UnityEngine.Event.current.mousePosition.y > 200.0f)
                    {
                        touchDown();
                    }
                }
                else if (UnityEngine.Event.current.type == EventType.MouseDrag)
                {

                }
                else if (UnityEngine.Event.current.type == EventType.MouseUp)
                {

                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                touchDown();
            }
        }

        public void update(float dt)
        {
            updateConveryor();
        }

        public void fixedUpdate(float dt)
        {
            if (BusinessManager.Instance._gameBiz._isGuiding)
            {
                return;
            }

            if (BusinessManager.Instance._gameBiz._isGameOver)
            {
                return;
            }

            updateDessert(dt);
            checkGuide();
            updatePlate(dt);
            checkRecord(dt);
            checkDestory();
        }

    }
}
