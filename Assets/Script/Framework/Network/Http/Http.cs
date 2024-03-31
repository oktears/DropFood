using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using BestHTTP;
using System;
using LitJson;
using Newtonsoft.Json;

namespace Chengzi
{
    public class Http
    {
        //回调响应
        public delegate void _response(HttpResponse response);

        //服务器URL
        private Dictionary<Channel, NetData> _serverUrlMap;

        //请求队列
        private HttpRequestQueue _httpResquestQuene;

        //请求队列
        private HttpRequestQueue _httpBackgroundQueue;

        //请求队列
        private DownLoadQueue _downQueue;

        //下行回调
        public Http._response _responseDelegate;

        //下行错误回调
        public Http._response _errorDelegate;

        public void init()
        {
            _serverUrlMap = NetConfig.getNetConfig();

            _httpResquestQuene = new HttpRequestQueue();

            _httpBackgroundQueue = new HttpRequestQueue();

            _downQueue = new DownLoadQueue();
        }

        public void update()
        {
            addHeart();

            if (_httpResquestQuene != null)
            {
                _httpResquestQuene.update();
            }
            if (_httpBackgroundQueue != null)
            {
                _httpBackgroundQueue.update();
            }
            if (_downQueue != null)
            {
                _downQueue.update();
            }
        }

        /////////////////////////////////////////////

        public void addHeart()
        {

        }

        //发送请求
        public void sendRequest(HttpRequest request)
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {

            }
            else
            {
                _httpResquestQuene.addRequest(request);
            }

        }

        public void addDownload(DownLoadRequest req)
        {
            _downQueue.addRequest(req);
        }

        public bool isBlock()
        {
            return _httpResquestQuene._isBlock;
        }

