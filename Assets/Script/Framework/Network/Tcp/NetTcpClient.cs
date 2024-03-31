using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.IO;
using UnityEngine;


namespace Chengzi
{

    /// <summary>
    /// tcpClient网络连接、发送、接收
    /// </summary>
    public class NetTcpClient
    {

        // 端口
        private int _port;
        // 异步连接后从dns获取的ip地址:可能获得多个地址
        private IPAddress[] _addresses;
        // 等待信号量
        private AutoResetEvent _addressesSet;
        // 提供了通过网络连接、发送和接收数据的简单方法。用于在同步阻止模式下通过网络来连接、发送和接收流数据。
        private TcpClient _tcpClient;
        // 是否处于连接状态
        public bool _isConnected;

        /**
         * 0    NetClient.Close()
         * 1    打开地址和端口时抛出异常
         * 2    实际写入networkStream.BeginWrite时出现异常
         * 3    WriteCallback写入回调时出现异常
         * 4    tcpClient未能正常连接
         * 5    ReadCallBack中开始读取出现Exception
         * 6    ReadCallBack中循环读取出现异常
         * 7    读取的字节数量为0
         * 8    连接网络超时,超时时间10秒钟
         * 9    ip和端口未能正常打开
         * 10   发送时fcNetClient的p_IsConnected==false抛出异常
         */
        public int _errorStep;

        // 网络异常的错误消息
        public string _errorMessage = string.Empty;
        // 是否进行重新联网
        public bool _isReConnect;
        // 是否由于close导致连接失败
        public bool _isConnectFailed;
        // 连接失败次数：最大错误数量是IPAddress[] addresses数组长度
        private int _failedConnectionCount;
        // 网络读写流
        private NetworkStream _networkStream;
        // 读取缓冲区:4096字节
        private ByteBuffer _byteBuffer = new ByteBuffer();
        // 收到的封包容器
        private Queue<NetReceiveResult> _receiveResultQueue = new Queue<NetReceiveResult>();
        // 对象锁
        private object _lockResult = new object();
        // 消息头长度
        public const int MSG_HEAD_MAX = 8;
        // 读取数据包
        private MemoryStream _readStream;
        private BinaryReader _reader;

        /// <summary>
        /// 开启指定地址与端口
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool open(string hostNameOrAddress, int port)
        {
            close();
            try
            {
                _addressesSet = new AutoResetEvent(false);
                Dns.BeginGetHostAddresses(hostNameOrAddress, new AsyncCallback(getHostAddressesCallback), null);
            }
            catch (Exception exception)
            {
                Debug.LogError("Failed to Open : " + exception.Message + " address(" + hostNameOrAddress + ")");
                disconnect(1, true, exception.Message);
                return false;
            }
            this._port = port;
            return connect();
        }

        /// <summary>
        /// 委托AsyncCallback的引用方法
        /// </summary>
        /// <param name="result"></param>
        private void getHostAddressesCallback(IAsyncResult result)
        {
            Debug.Log("1.GetHostAddressesCallback");
            _addresses = Dns.EndGetHostAddresses(result);
            for (int i = 0; i < _addresses.Length; i++)
                Debug.Log(string.Format("2.addresses[{0}]:{1}", i, _addresses[i].ToString()));
            _addressesSet.Set();
        }

        /// <summary>
        /// 连接网络
        /// </summary>
        /// <returns></returns>
        private bool connect()
        {
            if (_byteBuffer != null)
            {
                _byteBuffer.Reset();
            }
            if (_tcpClient == null)
            {
                _tcpClient = new TcpClient();
            }
            if (_addressesSet != null)
            {
                _addressesSet.WaitOne();
            }
            Interlocked.Exchange(ref _failedConnectionCount, 0);
            Debug.Log("3.Unity NetClient Connect connected is " + _tcpClient.Connected);
            _tcpClient.BeginConnect(_addresses, _port, new AsyncCallback(connectCallback), null);

            return true;
        }

