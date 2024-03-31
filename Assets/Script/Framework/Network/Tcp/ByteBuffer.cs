using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    public class ByteBuffer
    {

        MemoryStream stream = null;
        BinaryWriter writer = null;
        BinaryReader reader = null;

        public ByteBuffer()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        public ByteBuffer(byte[] data)
        {
            if (data != null)
            {
                stream = new MemoryStream(data);
                reader = new BinaryReader(stream);
            }
            else
            {
                stream = new MemoryStream();
                writer = new BinaryWriter(stream);
            }
        }

        public void close()
        {
            if (writer != null) writer.Close();
            if (reader != null) reader.Close();

            stream.Close();
            writer = null;
            reader = null;
            stream = null;
        }

        public void writeByte(byte v)
        {
            writer.Write(v);
        }

        public void writeInt(int v)
        {
            writer.Write((int)v);
        }

        public void writeUInt(uint v)
        {
            writer.Write(v);
        }

        public void writeUShort(ushort v)
        {
            writer.Write((ushort)v);
        }

        public void writeLong(long v)
        {
            writer.Write((long)v);
        }

        public void writeUlong(ulong v)
        {
            writer.Write((ulong)v);
        }

        public void writeFloat(float v)
        {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToSingle(temp, 0));
        }

        public void writeDouble(double v)
        {
            byte[] temp = BitConverter.GetBytes(v);
            Array.Reverse(temp);
            writer.Write(BitConverter.ToDouble(temp, 0));
        }

        public void writeString(string v)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(v);
            writer.Write((ushort)bytes.Length);
            writer.Write(bytes);
        }

        public void writeBytes(byte[] v)
        {
            //writer.Write((int)v.Length);
            writer.Write((ushort)v.Length);
            writer.Write(v);
        }

        public void writeBytesArr(byte[] v)
        {
            writer.Write(v);
        }

        public byte readByte()
        {
            return reader.ReadByte();
        }

        public int readInt()
        {
            return (int)reader.ReadInt32();
        }

        public ushort readShort()
        {
            return (ushort)reader.ReadInt16();
        }

        public long readLong()
        {
            return (long)reader.ReadInt64();
        }

        public float readFloat()
        {
            byte[] temp = BitConverter.GetBytes(reader.ReadSingle());
            Array.Reverse(temp);
            return BitConverter.ToSingle(temp, 0);
        }

        public double readDouble()
        {
            byte[] temp = BitConverter.GetBytes(reader.ReadDouble());
            Array.Reverse(temp);
            return BitConverter.ToDouble(temp, 0);
        }

        public string readString()
        {
            ushort len = readShort();
            byte[] buffer = new byte[len];
            buffer = reader.ReadBytes(len);
            return Encoding.UTF8.GetString(buffer);
        }

        public byte[] readBytes()
        {
            int len = readInt();
            return reader.ReadBytes(len);
        }

        public byte[] toBytes()
        {
            writer.Flush();
            return stream.ToArray();
        }

        public void flush()
        {
            writer.Flush();
        }

        /********************** Old ***********************/

        // ByteBuffer的整体容量
        protected int capacity;
        // 实际数据
        protected byte[] data;
        // 可用字节大小
        protected int freeSize;
        // 可以读取的最开始位置
        protected int readPos;
        // 数据长度
        protected int dataSize;
        // 可以写入的最后位置
        protected int writePos;

        /// <summary>
        /// 是否自动增长
        /// </summary>
        protected bool autoExpand = true;

        public void enableAutoExpand()
        {
            autoExpand = true;
        }

        //public ByteBuffer()
        //{
        //    Reset();
        //}

        //public ByteBuffer(int capacity)
        //{
        //    this.capacity = capacity;
        //    Reset();
        //    data = new byte[capacity];
        //}

        //public ByteBuffer(byte[] bytes)
        //{
        //    capacity = bytes.Length;
        //    Reset();
        //    data = new byte[capacity];
        //    PutBytes(bytes, bytes.Length);
        //}

        //public ByteBuffer(byte[] bytes, int length)
        //{
        //    capacity = length;
        //    Reset();
        //    data = new byte[capacity];
        //    PutBytes(bytes, length);
        //}

        //public static ByteBuffer operator +(ByteBuffer cb1, ByteBuffer cb2)
        //{
        //    ByteBuffer buffer = new ByteBuffer(cb1.Capacity + cb2.Capacity);
        //    buffer.PutBytes(cb1.Data, cb1.Capacity);
        //    buffer.PutBytes(cb2.Data, cb2.Capacity);
        //    return buffer;
        //}

        public void ResetRead()
        {
            readPos = 0;
        }

        public void Reset()
        {
            freeSize = Capacity;
            readPos = 0;
            writePos = 0;
            dataSize = 0;
        }

        /// <summary>
        /// bool
        /// </summary>
        /// <returns></returns>
        public bool GetBool()
        {
            return (bool)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Boolean);
        }

        public void PutBool(bool obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// bool[]
        /// </summary>
        /// <param name="obj"></param>
        public void GetBoolArray(bool[] obj)
        {
            EncodeDecodeStream.GetObject(this, obj);
        }

        /// <summary>
        /// byte
        /// </summary>
        /// <returns></returns>
        public byte GetByte()
        {
            return (byte)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Byte);
        }

        public void PutByte(byte obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// uint16 or ushort
        /// </summary>
        /// <returns></returns>
        public ushort GetUInt16()
        {
            return (ushort)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_UInt16);
        }

        public void PutUShort(ushort obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// int16 or short
        /// </summary>
        /// <returns></returns>
        public short GetInt16()
        {
            return (short)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Int16);
        }

        public void PutShort(short obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// uint or uint32
        /// </summary>
        /// <returns></returns>
        public uint GetUInt()
        {
            return (uint)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_UInt32);
        }

        public void PutUInt(uint obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// int or int32
        /// </summary>
        /// <returns></returns>
        public int GetInt()
        {
            return (int)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Int32);
        }

        public void PutInt(int obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// int[]
        /// </summary>
        /// <param name="intArray"></param>
        public void GetIntArray(int[] intArray)
        {
            EncodeDecodeStream.GetObject(this, intArray);
        }

        public void PutIntArray(int[] obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// ulong
        /// </summary>
        /// <returns></returns>
        public ulong GetULong()
        {
            return (ulong)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_UInt64);
        }

        public void PutULong(ulong obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// long
        /// </summary>
        /// <returns></returns>
        public long GetLong()
        {
            return (long)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Int64);
        }

        public void PutLong(long obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// long[]
        /// </summary>
        /// <returns></returns>
        public void GetLongArray(long[] longArray)
        {
            EncodeDecodeStream.GetObject(this, longArray);
        }

        public void PutLongArray(long[] longArray)
        {
            EncodeDecodeStream.PutObject(this, longArray);
        }

        /// <summary>
        /// float or single
        /// </summary>
        /// <returns></returns>
        public float GetFloat()
        {
            return (float)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Single);
        }

        public void PutFloat(float obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// double
        /// </summary>
        /// <returns></returns>
        public double GetDouble()
        {
            return (double)EncodeDecodeStream.GetObject(this, EncodeDecodeStream.Index_Double);
        }

        public void PutDouble(double obj)
        {
            EncodeDecodeStream.PutObject(this, obj);
        }

        /// <summary>
        /// string
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            string value = string.Empty;
            short size = GetInt16();
            if (size <= 0)
                return value;
            else
            {
                byte[] data = GetBytes(size);
                value = Encoding.UTF8.GetString(data, 0, size);
            }
            return value;
        }

        public void PutString(string str)
        {
            if (str == null) str = string.Empty;
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            short length = (short)bytes.Length;
            PutShort(length);
            PutBytes(bytes);
        }

        /// <summary>
        /// Vector3
        /// </summary>
        public Vector3 GetVector3()
        {
            Vector3 v = new Vector3();
            v.x = GetFloat();
            v.y = GetFloat();
            v.z = GetFloat();
            return v;
        }

        public void PutVector3(Vector3 v3)
        {
            PutFloat(v3.x);
            PutFloat(v3.y);
            PutFloat(v3.z);
        }

        /// <summary>
        /// 读取byte数组
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public byte[] GetBytes(int count)
        {
            byte[] buffer = new byte[count];
            GetBytes(buffer, count);
            return buffer;
        }

        /// <summary>
        /// 读取byte数组
        /// </summary>
        /// <param name="byte_stream"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public int GetBytes(byte[] byte_stream, int count)
        {
            if (count == 0)
            {
                return 0;
            }
            if (count > byte_stream.Length)
            {
                return -1;
            }
            if (count <= (Capacity - ReadPos))
            {
                Array.Copy(data, ReadPos, byte_stream, 0, count);
                readPos += count;

                if (ReadPos == Capacity)
                {
                    readPos = 0;
                }
            }
            else
            {
                int length = Capacity - ReadPos;
                Array.Copy(data, ReadPos, byte_stream, 0, length);
                int num2 = count - length;
                Array.Copy(data, 0, byte_stream, length, num2);
                readPos = num2;
            }
            //dataSize -= count;
            //            freeSize += count;

            return count;
        }

        /// <summary>
        /// 只读模式
        /// </summary>
        /// <param name="byte_stream"></param>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public int ReadOnly(byte[] byte_stream, int bytes)
        {
            if (bytes == 0)
            {
                return 0;
            }
            if (DataSize < bytes)
            {
                return -1;
            }
            if (byte_stream.Length < bytes)
            {
                return -2;
            }
            if (bytes <= (Capacity - ReadPos))
            {
                Array.Copy(data, ReadPos, byte_stream, 0, bytes);
                return bytes;
            }
            int length = Capacity - ReadPos;
            Array.Copy(data, ReadPos, byte_stream, 0, length);
            int num2 = bytes - length;
            Array.Copy(data, 0, byte_stream, length, num2);
            return bytes;
        }

        public int PutBytes(byte[] writeBytes)
        {
            int writeByteCnt = writeBytes.Length;
            return PutBytes(writeBytes, writeByteCnt);
        }

        public void TrimData(int offset)
        {
            if (offset > dataSize)
            {
                Debug.LogError(string.Format("offset:{0},data:{1}", offset, dataSize));
            }
            Array.Copy(data, offset, data, 0, dataSize - offset);
            dataSize -= offset;
            writePos -= offset;
            readPos -= offset;
            freeSize += offset;
            if (readPos <= 0) readPos = 0;
        }


        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="writeBytes"></param>
        /// <param name="writeByteCnt"></param>
        /// <returns></returns>
        public int PutBytes(byte[] writeBytes, int writeByteCnt)
        {
            if (writeByteCnt == 0)
            {
                return 0;
            }

            externCapacity(writeByteCnt);

            if (FreeSize < writeByteCnt)
            {
                return -1;
            }


            if (writeByteCnt <= (Capacity - WritePos))
            {
                Array.Copy(writeBytes, 0, data, WritePos, writeByteCnt);
                writePos += writeByteCnt;
                //                if (WritePos == Capacity)
                //                {
                //                    writePos = 0;
                //                }
            }
            else
            {
                int length = Capacity - WritePos;
                Array.Copy(writeBytes, 0, data, WritePos, length);
                int num2 = writeByteCnt - length;
                Array.Copy(writeBytes, length, data, 0, num2);
                writePos = num2;
            }
            dataSize += writeByteCnt;
            freeSize -= writeByteCnt;
            return writeByteCnt;
        }

        protected void externCapacity(int len)
        {
            if (autoExpand && len > (Capacity - writePos))
            {
                int new_size = Capacity + len * 2;
                byte[] new_data = new byte[new_size];
                Array.Copy(data, 0, new_data, 0, dataSize);
                data = new_data;
                capacity = new_size;
                freeSize = new_size - dataSize;
                Debug.Log("ByteBuffer容量不足，扩展空间");
            }
        }

        /// <summary>
        /// 容量
        /// </summary>
        public int Capacity
        {
            get
            {
                return capacity;
            }
        }

        /// <summary>
        /// byte数据
        /// </summary>
        public byte[] Data
        {
            get
            {
                return data;
            }
        }

        /// <summary>
        /// 剩余可写入字节
        /// </summary>
        public int FreeSize
        {
            get
            {
                return freeSize;
            }
        }

        /// <summary>
        /// 读取位置
        /// </summary>
        public int ReadPos
        {
            get
            {
                return readPos;
            }
        }

        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataSize
        {
            get
            {
                return dataSize;
            }
        }

        /// <summary>
        /// 写入位置
        /// </summary>
        public int WritePos
        {
            get
            {
                return writePos;
            }
        }
    }
}
