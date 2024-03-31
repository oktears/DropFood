using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    /// <summary>游戏中对象类基类</summary>
    public abstract class GameObj
    {

        protected Transform _target = null;

        // empty init func; 
        public virtual void init() { }

        public virtual void init(Transform root) { }

        /// <summary>绑定一个游戏对象，成功则为true，失败返回false</summary>
        public virtual bool bindTarget(Transform target)
        {
            if (_target == null)
            {
                _target = target;
                return true;
            }
            return false;
        }

        public virtual Transform getTarget()
        {
            return _target;
        }

        public abstract void update(float dt);
        public abstract void fixedUpdate(float dt);
        public abstract void lateUpdate(float dt);

        public virtual void destroy()
        {
            if (_target != null)
            {
                GameObject.Destroy(_target.gameObject);
            }
        }

    }
}
