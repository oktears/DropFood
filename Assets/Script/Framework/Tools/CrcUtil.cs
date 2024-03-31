using System;
using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// CRC校验工具类
    /// </summary>
    public class CrcUtil
    {
        public static long GetCrc32(byte[] data)
        {
            return GetCrc32(data, 0, data.Length);
        }

        public static long GetCrc32(byte[] data, int off, int length)
        {
            Crc32 crc32 = new Crc32();
            crc32.Crc(data, off, length);
            return crc32.Value;
        }


    }
}
