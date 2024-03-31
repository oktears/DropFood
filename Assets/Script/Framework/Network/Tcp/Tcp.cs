using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections;
using UnityEngine;


namespace Chengzi
{

    /// <summary>
    /// TcpЭ�������
    /// </summary>
    public class Tcp
    {

        // ��װTcpClient
        public NetTcpClient _netTcpClient = new NetTcpClient();
        // ��Ϣ����
        public MessageManager _messageManager = new MessageManager();
        // Handler����
        public static List<AbstractMessageHandler> _handlerList = new List<AbstractMessageHandler>();
        public LoginMessageHandler _loginHandler { get; set; }
        public AsynMessageHandler _asyncHandler { get; set; }

        // ����ʱ�������
        private float _heartbeatTime = 0.0f;
        // ������Ϣ����������
        public Dictionary<MsgCode, Type> _msgDict = new Dictionary<MsgCode, Type>();
        // ��һ��������ʱ��
        public static float _lastRealtimeSinceStartup = 0;
        // �������
        public static float HEARTBEAT_INTEVER = 10.0f;


        public void init()
        {
            initHandler();
        }

        public void update()
        {
            if (_netTcpClient._isConnected)
            {
                _heartbeatTime += Time.deltaTime;
                if (_heartbeatTime >= HEARTBEAT_INTEVER)
                {
                    _heartbeatTime = 0.0f;
                    _loginHandler.sendPlayerHeartbeatRequest();
                    _lastRealtimeSinceStartup = Time.realtimeSinceStartup;
                }
            }

            _messageManager.update();
            _netTcpClient.dispatch();
        }

        /// <summary>
        /// handlerʵ�����������HandlerList����
        /// </summary>
        private void initHandler()
        {
            _loginHandler = new LoginMessageHandler();
            _asyncHandler = new AsynMessageHandler();


            addHandler(_loginHandler);
            addHandler(_asyncHandler);



            //_msgDict.Add(MsgCode.C2S_MSG_HEART, typeof(msg_heart_test));
            //_msgDict.Add(MsgCode.S2C_MSG_HEART, typeof(msg_heart_test));
        }

        /// <summary>
        ///  ����tcp������
        /// </summary>
        /// <param name="strIP"></param>
        /// <param name="iPort"></param>
        /// <returns></returns>
        public bool connect(string strIP, int iPort)
        {
            //strIP = "192.168.124.252";
            //iPort = 8999;
            strIP = HttpProtocolId.game_Url;
            iPort = HttpProtocolId.socket_Port;
            if (!_netTcpClient.open(strIP, iPort))
            {
                return false;
            }
            NetDebug.Log("Connecting .....", "None");
            return true;
        }

        /// <summary>
        /// �Ͽ�����
        /// </summary>
        public void close()
        {
            _netTcpClient.close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public static void addHandler(AbstractMessageHandler handler)
        {
            _handlerList.Add(handler);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msgCode"></param>
        /// <returns></returns>
        public static AbstractMessage findMessage(MsgCode msgCode)
        {
            AbstractMessage message = null;
            foreach (AbstractMessageHandler handler in _handlerList)
            {
                message = handler.findMessage(msgCode);
                if (message != null)
                {
                    return message;
                }
            }
            NetDebug.LogError("Not Found Message Error - msgCode(" + msgCode.ToString() + ")", "None");
            return message;
        }

    }
}
