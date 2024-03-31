using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 数据加密工具类
    /// </summary>
    public class EncryptUtil
    {

        public static void EncryptData(ref byte[] data, int chunkSize, int startIndex)
        {
            int len = data.Length - startIndex;
            if (chunkSize <= 0 || chunkSize >= len)
            {
                return;
            }

            byte[] tmpArray = new byte[len];
            Array.Copy(data, startIndex, tmpArray, 0, len);

            int chunkNum = len / chunkSize;
            int oddSize = len % chunkSize;
            int srcPos = len;
            int destPos = startIndex;

            if (oddSize != 0)
            {
                srcPos -= oddSize;
                Array.Copy(tmpArray, srcPos, data, destPos, oddSize);
                destPos += oddSize;
            }

            for (int i = chunkNum - 1; i >= 0; i--)
            {
                srcPos -= chunkSize;
                Array.Copy(tmpArray, srcPos, data, destPos, chunkSize);
                destPos += chunkSize;
            }
        }

        public static void DecryptData(ref byte[] data, int chunkSize, int startIndex)
        {
            int len = data.Length - startIndex;
            if (chunkSize <= 0 || chunkSize >= len)
            {
                return;
            }

            byte[] tmpArray = new byte[len];
            Array.Copy(data, startIndex, tmpArray, 0, len);

            int chunkNum = len / chunkSize;
            int oddSize = len % chunkSize;
            int srcPos = 0;
            int destPos = startIndex + len;

            if (oddSize != 0)
            {
                destPos -= oddSize;
                Array.Copy(tmpArray, srcPos, data, destPos, oddSize);
                srcPos += oddSize;
            }

            for (int i = chunkNum - 1; i >= 0; i--)
            {
                destPos -= chunkSize;
                Array.Copy(tmpArray, srcPos, data, destPos, chunkSize);
                srcPos += chunkSize;
            }
        }


        public static string SimpleEncrypt(string data)
        {

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(data);
            string encryptedData = string.Empty;
            try
            {
                foreach (byte b in bytes)
                {
                    string s = string.Format("{0:x}", b);
                    if (s.Length < 2)
                        s = "0" + s;
                    encryptedData += s;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("[XTool] SimpleEncrypt Error! " + e.Message);
            }
            return encryptedData;
        }

        public static string SimpleDecrypt(string encryptedData)
        {

            try
            {
                List<byte> listByte = new List<byte>();
                int i = 0;
                int len = encryptedData.Length;
                while (i < len)
                {
                    int segementLen = 2;
                    if (len - i == 1)
                        segementLen = 1;
                    string segement = encryptedData.Substring(i, segementLen);
                    byte b = byte.Parse(segement, System.Globalization.NumberStyles.HexNumber);
                    listByte.Add(b);
                    i += 2;
                }
                byte[] bytes = listByte.ToArray();
                string _name = System.Text.Encoding.UTF8.GetString(bytes);
                return _name;
            }
            catch (System.Exception e)
            {
                Debug.LogError("[XTool] SimpleDecrypt Error! " + e.Message);
                return "";
            }
        }


    }
}
