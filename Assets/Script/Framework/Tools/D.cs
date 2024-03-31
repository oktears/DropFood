using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

namespace Chengzi
{
    /// <summary>Debug输出类</summary>
    public class D
    {
        public static void i(object log)
        {
            Debug.Log(log);
        }

         
        public static void e(object err)
        {
            Debug.LogError(err);
        }

        public static void c(object err)
        {
#if UNITY_EDITOR
            Debug.Log("<color=#7CFC00>" + err + "</color>");
#endif
        }

        public static void c(string color, object err)
        {
#if UNITY_EDITOR
            Debug.Log("<color=#" + color + ">" + err + "</color>");
#endif
        }

        public static int genRandomColor(long seed)
        {
            System.Random ran = new System.Random((int)(seed & 0xffffffffL) | (int)(seed >> 32));
            int R = ran.Next(255);
            int G = ran.Next(255);
            int B = ran.Next(255);
            B = (R + G > 400) ? R + G - 400 : B;
            B = (B > 255) ? 255 : B;
            return toHex(R, G, B);
        }

        public static int toHex(int r, int g, int b)
        {
            return r << 16 | g << 8 | b;
        }
    }

    public static class EunmAtrribute
    {
        public static string GetDescribe(this Enum em)
        {
            Type type = em.GetType();
            FieldInfo fd = type.GetField(em.ToString());
            if (fd == null)
                return string.Empty;
            object[] attrs = fd.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);
            string name = string.Empty;
            foreach (EnumDescriptionAttribute attr in attrs)
            {
                name = attr.Description;
            }
            return name;
        }
    }
}
