using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 道具：金币
    /// </summary>
    public class ItemGold : ItemBase
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision(collision);
            destorySelf();
        }

        private void destorySelf()
        {
            onDestory();
        }
    }
}
