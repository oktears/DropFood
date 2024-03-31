using UnityEngine;
using System.Collections;
using System;
using System.Linq;

namespace Chengzi
{
    public class LoginMessageHandler : AbstractMessageHandler
    {

        public override AbstractMessage findMessage(MsgCode msgCode)
        {
            AbstractMessage msg = null;
            switch (msgCode)
            {
                case MsgCode.S2C_MSG_LOGIN_REP:

                    break;
                case MsgCode.S2C_MSG_HEART:

                    break;
            }
            return msg;
        }

        /// <summary>
        /// 发送心跳请求
        /// </summary>
        public void sendPlayerHeartbeatRequest()
        {

            //NetworkManager.Instance._tcp._netTcpClient.send(req);
        }

        /// <summary>
        /// 发送登陆请求
        /// </summary>
        public void sendPlayerLoginRequest()
        {
            //NetworkManager.Instance._tcp._netTcpClient.send(req);
        }

        /// <summary>
        /// 发送Index请求
        /// </summary>
        public void sendIndexRequest()
        {
        }

    }

}
