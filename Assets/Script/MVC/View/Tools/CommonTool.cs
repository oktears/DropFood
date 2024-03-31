using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    public static class CommonTool
    {
        public static string MD5(string s)
        {
            char[] hexDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                    'a', 'b', 'c', 'd', 'e', 'f' };
            try
            {
                byte[] strTemp = System.Text.Encoding.Default.GetBytes(s);

                //MessageDigest mdTemp = MessageDigest.getInstance("MD5");
                //mdTemp.update(strTemp);
                //byte[] md = mdTemp.digest();
                System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                byte[] md = md5.ComputeHash(strTemp);
                //string cl1 = str;
                //string pwd = "";
                //System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
                //byte[] s = md5.ComputeHash(Encoding.Unicode.GetBytes(cl1));


                int j = md.Length;
                char[] str = new char[j * 2];
                int k = 0;
                for (int i = 0; i < j; i++)
                {
                    byte byte0 = md[i];
                    str[k++] = hexDigits[rightMove(byte0, 4) & 0xf];
                    str[k++] = hexDigits[byte0 & 0xf];
                }
                return new string(str);
            }
            catch (Exception e)
            {
                return "";
            }

        }
        public static int rightMove(byte value, int pos)
        {

            if (pos != 0)  //移动 0 位时直接返回原值
            {
                byte mask = byte.MaxValue;     // int.MaxValue = 0x7FFFFFFF 整数最大值
                value >>= 1;     //第一次做右移，把符号也算上，无符号整数最高位不表示正负            但操作数还是有符号的，有符号数右移1位，正数时高位补0，负数时高位补1
                value &= mask;     //和整数最大值进行逻辑与运算，运算后的结果为忽略表示正负值的最高位
                value >>= pos - 1;     //逻辑运算后的值无符号，对无符号的值直接做右移运算，计算剩下的位
            }
            return value;
        }

        public static byte[] FloatToByte(float[] ff)
        {

            byte[] value = new byte[ff.Length * 4];
            for (int i = 0; i < ff.Length; i++)
            {
                byte[] tempList = BitConverter.GetBytes(ff[i]);
                for (int j = 0; j < 4; j++)
                {
                    value[i * 4 + j] = tempList[j];
                }
            }
            return value;
        }

        public static float[] byteToFloat(byte[] bb)
        {
            float[] ff = new float[(int)(bb.Length * 0.25f + 0.01f)];
            for (int i = 0; i < ff.Length; i++)
            {
                ff[i] = BitConverter.ToSingle(bb, i * 4);
            }
            return ff;
        }

        /**
         * 将低字节数组转换为int
         * 
         * @param b
         *            byte[]
         * @return int
         */
        public static int lBytesToInt(byte[] b)
        {
            do
            {
                int s = 0;
                for (int i = 0; i < 3; i++)
                {
                    if (b[3 - i] >= 0)
                    {
                        s = s + b[3 - i];
                    }
                    else
                    {
                        s = s + 256 + b[3 - i];
                    }
                    s = s * 256;
                }
                if (b[0] >= 0)
                {
                    s = s + b[0];
                }
                else
                {
                    s = s + 256 + b[0];
                }
                return s;
            } while (false);
        }

        /**
         * 截取数组
         * 
         * @param b
         *            源数组
         * @param begin
         *            开始截取下标
         * @param size
         *            截取大小
         * @return byte[]
         */
        public static byte[] getPartOfBytes(byte[] b, int begin, int size)
        {
            //LogUtils.debug(Tag, "getPartOfBytes-----length-" + b.Length + "-----begin-" + begin + "-----size-" + size);
            byte[] b1 = new byte[size];
            try
            {
                Array.Copy(b, begin, b1, 0, size);
            }
            catch (Exception e)
            {
                //Debug.Log(e.ToString());
            }
            //System.arraycopy(b, begin, b1, 0, size);
            return b1;
        }

        /// <summary>地址</summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string bytes2String(byte[] bytes)
        {
            if (bytes != null && bytes.Length > 0)
            {
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
            return "";

        }
    }
}
