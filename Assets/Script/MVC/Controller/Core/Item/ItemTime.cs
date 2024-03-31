using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{
    /// <summary>
    /// 道具：增加游戏时长
    /// </summary>
    public class ItemTime : ItemBase
    {
        //增加游戏时长
        public int _time = 5;
        //是否触发
        private bool _isTrigger = false;

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            onCollision(collision);
            checkAddGameTime(tag);
        }

        private void checkAddGameTime(string tag)
        {
            if (_isTrigger)
                return;

            if (tag == TagManager.TAG_ITEM_BLOCK
                || tag == TagManager.TAG_ITEM_DESSERT
                || tag == TagManager.TAG_ITEM_TIEM
                || tag == TagManager.TAG_ITEM_ELIMINATE
                || tag == TagManager.TAG_ITEM_ROTATION
                || tag == TagManager.TAG_ITEM_GOLD)
            {
                _isTrigger = true;
                _itemManager._ctrl.addGameTime(_time);
            }
        }
    }
}
