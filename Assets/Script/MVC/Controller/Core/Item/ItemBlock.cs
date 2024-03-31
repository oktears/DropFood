using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 道具：板糖
    /// </summary>
    public class ItemBlock : ItemBase
    {
        //底部接触的道具
        private GameObject _bottomItem;
        //底部是否接触道具
        private bool _bottomHasItem = false;
        //检测底部碰撞时间
        private float _checkBottomTimer = 0.2f;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.tag == TagManager.TAG_ITEM_BLOCK
                || collision.gameObject.tag == TagManager.TAG_ITEM_DESSERT
                || collision.gameObject.tag == TagManager.TAG_ITEM_TIEM
                || collision.gameObject.tag == TagManager.TAG_ITEM_ACC_DESSERT
                || collision.gameObject.tag == TagManager.TAG_ITEM_ROTATION
                || collision.gameObject.tag == TagManager.TAG_ITEM_GOLD
                || collision.gameObject.tag == TagManager.TAG_ITEM_TEST
                )
            {
                if (collision.gameObject.transform.localPosition.y < transform.localPosition.y)
                {
                    //下方的元素
                    _bottomItem = collision.gameObject;
                    _bottomHasItem = true;
                }
            }
        }
        public override void onDropEnd()
        {
            base.onDropEnd();
            //锁定不再对下方道具受力
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
            //修改其他道具的摩擦力和弹性
            //_itemManager.updatePhysicRatioByBlock();
        }

        /// <summary>
        /// 检测板糖是否需要下落
        /// </summary>
        /// <returns></returns>
        public void checkBlockDrop()
        {
            if (_bottomHasItem)
            {
                if (_bottomItem != null)
                {
                    ItemBase item = _bottomItem.GetComponent<ItemBase>();
                    if (item._state == ItemState.DESTORY
                        || item._rigidbody.velocity.y < -0.05f)
                    {
                        _state = ItemState.DROP_DURATION;
                        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                        _rigidbody.velocity = new Vector2(0, -1.0f);
                        //Debug.Log("板糖底部元素被消除了！");
                    }
                }
            }
        }

        public override void fixedUpdate(float dt)
        {
            base.fixedUpdate(dt);
            _checkBottomTimer -= dt;
            if (_checkBottomTimer <= 0)
            {
                _checkBottomTimer = 0.2f;
                checkBlockDrop();
            }
        }

        public override void setDefaultPhysic()
        {
            base.setDefaultPhysic();
        }

    }

}
