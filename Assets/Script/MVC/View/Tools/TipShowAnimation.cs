
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 
    /// </summary>
	public class TipShowAnimation : UIBaseAnimation
    {
        #region MyRegion
        /// <summary>
        /// 放大
        /// </summary>
        private const float BIG_TIME = 0.2f;

        /// <summary>
        /// 缩小
        /// </summary>
        private const float SMALL_TIME = 0.2f;

        /// <summary>
        /// 放到的最大尺寸
        /// </summary>
        private const float MAX_SIZE = 1.05f;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private List<CanvasGroup> canvasGroups = new List<CanvasGroup>();

        /// <summary>
        /// 节点
        /// </summary>
        [SerializeField]
        private RectTransform rectTransform;

        /// <summary>
        /// 播放动画时 
        /// </summary>
        [SerializeField] private bool NeedScaleAnimation = true;

        /// <summary>
        /// 协程
        /// </summary>
        private Coroutine coroutine;

        /// <summary>
        /// 初始化
        /// </summary>
        public override void Init()
        {
            base.Init();
            rectTransform.localScale = Vector3.zero;
        }

        /// <summary>
        /// 开始动画
        /// </summary>
        /// <param name="finishAnimation"></param>
        public override void PlayAnimation(Action finishAnimation)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            coroutine = StartCoroutine(PlayShowAnimation(finishAnimation));
        }

        /// <summary>
        /// 开始动画
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayShowAnimation(Action finishAnimation)
        {
            rectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (canvasGroups != null)
            {
                for (int i = 0; i < canvasGroups.Count; i++)
                {
                    canvasGroups[i].alpha = 0;
                    canvasGroups[i].DOFade(1, BIG_TIME + SMALL_TIME);
                }
            }

            if (NeedScaleAnimation)
            {
                if (rectTransform != null)
                {
                    yield return TransformScaleAnimation(rectTransform, BIG_TIME, MAX_SIZE);
                    yield return TransformScaleAnimation(rectTransform, SMALL_TIME, 1f);
                }
            }
            else
            {
                rectTransform.localScale = Vector3.one;
                yield return new WaitForSeconds(BIG_TIME + SMALL_TIME);
            }

            if (finishAnimation != null)
            {
                finishAnimation();
            }
        }
    }
}