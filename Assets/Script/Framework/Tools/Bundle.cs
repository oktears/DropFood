using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 绑定数据集合
    /// </summary>
    public class Bundle
    {
        /// <summary>
        /// 保存数据map
        /// </summary>
        public Dictionary<string, object> DataMap { get; private set; }

        public const string defaultKey = "defaultKey";

        public Bundle()
        {
            this.Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            this.DataMap = new Dictionary<string, object>();
        }

        // Collection Interface

        /// <summary>
        /// 集合大小
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return DataMap.Count;
        }

        /// <summary>
        /// 是否为空
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this.Size() == 0;
        }

        /// <summary>
        /// 清空集合
        /// </summary>
        public void Clear()
        {
            this.DataMap.Clear();
        }

        /// <summary>
        /// 是否包含key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(string key)
        {
            return DataMap.ContainsKey(key);
        }

        /// <summary>
        /// 移除指定key的元素
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            this.DataMap.Remove(key);
        }

        /// <summary>
        /// 合并Bundle数据
        /// </summary>
        /// <param name="bundle"></param>
        public void PushAll(Bundle bundle)
        {
            this.DataMap.Union(bundle.DataMap);
        }

        //Put Any Type Value

        public void PutBoolean(string key, bool value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutByte(string key, byte value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutChar(string key, char value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutShort(string key, short value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutInt(string key, int value)
        {
            this.DataMap.Add(key, value);
        }


        public void PutUint(string key, uint value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutLong(string key, long value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutFloat(string key, float value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutDouble(string key, double value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutString(string key, string value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutBooleanArray(string key, bool[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutByteArray(string key, byte[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutShortArray(string key, short[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutCharArray(string key, char[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutIntArray(string key, int[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutLongArray(string key, long[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutFloatArray(string key, float[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutDoubleArray(string key, double[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutStringArray(string key, string[] value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutBundle(string key, Bundle value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutObject(string key, object value)
        {
            this.DataMap.Add(key, value);
        }

        public void PutObject(object o)
        {
            PutObject(defaultKey, o);
        }
        //Get Any Type Value

        public bool GetBoolean(string key)
        {
            return GetBoolean(key, false);
        }

        public bool GetBoolean(string key, bool defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (bool)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public byte GetByte(string key)
        {
            return GetByte(key, (byte)0);
        }

        public byte GetByte(string key, byte defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (byte)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public char GetChar(string key)
        {
            return GetChar(key, (char)0);
        }

        public char GetChar(string key, char defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (char)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public short GetShort(string key)
        {
            return GetShort(key, (short)0);
        }

        public short GetShort(string key, short defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (short)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        public uint GetUint(string key)
        {
            return GetUint(key, 0);
        }

        public int GetInt(string key, int defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (int)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }
        public uint GetUint(string key, uint defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (uint)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }


        public long GetLong(string key)
        {
            return GetLong(key, 0L);
        }

        public long GetLong(string key, long defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (long)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public float GetFloat(string key)
        {
            return GetFloat(key, 0.0f);
        }

        public float GetFloat(string key, float defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (float)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public double GetDouble(string key)
        {
            return GetDouble(key, 0.0);
        }

        public double GetDouble(string key, double defaultValue)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return defaultValue;
            }
            try
            {
                return (double)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return defaultValue;
            }
        }

        public string GetString(string key)
        {
            object o = this.DataMap[key];
            try
            {
                return (string)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public string GetString(string key, string defaultValue)
        {
            string s = GetString(key);
            return (s == null) ? defaultValue : s;
        }

        public Bundle GetBundle(string key)
        {
            object o = this.DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (Bundle)o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public object GetObject(string key)
        {
            object o = this.DataMap[key];
            return o;
        }

        public object GetObject()
        {
            object o = this.DataMap[defaultKey];
            return o;
        }

        public bool[] GetBooleanArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (bool[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public byte[] GetByteArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (byte[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public short[] GetShortArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (short[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public char[] GetCharArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (char[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public int[] GetIntArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (int[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public long[] GetLongArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (long[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public float[] GetFloatArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (float[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public double[] GetDoubleArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (double[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }

        public string[] GetStringArray(string key)
        {
            object o = DataMap[key];
            if (o == null)
            {
                return null;
            }
            try
            {
                return (string[])o;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e);
                return null;
            }
        }


    }
}
