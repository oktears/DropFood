using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Chengzi
{
    public class EncodeDecodeStream
    {
        /// <summary>
        /// 基础类型索引
        /// </summary>
        public const int Index_Boolean = 0;
        public const int Index_BooleanArray = 1;
        public const int Index_Char = 2;
        public const int Index_CharArray = 3;
        public const int Index_Byte = 4;
        public const int Index_ByteArray = 5;
        public const int Index_UInt16 = 6;
        public const int Index_UInt16Array = 7;
        public const int Index_Int16 = 8;
        public const int Index_Int16Array = 9;
        public const int Index_UInt32 = 10;
        public const int Index_UInt32Array = 11;
        public const int Index_Int32 = 12;
        public const int Index_Int32Array = 13;
        public const int Index_UInt64 = 14;
        public const int Index_UInt64Array = 15;
        public const int Index_Int64 = 16;
        public const int Index_Int64Array = 17;
        public const int Index_Single = 18;
        public const int Index_SingleArray = 19;
        public const int Index_Double = 20;
        public const int Index_DoubleArray = 21;
        public const int PrimitiveCount = Index_DoubleArray + 1;

        private static Dictionary<string, int> dictionary;

        static EncodeDecodeStream()
        {
            dictionary = new Dictionary<string, int>(PrimitiveCount);
            dictionary.Add("Boolean", Index_Boolean);
            dictionary.Add("Boolean[]", Index_BooleanArray);
            dictionary.Add("Char", Index_Char);
            dictionary.Add("Char[]", Index_CharArray);
            dictionary.Add("Byte", Index_Byte);
            dictionary.Add("Byte[]", Index_ByteArray);
            dictionary.Add("UInt16", Index_UInt16);
            dictionary.Add("UInt16[]", Index_UInt16Array);
            dictionary.Add("Int16", Index_Int16);
            dictionary.Add("Int16[]", Index_Int16Array);
            dictionary.Add("UInt32", Index_UInt32);
            dictionary.Add("UInt32[]", Index_UInt32Array);
            dictionary.Add("Int32", Index_Int32);
            dictionary.Add("Int32[]", Index_Int32Array);
            dictionary.Add("UInt64", Index_UInt64);
            dictionary.Add("UInt64[]", Index_UInt64Array);
            dictionary.Add("Int64", Index_Int64);
            dictionary.Add("Int64[]", Index_Int64Array);
            dictionary.Add("Single", Index_Single);
            dictionary.Add("Single[]", Index_SingleArray);
            dictionary.Add("Double", Index_Double);
            dictionary.Add("Double[]", Index_DoubleArray);
        }

        private static short GetShort(byte[] value)
        {
            return (short)(((value[0]) << 8) |
                           ((value[1] & 0xff))
                           );
        }

        private static ushort GetUInt16(byte[] value)
        {
            return (ushort)GetShort(value);
        }

        private static int GetInt(byte[] value)
        {
            return (int)(((value[0]) << 24) |
                         ((value[1] & 0xff) << 16) |
                         ((value[2] & 0xff) << 8) |
                         ((value[3] & 0xff))
                         );
        }

        private static uint GetUint(byte[] value)
        {
            return (uint)GetInt(value);
        }

        private static long GetLong(byte[] value)
        {
            return (long)(((value[0]) << 56) |
                         ((value[1] & 0xff) << 48) |
                         ((value[2] & 0xff) << 40) |
                         ((value[3] & 0xff) << 32) |
                         ((value[4] & 0xff) << 24) |
                         ((value[5] & 0xff) << 16) |
                         ((value[6] & 0xff) << 8) |
                         ((value[7] & 0xff))
                         );
        }

        private static ulong GetUlong(byte[] value)
        {
            return (ulong)GetLong(value);
        }

        private static byte short1(short x) { return (byte)(x >> 8); }
        private static byte short0(short x) { return (byte)(x); }
        private static byte short1(ushort x) { return (byte)(x >> 8); }
        private static byte short0(ushort x) { return (byte)(x); }
        private static byte int3(int x) { return (byte)(x >> 24); }
        private static byte int2(int x) { return (byte)(x >> 16); }
        private static byte int1(int x) { return (byte)(x >> 8); }
        private static byte int0(int x) { return (byte)(x); }
        private static byte int3(uint x) { return (byte)(x >> 24); }
        private static byte int2(uint x) { return (byte)(x >> 16); }
        private static byte int1(uint x) { return (byte)(x >> 8); }
        private static byte int0(uint x) { return (byte)(x); }
        private static byte long7(long x) { return (byte)(x >> 56); }
        private static byte long6(long x) { return (byte)(x >> 48); }
        private static byte long5(long x) { return (byte)(x >> 40); }
        private static byte long4(long x) { return (byte)(x >> 32); }
        private static byte long3(long x) { return (byte)(x >> 24); }
        private static byte long2(long x) { return (byte)(x >> 16); }
        private static byte long1(long x) { return (byte)(x >> 8); }
        private static byte long0(long x) { return (byte)(x); }
        private static byte long7(ulong x) { return (byte)(x >> 56); }
        private static byte long6(ulong x) { return (byte)(x >> 48); }
        private static byte long5(ulong x) { return (byte)(x >> 40); }
        private static byte long4(ulong x) { return (byte)(x >> 32); }
        private static byte long3(ulong x) { return (byte)(x >> 24); }
        private static byte long2(ulong x) { return (byte)(x >> 16); }
        private static byte long1(ulong x) { return (byte)(x >> 8); }
        private static byte long0(ulong x) { return (byte)(x); }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="indexPrimitive"></param>
        /// <returns></returns>
        public static object GetObject(ByteBuffer buffer, int indexPrimitive)
        {
            object destObj = null;
            try
            {
                switch (indexPrimitive)
                {
                    case Index_Boolean:
                        destObj = buffer.GetBytes(1)[0] != 0;
                        return destObj;
                    case Index_Char:
                        // 根据后续需求进行实现
                        return destObj;
                    case Index_Byte:
                        destObj = buffer.GetBytes(1)[0];
                        return destObj;
                    case Index_UInt16:
                        destObj = GetUInt16(buffer.GetBytes(2));
                        return destObj;
                    case Index_Int16:
                        destObj = GetShort(buffer.GetBytes(2));
                        return destObj;
                    case Index_UInt32:
                        destObj = GetUint(buffer.GetBytes(4));
                        return destObj;
                    case Index_Int32:
                        destObj = GetInt(buffer.GetBytes(4));
                        return destObj;
                    case Index_UInt64:
                        destObj = GetUlong(buffer.GetBytes(8));
                        return destObj;
                    case Index_Int64:
                        destObj = GetLong(buffer.GetBytes(8));
                        return destObj;
                    case Index_Single:
                        if (BitConverter.IsLittleEndian == false)
                            destObj = BitConverter.ToSingle(buffer.GetBytes(4), 0);
                        else
                        {
                            byte[] reverseByte = buffer.GetBytes(4);
                            Array.Reverse(reverseByte);
                            destObj = BitConverter.ToSingle(reverseByte, 0);
                        }
                        return destObj;
                    case Index_Double:
                        if (BitConverter.IsLittleEndian == false)
                            destObj = BitConverter.ToDouble(buffer.GetBytes(8), 0);
                        else
                        {
                            byte[] reverseByte = buffer.GetBytes(8);
                            Array.Reverse(reverseByte);
                            destObj = BitConverter.ToDouble(reverseByte, 0);
                        }
                        return destObj;
                }
            }
            catch (Exception exception)
            {
                NetDebug.Log("EncodeDecodeStream::decode Exception!!! ( " + exception.ToString() + " )", "None");
            }
            return destObj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="refObj"></param>
        public static void GetObject(ByteBuffer buffer, object refObj)
        {
            try
            {
                string name = "";
                int i = 0, length = 0, indexPrimitive = 0;
                // 读取字段的类型名称
                if (refObj != null)
                {
                    name = refObj.GetType().Name;
                    if (name != null)
                    {
                        // 通过name查找Dictionary中数据类型对应的索引
                        if (dictionary.TryGetValue(name, out indexPrimitive))
                        {
                            switch (indexPrimitive)
                            {
                                case Index_BooleanArray:
                                    length = ((bool[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((bool[])refObj)[i] = buffer.GetBytes(1)[0] != 0;
                                        i++;
                                    }
                                    break;
                                case Index_CharArray:
                                    // 根据后续需求进行实现
                                    break;
                                case Index_ByteArray:
                                    length = ((byte[])refObj).Length;
                                    Array.Copy(buffer.GetBytes(length), (byte[])refObj, length);
                                    break;
                                case Index_UInt16Array:
                                    length = ((ushort[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((ushort[])refObj)[i] = GetUInt16(buffer.GetBytes(2));
                                        i++;
                                    }
                                    break;
                                case Index_Int16Array:
                                    length = ((short[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((short[])refObj)[i] = GetShort(buffer.GetBytes(2));
                                        i++;
                                    }
                                    break;
                                case Index_UInt32Array:
                                    length = ((uint[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((uint[])refObj)[i] = GetUint(buffer.GetBytes(4));
                                        i++;
                                    }
                                    break;
                                case Index_Int32Array:
                                    length = ((int[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((int[])refObj)[i] = GetInt(buffer.GetBytes(4));
                                        i++;
                                    }
                                    break;
                                case Index_UInt64Array:
                                    length = ((ulong[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((ulong[])refObj)[i] = GetUlong(buffer.GetBytes(8));
                                        i++;
                                    }
                                    break;
                                case Index_Int64Array:
                                    length = ((long[])refObj).Length;
                                    while (i < length)
                                    {
                                        ((long[])refObj)[i] = GetLong(buffer.GetBytes(8));
                                        i++;
                                    }
                                    break;
                                case Index_SingleArray:
                                    length = ((float[])refObj).Length;
                                    while (i < length)
                                    {
                                        if (BitConverter.IsLittleEndian == false)
                                            ((float[])refObj)[i] = BitConverter.ToSingle(buffer.GetBytes(4), 0);
                                        else
                                        {
                                            byte[] reverseByte = buffer.GetBytes(4);
                                            Array.Reverse(reverseByte);
                                            ((float[])refObj)[i] = BitConverter.ToSingle(reverseByte, 0);
                                        }
                                        i++;
                                    }
                                    break;
                                case Index_DoubleArray:
                                    length = ((double[])refObj).Length;
                                    while (i < length)
                                    {
                                        if (BitConverter.IsLittleEndian == false)
                                            ((double[])refObj)[i] = BitConverter.ToDouble(buffer.GetBytes(8), 0);
                                        else
                                        {
                                            byte[] reverseByte = buffer.GetBytes(8);
                                            Array.Reverse(reverseByte);
                                            ((double[])refObj)[i] = BitConverter.ToDouble(reverseByte, 0);
                                        }
                                        i++;
                                    }
                                    break;
                            }
                        }
                        else
                            NetDebug.LogError("EncodeDecodeStream::decode 未知数据类型(" + refObj == null ? "obj==null" : refObj.ToString() + ")", "None");
                    }
                    else
                        NetDebug.LogError("EncodeDecodeStream::decode 未知数据类型(" + refObj == null ? "obj==null" : refObj.ToString() + ")", "None");
                }
                else
                    NetDebug.LogError("EncodeDecodeStream::decode 未知数据类型(" + refObj == null ? "obj==null" : refObj.ToString() + ")", "None");
            }
            catch (Exception exception)
            {
                NetDebug.LogError("EncodeDecodeStream::decode Exception!!! ( " + exception.ToString() + " )", "None");
            }
        }

        /// <summary>
        /// 在ByteBuffer的0，1位置写入当前数据长度
        /// </summary>
        /// <param name="buffer"></param>
        public static void PutMessageLength(ByteBuffer buffer)
        {
            ushort length = (ushort)(buffer.WritePos - NetTcpClient.MSG_HEAD_MAX);
            byte[] bytes = new byte[2];
            bytes[0] = short1(length);
            bytes[1] = short0(length);
            Array.Copy(bytes, buffer.Data, 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="refObj"></param>
        public static void PutObject(ByteBuffer buffer, object refObj)
        {
            try
            {
                string name = "";
                byte[] bytes = null;
                int i = 0, length = 0, indexPrimitive = 0;
                // 读取字段的类型名称
                if (refObj != null)
                {
                    name = refObj.GetType().Name;
                    if (name != null)
                    {
                        // 通过name查找Dictionary中数据类型对应的索引
                        if (dictionary.TryGetValue(name, out indexPrimitive))
                        {
                            switch (indexPrimitive)
                            {
                                case Index_Boolean:
                                    bytes = new byte[1];
                                    bytes[0] = ((bool)refObj) ? (byte)1 : (byte)0;
                                    break;
                                case Index_BooleanArray:
                                    length = ((bool[])refObj).Length;
                                    bytes = new byte[length];
                                    while (i < length)
                                    {
                                        bytes[i] = ((bool[])refObj)[i] ? (byte)1 : (byte)0;
                                        i++;
                                    }
                                    break;
                                case Index_Char:
                                    // 根据后续需求进行实现
                                    break;
                                case Index_CharArray:
                                    // 根据后续需求进行实现
                                    break;
                                case Index_Byte:
                                    bytes = new byte[1];
                                    bytes[0] = (byte)refObj;
                                    break;
                                case Index_ByteArray:
                                    bytes = (byte[])refObj;
                                    break;
                                case Index_UInt16:
                                    bytes = new byte[2];
                                    bytes[0] = short1((ushort)refObj);
                                    bytes[1] = short0((ushort)refObj);
                                    break;
                                case Index_UInt16Array:
                                    length = ((ushort[])refObj).Length;
                                    bytes = new byte[length * 2];
                                    while (i < length)
                                    {
                                        bytes[i] = short1((ushort)refObj);
                                        bytes[i + 1] = short0((ushort)refObj);
                                        i++;
                                    }
                                    break;
                                case Index_Int16:
                                    bytes = new byte[2];
                                    bytes[0] = short1((short)refObj);
                                    bytes[1] = short0((short)refObj);
                                    break;
                                case Index_Int16Array:
                                    length = ((short[])refObj).Length;
                                    bytes = new byte[length * 2];
                                    while (i < length)
                                    {
                                        bytes[i] = short1((short)refObj);
                                        bytes[i + 1] = short0((short)refObj);
                                        i++;
                                    }
                                    break;
                                case Index_UInt32:
                                    bytes = new byte[4];
                                    bytes[0] = int3((uint)refObj);
                                    bytes[1] = int2((uint)refObj);
                                    bytes[2] = int1((uint)refObj);
                                    bytes[3] = int0((uint)refObj);
                                    break;
                                case Index_UInt32Array:
                                    length = ((uint[])refObj).Length;
                                    bytes = new byte[length * 4];
                                    while (i < length)
                                    {
                                        bytes[i] = int3((uint)refObj);
                                        bytes[i + 1] = int2((uint)refObj);
                                        bytes[i + 2] = int1((uint)refObj);
                                        bytes[i + 3] = int0((uint)refObj);
                                        i++;
                                    }
                                    break;
                                case Index_Int32:
                                    bytes = new byte[4];
                                    bytes[0] = int3((int)refObj);
                                    bytes[1] = int2((int)refObj);
                                    bytes[2] = int1((int)refObj);
                                    bytes[3] = int0((int)refObj);
                                    break;
                                case Index_Int32Array:
                                    length = ((int[])refObj).Length;
                                    bytes = new byte[length * 4];
                                    while (i < length)
                                    {
                                        bytes[i] = int3((int)refObj);
                                        bytes[i + 1] = int2((int)refObj);
                                        bytes[i + 2] = int1((int)refObj);
                                        bytes[i + 3] = int0((int)refObj);
                                        i++;
                                    }
                                    break;
                                case Index_UInt64:
                                    bytes = new byte[8];
                                    bytes[0] = long7((ulong)refObj);
                                    bytes[1] = long6((ulong)refObj);
                                    bytes[2] = long5((ulong)refObj);
                                    bytes[3] = long4((ulong)refObj);
                                    bytes[4] = long3((ulong)refObj);
                                    bytes[5] = long2((ulong)refObj);
                                    bytes[6] = long1((ulong)refObj);
                                    bytes[7] = long0((ulong)refObj);
                                    break;
                                case Index_UInt64Array:
                                    length = ((ulong[])refObj).Length;
                                    bytes = new byte[length * 8];
                                    while (i < length)
                                    {
                                        bytes[i] = long7((ulong)refObj);
                                        bytes[i + 1] = long6((ulong)refObj);
                                        bytes[i + 2] = long5((ulong)refObj);
                                        bytes[i + 3] = long4((ulong)refObj);
                                        bytes[i + 4] = long3((ulong)refObj);
                                        bytes[i + 5] = long2((ulong)refObj);
                                        bytes[i + 6] = long1((ulong)refObj);
                                        bytes[i + 7] = long0((ulong)refObj);
                                        i++;
                                    }
                                    break;
                                case Index_Int64:
                                    bytes = new byte[8];
                                    bytes[0] = long7((long)refObj);
                                    bytes[1] = long6((long)refObj);
                                    bytes[2] = long5((long)refObj);
                                    bytes[3] = long4((long)refObj);
                                    bytes[4] = long3((long)refObj);
                                    bytes[5] = long2((long)refObj);
                                    bytes[6] = long1((long)refObj);
                                    bytes[7] = long0((long)refObj);
                                    break;
                                case Index_Int64Array:
                                    length = ((long[])refObj).Length;
                                    bytes = new byte[length * 8];
                                    while (i < length)
                                    {
                                        bytes[i] = long7((long)refObj);
                                        bytes[i + 1] = long6((long)refObj);
                                        bytes[i + 2] = long5((long)refObj);
                                        bytes[i + 3] = long4((long)refObj);
                                        bytes[i + 4] = long3((long)refObj);
                                        bytes[i + 5] = long2((long)refObj);
                                        bytes[i + 6] = long1((long)refObj);
                                        bytes[i + 7] = long0((long)refObj);
                                        i++;
                                    }
                                    break;
                                case Index_Single:
                                    bytes = BitConverter.GetBytes((float)refObj);
                                    if (BitConverter.IsLittleEndian)
                                        Array.Reverse(bytes);
                                    break;
                                case Index_SingleArray:
                                    length = ((float[])refObj).Length;
                                    bytes = new byte[length * 4];
                                    while (i < length)
                                    {
                                        byte[] temp = BitConverter.GetBytes((float)refObj);
                                        if (BitConverter.IsLittleEndian)
                                            Array.Reverse(temp);
                                        bytes[i] = temp[3];
                                        bytes[i + 1] = temp[2];
                                        bytes[i + 2] = temp[1];
                                        bytes[i + 3] = temp[0];
                                        i++;
                                    }
                                    break;
                                case Index_Double:
                                    bytes = BitConverter.GetBytes((double)refObj);
                                    if (BitConverter.IsLittleEndian)
                                        Array.Reverse(bytes);
                                    break;
                                case Index_DoubleArray:
                                    length = ((double[])refObj).Length;
                                    bytes = new byte[length * 8];
                                    while (i < length)
                                    {
                                        byte[] temp = BitConverter.GetBytes((float)refObj);
                                        if (BitConverter.IsLittleEndian)
                                            Array.Reverse(temp);
                                        bytes[i] = temp[7];
                                        bytes[i + 1] = temp[6];
                                        bytes[i + 2] = temp[5];
                                        bytes[i + 3] = temp[4];
                                        bytes[i + 4] = temp[3];
                                        bytes[i + 5] = temp[2];
                                        bytes[i + 6] = temp[1];
                                        bytes[i + 7] = temp[0];
                                        i++;
                                    }
                                    break;
                            }
                            buffer.PutBytes(bytes);
                        }
                        else
                            NetDebug.LogError("EncodeDecodeStream::PutObject 未知数据类型(" + refObj == null ? "refObj==null" : refObj.ToString() + ")", "None");
                    }
                    else
                        NetDebug.LogError("EncodeDecodeStream::PutObject 未知数据类型(" + refObj == null ? "refObj==null" : refObj.ToString() + ")", "None");
                }
                else
                {
                    NetDebug.LogError("EncodeDecodeStream::PutObject 未知数据类型(" + refObj == null ? "refObj==null" : refObj.ToString() + ")", "None");
                }
            }
            catch (Exception exception)
            {
                NetDebug.LogError("EncodeDecodeStream::decode Exception!!! ( " + exception.ToString() + " )", "None");
            }
        }

    }
}
