
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 通用tip动画
    /// </summary>
	public class TipHideAnimation : UIBaseAnimation
    {
        #region MyRegion
        /// <summary>
        /// 放大
        /// </summary>
        private const float BIG_TIME = 0.26f;

        /// <summary>
        /// 缩小
        /// </summary>
        private const float SMALL_TIME = 0.16f;

        /// <summary>
        /// 放到的最大尺寸
        /// </summary>
        private const float MAX_SIZE = 1.05f;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [SerializeField]
        private List<CanvasGroup> canvasGroups;

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
            if (canvasGroups != null)
            {
                for (int i = 0; i < canvasGroups.Count; i++)
                {
                    canvasGroups[i].DOFade(0, BIG_TIME + SMALL_TIME);
                }
                if (rectTransform == null)
                {
                    yield return new WaitForSeconds(BIG_TIME + SMALL_TIME);
                }
            }
            if (NeedScaleAnimation)
            {
                if (rectTransform != null)
                {
                    yield return TransformScaleAnimation(rectTransform, SMALL_TIME, MAX_SIZE);
                    yield return TransformScaleAnimation(rectTransform, BIG_TIME, 0f);
                }
            }
            else
            {
                yield return new WaitForSeconds(BIG_TIME + SMALL_TIME);
            }

            if (finishAnimation != null)
            {
                finishAnimation();
            }
        }

    }
}