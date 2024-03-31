using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace Chengzi
{
    public class HttpHeartQueue
    {
        /// <summary>请求超时时间</summary>
        protected const int TIME_OUT_SECOND = 10;

        protected const float CD = 10;

        ////////////////////////////////////////////////////////

        /// <summary>请求列表</summary>
        public List<HttpRequest> _requestList { get; private set; }

        /// <summary>是否完成所有</summary>
        public bool _isAllFinish { get; private set; }

        /// <summary>当前Id</summary>
        public int _id { get; private set; }

        /// <summary>当前请求</summary>
        protected HttpRequest _curRequest { get; private set; }

        /// <summary></summary>
        private bool _isbusy = false;

        /// <summary>这次请求的开始时间</summary>
        private long _startTime = 0;

        /// <summary>请求重连时间</summary>
        private long _curTime = 0;

        private HttpRequest _cur;

        private float _timer = 0;

        public HttpHeartQueue()
        {
            _requestList = new List<HttpRequest>();
            _curRequest = null;
            _isAllFinish = false;
        }

        public void update()
        {
            if (_isbusy)
            {
                _curTime = DateTimeUtil.GetTimestampSecond();
                if (_curTime - _startTime > TIME_OUT_SECOND)
                {
                    setIdle();
                    Debug.Log("请求超时");

                }
            }
            else
            {
                if (_requestList != null && _requestList.Count > 0 && !_isAllFinish)
                {

                    HttpRequest request = _requestList[_id];
                    CoroutineTool.startCoroutine(sendRequest(request));
                    _requestList.Remove(request);

                    if (++_id > _requestList.Count) { _id = 0; _isAllFinish = true; }
                }
            }

            if (_isAllFinish)
            {
                _timer += Time.deltaTime;
                if (_timer >= CD)
                {
                    _timer = 0;
                    _isAllFinish = false;
                }
            }
        }

        /// <summary>添加请求</summary>
        /// <param name="request"></param>
        /// <param name="isReconnect"></param>
        public void addRequest(HttpRequest request)
        {
            _requestList.Add(request);
        }

        /// <summary>发送请求</summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected IEnumerator sendRequest(HttpRequest request)
        {
            if (request._isBlockStartOpera)
            {

            }

            setBusy(request);
            _cur = request;

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

            }

            if (_cur._protocolId == request._protocolId)
            {
                if (www.error != null)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    HttpResponse respond = HttpResponse.createResponse(request._protocolId, www.bytes, request);
                    NetworkManager.Instance._http._responseDelegate(respond);
                }
                setIdle();
            }
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
        }

    }
}