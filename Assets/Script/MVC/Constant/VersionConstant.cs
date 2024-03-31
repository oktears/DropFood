using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    /// <summary>
    /// 版本号常量
    /// </summary>
    public class VersionConstant
    {

        /// <summary>
        /// 客户端版本号
        /// </summary>
        public const int CLIENT_VERSION_CODE = 92;

        /// <summary>
        /// 客户端版本名
        /// </summary>
        public const string CLIENT_VERSION_NAME = "0.00.92";

        /// <summary>
        /// 资源版本号
        /// </summary>
        public const int PACKAGE_RES_VERSION = 163;

        /// <summary>
        /// Http头版本号
        /// </summary>
        public static byte HTTP_PROTOCOL_HEAD_VERSION = 0;

        /// <summary>
        /// HttpBody版本号
        /// </summary>
        public static short HTTP_PROTOCOL_BODY_VERSION = 8;
    }
}
