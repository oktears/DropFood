using UnityEngine;
using System.Collections;
using System;


namespace Chengzi
{
    public class AsynMessageHandler : AbstractMessageHandler
    {
        public AsynMessageHandler()
            : base()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgCode"></param>
        /// <returns></returns>
        public override AbstractMessage findMessage(MsgCode msgCode)
        {
            AbstractMessage msg = null;
            switch (msgCode)
            {
                case MsgCode.MSGCODE_CLIENT_NET_SUCCESS_RESP:
                    msg = new NetSuccessResponse();
                    break;
                case MsgCode.MSGCODE_CLIENT_NET_ERROR_RESP:
                    msg = new NetErrorResponse();
                    break;
            }
            return msg;
        }
    }

}
