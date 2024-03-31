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
    /// 道具：自旋转
    /// </summary>
    public class ItemRotate : ItemBase
    {
        //是否触发
        private bool _isTrigger = false;

        public override void onDropBegin()
        {
            base.onDropBegin();
            //开始下落时旋转
            this.transform.DOLocalRotate(new Vector3(0, 0, -360), 3.0f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
            _isTrigger = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            onCollision(collision);
            if (_isTrigger)
            {
                DOTween.Pause(this.transform);
                _isTrigger = false;
            }
        }
    }
}
