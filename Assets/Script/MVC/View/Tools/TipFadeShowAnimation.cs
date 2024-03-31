

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 
    /// </summary>
	public class TipFadeShowAnimation : UIBaseAnimation
    {
        #region MyRegion
        /// <summary>
        /// 放大
        /// </summary>
        private const float FADE_TIME = 0.2f;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private List<CanvasGroup> canvasGroups;

        /// <summary>
        /// 动画节点
        /// </summary>
        [SerializeField]
        private RectTransform animationRectTransform;

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
            for (int i = 0; i < canvasGroups.Count; i++)
            {
                canvasGroups[i].alpha = 0;
            }
            if (animationRectTransform != null)
            {
                animationRectTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
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
            if (animationRectTransform != null)
            {
                animationRectTransform.DOScale(1, FADE_TIME);
            }
            if (canvasGroups != null)
            {
                yield return CanvasGroupAnimation(canvasGroups, FADE_TIME, 1);
            }
            if (finishAnimation != null)
            {
                finishAnimation();
            }
        }
    }
}