        /// <summary>
        /// Connect回调
        /// 未出现异常则开始网络流数据读取
        /// </summary>
        /// <param name="result"></param>
        private void connectCallback(IAsyncResult result)
        {
            Debug.Log("ConnectCallback");
            if (_tcpClient != null)
            {
                Debug.Log(string.Concat(new object[] { "4.Unity ConnectCallback result completed:", result.IsCompleted, string.Empty, result.AsyncState }));
                try
                {
                    _tcpClient.EndConnect(result);
                }
                catch (Exception exception)
                {
                    Interlocked.Increment(ref _failedConnectionCount);
                    if (_failedConnectionCount >= _addresses.Length)
                    {
                        disconnect(4, true, exception.Message);
                        return;
                    }
                }
                _tcpClient.NoDelay = true;
                _networkStream = _tcpClient.GetStream();
                _readStream = new MemoryStream();
                _reader = new BinaryReader(_readStream);
                byte[] buffer = new byte[512];
                _isConnected = true;
                _networkStream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(readCallback), buffer);
                Debug.Log("5.================================isConnected");
                //BusinessManager.Instance._roomBiz.isConnect();
                // 创建联网成功的异步消息
                NetReceiveResult NetReceiveResult = new NetReceiveResult((ushort)MsgCode.MSGCODE_CLIENT_NET_SUCCESS_RESP, "");
                lock (_lockResult)
                {
                    // 将封包放入容器
                    _receiveResultQueue.Enqueue(NetReceiveResult);
                }

            }
        }

