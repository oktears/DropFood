
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 界面展示基类
    /// </summary>
	public abstract class UIBaseAnimation : MonoBehaviour
    {
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPause { get; protected set; }

        /// <summary>
        /// 动画
        /// </summary>
        public List<Tweener> tweeners { get; protected set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// 开始播放都规划
        /// </summary>
        /// <param name="finishAnimation">完成动画回调</param>
        public virtual void PlayAnimation(Action finishAnimation)
        {
            if (finishAnimation != null)
            {
                finishAnimation();
            }
        }

        /// <summary>
        /// 延迟等待
        /// </summary>
        /// <param name="waitTime">等待时间</param>
        /// <param name="finish">完成回调</param>
        /// <returns></returns>
        protected IEnumerator WaitTime(float waitTime, Action finish = null)
        {
            float timer = 0;
            if (waitTime > 0)
            {
                do
                {
                    timer += GetDeltaTime();
                    yield return null;
                } while (timer <= waitTime);
            }
            if (finish != null)
            {
                finish();
            }
        }

        /// <summary>
        /// 开始动画
        /// </summary>
        /// <param name="finish">完成回调</param>
        /// <returns></returns>
        protected IEnumerator TransformScaleAnimation(Transform tempTransform, float time, float endSize, Action finish = null)
        {
            float timer = 0;
            Vector3 ensSize = new Vector3(endSize, endSize, endSize);
            Vector3 startSize = tempTransform.localScale;
            if (time > 0)
            {
                do
                {
                    timer += GetDeltaTime();

                    tempTransform.localScale = Vector3.Lerp(startSize, ensSize, timer / time);
                    yield return null;
                } while (timer < time);
            }
            tempTransform.localScale = ensSize;
            if (finish != null)
            {
                finish();
            }
        }


        /// <summary>
        /// 开始动画
        /// </summary>
        /// <param name="finish">完成回调</param>
        /// <returns></returns>
        protected IEnumerator TransformPositionAnimation(RectTransform tempTransform, float time, Vector2 endPosition, Action finish = null)
        {
            float timer = 0;
            Vector2 startPosition = tempTransform.anchoredPosition;
            if (time > 0)
            {
                do
                {
                    timer += GetDeltaTime();
                    tempTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, timer / time);
                    yield return null;
                } while (timer < time);
            }
            tempTransform.anchoredPosition = endPosition;
            if (finish != null)
            {
                finish();
            }
        }

        /// <summary>
        /// canvas动画
        /// </summary>
        /// <param name="finish">完成回调</param>
        /// <returns></returns>
        protected IEnumerator CanvasGroupAnimation(List<CanvasGroup> canvasGroup, float time, float endSize, Action finish = null)
        {
            float timer = 0;
            float startSize = 0;
            for (int i = 0; i < canvasGroup.Count; i++)
            {
                if (canvasGroup[i] != null)
                {
                    startSize = canvasGroup[i].alpha;
                    break;
                }
            }
            if (time > 0)
            {
                do
                {
                    timer += GetDeltaTime();

                    SetCanvasGroupAlpha(canvasGroup, Mathf.Lerp(startSize, endSize, timer / time));
                    yield return null;
                } while (timer < time);
            }
            SetCanvasGroupAlpha(canvasGroup, endSize);
            if (finish != null)
            {
                finish();
            }
        }

        /// <summary>
        /// 设置透明度
        /// </summary>
        /// <param name="alpha"></param>
        protected void SetCanvasGroupAlpha(List<CanvasGroup> canvasGroups, float alpha)
        {
            for (int i = 0; i < canvasGroups.Count; i++)
            {
                if (canvasGroups[i] != null)
                {
                    canvasGroups[i].alpha = alpha;
                }
            }
        }

        /// <summary>
        /// 获取时间增量
        /// </summary>
        /// <returns></returns>
        protected float GetDeltaTime()
        {
            if (IsPause)
            {
                return 0;
            }
            return Time.deltaTime;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public virtual void Pause(bool pause)
        {
            IsPause = pause;
            if (pause)
            {
                if (tweeners != null)
                {
                    for (int i = 0; i < tweeners.Count; i++)
                    {
                        if (tweeners[i] != null)
                        {
                            tweeners[i].Pause();
                        }
                    }
                }
            }
            else
            {
                if (tweeners != null)
                {
                    for (int i = 0; i < tweeners.Count; i++)
                    {
                        if (tweeners[i] != null)
                        {
                            tweeners[i].Play();
                        }
                    }
                }
            }
        }

        public virtual void Stop()
        {

        }
    }
}