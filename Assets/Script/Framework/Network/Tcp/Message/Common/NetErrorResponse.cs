
namespace Chengzi
{
    public class NetErrorResponse : AsynchronizedMessage
    {
        public NetErrorResponse()
        {
            setMsgCode((ushort)MsgCode.MSGCODE_CLIENT_NET_ERROR_RESP);
        }

        public override bool update()
        {
            base.update();
            // 通知界面连接异常

            //NotificationCenter.getInstance().notify(Event.NET_PAUSE_RACE, null);
            //CommonViewManager.Instance.showTip(_asynMsg);
            // 2017.11.9 后面再做处理
            //ViewEvent viewEvent = null;
            //viewEvent._bundleData.Putstring("msg", m_AsynMsg);
            //ViewEventHandler.HandleEvent(this, viewEvent);

            return true;
        }
    }
}