        /// <summary>后台发送请求专用接口</summary>
        /// <param name="request"></param>
        public void sendRequestBackground(HttpRequest request)
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                request._isBlockStartOpera = false;
                request._isBlockEndOpera = false;
                _httpBackgroundQueue.addRequest(request);
            }
        }

        //错误回调请求
        public void handleHttpError()
        {

        }

        //发情请求
        public void sendReust<T>(JsonData data, OnRequestFinishedDelegate onRequestFinishedDelegate)
        {
            HTTPRequest request = new HTTPRequest(new Uri(HttpProtocolId.debug_Url),
                HTTPMethods.Post, false, httpRespond<T>);
            request.AddField("win_param", data.ToJson());
            request.Send();
        }

        //响应
        public void httpRespond<T>(HTTPRequest originalRequest, HTTPResponse response)
        {
            switch (originalRequest.State)
            {
                case HTTPRequestStates.Finished:
                    T t = JsonConvert.DeserializeObject<T>(response.DataAsText);
                    Debug.Log("请求成功");
                    Debug.Log(response.DataAsText);
                    break;
                case HTTPRequestStates.Error:
                    Debug.Log("请求错误");
                    break;
                case HTTPRequestStates.Aborted:
                    Debug.Log("请求无效");
                    break;
                case HTTPRequestStates.ConnectionTimedOut:
                    Debug.Log("连接超时");
                    break;
                case HTTPRequestStates.TimedOut:
                    Debug.Log("请求超时");
                    break;
            }
        }
        /////////////////////////////////////////////
        public enum RequestState
        {
            /// <summary>错误</summary>
            Error,
            /// <summary>正常</summary>
            Finish,
            /// <summary>jason格式错误</summary>
            JasonError,
            /// <summary>连接超时</summary>
            ConnectionTimedOut,
            /// <summary>请求超时</summary>
            TimedOut,
            /// <summary>请求无效</summary>
            Aborted,
        }

        public int Block = 0;

        public delegate void downCallBundle<T>(T data, Bundle bundle, RequestState state);//下行带bundle的回调
        public delegate void downCall<T>(T data, RequestState state);//下行回调
        public delegate void errorCall(RequestState err);

        public delegate void error(RequestState data);//

        /// <summary>发送请求,debug地址</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comreq"></param>
        /// <param name="t"></param>
        public void send<T>(CommonRequest comreq, downCall<T> t)
        {
            send<T>(comreq, delegate (T data, Bundle bundle, RequestState state) { t(data, state); }, HttpProtocolId.debug_Url, null, true);
        }

        /// <summary>发送请求,debug地址</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comreq"></param>
        /// <param name="t"></param>
        public void send<T>(CommonRequest comreq, downCall<T> t, string url)
        {
            send<T>(comreq, delegate (T data, Bundle bundle, RequestState state) { t(data, state); }, url, null, true);
        }

        /// <summary>发送请求,debug地址</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comreq"></param>
        /// <param name="t"></param>
        public void send<T>(CommonRequest comreq, downCallBundle<T> t, Bundle bundle)
        {
            send<T>(comreq, t, HttpProtocolId.debug_Url, bundle, true);
        }

        /// <summary>发送请求</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comreq"></param>
        /// <param name="t"></param>
        /// <param name="url"></param>
        public void send<T>(CommonRequest comreq, downCallBundle<T> t, string url, Bundle bundle, bool isNeedWait)
        {
            if (isNeedWait && comreq.type != NetTipType.NoTip)
            {
                if (Block == 0)
                {
                    if (comreq.type != NetTipType.Login)
                    {

                    }
                    else
                    {

                    }
                }
                Block++;
            }
            Debug.Log("httpurl:" + url);
            HTTPRequest request = new HTTPRequest(new Uri(url),
                 HTTPMethods.Post, true, delegate (HTTPRequest req, HTTPResponse response)
            {
                if (isNeedWait && comreq.type != NetTipType.NoTip)
                {
                    Block--;
                    if (Block == 0)
                    {
                        Debug.Log("结束等待");
                    }
                }
                RequestState state = onError(req);
                if (state == RequestState.Finish)
                {
                    if (t != null)
                    {
                        try
                        {
                            t(convert<T>(response), bundle, state);
                        }
                        catch (Exception e)
                        {
#if DEBUG
                            Debug.Log(comreq.data.ToJson());
                            Debug.Log(e.ToString());
                            Debug.Log("错误下行：" + response.DataAsText);
#endif
                            t(default(T), null, state);
                        }
                    }
                }
                else
                {
                    //if (err != null) err(state);
                    if (t != null) t(default(T), null, state);
                    //Debug.Log("状态错误");
                }
            });

            request.AddField("win_param", comreq.data.ToJson());
            request.Send();
        }

        /// <summary>下行错误状态</summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public RequestState onError(HTTPRequest req)
        {
            RequestState ret = RequestState.Error;
            switch (req.State)
            {
                case HTTPRequestStates.Finished:
                    ret = RequestState.Finish;
                    Debug.Log("请求成功");
                    break;
                case HTTPRequestStates.Error:
                    ret = RequestState.Error;
                    Debug.Log("请求错误");
                    break;
                case HTTPRequestStates.Aborted:
                    ret = RequestState.Aborted;
                    Debug.Log("请求无效");
                    break;
                case HTTPRequestStates.ConnectionTimedOut:
                    ret = RequestState.ConnectionTimedOut;
                    Debug.Log("连接超时");
                    break;
                case HTTPRequestStates.TimedOut:
                    ret = RequestState.TimedOut;
                    Debug.Log("请求超时");
                    break;
            }
            return ret;
        }

        /// <summary>下行状态转换</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public T convert<T>(HTTPResponse response)
        {
            T ret = JsonConvert.DeserializeObject<T>(response.DataAsText);
            return ret;
        }
        /////////////////////////////////////////////
        string audioPath = "http://192.168.124.225/api/upload_chatvoice.php";

        //public List<WWWForm> 

        public void sendSound(WWWForm form)
        {
            MainLoop.Instance.mono.StartCoroutine(wwwRequest(form));
        }

        public void getSound(string form)
        {
            MainLoop.Instance.mono.StartCoroutine(wwwRespond(form));
        }

        public IEnumerator wwwRequest(WWWForm form)
        {
            WWW www = new WWW(audioPath, form);

            yield return www;
            string path = www.text;
            if (www.error != null)
            {
                string str1 = "";
                string str2 = "";
                NetworkTool.Get_AudioUrl(path, ref str1, ref str2);
                if (str1.Equals("0"))
                {
                    //给服务器发送消息

                }
            }
        }


        public IEnumerator wwwRespond(string form)
        {
            WWW www = new WWW(form);

            yield return www;
            string path = www.text;
            int index = 0;
            int timeNum = int.Parse(path.Substring(index, 1));
            index += 1;
            int time = int.Parse(path.Substring(index, timeNum));
            index += timeNum;
            int channelsNum = int.Parse(path.Substring(index, 1));
            index += 1;
            int channels = int.Parse(path.Substring(index, channelsNum));
            index += channelsNum;

            string data = path.Remove(0, index);


            Loom.RunAsync(() =>
            {
                byte[] bytelist = Convert.FromBase64String(data);
                Debug.Log(data.Length + "  " + bytelist.Length);
                byte[] decompressList = AudioCompress.LzmaDecompress(bytelist);
                Loom.QueueOnMainThread(() =>
                {
                    float[] fff = CommonTool.byteToFloat(decompressList);
                    SetRecordAudio(fff, 8000 * time * channels, channels, 8000, true);
                });

            });
        }

        public void SetRecordAudio(float[] data, int lengthSamples, int channels, int frequency, bool stream)
        {
            AudioClip clip = AudioClip.Create("newClip", lengthSamples, channels, frequency, false);
            clip.SetData(data, 0);
            AudioManager.Instance.playOneShot(clip);
        }
    }
}