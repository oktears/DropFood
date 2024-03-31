using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Text;
using System;
using System.Security.Cryptography;

namespace Chengzi
{

    /// <summary>
    /// 字符串工具类
    /// </summary>
    public class StringUtil
    {

        /// <summary>
        /// 提取字符串中的包含的所有数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int getIntFromString(string str)
        {
            int ret = -1;
            int index = 0;
            int intIndex = -1;
            int charIndex = 0;
            int charStartIndex = -1;
            bool isInIntString = false;

            while (true)
            {
                if (charIndex >= str.Length)
                {
                    if (isInIntString)
                    {
                        intIndex++;
                        if (intIndex == index)
                        {
                            ret = int.Parse(str.Substring(charStartIndex, charIndex - charStartIndex));
                        }
                    }

                    break;
                }

                if (!isInIntString)
                {
                    if (char.IsDigit(str[charIndex]))
                    {
                        isInIntString = true;
                        charStartIndex = charIndex;
                    }
                    charIndex++;
                }
                else
                {
                    if (char.IsDigit(str[charIndex]))
                    {
                        charIndex++;
                    }
                    else
                    {
                        intIndex++;
                        if (intIndex == index)
                        {
                            ret = int.Parse(str.Substring(charStartIndex, charIndex - charStartIndex));
                            break;
                        }
                        else
                        {
                            isInIntString = false;
                            charStartIndex = -1;
                        }
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// 在指定字符串中移除子串
        /// </summary>
        /// <param name="content"></param>
        /// <param name="subString"></param>
        public static string removeSubString(string content, string subString)
        {
            if (content.Contains(subString))
            {
                content = content.Replace(subString, string.Empty);
            }
            return content;
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isNumber(string str)
        {
            Regex regex = new Regex(@"^(-)?\d+(\.\d+)?$");
            if (regex.IsMatch(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 字符串转UTF-8编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string convert2UTF(string str)
        {
            byte[] bs = Encoding.GetEncoding("GB2312").GetBytes(str);
            return System.Text.Encoding.Default.GetString(bs);
        }

        /// <summary>
        /// 生成16位随机字符串
        /// </summary>
        /// <returns></returns>
        public static string genUniqueString16bit()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        /// <summary>
        /// 获取md5加密串
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string md5Encrypt(string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(strText));
            return System.Text.Encoding.Default.GetString(result);
        }

        public static bool Contains(string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
