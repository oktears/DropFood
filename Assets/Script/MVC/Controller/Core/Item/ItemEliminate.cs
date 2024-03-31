using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 道具：消除
    /// </summary>
    public class ItemEliminate : ItemBase
    {

        public override void init(ItemManager itemManager, int no)
        {
            base.init(itemManager, no);
            _effect = new EffectItemEliminate(transform);
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            onCollision(collision);
            checkEliminate(collision);
        }

        private void checkEliminate(UnityEngine.Collision2D collision)
        {
            if (collision.gameObject.tag == TagManager.TAG_ITEM_BLOCK
                || collision.gameObject.tag == TagManager.TAG_ITEM_DESSERT
                || collision.gameObject.tag == TagManager.TAG_ITEM_TIEM
                || collision.gameObject.tag == TagManager.TAG_ITEM_ACC_DESSERT
                || collision.gameObject.tag == TagManager.TAG_ITEM_ROTATION
                || collision.gameObject.tag == TagManager.TAG_ITEM_GOLD
                || collision.gameObject.tag == TagManager.TAG_ITEM_TEST)
            {
                _effect.attach(true);
                _effect.playParticleImmediately(true);
                collision.gameObject.GetComponent<ItemBase>().onDestory();
                onDestory();
            }
            else
            {
                //掉在桌子上，自身消失
                onDestory();
            }

            vp_Timer.In(0.02f, delegate ()
            {
                checkBlockDrop();
            });
        }

        private void checkBlockDrop()
        {
            for (int i = 0; i < _itemManager._itemList.Count; i++)
            {
                ItemBase item = _itemManager._itemList[i];
                if (item is ItemBlock)
                {
                    ItemBlock block = (item as ItemBlock);
                    block.checkBlockDrop();
                }
            }
        }
    }
}
