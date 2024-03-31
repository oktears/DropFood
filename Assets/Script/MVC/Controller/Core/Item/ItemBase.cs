// using DG.Tweening;
// using Fishtail.PlayTheBall.Vibration;
// using MoreMountains.NiceVibrations;

using DG.Tweening;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 道具类型
    /// </summary>
    public enum ItemType
    {
        UNKNOWN = 0,

        /// <summary>
        /// 普通
        /// </summary>
        DESSERT = 1,

        /// <summary>
        /// 阻挡
        /// </summary>
        BLOCK = 2,

        /// <summary>
        /// 消除
        /// </summary>
        ELIMINATE = 3,

        /// <summary>
        /// 时间
        /// </summary>
        TIME = 4,

        /// <summary>
        /// 加速甜点
        /// </summary>
        ACC_DESSERT = 5,

        /// <summary>
        /// 旋转
        /// </summary>
        ROTATE = 6,

        /// <summary>
        /// 金币
        /// </summary>
        GOLD = 7,

        /// <summary>
        /// 测试类型
        /// </summary>
        TEST = 100,
    }

    /// <summary>
    /// 道具状态
    /// </summary>
    public enum ItemState
    {
        UNKNOWN = 0,

        /// <summary>
        /// 初始化
        /// </summary>
        INIT = 1,

        /// <summary>
        /// 可下落
        /// </summary>
        READY = 2,

        /// <summary>
        /// 下落中
        /// </summary>
        DROP_DURATION = 3,

        /// <summary>
        /// 下落结束
        /// </summary>
        DROP_END = 4,

        /// <summary>
        /// 落到地面
        /// </summary>
        DROP_GROUND = 5,

        /// <summary>
        /// 传送带出屏
        /// </summary>
        OUT_SCREEN = 6,

        /// <summary>
        /// 被消除
        /// </summary>
        DESTORY = 7,
    }

    public class ItemBase : MonoBehaviour
    {
        public SpriteRenderer _render;

        //道具管理器
        public ItemManager _itemManager;

        //刚体
        public Rigidbody2D _rigidbody;

        //碰撞体
        public Collider2D _collider;

        //类型
        public ItemType _type;

        //奖励金币数
        public int _gold;

        //道具数据
        public ItemData _data;

        //速度
        public float _speedRatio = 1;

        //是否可见
        public bool _isVisable { get; set; }

        //状态
        public ItemState _state { get; set; }

        //移动动画
        private Tweener _moveAnim;

        //默认移动速度
        private static float DEFAULT_MOVE_SEPEED = 6.25f;

        //移动速度
        public static float _moveSpeed = 0;

        //进入，离开下落区X坐标
        private float _enterDropAreaX = SCREEN_WIDTH / 2 + 100;

        private float _exitDropAreaX = -SCREEN_WIDTH / 2 - 100;

        //下落时间
        private float _dropTime = 0;

        //编号
        private int _no;

        //是否触发碰撞
        public bool _isCollisionTrigger;

        //在传送带上Y坐标偏移
        public float _posOffsetY = 60;

        //是否加过分
        private bool _isAddScore = false;

        public readonly static float SCREEN_WIDTH = 750;
        public readonly static float SCREEN_HEIGHT = 1334;

        private readonly static Vector2 INIT_POS = new Vector2(SCREEN_WIDTH / 2 + 300, SCREEN_HEIGHT / 2 - 100);

        private const string ANIM_IDLE = "ele01_idle";
        private const string ANIM_HIT = "ele01_hit";
        private const string ANIM_HITME = "ele01_hitme";
        private const string ANIM_DROP = "ele01_drop";

        //特效
        protected EffectBase _effect;

        //上一帧的位置
        private Vector2 _lastPos;

        public virtual void init(ItemManager itemManager, int no)
        {
            _no = no;
            _itemManager = itemManager;

            _state = ItemState.INIT;
            Transform conveyor = (ViewManager.Instance._firstViewManager._curView as GameView)._conveyor;
            Vector3 screenPos = ViewManager.Instance._UI2DCamera.WorldToScreenPoint(conveyor.position);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Vector3 targetObjPos = transform.parent.InverseTransformPoint(worldPos);

            transform.localPosition = new Vector2(INIT_POS.x, targetObjPos.y + _posOffsetY);

            _rigidbody.simulated = false;
            _rigidbody.velocity = new Vector2(0, -2);
            _render = GetComponent<SpriteRenderer>();
            _render.sortingOrder = _no;
            _exitDropAreaX = -SCREEN_WIDTH / 2 - _render.size.x / 2;
            _moveSpeed = DEFAULT_MOVE_SEPEED * _speedRatio;

            setDefaultPhysic();

            //_moveAnim = transform.DOLocalMoveX(transform.localPosition.x - 1500.0f, 5.0f).SetEase(Ease.Linear);
            Debug.Log(string.Format("{0}状态：甜点-初始化", _no));
            //_collider.enabled = false;
            //GetComponent<BoxCollider2D>().enabled = true;
        }

        public virtual void onCollision(Collision2D collision)
        {
            if (!_isCollisionTrigger)
            {
                //未收藏过
                if (!BusinessManager.Instance._userBiz.isOwnCollection(_data._itemId)
                    && !BusinessManager.Instance._gameBiz._activeCollection[_data._itemId])
                {
                    //金币类型不会被解锁
                    if (_data._itemType != ItemType.GOLD
                        && _data._itemType != ItemType.TEST)
                    {
                        ViewEvent newCollectEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                            ViewEventConstant.EVENT_GAME_VIEW_NEW_COLLECT);
                        newCollectEvent._bundleData.PutInt("ItemId", _data._itemId);
                        EventCenter.Instance.send(newCollectEvent);
                    }

                    //激活收藏品
                    BusinessManager.Instance._gameBiz.activeCollection(_data._itemId);
                }

                _isCollisionTrigger = true;
                ++BusinessManager.Instance._gameBiz._score;
                Debug.Log(string.Format("{0}状态：甜点 - 碰撞开始", _no));
                _itemManager.updateCamera();
                playHitAnim(collision);
                hitOtherItem(collision);

                //碰撞音效
                if (_type == ItemType.BLOCK)
                {
                    AudioManager.Instance.play(AudioManager.SOUND_SFX_PIZZA);
                }
                else if (_type == ItemType.ELIMINATE)
                {
                    AudioManager.Instance.play(AudioManager.SOUND_SFX_ELIMINATE);
                }
                else if (_type == ItemType.GOLD)
                {
                    AudioManager.Instance.play(AudioManager.SOUND_SFX_ITEM_GOLD);
                    AudioManager.Instance.playDelay(AudioManager.SOUND_SFX_GAIN_GOLD, 0.2f);
                }
                else
                {
                    AudioManager.Instance.play(AudioManager.SOUND_SFX_ITEM_COLL);
                }

                if (EntityManager.Instance._userEntity._isOpenSfx)
                {
                    //震动
                    // MMVibrationManager.Haptic(HapticTypes.Selection);
                }
            }

            if (checkAddScore(tag))
            {
                addScore();
            }
        }

        public virtual void onDropBegin()
        {
            _state = ItemState.DROP_DURATION;
            _rigidbody.simulated = true;
            _itemManager._lastDropInstanceId = this.gameObject.GetInstanceID();
            _itemManager.checkGenItem();
            //playDropAnim();
            Debug.Log(string.Format("{0}状态：甜点 - 下落中", _no));
        }

        public virtual void onDropGround()
        {
            if (!BusinessManager.Instance._gameBiz._isGameOver)
            {
                _state = ItemState.DROP_GROUND;
                BusinessManager.Instance._gameBiz._score--;
                _itemManager._ctrl.finishGame();
                Debug.Log(string.Format("{0}状态：甜点-落到地板上", _no));
            }
        }

        public virtual void onDropEnd()
        {
            _state = ItemState.DROP_END;


            //记录下落状态
            //_itemManager.recordItemState();
            Debug.Log(string.Format("{0}状态：甜点-下落结束", _no));
        }

        public virtual void onEnterDropArea()
        {
            _isVisable = true;
            _state = ItemState.READY;
            if (_type == ItemType.BLOCK)
            {
                AudioManager.Instance.play(AudioManager.SOUND_SFX_SPECIAL_ITEM_SHOW);
            }
            else if (_type == ItemType.GOLD)
            {
                //奖励金币出现提示
                ViewEvent newCollectEvent =
                    new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_BONUS_COLD);
                newCollectEvent._bundleData.PutInt("ItemId", _data._itemId);
                EventCenter.Instance.send(newCollectEvent);
            }
            else if (_type == ItemType.ACC_DESSERT
                     || _type == ItemType.GOLD)
            {
                AudioManager.Instance.play(AudioManager.SOUND_SFX_ACC_SHOW);
            }
            else if (_type == ItemType.ELIMINATE)
            {
                AudioManager.Instance.play(AudioManager.SOUND_SFX_ELIMINATE_SHOW);
            }

            Debug.Log(string.Format("{0}状态：甜点-就绪-可下落", _no));
        }

        public virtual void onExitDropArea()
        {
            _isVisable = false;
            _state = ItemState.OUT_SCREEN;
            gameObject.SetActive(false);
            _itemManager.checkGenItem();
            Debug.Log(string.Format("{0}状态：甜点-传送带-出屏", _no));
        }

        public virtual void onDestory()
        {
            gameObject.SetActive(false);
            _state = ItemState.DESTORY;
            Debug.Log(string.Format("{0}状态：甜点-被消除", _no));
        }

        public void destory()
        {
            GameObject.Destroy(gameObject);
        }

        protected virtual bool checkAddScore(string tag)
        {
            if (_isAddScore)
            {
                return false;
            }

            if (_state != ItemState.DROP_DURATION)
            {
                return false;
            }

            if (tag == TagManager.TAG_ITEM_BLOCK
                || tag == TagManager.TAG_ITEM_DESSERT
                || tag == TagManager.TAG_ITEM_TIEM
                || tag == TagManager.TAG_ITEM_ELIMINATE
                || tag == TagManager.TAG_TABLE
                || tag == TagManager.TAG_ITEM_ACC_DESSERT
                || tag == TagManager.TAG_ITEM_ROTATION
                || tag == TagManager.TAG_ITEM_GOLD
                || tag == TagManager.TAG_ITEM_TEST)
            {
                return true;
            }

            return false;
        }

        protected virtual void addScore()
        {
            _isAddScore = true;
            BusinessManager.Instance._gameBiz._gainGold += _gold;
            ViewEvent addScoreEvent = new ViewEvent(ViewConstant.ViewId.GAME,
                ViewEventConstant.EVENT_GAME_VIEW_UPDATE_SCORE_AND_GOLD);
            addScoreEvent._bundleData = new Bundle();
            //float boxH = _anim.AnimationState.Data.skeletonData.height;

            Vector2 itemWorldPos = transform.position;
            addScoreEvent._bundleData.PutObject("itemPos", itemWorldPos);
            addScoreEvent._bundleData.PutFloat("offsetY", 60.0f);
            addScoreEvent._bundleData.PutInt("gold", _gold);
            EventCenter.Instance.send(addScoreEvent);
        }

        public virtual void fixedUpdate(float dt)
        {
            if (_state == ItemState.DROP_DURATION)
            {
                //持续下落状态
                //Debug.Log("````````···" + _rigidbody.velocity.y);
                //以坐标判断是否下落结束，而非刚体速度，刚体速度有时候永远不等于0
                if (_dropTime > 0 && _lastPos.Equals(transform.localPosition))
                {
                    onDropEnd();
                }

                //Debug.Log(_rigidbody.velocity);
                _dropTime += dt;
            }
            else if (_state == ItemState.INIT)
            {
                //初始状态
                float nextPosX = transform.localPosition.x - _moveSpeed;
                transform.localPosition = new Vector2(nextPosX, transform.localPosition.y);
                if (transform.localPosition.x < _enterDropAreaX)
                {
                    //进入下落区
                    onEnterDropArea();
                }
            }
            else if (_state == ItemState.READY)
            {
                //可下落状态
                float nextPosX = transform.localPosition.x - _moveSpeed;
                transform.localPosition = new Vector2(nextPosX, transform.localPosition.y);
                if (transform.localPosition.x < _exitDropAreaX)
                {
                    //离开下落区，出屏
                    onExitDropArea();
                }
            }

            _lastPos = transform.localPosition;
        }

        private void hitOtherItem(Collision2D collision)
        {
            if (collision.gameObject.tag == TagManager.TAG_ITEM_BLOCK
                || collision.gameObject.tag == TagManager.TAG_ITEM_DESSERT
                || collision.gameObject.tag == TagManager.TAG_ITEM_TIEM
                || collision.gameObject.tag == TagManager.TAG_ITEM_ELIMINATE
                || collision.gameObject.tag == TagManager.TAG_ITEM_ACC_DESSERT
                || collision.gameObject.tag == TagManager.TAG_ITEM_ROTATION
                || collision.gameObject.tag == TagManager.TAG_ITEM_GOLD
                || collision.gameObject.tag == TagManager.TAG_ITEM_TEST)
            {
                collision.gameObject.GetComponent<ItemBase>().playHitmeAnim();
            }
        }

        public void playHitAnim(Collision2D collision)
        {
            if (collision.gameObject.tag == TagManager.TAG_GROUND
                || collision.gameObject.tag == TagManager.TAG_TABLE)
            {
                return;
            }
        }

        public void playHitmeAnim()
        {
            if (_render != null)
            {
                transform.DOShakeScale(0.1f, 0.3f, 5, 90);
            }
        }

        public void playDropAnim()
        {
        }

        public virtual void setDefaultPhysic()
        {
            _rigidbody.mass = _rigidbody.mass * DebugConfig.s_itemMassRatio;
            _rigidbody.sharedMaterial.friction = DebugConfig.s_itemDefaultFriction;
            _rigidbody.sharedMaterial.bounciness = DebugConfig.s_itemDefaultBounciness;
        }

        public void updatePhysicRatio()
        {
            _rigidbody.sharedMaterial.friction = DebugConfig.s_itemDefaultFriction * DebugConfig.s_blockFrictionRatio;
            _rigidbody.sharedMaterial.bounciness =
                DebugConfig.s_itemDefaultBounciness * DebugConfig.s_blockBouncinessRatio;
        }

        /// <summary>
        /// 冻结刚体
        /// </summary>
        public void freezeRigidbody()
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        public void unfreezeRigidbody()
        {
            _rigidbody.constraints = RigidbodyConstraints2D.None;
        }
    }
}