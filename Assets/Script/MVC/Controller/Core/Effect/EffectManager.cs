using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 特效类型
    /// </summary>
    public enum EffectType
    {
        /// <summary>
        /// 道具消失
        /// </summary>
        ITEM_ELIMINATE = 1,
        /// <summary>
        /// 动态模糊滤镜
        /// </summary>
        RADIAL_BLUR_FILTER = 2,
        /// <summary>
        /// 速度线
        /// </summary>
        SPEED_LINE = 3,
    }

    /// <summary>
    /// 特效管理器
    /// </summary>
    public class EffectManager
    {
        //特效map
        protected Dictionary<EffectType, EffectBase> _effectDic = new Dictionary<EffectType, EffectBase>();
        //特效Id列表
        private List<EffectType> _idList = new List<EffectType>();
        //状态
        private ulong _state = 0;

        public EffectManager()
        {
            init();
        }

        private void init()
        {
            //加速滤镜
            //addEffect(EffectType.RADIAL_BLUR_FILTER, null);
            //addEffect(EffectType.SPEED_LINE, null);
        }

        /// <summary>
        /// 添加特效
        /// </summary>
        /// <param name="type"></param>
        /// <param name="target"></param>
        public void addEffect(EffectType type, Transform target)
        {
            _effectDic.Add(type, createEffect(target, type));
            _idList.Add(type);
        }

        public void fixedUpdate(float dt)
        {
            for (int i = 0; i < _effectDic.Count; i++)
            {
                _effectDic[_idList[i]].loop(dt);
            }
        }

        public virtual bool play(EffectType artId, EffectPlayMode mode)
        {
            if (_effectDic.ContainsKey(artId))
            {
                _effectDic[artId].playParticle(true, mode);
                _state |= (uint)artId;
            }
            else
            {
                return false;
            }
            return true;
        }

        public virtual bool play(EffectType artId)
        {
            return play(artId, EffectPlayMode.Normal);
        }

        public virtual bool play(EffectType artId, string name)
        {
            bool ret = false;
            if (_effectDic.ContainsKey(artId))
            {
                _effectDic[artId].playAnimatorByName(name);
                _state |= (uint)artId;
                ret = true;
            }

            return ret;
        }

        public virtual bool playState(EffectType artId, bool isShow)
        {
            bool ret = false;
            if (_effectDic.ContainsKey(artId))
            {
                _effectDic[artId].playIsShow(isShow);
                if (isShow)
                {
                    _state |= (uint)artId;
                }
                else
                {
                    _state &= ~(uint)artId;
                }
                ret = true;
            }

            return ret;
        }

        public virtual void attach(EffectType artId, bool isAttach)
        {
            if (_effectDic.ContainsKey(artId))
            {
                _effectDic[artId].attach(isAttach);
            }
        }

        //public void updateAlpha(float alpha)
        //{
        //    if (_artDic.ContainsKey((int)EffectType.Nos))
        //    {
        //        ((Nos)_artDic[(int)EffectType.Nos]).updateAlpha(alpha);
        //    }
        //}

        public virtual bool stopFilter(EffectType artId)
        {
            if (_effectDic.ContainsKey(artId))
            {
                _effectDic[artId].stopFilter();
            }
            return true;
        }

        public virtual bool stop(EffectType artId)
        {
            return stop(artId, EffectPlayMode.Normal);
        }

        public virtual bool stop(EffectType artId, EffectPlayMode mode)
        {
            if (_effectDic.ContainsKey(artId))
            {
                _effectDic[artId].playParticle(false, mode);
                _state &= ~(uint)artId;
            }
            else
            {
                return false;
            }
            return true;
        }

        public bool pause(EffectType artId)
        {
            return true;
        }

        public EffectBase createEffect(Transform target, EffectType effectId)
        {
            EffectBase art = null;
            switch (effectId)
            {
                case EffectType.ITEM_ELIMINATE:
                    art = new EffectItemEliminate(target);
                    break;
                case EffectType.RADIAL_BLUR_FILTER:
                    art = new EffectRadialBlur(target);
                    break;
                case EffectType.SPEED_LINE:
                    art = new EffectSpeedLine(target);
                    break;
            }
            return art;
        }

        public ulong getArtState()
        {
            return (ulong)_state;
        }

        public void setArtState(ulong state)
        {
            ulong newState = (state & 0xFFFFFFFFFFFFFFFFul);
            ulong change = newState ^ _state;
            if (change > 0)
            {
                for (int i = 0; i < 32; i++)
                {
                    if ((change & ((ulong)1 << i)) > 0)
                    {
                        bool play = (newState & ((ulong)1 << i)) > 0;
                        if (_effectDic.ContainsKey((EffectType)(1 << i)))
                        {
                            _effectDic[(EffectType)(1 << i)].playParticleImmediately(play);
                        }
                        else
                        {
                            Debug.Log("this key not found:" + (1 << i));
                        }
                    }
                }
                //Debug.Log("Setstate:" + m_state);
                _state = newState;
            }
        }

        public virtual bool onEvent(int eventId, Collision param)
        {
            return false;
        }

        public virtual void destroy()
        {
            _effectDic.Clear();
        }
    }
}
