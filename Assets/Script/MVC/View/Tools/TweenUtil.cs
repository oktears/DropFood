
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Chengzi
{
    public class TweenUtil
    {

        public static void fade(GameObject ob, float value, float during)
        {
            fade(ob, value, during, 0);
        }

        public static void fade(GameObject ob, float value, float during, float delay)
        {
            Transform[] allChildren = ob.transform.GetComponentsInChildren<Transform>();
            if (allChildren != null && allChildren.Length > 0)
            {
                for (int i = 0; i < allChildren.Length; i++)
                {
                    GameObject obj = allChildren[i].gameObject;
                    Image img = obj.GetComponent<Image>();
                    RawImage rawImg = obj.GetComponent<RawImage>();
                    Text text = obj.GetComponent<Text>();
                    if (img != null)
                        img.DOFade(value, during).SetDelay(delay).Play();
                    if (rawImg != null)
                        rawImg.DOFade(value, during).SetDelay(delay).Play();
                    if (text != null)
                        text.DOFade(value, during).SetDelay(delay).Play();
                }
            }
        }

        public static void localMove(GameObject ob, Vector3 value, float during)
        {
            localMove(ob, value, during, 0);
        }

        public static void localMove(GameObject ob, Vector3 value, float during, float delay)
        {
            Transform[] allChildren = ob.transform.GetComponentsInChildren<Transform>();
            if (allChildren != null && allChildren.Length > 0)
            {
                for (int i = 0; i < allChildren.Length; i++)
                {
                    GameObject obj = allChildren[i].gameObject;
                    RectTransform trans = obj.GetComponent<RectTransform>();
                    if (trans != null)
                        trans.DOLocalMove(value, during).SetDelay(delay).Play();
                }
            }
        }

    }

}
