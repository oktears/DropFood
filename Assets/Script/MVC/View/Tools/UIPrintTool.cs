using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 
 * ================================================================================
 * 类摘要:UI的输出类
 * 
 * 
 * ================================================================================
 */
namespace Chengzi
{
    public static class UIPrintTool
    {


        /// <summary>
        /// 用于显示UI的相关函数从左侧填满
        /// </summary>
        /// <param name="num">需要输出的数字</param>
        /// <param name="Objs">输出的对象（从个位开始）</param>
        /// <param name="spriteNames">需要的sprite(0-9的排序)</param>
        public static void PrintFromLeft(int num, List<Image> UISprites, List<Sprite> spriteNames)
        {
            Color fullColor = new Color(1, 1, 1, 1);
            Color noColor = new Color(1, 1, 1, 0);
            int length = 1;
            while (true)
            {
                if (num < UICommonTool.Digit(length + 1))
                    break;
                length++;
            }

            if (num < 10)
            {
                for (int i = 0; i < UISprites.Count; i++)
                {
                    if (i == UISprites.Count - 1)
                    {
                        UISprites[i].sprite = spriteNames[num / 1];
                        if (UISprites[i].enabled == false)
                        {
                            UISprites[i].enabled = true;
                        }
                    }
                    else
                    {
                        if (UISprites[i].enabled == true)
                        {
                            UISprites[i].enabled = false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < UISprites.Count; i++)
                {
                    if (i < length)
                    {
                        UISprites[i + (UISprites.Count - length)].sprite = spriteNames[(num % UICommonTool.Digit(i + 2) - num % UICommonTool.Digit(i + 1)) / UICommonTool.Digit(i + 1)];
                        if (UISprites[i + (UISprites.Count - length)].enabled == false)
                        {
                            UISprites[i + (UISprites.Count - length)].enabled = true;
                        }
                    }
                    else
                    {
                        if (UISprites[i - length].enabled == true)
                        {
                            UISprites[i - length].enabled = false;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 用于显示UI的相关函数从右侧填满
        /// </summary>
        /// <param name="num">需要输出的数字</param>
        /// <param name="Objs">输出的对象（从个位开始）</param>
        /// <param name="spriteNames">需要的sprite(0-9的排序)</param>
        public static void PrintFromRight(int num, List<Image> UISprites, List<Sprite> spriteNames)
        {
            Color fullColor = new Color(1, 1, 1, 1);
            Color noColor = new Color(1, 1, 1, 0);
            int length = 1;
            while (true)
            {
                if (num < UICommonTool.Digit(length + 1))
                    break;
                length++;
            }

            if (num < 10)
            {
                for (int i = 0; i < UISprites.Count; i++)
                {
                    if (i == 0)
                    {
                        UISprites[i].sprite = spriteNames[num / 1];
                        if (UISprites[i].enabled == false)
                        {
                            UISprites[i].enabled = true;
                        }
                    }
                    else
                    {
                        if (UISprites[i].enabled == true)
                        {
                            UISprites[i].enabled = false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < UISprites.Count; i++)
                {
                    if (i < length)
                    {
                        Sprite current = spriteNames[(num % UICommonTool.Digit(i + 2) - num % UICommonTool.Digit(i + 1)) / UICommonTool.Digit(i + 1)];
                        UISprites[i].sprite = spriteNames[(num % UICommonTool.Digit(i + 2) - num % UICommonTool.Digit(i + 1)) / UICommonTool.Digit(i + 1)];
                        if (UISprites[i].enabled == false)
                        {
                            UISprites[i].enabled = true;
                        }

                    }
                    else
                    {
                        if (UISprites[i].enabled == true)
                        {
                            UISprites[i].enabled = false;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 输入六个Image
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeObj"></param>
        /// <param name="timeSprites"></param>
        public static void PrintfTime(int time, List<Image> timeObj, List<Sprite> timeSprites)
        {
            int length = timeObj.Count;
            if (time >= 10)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i == 5)
                        timeObj[i].sprite = timeSprites[time / 60000];
                    else if (i == 4)
                        timeObj[i].sprite = timeSprites[(time / 6000) % 10];
                    else if (i == 3)
                        timeObj[i].sprite = timeSprites[(time % 6000) / 1000];
                    else
                        timeObj[i].sprite = timeSprites[(time % UICommonTool.Digit(i + 2) - time % UICommonTool.Digit(i + 1)) / UICommonTool.Digit(i + 1)];
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    timeObj[i].sprite = timeSprites[0];
                    if (i == 0)
                        timeObj[i].sprite = timeSprites[time / 1];
                }
            }
        }

        /// <summary>
        /// 设置时间相关
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeObj"></param>
        /// <param name="timeSprites"></param>
        /// <param name="timeMinute"></param>
        public static void SetTimeDiff(int time, List<Image> images, List<Sprite> timeSprites, List<Sprite> timeMinute)
        {

            string numStrings = time.ToString();
            char[] numCharAyyay = numStrings.ToCharArray();
            int length = numCharAyyay.Length;
            int[] numIntArray = UICommonTool.ReverseNumber(ref numCharAyyay);

            int[] newChar = new int[6];

            for (int j = 0; j < length; j++)
            {
                if (j < numIntArray.Length)
                {
                    newChar[j] = numIntArray[j] - 48;
                }
                else
                {
                    newChar[j] = 0;
                }
            }
            int total = newChar[3] + newChar[4] * 10 + newChar[5] * 100;
            newChar[3] = total % 6;
            int hours = total / 6;
            char[] hourChar = hours.ToString().ToCharArray();
            if (hourChar.Length == 1)
            {
                newChar[4] = (int)hourChar[0] - 48;
                newChar[5] = 0;
            }
            else
            {
                newChar[5] = (int)hourChar[0] - 48;
                newChar[4] = (int)hourChar[1] - 48;
            }

            for (int k = 0; k < images.Count; k++)
            {
                if (k == 5 && k == 4)
                {
                    images[k].sprite = timeMinute[newChar[k]];
                }
                else
                {
                    images[k].sprite = timeSprites[newChar[k]];
                }
            }
        }

        /// <summary>
        /// 输入七个Image
        /// </summary>
        /// <param name="time"></param>
        /// <param name="timeObj"></param>
        /// <param name="timeSprites"></param>
        public static void PrintfSevenTime(int time, List<Image> timeObj, List<Sprite> timeSprites)
        {
            int length = timeObj.Count;
            if (time >= 10)
            {
                for (int i = 0; i < length; i++)
                {
                    if (i == 6)
                        timeObj[i].sprite = timeSprites[time / 60000];
                    else if (i == 5)
                        timeObj[i].sprite = timeSprites[(time / 6000) % 10];
                    else if (i == 4)
                        timeObj[i].sprite = timeSprites[(time % 6000) / 1000];
                    else
                        timeObj[i].sprite = timeSprites[(time % UICommonTool.Digit(i + 2) - time % UICommonTool.Digit(i + 1)) / UICommonTool.Digit(i + 1)];
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    timeObj[i].sprite = timeSprites[0];
                    if (i == 0)
                        timeObj[i].sprite = timeSprites[time / 1];
                }
            }
        }
    }
}
