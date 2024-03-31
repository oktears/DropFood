using UnityEngine;
using System.Collections;

namespace Chengzi
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AbstractMessageHandler
    {

        public AbstractMessageHandler()
        {
        }

        public abstract AbstractMessage findMessage(MsgCode msgCode);
    }

}
