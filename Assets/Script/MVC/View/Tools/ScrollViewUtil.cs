using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{
    public class ScrollViewUtil
    {

        static Vector3 GetWidgetWorldPoint(RectTransform target)
        {
            //pivot position + item size has to be included
            var pivotOffset = new Vector3(
                (0.5f - target.pivot.x) * target.rect.size.x,
                (0.5f - target.pivot.y) * target.rect.size.y,
                0f);
            var localPosition = target.localPosition + pivotOffset;
            return target.parent.TransformPoint(localPosition);
        }

        static Vector3 GetWorldPointInWidget(RectTransform target, Vector3 worldPoint)
        { 
            return target.InverseTransformPoint(worldPoint);
        }

        /// <summary>
        /// 指定一个 item让其定位到ScrollRect中间
        /// </summary>
        /// <param name="target">需要定位到的目标</param>
        public static void CenterOnItem(RectTransform target, ScrollRect scrollRect, RectTransform content, RectTransform viewport)
        {
            // Item is here
            var itemCenterPositionInScroll = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPoint(target));
            //Debug.Log("Item Anchor Pos In Scroll: " + itemCenterPositionInScroll);
            // But must be here
            var targetPositionInScroll = GetWorldPointInWidget(scrollRect.GetComponent<RectTransform>(), GetWidgetWorldPoint(viewport));
            //Debug.Log("Target Anchor Pos In Scroll: " + targetPositionInScroll);
            // So it has to move this distance
            var difference = targetPositionInScroll - itemCenterPositionInScroll;
            difference.z = 0f;

            var newNormalizedPosition = new Vector2(difference.x / (content.rect.width - viewport.rect.width),
                difference.y / (content.rect.height - viewport.rect.height));

            newNormalizedPosition = scrollRect.normalizedPosition - newNormalizedPosition;

            newNormalizedPosition.x = Mathf.Clamp01(newNormalizedPosition.x);
            newNormalizedPosition.y = Mathf.Clamp01(newNormalizedPosition.y);

            scrollRect.normalizedPosition = newNormalizedPosition;
            //DOTween.To(() => scrollRect.normalizedPosition, x => scrollRect.normalizedPosition = x, newNormalizedPosition, 3);
        }

    }
}
