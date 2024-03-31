using System;
using System.Collections.Generic;

namespace Chengzi
{
    public class DataUtil
    {

        public static void Reverse(ref byte[] data, int length)
        {
            byte tmp = 0;
            if (length <= 0)
            {
                length = data.Length;
            }
            for (int i = 0; i < length / 2; i++)
            {
                tmp = data[i];
                data[i] = data[length - i - 1];
                data[length - i - 1] = tmp;
            }
        }


        public static void Reverse(ref byte[] data)
        {
            byte tmp = 0;
            int length = data.Length;
            for (int i = 0; i < length / 2; i++)
            {
                tmp = data[i];
                data[i] = data[length - i - 1];
                data[length - i - 1] = tmp;
            }
        }

        public static short Reverse(short value)
        {
            int ret = 0;
            byte size = sizeof(short);
            for (int i = 0; i < size; i++)
            {
                ret += ((value >> (size - i - 1) * 8) & 0xff) << (i * 8);
            }
            return (short)ret;
        }

        public static int Reverse(int value)
        {
            int ret = 0;
            byte size = sizeof(int);
            for (int i = 0; i < size; i++)
            {
                ret += ((value >> (size - i - 1) * 8) & 0xff) << (i * 8);
            }
            return ret;
        }

        public static long Reverse(long value)
        {
            long ret = 0;
            byte size = sizeof(long);
            for (int i = 0; i < size; i++)
            {
                ret += ((value >> (size - i - 1) * 8) & 0xff) << (i * 8);
            }
            return ret;
        }
    }

}
