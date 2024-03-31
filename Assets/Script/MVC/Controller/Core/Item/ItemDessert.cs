
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 道具：甜点
    /// </summary>
    public class ItemDessert : ItemBase
    {

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision(collision);
        }
    }
}
