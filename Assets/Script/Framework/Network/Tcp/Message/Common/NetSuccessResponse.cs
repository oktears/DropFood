using UnityEngine;
using System.Collections;


namespace Chengzi
{
    public class NetSuccessResponse : AsynchronizedMessage
    {
        public NetSuccessResponse()
        {
            setMsgCode((ushort)MsgCode.MSGCODE_CLIENT_NET_SUCCESS_RESP);
        }

        public override bool update()
        {
            base.update();
            //连接成功后发一次心跳
            NetworkManager.Instance._tcp._loginHandler.sendPlayerLoginRequest();
            // 通知界面连接成功
            //ViewEvent viewEvent = new ViewEvent(ViewConstant.ViewId.PVP_LOBBY, 12);
            //ViewEventHandler.HandleEvent(this, viewEvent);
            Debug.Log("tcp连接成功!"); 
            return true;
        }
    }
}
