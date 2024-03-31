using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;


namespace Chengzi
{
    public class DownLoadQueue
    {

        ////////////////////////////////////////////////////////

        /// <summary>请求列表</summary>
        public List<DownLoadRequest> _requestList { get; private set; }

        /// <summary>当前请求</summary>
        protected DownLoadRequest _curRequest { get; private set; }

        /// <summary></summary>
        private bool _isbusy = false;

        public DownLoadQueue()
        {
            _requestList = new List<DownLoadRequest>();
            _curRequest = null;
        }

        public void update()
        {
            if (!_isbusy)
            {
                if (_requestList != null && _requestList.Count > 0)
                {
                    DownLoadRequest request = _requestList[0];
                    CoroutineTool.startCoroutine(sendRequest(request));
                    _requestList.Remove(request);
                }
            }
        }

        /// <summary>添加请求</summary>
        /// <param name="request"></param>
        /// <param name="isReconnect"></param>
        public void addRequest(DownLoadRequest request)
        {
            do
            {
                _requestList.Add(request);
            } while (false);
        }

        /// <summary>发送请求</summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected IEnumerator sendRequest(DownLoadRequest request)
        {
            do
            {
                if (Application.internetReachability == NetworkReachability.NotReachable)
                {

                    break;
                }

                setBusy(request);
                _curRequest = request;

                WWW www = request.getWWW();
                yield return www;

                if (_curRequest != null)
                {
                    if (string.IsNullOrEmpty(www.error))
                    {
                        //处理下行结果
                        DownLoadRespond res = HttpResponse.createResponse(_curRequest, www);
                        if (res != null) res.update();
                    }
                }
                setIdle();
            } while (false);
        }


        /////////////////////////////////////////////////
        //内部访问


        /// <summary>设置阻塞状态</summary>
        /// <param name="request"></param>
        private void setBusy(DownLoadRequest request)
        {
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
