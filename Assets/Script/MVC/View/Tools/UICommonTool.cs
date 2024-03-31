using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * ================================================================================
 * 类摘要:UI的通用工具类
 * 
 * 
 * ================================================================================
 */
namespace Chengzi
{
    public static class UICommonTool
    {
        /// <summary>
        /// get int length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int Digit(int length)
        {
            int value = 1;
            for (int i = 1; i < length; i++)
            {
                value *= 10;
            }
            return value;
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="num">需要反转的字符串返回一组int[]</param>
        /// <returns></returns>
        public static int[] ReverseNumber(ref char[] num)
        {
            int length = num.Length;
            int[] newChar = new int[length];
            for (int i = 0; i < length; i++)
            {
                newChar[i] = (int)num[length - 1 - i];
            }
            return newChar;
        }

        /// <summary>
        /// 归零
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="father">父物体</param>
        public static void ResetFather(this Transform transform, Transform father)
        {
            transform.parent = father;
            transform.ResetZero();
        }

        /// <summary>
        /// 归零
        /// </summary>
        /// <param name="transform"></param>
        public static void ResetZero(this Transform transform)
        {
            transform.localPosition = new Vector3(0, 0, 0);
            transform.localRotation = Quaternion.identity;
            transform.localScale = new Vector3(1, 1, 1);
        }

        public static List<Image> GetSpriteList(this GameObject gameObject, int length)
        {
            return gameObject.transform.GetSpriteList(length);
        }

        public static List<Image> GetSpriteList(this Transform transform, int length)
        {
            List<Image> spriteList = new List<Image>();
            string path = "";
            for (int i = 0; i < length; i++)
            {
                path += "0";
                Image sprite = transform.Find(path).GetComponent<Image>();
                spriteList.Add(sprite);
            }
            return spriteList;
        }

        public static string GetHeadName(long uid)
        {
            if (uid < 0)
                uid = -uid;
            long index = uid % 8;
            return "base_head" + (int)index;
        }
    }
}
