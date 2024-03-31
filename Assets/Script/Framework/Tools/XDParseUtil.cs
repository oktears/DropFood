using UnityEngine;
using System;
using System.Collections.Generic;


namespace Chengzi
{

    /// <summary>
    /// XD解析工具类
    /// </summary>
    public class XDParseUtil
    {

        /// <summary>
        /// 解析颜色字符串
        /// </summary>
        /// <param name="content">颜色字符串</param>
        /// <returns>Color对象</returns>
        public static Color parseColor(string content)
        {
            string[] contentArray = content.Split(',');
            Color color = new Color(int.Parse(contentArray[0]) / 255.0f, int.Parse(contentArray[1]) / 255.0f, int.Parse(contentArray[2]) / 255.0f, int.Parse(contentArray[3]) / 255.0f);
            return color;
        }

        /// <summary>
        /// 解析Vector2字符串
        /// </summary>
        /// <param name="content">Vector2字符串</param>
        /// <returns>Vector2对象</returns>
        public static Vector2 parseVector2(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                Vector2 vec = new Vector2(float.Parse(contentArray[0]), float.Parse(contentArray[1]));
                return vec;
            }
            else
            {
                return new Vector2();
            }
        }

        /// <summary>
        /// 解析Vector3字符串
        /// </summary>
        /// <param name="content">Vector3字符串</param>
        /// <returns>Vector3对象</returns>
        public static Vector3 parseVector3(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                Vector3 vec = new Vector3(float.Parse(contentArray[0]), float.Parse(contentArray[1]), float.Parse(contentArray[2]));
                return vec;
            }
            else
            {
                return new Vector3();
            }
        }

        /// <summary>
        /// 解析Vector4字符串
        /// </summary>
        /// <param name="content">Vector4字符串</param>
        /// <returns>Vector4对象</returns>
        public static Vector4 parseVector4(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                Vector4 vec = new Vector4(float.Parse(contentArray[0]), float.Parse(contentArray[1]), float.Parse(contentArray[2]), float.Parse(contentArray[3]));
                return vec;
            }
            else
            {
                return new Vector4();
            }
        }

        /// <summary>
        /// 解析Quaternion字符串
        /// </summary>
        /// <param name="content">Quaternion字符串</param>
        /// <returns>Quaternion对象</returns>
        public static Quaternion parseQuaternion(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                Quaternion quaternion = new Quaternion(float.Parse(contentArray[0]), float.Parse(contentArray[1]), float.Parse(contentArray[2]), float.Parse(contentArray[3]));
                return quaternion;
            }
            else
            {
                return new Quaternion();
            }
        }

        /// <summary>
        /// 解析Rect字符串
        /// </summary>
        /// <param name="content">Rect字符串</param>
        /// <returns>Rect对象</returns>
        public static Rect parseRect(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                Rect rect = new Rect(float.Parse(contentArray[0]), float.Parse(contentArray[1]), float.Parse(contentArray[2]), float.Parse(contentArray[3]));
                return rect;
            }
            else
            {
                return new Rect();
            }
        }

        /// <summary>
        /// 解析Bounds字符串
        /// </summary>
        /// <param name="content">Bounds字符串</param>
        /// <returns>Bounds对象</returns>
        public static Bounds parseBounds(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                Vector3 center = new Vector3(float.Parse(contentArray[0]), float.Parse(contentArray[1]), float.Parse(contentArray[2]));
                Vector3 size = new Vector3(float.Parse(contentArray[3]), float.Parse(contentArray[4]), float.Parse(contentArray[5]));
                Bounds boundsValue = new Bounds(center, size);
                return boundsValue;
            }
            else
            {
                return new Bounds();
            }
        }

        /// <summary>
        /// 解析AnimationCurve字符串
        /// </summary>
        /// <param name="content">AnimationCurve字符串</param>
        /// <returns>AnimationCurve对象</returns>
        public static AnimationCurve parseAnimationCurve(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(';');
                Keyframe[] keys = new Keyframe[contentArray.Length];

                for (int i = 0; i < contentArray.Length; i++)
                {
                    string keyFrameString = contentArray[i];
                    string[] keyFrameAttrs = keyFrameString.Split(',');
                    Keyframe keyFrame = new Keyframe(float.Parse(keyFrameAttrs[0]), float.Parse(keyFrameAttrs[1]), float.Parse(keyFrameAttrs[2]), float.Parse(keyFrameAttrs[3]));
                    keyFrame.tangentMode = int.Parse(keyFrameAttrs[4]);
                    keys[i] = keyFrame;
                }

                AnimationCurve curve = new AnimationCurve(keys);
                return curve;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析泛型数组
        /// </summary>
        /// <param name="content">泛型数组字符串</param>
        /// <returns>泛型数组对象</returns>
        public static T[] parseArray<T>(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                T[] enumArray = Array.ConvertAll<string, T>(contentArray, s => GenericityUtil.convertType<T>(s));
                return enumArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析泛型数组
        /// </summary>
        /// <param name="content">泛型数组字符串</param>
        /// <returns>泛型数组对象</returns>
        public static T[] parseArray<T>(string content, char separator)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(separator);
                T[] enumArray = Array.ConvertAll<string, T>(contentArray, s => GenericityUtil.convertType<T>(s));
                return enumArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析String数组
        /// </summary>
        /// <param name="content">String数组字符串</param>
        /// <returns>String数组对象</returns>
        public static string[] parseStringArray(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                return contentArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析String数组
        /// </summary>
        /// <param name="content">String数组字符串</param>
        /// <returns>String数组对象</returns>
        public static string[] parseStringArray(string content, char separator)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(separator);
                return contentArray;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解析Int数组
        /// </summary>
        /// <param name="content">Int数组字符串</param>
        /// <returns>Int数组对象</returns>
        public static int[] parseIntArray(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                int[] intArray = Array.ConvertAll<string, int>(contentArray, s => int.Parse(s));
                return intArray;
            }

            return null;
        }

        /// <summary>
        /// 解析Int数组
        /// </summary>
        /// <param name="content">Int数组字符串</param>
        /// <returns>Int数组对象</returns>
        public static int[] parseIntArray(string content, char separator)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(separator);
                int[] intArray = Array.ConvertAll<string, int>(contentArray, s => int.Parse(s));
                return intArray;
            }

            return null;
        }

        /// <summary>
        /// 解析Byte数组
        /// </summary>
        /// <param name="content">Byte数组字符串</param>
        /// <returns>Byte数组对象</returns>
        public static byte[] parseByteArray(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                byte[] byteArray = Array.ConvertAll<string, byte>(contentArray, s => byte.Parse(s));
                return byteArray;
            }

            return null;
        }

        /// <summary>
        /// 解析Byte数组
        /// </summary>
        /// <param name="content">Byte数组字符串</param>
        /// <returns>Int数组对象</returns>
        public static byte[] parseByteArray(string content, char separator)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(separator);
                byte[] intArray = Array.ConvertAll<string, byte>(contentArray, s => byte.Parse(s));
                return intArray;
            }

            return null;
        }

        /// <summary>
        /// 解析Short数组
        /// </summary>
        /// <param name="content">Short数组字符串</param>
        /// <returns>Short数组对象</returns>
        public static short[] parseShortArray(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                short[] shortArray = Array.ConvertAll<string, short>(contentArray, s => short.Parse(s));
                return shortArray;
            }
            return null;
        }

        /// <summary>
        /// 解析Short数组
        /// </summary>
        /// <param name="content">Short数组字符串</param>
        /// <returns>Short数组对象</returns>
        public static short[] parseShortArray(string content, char separator)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(separator);
                short[] shortArray = Array.ConvertAll<string, short>(contentArray, s => short.Parse(s));
                return shortArray;
            }
            return null;
        }

        /// <summary>
        /// 解析Float数组
        /// </summary>
        /// <param name="content">Float数组字符串</param>
        /// <returns>Float数组对象</returns>
        public static float[] parseFloatArray(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                string[] contentArray = content.Split(',');
                float[] floatArray = Array.ConvertAll<string, float>(contentArray, s => float.Parse(s));
                return floatArray;
            }
            return null;
        }

        /// <summary>
        /// 解析ShortList
        /// </summary>
        /// <param name="content">ShortList字符串</param>
        /// <returns>ShortList对象</returns>
        public static List<short> parseShortList(string content)
        {
            List<short> list;
            if (parseShortArray(content) != null)
            {
                list = new List<short>(parseShortArray(content));
            }
            else
            {
                list = new List<short>();
            }

            return list;
        }

        /// <summary>
        /// 解析ShortList
        /// </summary>
        /// <param name="content">ShortList字符串</param>
        /// <returns>ShortList对象</returns>
        public static List<short> parseShortList(string content, char separator)
        {
            List<short> list = new List<short>(parseShortArray(content, separator));
            return list;
        }

        /// <summary>
        /// 解析IntList
        /// </summary>
        /// <param name="content">IntList字符串</param>
        /// <returns>IntList对象</returns>
        public static List<int> parseIntList(string content)
        {
            List<int> list = new List<int>(parseIntArray(content));
            return list;
        }

        /// <summary>
        /// 解析ByteList
        /// </summary>
        /// <param name="content">ByteList</param>
        /// <returns>ByteList对象</returns>
        public static List<byte> parseByteList(string content)
        {
            byte[] byteArr = parseByteArray(content);
            List<byte> list;
            if (byteArr != null)
            {
                list = new List<byte>(byteArr);
            }
            else
            {
                list = new List<byte>();
            }
            return list;
        }

        /// <summary>
        /// 解析FloatList
        /// </summary>
        /// <param name="content">FloatList字符串</param>
        /// <returns>FloatList对象</returns>
        public static List<float> parseFloatList(string content)
        {
            List<float> list = new List<float>(parseFloatArray(content));
            return list;
        }

        /// <summary>
        /// 解析StringList    
        /// </summary>
        /// <param name="content">StringList字符串</param>
        /// <returns>StringList对象</returns>
        public static List<string> parseStringList(string content)
        {
            List<string> list = new List<string>(parseStringArray(content));
            return list;
        }

        /// <summary>
        /// 解析泛型字典容器
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="content"></param>  
        /// <returns></returns>
        public static Dictionary<K, V> parseDictionary<K, V>(string content)
            where K : new()
            where V : new()
        {
            Dictionary<K, V> map = new Dictionary<K, V>();
            if (!content.Equals(string.Empty))
            {
                string[] strArr = parseStringArray(content, ';');
                for (int i = 0; i < strArr.Length; i++)
                {
                    string str = strArr[i];
                    string[] arr = str.Split(',');
                    K key = GenericityUtil.convertType<K>(arr[0]);
                    V value = GenericityUtil.convertType<V>(arr[1]);
                    map.Add(key, value);
                }
            }

            return map;
        }

        /// <summary>
        /// 解析泛型字典容器[Value为string类型]
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="content"></param>  
        /// <returns></returns>
        public static Dictionary<K, string> parseSVDictionary<K>(string content)
            where K : new()
        {
            Dictionary<K, string> map = new Dictionary<K, string>();
            string[] strArr = parseStringArray(content, ';');
            for (int i = 0; i < strArr.Length; i++)
            {
                string str = strArr[i];
                string[] arr = str.Split(',');
                K key = GenericityUtil.convertType<K>(arr[0]);
                string value = "";
                if (arr.Length >= 2)
                {
                    value = arr[1];
                }

                map.Add(key, value);
            }

            return map;
        }


        /// <summary>
        /// 打印AnimationCurve的格式化为存储为xd的字符串
        /// </summary>
        /// <param name="curve"></param>
        public static void printAnimationCurveInfo(AnimationCurve curve)
        {
            string output = "";
            for (int i = 0; i < curve.keys.Length; i++)
            {
                Keyframe keyFrame = curve.keys[i];
                output += keyFrame.time;
                output += ",";
                output += keyFrame.value;
                output += ",";
                output += keyFrame.inTangent;
                output += ",";
                output += keyFrame.outTangent;
                output += ",";
                output += keyFrame.tangentMode;
                output += ";";
            }
            //Debug.Log(output);
        }
    }
}