        /// <summary>
        /// 读取服务器数据
        /// </summary>
        /// <param name="result"></param>
        private void readCallback(IAsyncResult result)
        {
            int numberOfBytesRead = 0;
            try
            {
                if (_networkStream == null)
                {
                    return;
                }

                numberOfBytesRead = _networkStream.EndRead(result);
            }
            catch (ArgumentException ae)
            {
                Debug.Log("asyncResult 为 空引用 :" + ae.Message);
                return;
            }
            catch (ObjectDisposedException ode)
            {
                Debug.Log("NetworkStream 是关闭的:" + ode.Message);
                return;
            }
            catch (IOException exception)
            {
                disconnect(5, true, exception.Message);
                return;
            }
            if (numberOfBytesRead == 0)
            {
                if (_isConnected)
                {
                    disconnect(7, true, string.Empty);
                }
                Debug.Log("=======================Net read = 0");
                return;
            }


            byte[] readBytes = result.AsyncState as byte[];
            _readStream.Seek(0, SeekOrigin.End);
            _readStream.Write(readBytes, 0, numberOfBytesRead);
            _readStream.Seek(0, SeekOrigin.Begin);

            try
            {
                ///包体是4位
                int messageNum = 0;
                while (_readStream.Length - _readStream.Position > MSG_HEAD_MAX)
                {
                    messageNum++;
                    _reader.ReadInt32();
                    ushort msgCode = _reader.ReadUInt16();
                    ushort bodyBytes = _reader.ReadUInt16();
#if DEBUG
                    if (msgCode != MsgCode.C2S_MSG_HEART.GetHashCode() && msgCode != MsgCode.S2C_MSG_HEART.GetHashCode())
                    {
                        //Debug.Log("msgCode:" + msgCode);
                    }
#endif
                    if (_readStream.Length - _readStream.Position >= bodyBytes)
                    {
                        byte[] arr = _reader.ReadBytes(bodyBytes);
                        if (NetworkManager.Instance._tcp._msgDict.ContainsKey((MsgCode)msgCode))
                        {
                            //构建封包
                            NetReceiveResult NetReceiveResult = new NetReceiveResult(msgCode, arr);
                            lock (_lockResult)
                            {
                                // 将封包放入容器
                                _receiveResultQueue.Enqueue(NetReceiveResult);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        _readStream.Position = _readStream.Position - 8;
                        break;
                    }
                }
                byte[] leftover = _reader.ReadBytes((int)(_readStream.Length - _readStream.Position));
                _readStream.SetLength(0);
                _readStream.Write(leftover, 0, leftover.Length);
                goto Label_ContinueRead;
            }
            catch (Exception e)
            {
                disconnect(6, true, e.Message);
                return;
            }

            Label_ContinueRead:
            _networkStream.BeginRead(readBytes, 0, readBytes.Length, new AsyncCallback(readCallback), readBytes);

            /*

                        byte[] readBytes = result.AsyncState as byte[];
                        try
                        {
                            byteBuffer.writeBytes(readBytes);
                            byte[] dataArr = byteBuffer.toBytes();
                            //包大小
                            //Debug.LogError("Package Size:" + byteBuffer.DataSize);
                            while (true)
                            {
                                //Debug.LogError("byteBuffer Size 1:" + byteBuffer.DataSize);
                                if (dataArr.Length < HeaderLength)
                                {
                                    //Debug.LogError("Package Error:" + byteBuffer.DataSize);
                                    byteBuffer.close();
                                    byteBuffer = new ByteBuffer();
                                    goto Lebel_ContinueRead;
                                }

                                //读取Header部分数据

                                //消息头默认[8byte] 
                                byte[] headerBytes = new byte[HeaderLength];
                                byteBuffer.ReadOnly(headerBytes, HeaderLength);
                                ByteBuffer headerBuffer = new ByteBuffer(headerBytes);

                                //保留4字节 不知道干嘛用的
                                int unknown = headerBuffer.readInt();
                                //消息编号[2byte]
                                ushort msgCode = headerBuffer.readShort();
                                //消息体长度[2byte]
                                ushort dataSize = headerBuffer.readShort();

                                Debug.LogError("BodySize:" + dataSize + "|MsgCode:" + msgCode);

                                //长度不足
                                if (byteBuffer - HeaderLength < dataSize)
                                {
                                    byteBuffer.ResetRead();
                                    goto Lebel_ContinueRead;
                                }

                                //byte[] bodyBytes = new byte[dataSize - 2];
                                byte[] headBytes = new byte[HeaderLength];
                                byte[] bodyBytes = new byte[dataSize];
                                //if (msgCode > 2000 && msgCode < 2500)
                                //{
                                //    if (byteBuffer.DataSize - HeaderLength < dataSize + 2)
                                //    {
                                //        byteBuffer.ResetRead();
                                //        Debug.Log("LeftSize:" + byteBuffer.DataSize);
                                //        goto Lebel_ContinueRead;
                                //    }
                                //    bodyBytes = new byte[dataSize + 2];
                                //}
                                //else
                                //{
                                //    bodyBytes = new byte[dataSize];
                                //}

                                //去掉已读过的字节
                                byteBuffer.GetBytes(headBytes, headBytes.Length);
                                byteBuffer.GetBytes(bodyBytes, bodyBytes.Length);
                                int cut_len = headBytes.Length + bodyBytes.Length;
                                byteBuffer.TrimData(cut_len);

                                Debug.LogError("byteBuffer Size 2:" + byteBuffer.DataSize);
                                //构建封包
                                //Debug.Log("Send Net Package");
                                NetReceiveResult NetReceiveResult = new NetReceiveResult(msgCode, bodyBytes);

                                //StringBuilder sb = new StringBuilder();
                                //for (int i = 0; i < bodyBytes.Length; i++)
                                //{
                                //    sb.Append(string.Format("{0:X2} ", bodyBytes[i]));
                                //}

                                //songrui modify 2016/3/8
                                //string s = NetTcpManager.Instance.m_RecvMsgNameHash[(MsgCode)msgCode];
                                //Debug.LogError("MsgCode:" + msgCode + "|Name:" + s);
                                //Debug.Log("_NET_RECV_ : msgCode(" + string.Format(s + " 0x{0:X4}", msgCode) + ") size(" + dataSize.ToString() + ") data(" + sb.ToString() + ")");

                                lock (lockResult)
                                {
                                    // 将封包放入容器
                                    receiveResultQueue.Enqueue(NetReceiveResult);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            Disconnect(6, true, exception.Message);
                            return;
                        }


                    Lebel_ContinueRead:
                        m_NetworkStream.BeginRead(readBytes, 0, readBytes.Length, new AsyncCallback(ReadCallback), readBytes);
            */
        }

        public void send(ProtocolMessage msg)
        {
            if (msg.type != NetTipType.NoTip)
            {
                //Debug.Log("当前：" + msg.getMsgCode());
            }
            send(msg.encode());
        }

        /// <summary>
        /// 发送字节
        /// </summary>
        /// <param name="ByteBuffer">byteBuffer.</param>
        public void send(ByteBuffer byteBuffer)
        {
            if (_isConnected == false)
            {
                //disconnect(10, true, "客户端TcpClient连接已经关闭");
            }
            else
            {
                sendPacket(byteBuffer);
            }
        }


        /// <summary>
        /// 实际发送
        /// </summary>
        /// <param name="ByteBuffer">byteBuffer.</param>
        private void sendPacket(ByteBuffer byteBuffer)
        {
            try
            {
                // 写入networkStream
                byte[] data = byteBuffer.toBytes();
                _networkStream.BeginWrite(data, 0, data.Length, new AsyncCallback(sendCallback), null);
            }
            catch (ObjectDisposedException ode)
            {
                Debug.Log("NetworkStream 是关闭的:" + ode.Message);
                return;
            }
            catch (Exception exception)
            {
                disconnect(2, true, exception.Message);
            }
        }

        /// <summary>
        /// 发送回调
        /// </summary>
        /// <param name="result">Result.</param>
        private void sendCallback(IAsyncResult result)
        {
            try
            {
                if (_networkStream != null)
                {
                    _networkStream.EndWrite(result);
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (Exception exception)
            {
                disconnect(3, true, exception.Message);
            }
        }

        /// <summary>
        /// 将消息放入容器
        /// </summary>
        /// <returns></returns>
        public bool dispatch()
        {
            NetReceiveResult result = null;
            lock (_lockResult)
            {
                while (_receiveResultQueue.Count > 0)
                {
                    result = _receiveResultQueue.Dequeue();
                    if (result != null)
                    {
                        AbstractMessage msg = Tcp.findMessage((MsgCode)result.m_usMsgCode);
                        if (msg == null)
                            continue;
                        if (msg is ProtocolMessage)
                        {
                            ByteBuffer buffer = new ByteBuffer();
                            buffer.writeBytesArr(result.m_Data);
                            msg.decode(buffer);
                            NetworkManager.Instance._tcp._messageManager.addMessage(msg);
                        }
                        else if (msg is AsynchronizedMessage)
                        {
                            ((AsynchronizedMessage)msg)._asynMsg = result.m_AsynMsg;
                            msg.update();
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 断开tcpClient连接
        /// </summary>
        /// <param name="iErrorStep">错误步骤</param>
        /// <param name="bReconnect">是否重新连接</param>
        /// <param name="strErrorString"></param>
        public void disconnect(int iErrorStep, bool bReconnect, string strErrorString)
        {
            // 断开tcpClient连接
            if (_tcpClient != null)
            {
                _tcpClient.Close();
                _tcpClient = null;
                _isConnected = false;
            }

            if (_reader != null)
            {
                _reader.Close();
            }

            if (_readStream != null)
            {
                _readStream.Close();
            }

            _errorStep = iErrorStep;
            _isReConnect = bReconnect;
            _errorMessage = "DisconnectError : " + strErrorString + ",ErrorStep:" + _errorStep;
            Debug.LogError(_errorMessage);
            // 创建联网成功的异步消息
            NetReceiveResult NetReceiveResult = new NetReceiveResult((ushort)MsgCode.MSGCODE_CLIENT_NET_ERROR_RESP, _errorMessage);
            lock (_lockResult)
            {
                // 将封包放入容器
                _receiveResultQueue.Enqueue(NetReceiveResult);
            }


        }

        /// <summary>
        /// 关闭网络连接
        /// </summary>
        public void close()
        {
            Debug.Log("================================Net Close");
            _isReConnect = false;
            _isConnectFailed = false;
            _errorStep = 0;
            _isConnected = false;
            try
            {
                if (_tcpClient != null)
                {
                    if (_networkStream != null)
                    {
                        _networkStream.Close();
                    }

                    if (_reader != null)
                    {
                        _reader.Close();
                    }

                    if (_readStream != null)
                    {
                        _readStream.Close();
                    }

                    _tcpClient.Close();
                    _tcpClient = null;
                }
                _receiveResultQueue.Clear();
            }
            catch (Exception exception)
            {
                _errorMessage = exception.Message;
                Debug.LogError("Net Error - Close : " + _errorMessage);
            }
        }

    }
}
