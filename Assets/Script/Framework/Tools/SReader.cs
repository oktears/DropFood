using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public class SReader
    {

        public enum DataMode
        {
            Unknown = -1,

            // 小端模式，数据的低位存在内存的低位，如short值100，转换为byte[]后为{0, 100}，xd文件均为小端模式
            LittleEndian,

            // 大端模式，数据的低位存在内存的高位，如short值100，转换为byte[]后为{100, 0}，上下行报文数据均为大端模式
            BigEndian,
        }

        private static Encoding s_utf8Encoding = Encoding.UTF8;
        private byte[] m_readBuffer = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private byte[] m_data;
        private int m_index;
        private DataMode m_mode = DataMode.Unknown;

        public int Version { get; set; }
        public string Desc { get; set; }
        public int RecordCount { get; set; }


        public static void CreateWebGL(string filePath, Action<byte[]> callback)
        {
            FileUtils.getFileBytes(filePath, callback);
        }

        public static SReader Create(string filePath)
        {
            SReader reader = null;
            byte[] xdData = FileUtils.XUnZipXD(filePath);

            if (xdData != null)
            {
                reader = Create(xdData);
                reader.ReadCommon();
            }
            else
            {
                Debug.LogError("Can't get data of file:" + filePath);
            }
            return reader;
        }




        public static SReader Create(byte[] data)
        {
            SReader sr = new SReader();
            if (sr.Init(data, DataMode.LittleEndian))
            {
                return sr;
            }
            else
            {
                return null;
            }
        }

        protected bool Init(byte[] data, DataMode mode)
        {
            m_data = data;
            if (m_data == null)
            {
                return false;
            }

            m_index = 0;
            m_mode = mode;

            return true;
        }

        public void Close()
        {
            m_data = null;
            m_index = 0;
        }

        public void Reset()
        {
            m_index = 0;
        }

        public byte[] Bytes
        {
            get { return m_data; }
        }

        public int index
        {
            get { return m_index; }
        }

        public int length
        {
            get { return m_data.Length; }
        }

        public bool CheckSize(int size)
        {
            return m_index + size <= m_data.Length;
        }

        public SReader ReadCommon()
        {
            Version = ReadInt();
            Desc = ReadString();
            RecordCount = ReadInt();
            return this;
        }

        public bool ReadBytes(int size, ref byte[] buffer)
        {
            if (buffer.Length < size)
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadBytes Error! buffer not big enough");
                return false;
            }

            if (!CheckSize(size))
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadBytes Error! out of range exception");
                return false;
            }

            //            XTool.StartSample("ReadBytes");

            Array.Copy(m_data, m_index, buffer, 0, size);
            m_index += size;

            //            XTool.EndSample();
            return true;
        }

        public byte ReadByte()
        {
            byte value = 0;
            int size = 1;
            if (!CheckSize(size))
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadBytes Error! out of range exception");
                return value;
            }

            value = m_data[m_index];
            m_index += size;
            return value;
        }

        public byte readByte()
        {
            return this.ReadByte();
        }

        public bool ReadBoolean()
        {
            return ReadByte() != 0;
        }

        public bool readBoolean()
        {
            return this.ReadBoolean();
        }

        public sbyte ReadSByte()
        {
            sbyte value = 0;
            int size = 1;
            if (!CheckSize(size))
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadBytes Error! out of range exception");
                return value;
            }

            value = (sbyte)m_data[m_index];
            m_index += size;
            return value;
        }

        public short ReadShort()
        {
            //            XTool.StartSample("ReadShort");

            short value = 0;
            int size = 2;

            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToInt16(m_readBuffer, 0);
            }

            //            XTool.EndSample();
            return value;
        }

        public short readShort()
        {
            return this.ReadShort();
        }

        public SReader SkipShort()
        {
            short value = 0;
            int size = 2;

            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToInt16(m_readBuffer, 0);
            }

            return this;
        }

        public int ReadInt()
        {
            //            XTool.StartSample("ReadInt");

            int value = 0;
            int size = 4;

            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToInt32(m_readBuffer, 0);
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadInt Fail!");
            }

            //            XTool.EndSample();
            return value;
        }

        public int readInt()
        {
            return this.ReadInt();
        }

        public SReader SkipInt()
        {
            int value = 0;
            int size = 4;

            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToInt32(m_readBuffer, 0);
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadInt Fail!");
            }
            return this;
        }

        public long ReadLong()
        {
            //            XTool.StartSample("ReadLong");

            long value = 0;
            int size = 8;
            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToInt64(m_readBuffer, 0);
            }

            //            XTool.EndSample();
            return value;
        }

        public long readLong()
        {
            return this.ReadLong();
        }

        public System.Object readEnum(Type enumType)
        {
            return Enum.ToObject(enumType, readByte());
        }

        public SReader SkipLong()
        {
            //            XTool.StartSample("ReadLong");

            long value = 0;
            int size = 8;
            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToInt64(m_readBuffer, 0);
            }

            //            XTool.EndSample();
            return this;
        }

        public float ReadFloat()
        {
            //            XTool.StartSample("ReadFloat");

            float value = 0.0f;
            int size = 4;
            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToSingle(m_readBuffer, 0);
            }

            //            XTool.EndSample();
            return value;
        }

        public float readFloat()
        {
            return this.ReadFloat();
        }

        public SReader SkipFloat()
        {
            float value = 0.0f;
            int size = 4;
            if (ReadBytes(size, ref m_readBuffer))
            {
                if (m_mode == DataMode.LittleEndian)
                {
                    DataUtil.Reverse(ref m_readBuffer, size);
                }
                value = BitConverter.ToSingle(m_readBuffer, 0);
            }
            return this;
        }

        #region Byte Array

        private byte[] LoadByteArray()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new byte[0];
            }
            else
            {
                byte[] ret = new byte[length];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = ReadByte();
                }
                return ret;
            }
        }

        private byte[][] LoadByteArray2()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new byte[0][];
            }
            else
            {
                byte[][] ret = new byte[length][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadByteArray();
                }
                return ret;
            }
        }

        private byte[][][] LoadByteArray3()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new byte[0][][];
            }
            else
            {
                byte[][][] ret = new byte[length][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadByteArray2();
                }
                return ret;
            }
        }

        private byte[][][][] LoadByteArray4()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new byte[0][][][];
            }
            else
            {
                byte[][][][] ret = new byte[length][][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadByteArray3();
                }
                return ret;
            }
        }

        public byte[] ReadByteArray()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new byte[0];
            }
            else if (dimension == 1)
            {
                return LoadByteArray();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadByteArray Wrong Dimension:" + dimension);
                return new byte[0];
            }
        }

        public byte[][] ReadByteArray2()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new byte[0][];
            }
            else if (dimension == 1)
            {
                byte[][] ret = new byte[1][];
                ret[0] = LoadByteArray();
                return ret;
            }
            else if (dimension == 2)
            {
                return LoadByteArray2();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadByteArray2 Wrong Dimension:" + dimension);
                return new byte[0][];
            }
        }

        public byte[][][] ReadByteArray3()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new byte[0][][];
            }
            else if (dimension == 1)
            {
                byte[][][] ret = new byte[1][][];
                ret[0] = new byte[1][];
                ret[0][0] = LoadByteArray();
                return ret;
            }
            else if (dimension == 2)
            {
                byte[][][] ret = new byte[1][][];
                ret[0] = LoadByteArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                return LoadByteArray3();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadByteArray3 Wrong Dimension:" + dimension);
                return new byte[0][][];
            }
        }

        public byte[][][][] ReadByteArray4()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new byte[0][][][];
            }
            else if (dimension == 1)
            {
                byte[][][][] ret = new byte[1][][][];
                ret[0] = new byte[1][][];
                ret[0][0] = new byte[1][];
                ret[0][0][0] = LoadByteArray();
                return ret;
            }
            else if (dimension == 2)
            {
                byte[][][][] ret = new byte[1][][][];
                ret[0] = new byte[1][][];
                ret[0][0] = LoadByteArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                byte[][][][] ret = new byte[1][][][];
                ret[0] = LoadByteArray3();
                return ret;
            }
            else if (dimension == 4)
            {
                return LoadByteArray4();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadByteArray4 Wrong Dimension:" + dimension);
                return new byte[0][][][];
            }
        }

        #endregion

        #region Short Array

        private short[] LoadShortArray()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new short[0];
            }
            else
            {
                short[] ret = new short[length];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = ReadShort();
                }
                return ret;
            }
        }

        private short[][] LoadShortArray2()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new short[0][];
            }
            else
            {
                short[][] ret = new short[length][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadShortArray();
                }
                return ret;
            }
        }

        private short[][][] LoadShortArray3()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new short[0][][];
            }
            else
            {
                short[][][] ret = new short[length][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadShortArray2();
                }
                return ret;
            }
        }

        private short[][][][] LoadShortArray4()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new short[0][][][];
            }
            else
            {
                short[][][][] ret = new short[length][][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadShortArray3();
                }
                return ret;
            }
        }

        public short[] ReadShortArray()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new short[0];
            }
            else if (dimension == 1)
            {
                return LoadShortArray();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadShortArray Wrong Dimension:" + dimension);
                return new short[0];
            }
        }

        public short[][] ReadShortArray2()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new short[0][];
            }
            else if (dimension == 1)
            {
                short[][] ret = new short[1][];
                ret[0] = LoadShortArray();
                return ret;
            }
            else if (dimension == 2)
            {
                return LoadShortArray2();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadShortArray2 Wrong Dimension:" + dimension);
                return new short[0][];
            }
        }

        public short[][][] ReadShortArray3()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new short[0][][];
            }
            else if (dimension == 1)
            {
                short[][][] ret = new short[1][][];
                ret[0] = new short[1][];
                ret[0][0] = LoadShortArray();
                return ret;
            }
            else if (dimension == 2)
            {
                short[][][] ret = new short[1][][];
                ret[0] = LoadShortArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                return LoadShortArray3();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadShortArray3 Wrong Dimension:" + dimension);
                return new short[0][][];
            }
        }

        public short[][][][] ReadShortArray4()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new short[0][][][];
            }
            else if (dimension == 1)
            {
                short[][][][] ret = new short[1][][][];
                ret[0] = new short[1][][];
                ret[0][0] = new short[1][];
                ret[0][0][0] = LoadShortArray();
                return ret;
            }
            else if (dimension == 2)
            {
                short[][][][] ret = new short[1][][][];
                ret[0] = new short[1][][];
                ret[0][0] = LoadShortArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                short[][][][] ret = new short[1][][][];
                ret[0] = LoadShortArray3();
                return ret;
            }
            else if (dimension == 4)
            {
                return LoadShortArray4();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadShortArray4 Wrong Dimension:" + dimension);
                return new short[0][][][];
            }
        }

        #endregion

        #region Int Array

        private int[] LoadIntArray()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new int[0];
            }
            else
            {
                int[] ret = new int[length];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = ReadInt();
                }
                return ret;
            }
        }

        private int[][] LoadIntArray2()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new int[0][];
            }
            else
            {
                int[][] ret = new int[length][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadIntArray();
                }
                return ret;
            }
        }

        private int[][][] LoadIntArray3()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new int[0][][];
            }
            else
            {
                int[][][] ret = new int[length][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadIntArray2();
                }
                return ret;
            }
        }

        private int[][][][] LoadIntArray4()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new int[0][][][];
            }
            else
            {
                int[][][][] ret = new int[length][][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadIntArray3();
                }
                return ret;
            }
        }

        public int[] ReadIntArray()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new int[0];
            }
            else if (dimension == 1)
            {
                return LoadIntArray();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadIntArray Wrong Dimension:" + dimension);
                return new int[0];
            }
        }

        public SReader SkipIntArray()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
            }
            else if (dimension == 1)
            {
                LoadIntArray();
            }
            else
            {
            }
            return this;
        }

        public int[][] ReadIntArray2()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new int[0][];
            }
            else if (dimension == 1)
            {
                int[][] ret = new int[1][];
                ret[0] = LoadIntArray();
                return ret;
            }
            else if (dimension == 2)
            {
                return LoadIntArray2();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadIntArray2 Wrong Dimension:" + dimension);
                return new int[0][];
            }
        }

        public int[][][] ReadIntArray3()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new int[0][][];
            }
            else if (dimension == 1)
            {
                int[][][] ret = new int[1][][];
                ret[0] = new int[1][];
                ret[0][0] = LoadIntArray();
                return ret;
            }
            else if (dimension == 2)
            {
                int[][][] ret = new int[1][][];
                ret[0] = LoadIntArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                return LoadIntArray3();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadIntArray3 Wrong Dimension:" + dimension);
                return new int[0][][];
            }
        }

        public int[][][][] ReadIntArray4()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new int[0][][][];
            }
            else if (dimension == 1)
            {
                int[][][][] ret = new int[1][][][];
                ret[0] = new int[1][][];
                ret[0][0] = new int[1][];
                ret[0][0][0] = LoadIntArray();
                return ret;
            }
            else if (dimension == 2)
            {
                int[][][][] ret = new int[1][][][];
                ret[0] = new int[1][][];
                ret[0][0] = LoadIntArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                int[][][][] ret = new int[1][][][];
                ret[0] = LoadIntArray3();
                return ret;
            }
            else if (dimension == 4)
            {
                return LoadIntArray4();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadIntArray4 Wrong Dimension:" + dimension);
                return new int[0][][][];
            }
        }

        #endregion

        #region Vector

        private Vector2 LoadVector2()
        {
            Vector2 ret = Vector2.zero;
            byte length = ReadByte();
            for (int i = 0; i < length; i++)
            {
                float value = ReadFloat();
                switch (i)
                {
                    case 0:
                        ret.x = value;
                        break;
                    case 1:
                        ret.y = value;
                        break;
                    default:
                        break;
                }
            }
            return ret;
        }

        private Vector3 LoadVector3()
        {
            Vector3 ret = Vector3.zero;
            byte length = ReadByte();
            for (int i = 0; i < length; i++)
            {
                float value = ReadFloat();
                switch (i)
                {
                    case 0:
                        ret.x = value;
                        break;
                    case 1:
                        ret.y = value;
                        break;
                    case 2:
                        ret.z = value;
                        break;
                    default:
                        break;
                }
            }
            return ret;
        }

        public Vector2 ReadVector2()
        {
            Vector2 ret = Vector2.zero;
            byte dimension = ReadByte();
            if (dimension == 0)
            {
                return ret;
            }
            else if (dimension == 1)
            {
                return LoadVector2();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadVector2 Wrong Dimension:" + dimension);
                return ret;
            }
        }

        public Vector3 ReadVector3()
        {
            Vector3 ret = Vector3.zero;
            byte dimension = ReadByte();
            if (dimension == 0)
            {
                return ret;
            }
            else if (dimension == 1)
            {
                return LoadVector3();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadVector3 Wrong Dimension:" + dimension);
                return ret;
            }
        }

        public List<Vector3> ReadVector3List(bool ensureAtLeastOneElement)
        {
            byte dimension = ReadByte();
            if (dimension == 0)
            {
                List<Vector3> ret = new List<Vector3>(1);
                if (ensureAtLeastOneElement)
                {
                    ret.Add(Vector3.one);
                }
                return ret;
            }
            else if (dimension == 1)
            {
                List<Vector3> ret = new List<Vector3>(1);
                ret.Add(LoadVector3());
                return ret;
            }
            else if (dimension == 2)
            {
                byte length = ReadByte();
                List<Vector3> ret = new List<Vector3>(length);
                for (int i = 0; i < length; i++)
                {
                    ret.Add(LoadVector3());
                }
                return ret;
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadVector3List Wrong Dimension:" + dimension);
                return new List<Vector3>(0);
            }
        }

        #endregion

        #region Float Array

        private float[] LoadFloatArray()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new float[0];
            }
            else
            {
                float[] ret = new float[length];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = ReadFloat();
                }
                return ret;
            }
        }

        private float[][] LoadFloatArray2()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new float[0][];
            }
            else
            {
                float[][] ret = new float[length][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadFloatArray();
                }
                return ret;
            }
        }

        private float[][][] LoadFloatArray3()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new float[0][][];
            }
            else
            {
                float[][][] ret = new float[length][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadFloatArray2();
                }
                return ret;
            }
        }

        private float[][][][] LoadFloatArray4()
        {
            byte length = ReadByte();
            if (length <= 0)
            {
                return new float[0][][][];
            }
            else
            {
                float[][][][] ret = new float[length][][][];
                for (int i = 0; i < length; i++)
                {
                    ret[i] = LoadFloatArray3();
                }
                return ret;
            }
        }

        public float[] ReadFloatArray()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new float[0];
            }
            else if (dimension == 1)
            {
                return LoadFloatArray();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadFloatArray Wrong Dimension:" + dimension);
                return new float[0];
            }
        }

        public float[][] ReadFloatArray2()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new float[0][];
            }
            else if (dimension == 1)
            {
                float[][] ret = new float[1][];
                ret[0] = LoadFloatArray();
                return ret;
            }
            else if (dimension == 2)
            {
                return LoadFloatArray2();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadFloatArray2 Wrong Dimension:" + dimension);
                return new float[0][];
            }
        }

        public float[][][] ReadFloatArray3()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new float[0][][];
            }
            else if (dimension == 1)
            {
                float[][][] ret = new float[1][][];
                ret[0] = new float[1][];
                ret[0][0] = LoadFloatArray();
                return ret;
            }
            else if (dimension == 2)
            {
                float[][][] ret = new float[1][][];
                ret[0] = LoadFloatArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                return LoadFloatArray3();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadFloatArray3 Wrong Dimension:" + dimension);
                return new float[0][][];
            }
        }

        public float[][][][] ReadFloatArray4()
        {
            byte dimension = ReadByte();
            if (dimension <= 0)
            {
                return new float[0][][][];
            }
            else if (dimension == 1)
            {
                float[][][][] ret = new float[1][][][];
                ret[0] = new float[1][][];
                ret[0][0] = new float[1][];
                ret[0][0][0] = LoadFloatArray();
                return ret;
            }
            else if (dimension == 2)
            {
                float[][][][] ret = new float[1][][][];
                ret[0] = new float[1][][];
                ret[0][0] = LoadFloatArray2();
                return ret;
            }
            else if (dimension == 3)
            {
                float[][][][] ret = new float[1][][][];
                ret[0] = LoadFloatArray3();
                return ret;
            }
            else if (dimension == 4)
            {
                return LoadFloatArray4();
            }
            else
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadFloatArray4 Wrong Dimension:" + dimension);
                return new float[0][][][];
            }
        }

        #endregion

        public SReader SkipString()
        {
            short size = ReadShort();
            if (size < 0)
            {
                //Debug.LogError(XTool.LogHead(this) + "SkipUtf Error! size < 0");
            }
            else if (size > 0)
            {
                SkipBytes(size);
            }
            return this;
        }

        public string ReadString()
        {
            string value = string.Empty;
            short size = ReadShort();
            if (size <= 0)
            {
                return value;
            }
            else
            {
                if (!CheckSize(size))
                {
                    return value;
                }

                value = s_utf8Encoding.GetString(m_data, m_index, size);
                m_index += size;
            }

            return value;
        }

        public string readString()
        {
            return this.ReadString();
        }

        public void SkipBytes(int count)
        {
            if (count < 0)
            {
                //Debug.LogError(XTool.LogHead(this) + "SkipBytes Error! size < 0");
            }
            else if (count > 0)
            {
                if (!CheckSize(count))
                {
                    //Debug.LogError(XTool.LogHead(this) + "SkipBytes Error! out of range exception");
                    return;
                }
                m_index += count;
            }
        }

        public byte[] ReadBytes(int count)
        {
            //            XTool.StartSample("ReadBytes");

            byte[] ret = new byte[count];

            if (!CheckSize(count))
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadBytes Error! out of range exception");
                return ret;
            }

            Array.Copy(m_data, m_index, ret, 0, count);
            m_index += count;

            //            XTool.EndSample();
            return ret;
        }

        public int[] ReadInts(int count)
        {
            //            XTool.StartSample("ReadBytes");

            int[] ret = new int[count];

            if (!CheckSize(count))
            {
                //Debug.LogError(XTool.LogHead(this) + "ReadBytes Error! out of range exception");
                return ret;
            }

            Array.Copy(m_data, m_index, ret, 0, count);
            m_index += count;

            //            XTool.EndSample();
            return ret;
        }
    }
}
