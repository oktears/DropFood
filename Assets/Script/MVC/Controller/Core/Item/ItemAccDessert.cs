
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 道具：加速甜点
    /// </summary>
    public class ItemAccDessert : ItemBase
    {
        public Transform _effectTrans;

        public override void onDropBegin()
        {
            base.onDropBegin();
            if (_effectTrans != null)
            {
                _effectTrans.gameObject.SetActive(false);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision(collision);
        }
    }
}
