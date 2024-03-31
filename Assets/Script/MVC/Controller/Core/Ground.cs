using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chengzi
{

    public class Ground : MonoBehaviour
    {

        public Rigidbody2D _rigidbody;
        public BoxCollider2D _collider;

        public void Start()
        {
        }

        public void OnTriggerEnter2D(Collider2D c)
        {
        }

        public void OnTriggerStay2D(Collider2D c)
        {
        }

        public void OnTriggerExit2D(Collider2D c)
        {
        }

        public void OnCollisionEnter2D(Collision2D info)
        {
            if (info.collider == null) return;

            int layer = info.collider.gameObject.layer;
            if (info.collider.gameObject.tag == TagManager.TAG_ITEM_BLOCK
                || info.collider.gameObject.tag == TagManager.TAG_ITEM_DESSERT
                || info.collider.gameObject.tag == TagManager.TAG_ITEM_ELIMINATE
                || info.collider.gameObject.tag == TagManager.TAG_ITEM_TIEM
                || info.collider.gameObject.tag == TagManager.TAG_ITEM_ROTATION
                || info.collider.gameObject.tag == TagManager.TAG_ITEM_GOLD
                || info.collider.gameObject.tag == TagManager.TAG_ITEM_ACC_DESSERT)
            {
                ItemBase item = info.collider.gameObject.GetComponent<ItemBase>();
                if (item != null)
                {
                    item.onDropGround();
                }
            }
        }

        public void OnCollisionStay2D(Collision2D info)
        {

        }

        public void OnCollisionExit2D(Collision2D info)
        {

        }
    }
}