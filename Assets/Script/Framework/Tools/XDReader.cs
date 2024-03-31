using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Chengzi
{
    public class XDReader : IReader
    {
        private byte[] data = null;

        private int m_index = 0;
        private int max_length = 0;
        private int recordCount = -1;
        private int version = 0;
        private string describe = "";

        public static XDReader Create(string path)
        {
            //path = PathHelper.GetFilePath(path);
            XDReader reader = new XDReader();

            byte[] buffer = FileUtils.XUnZipXD(path);
            if (buffer != null && reader.init(new MemoryStream(buffer)))
            {
                return reader;
            }

            reader.close();
            return null;
        }

        private XDReader()
        {

        }

        public bool init(Stream s)
        {
            try
            {
                bool ret = false;
                do
                {
                    if (s == null)
                    {
                        break;
                    }
                    data = new byte[s.Length];
                    max_length = data.Length;

                    s.Read(data, 0, max_length);

                    version = readInt();
                    describe = readString();
                    recordCount = readInt();
                    ret = true;

                } while (false);

                return ret;
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
            }

            return false;
        }

        public unsafe bool readBoolean()
        {
            return Convert.ToBoolean(readByte());
        }

        public unsafe byte readByte()
        {
            byte ret = 0;
            byte* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(byte)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe int readInt()
        {
            int ret = 0;
            int* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(int)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe uint readUint()
        {
            uint ret = 0;
            uint* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(uint)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe long readLong()
        {
            long ret = 0;
            long* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(long)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe ulong readUlong()
        {
            ulong ret = 0;
            ulong* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(ulong)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe short readShort()
        {
            short ret = 0;
            short* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(short)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe ushort readUShort()
        {
            ushort ret = 0;
            ushort* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(ushort)))
            {
                ret = 0;
            }
            return ret;
        }

        public unsafe float readFloat()
        {
            float ret = 0;
            float* outbuff = &ret;
            if (!readBuffer((byte*)outbuff, sizeof(float)))
            {
                ret = 0;
            }
            return ret;
        }

        public Vector3 readVec3()
        {
            return new Vector3(readFloat(), readFloat(), readFloat());
        }

        public Vector2 readVec2()
        {
            return new Vector2(readFloat(), readFloat());
        }

        public string readString()
        {
            short len = readShort();
            if (len <= 0 || m_index + len > max_length) return string.Empty;
            string ret = Encoding.UTF8.GetString(data, m_index, len);
            m_index += len;
            return ret;
        }

        public void close()
        {
            data = null;
            max_length = 0;
            m_index = 0;
        }


        public int RecordCount
        {
            get
            {
                return recordCount;
            }
        }

        public int Version
        {
            get { return version; }
        }

        public string Describe { get { return describe; } }

        private unsafe bool readBuffer(byte* buffer, int size)
        {
            if (m_index + size > max_length) return false;

            for (int i = 0; i < size; i++)
            {
                buffer[size - i - 1] = data[m_index++];
            }
            return true;
        }
    }

}