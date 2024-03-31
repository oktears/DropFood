using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Chengzi
{
    public class HttpRequestQueue
    {
        /// <summary>请求超时时间</summary>
        protected const int TIME_OUT_SECOND = 10;


        ////////////////////////////////////////////////////////

        /// <summary>请求列表</summary>
        public List<HttpRequest> _requestList { get; private set; }

        /// <summary>当前请求</summary>
        protected HttpRequest _curRequest { get; private set; }

        /// <summary></summary>
        private bool _isbusy = false;

        /// <summary>这次请求的开始时间</summary>
        private long _startTime = 0;

        /// <summary>请求重连时间</summary>
        private long _curTime = 0;

        public bool _isBlock = false;

        public HttpRequestQueue()
        {
            _requestList = new List<HttpRequest>();
            _curRequest = null;
        }

        public void update()
        {
            if (_isbusy)
            {
                _curTime = DateTimeUtil.GetTimestampSecond();
                if (_curTime - _startTime > TIME_OUT_SECOND)
                {
                    if (_curRequest._isBlockStartOpera)
                    {
                        Debug.Log("抱歉，您的请求超时了，请检查网络状态!");
                    }
                    setIdle();
                }
            }
            else
            {
                if (_requestList != null && _requestList.Count > 0)
                {
                    HttpRequest request = _requestList[0];
                    CoroutineTool.startCoroutine(sendRequest(request));
                    _requestList.Remove(request);
                }
            }
        }

        /// <summary>添加请求</summary>
        /// <param name="request"></param>
        /// <param name="isReconnect"></param>
        public void addRequest(HttpRequest request)
        {
            do
            {
                _requestList.Add(request);
            } while (false);
        }

        /// <summary>发送请求</summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected IEnumerator sendRequest(HttpRequest request)
        {
            do
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {

                    break;
                }

                if (request._isBlockStartOpera)
                {
                    Debug.Log( "网络请求中...");
                    _isBlock = true;
                }

                setBusy(request);
                _curRequest = request;

                WWW www = null;
                byte[] s = request.getSendBytes();
                if (request._requestType == HttpRequestType.POST)
                {
                    www = new WWW(request._serverUrl, request.getSendBytes());
                }
                else if (request._requestType == HttpRequestType.GET)
                {
                    www = new WWW(request._serverUrl);
                }
                yield return www;

                if (request._isBlockEndOpera)
                {
                    _isBlock = false;
                }

                if (_curRequest != null && _curRequest._protocolId == request._protocolId)
                {
                    if (!string.IsNullOrEmpty(www.error))
                    {
                        Debug.Log(www.error);
                        if (_curRequest._isBlockStartOpera)
                        {
                            Debug.Log("网络异常，请检查网络状态!");
                        }
                    }
                    else
                    {
                        HttpResponse respond = HttpResponse.createResponse(request._protocolId, www.bytes, request);
                        NetworkManager.Instance._http._responseDelegate(respond);
                    }
                    setIdle();
                }
            } while (false);


        }

        /////////////////////////////////////////////////
        //内部访问

        /// <summary>处理回调请求</summary>
        private void handleResponse()
        {

        }

        /// <summary>设置阻塞状态</summary>
        /// <param name="request"></param>
        private void setBusy(HttpRequest request)
        {
            _startTime = DateTimeUtil.GetTimestampSecond();
            _isbusy = true;
            _curRequest = request;
        }

        /// <summary>解除阻塞状态</summary>
        private void setIdle()
        {
            _curRequest = null;
            _isbusy = false;
            _isBlock = false;
        }

    }
}