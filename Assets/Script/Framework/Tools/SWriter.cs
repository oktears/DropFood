using System;
using System.Collections.Generic;
using System.IO;

namespace Chengzi
{

    /// <summary>
    /// 二进制流写入
    /// </summary>
    public class SWriter
    {

        private int m_length = 0;
        private byte[] m_data = null;
        private BinaryWriter m_writer = null;

        protected void Init(int cacheSize)
        {
            m_length = 0;
            m_data = new byte[cacheSize];
            m_writer = new BinaryWriter(new MemoryStream(m_data));
        }


		public static SWriter Create(int cacheSize)
        {
			SWriter sw = new SWriter();
            sw.Init(cacheSize);
            return sw;
        }

        // data
        public byte[] GetData()
        {
            byte[] retData = new byte[m_length];
            for (int i = 0; i < m_length; i++)
            {
                retData[i] = m_data[i];
            }
            return retData;
        }

        public int length
        {
            get { return m_length; }
        }

        public void WriteByte(byte v)
        {
            int size = sizeof(byte);
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;
                m_writer.Write(v);
            }
        }

        public void WriteBool(bool v)
        {
            WriteByte((byte)(v ? 1 : 0));
        }

        public void WriteShort(short v)
        {
            int size = sizeof(short);
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;
                byte[] bytes = System.BitConverter.GetBytes(v);
                DataUtil.Reverse(ref bytes);
                m_writer.Write(bytes);
            }
        }

        public void WriteInt(int v)
        {
            int size = sizeof(int);
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;
                byte[] bytes = System.BitConverter.GetBytes(v);
                DataUtil.Reverse(ref bytes);
                m_writer.Write(bytes);
            }
        }

        public void WriteFloat(float v)
        {
            int size = sizeof(float);
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;
                byte[] bytes = System.BitConverter.GetBytes(v);
                DataUtil.Reverse(ref bytes);
                m_writer.Write(bytes);
            }
        }

        public void WriteLong(long v)
        {
            int size = sizeof(long);
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;
                byte[] bytes = System.BitConverter.GetBytes(v);
                DataUtil.Reverse(ref bytes);
                m_writer.Write(bytes);
            }
        }

        public int WriteIntArray(int[] array)
        {
            int ret = m_length;
            if (array != null)
            {
                WriteInt(array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    WriteInt(array[i]);
                }
            }
            else
            {
                WriteInt(0);
            }
            return ret;
        }

        public int WriteIntArray2(int[][] array)
        {
            int ret = m_length;
            if (array != null)
            {
                WriteInt(array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    WriteIntArray(array[i]);
                }
            }
            else
            {
                WriteInt(0);
            }
            return ret;
        }

        public int WriteFloatArray(float[] array)
        {
            int ret = m_length;
            if (array != null)
            {
                WriteInt(array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    WriteFloat(array[i]);
                }
            }
            else
            {
                WriteInt(0);
            }
            return ret;
        }

        public int WriteFloatArray2(float[][] array)
        {
            int ret = m_length;
            if (array != null)
            {
                WriteInt(array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    WriteFloatArray(array[i]);
                }
            }
            else
            {
                WriteInt(0);
            }
            return ret;
        }

        public void WriteString(string str)
        {
            bool isWriteLength = true;
            if (str == null)
            {
                str = "";
            }

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            short length = (short)bytes.Length;
            int size = (isWriteLength ? sizeof(short) : 0) + length;
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;

                if (isWriteLength)
                {
                    byte[] lengthBytes = BitConverter.GetBytes(length);
                    DataUtil.Reverse(ref lengthBytes);
                    m_writer.Write(lengthBytes);
                }

                m_writer.Write(bytes);
            }
        }

        public void WriteBytes(byte[] bytes)
        {
            int size = bytes.Length;
            CheckSize(size);

            if (m_writer != null)
            {
                m_length += size;
                m_writer.Write(bytes);
            }
        }

        public void Close()
        {
            if (m_writer != null)
            {
                m_writer.Close();
            }
        }

        private void CheckSize(int size)
        {
            int afterWriterSize = m_length + size;
            if (afterWriterSize > m_data.Length)
            {
                int newSize = m_data.Length * 2;
                while (newSize < afterWriterSize)
                {
                    newSize *= 2;
                }
                byte[] newData = new byte[newSize];

                for (int i = 0; i < m_length; i++)
                {
                    newData[i] = m_data[i];
                }
                m_data = newData;

                m_writer.Close();
                m_writer = new BinaryWriter(new MemoryStream(m_data));
                m_writer.Seek(m_length, SeekOrigin.Begin);
            }
        }

    }
}
