using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 特效播放类型
    /// </summary>
    public enum EffectPlayMode
    {
        Normal,// 正常播放  特效播完自动回收
        Immediately// 强制播放 无论当前处于何种状态
    }

    /// <summary>
    /// 特效基类
    /// </summary>
    public class EffectBase
    {
        protected EffectType _effectId;
        protected List<ParticleSystem> _particle = new List<ParticleSystem>();
        protected List<GameObject> _gameObject = new List<GameObject>();
        protected Animator _animator;
        protected bool _playing = false;
        protected GameObject _targetObject;
        protected GameObject _root;

        //粒子回收间隔
        protected float _recycleInterval = 0.5f;

        public EffectBase(Transform trans)
        {
            //初始化数据
            if (trans != null)
            {
                _targetObject = trans.gameObject;
            }
        }

        /// <summary>
        /// 每0.5s检测一次粒子回收
        /// </summary>
        /// <param name="dt"></param>
        public virtual void loop(float dt)
        {
            if (_recycleInterval > 0)
            {
                _recycleInterval -= dt;
                if (_recycleInterval <= 0)
                {
                    _recycleInterval = 0.5f;
                    checkRecycle();
                }
            }
        }

        public virtual void attach(bool isAttach)
        {
            _root.transform.position = _targetObject.transform.position;
        }

        public virtual bool playParticle(bool isPlay, EffectPlayMode playMode)
        {
            bool result = true;
            if (playMode == EffectPlayMode.Normal)
            {
                result = playParticleNormal(isPlay);
            }
            if (playMode == EffectPlayMode.Immediately)
            {
                result = playParticleImmediately(isPlay);
            }
            return result;
        }

        public virtual bool playParticleNormal(bool isPlay)
        {
            bool result = true;

            for (int i = 0; i < _particle.Count; i++)
            {

                if (isPlay)
                {
                    result = true;
                    _particle[i].gameObject.SetActive(true);
                    //if (!particle[i].isPlaying)
                    //{
                    _particle[i].Play();
                    //}
                }
                else
                {
                    result = false;
                    _particle[i].Stop();
                }
            }

            for (int i = 0; i < _gameObject.Count; i++)
            {
                if (isPlay)
                {
                    //playing = true;
                    result = true;
                    _gameObject[i].SetActive(true);
                }
                else
                {
                    //playing = false;
                    result = false;
                    _gameObject[i].SetActive(false);
                }
            }

            return result;
        }

        public virtual bool playAnimatorByName(string name)
        {
            if (_animator != null && !String.IsNullOrEmpty(name))
            {
                _animator.gameObject.SetActive(true);
                _animator.Play(name, 0, 0);
            }
            else
            {
                _animator.gameObject.SetActive(false);
            }

            return true;
        }

        public virtual bool playParticleImmediately(bool isPlay)
        {
            bool result = true;
            for (int i = 0; i < _particle.Count; i++)
            {
                if (isPlay)
                {
                    //playing = true;
                    result = true;
                    _particle[i].gameObject.SetActive(true);
                    if (!_particle[i].isPlaying)
                    {
                        _particle[i].Play();
                    }
                }
                else
                {
                    //playing = false;
                    result = false;
                    _particle[i].gameObject.SetActive(false);
                    _particle[i].Stop();
                }
            }

            for (int i = 0; i < _gameObject.Count; i++)
            {
                if (isPlay)
                {
                    //playing = true;
                    result = true;
                    _gameObject[i].SetActive(true);
                }
                else
                {
                    //playing = false;
                    result = false;
                    _gameObject[i].SetActive(false);
                }
            }
            return result;
        }

        protected float particleSystemLength(ParticleSystem ps)
        {
            float dunration = 0;
            if (ps.emissionRate <= 0)
            {
                dunration = ps.startDelay + ps.startLifetime;
            }
            else
            {
                dunration = ps.startDelay + Mathf.Max(ps.duration, ps.startLifetime);
            }
            return dunration;
        }

        protected void restTransform(Transform trans)
        {
            trans.localPosition = Vector3.zero;
            trans.localEulerAngles = Vector3.zero;
            trans.localScale = Vector3.one;
        }

        public virtual void setPosAndForward(Vector3 pos, Vector3 forward)
        {
            for (int i = 0; i < _gameObject.Count; i++)
            {
                _gameObject[i].transform.position = pos;
                _gameObject[i].transform.forward = forward;
            }

            for (int i = 0; i < _particle.Count; i++)
            {
                _particle[i].transform.position = pos;
                _particle[i].transform.forward = forward;
            }
        }

        public virtual void checkRecycle()
        {
            for (int i = 0; i < _particle.Count; i++)
            {
                if (!_particle[i].IsAlive(true))
                {
                    _particle[i].gameObject.SetActive(false);
                }
            }
        }

        public virtual void playIsShow(bool isShow)
        {
            do
            {
                if (_targetObject == null) break;
                if (_targetObject.activeSelf != isShow) _targetObject.SetActive(isShow);

            } while (false);
        }

        public virtual void stopFilter()
        {

        }

    }
}
