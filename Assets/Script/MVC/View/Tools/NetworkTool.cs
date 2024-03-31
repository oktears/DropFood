using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

namespace Chengzi
{
    public static class NetworkTool
    {
        public static void Get_AudioUrl(string str, ref string err, ref string mess)
        {
            //FileIO.Debug_Write(str); 
            str = str.Remove(str.Length - 1, 1).Remove(0, 1).Trim();
            //FileIO.Debug_Write(str);


            string[] tempStr = str.Split(',');
            for (int i = 0; i < tempStr.Length; i++)
            {
                tempStr[i] = tempStr[i].Trim();
                //FileIO.Debug_Write(tempStr[i]);
                tempStr[i] = tempStr[i].Remove(0, 1);
                //FileIO.Debug_Write(tempStr[i]);
                string para = "";
                string value = "";
                int index = 0;
                char tempChar = tempStr[i][index];
                while (tempChar != '"')
                {
                    para += tempChar;
                    index++;
                    tempChar = tempStr[i][index];
                    if (index >= tempStr[i].Length)
                        break;
                }
                if (para.Equals("errorCode"))
                {
                    index += 3;
                    if (index >= tempStr[i].Length)
                        break;
                    tempChar = tempStr[i][index];

                    while (tempChar != '"')
                    {
                        value += tempChar;
                        index++;
                        tempChar = tempStr[i][index];
                        if (index >= tempStr[i].Length)
                            break;
                    }

                    err = value;
                    //FileIO.Debug_Write("userID=" + para);
                }
                if (para.Equals("errorMsg"))
                {
                    index += 3;
                    if (index >= tempStr[i].Length)
                        break;
                    tempChar = tempStr[i][index];

                    while (tempChar != '"')
                    {
                        value += tempChar;
                        index++;
                        tempChar = tempStr[i][index];
                        if (index >= tempStr[i].Length)
                            break;
                    }

                    mess = value;
                    //FileIO.Debug_Write("token=" + para);
                }
                else
                {
                    continue;
                }
            }
        }

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
                Debug.Log(e.ToString());
            }
            //System.arraycopy(b, begin, b1, 0, size);
            return b1;
        }
    }
}
