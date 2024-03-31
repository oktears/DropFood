using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public enum DownLoadType
    {
        /// <summary>下载头像</summary>
        Head,

        /// <summary>上传声音</summary>
        PostVoice,

        /// <summary>下载声音</summary>
        GetVoice,
    }

    public abstract class DownLoadRequest
    {
        public DownLoadType type;

        public abstract WWW getWWW();
    }
}
