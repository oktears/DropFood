using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

namespace Chengzi
{
    public class HttpRequest
    {
        //创建请求
        public static HttpRequest createRequest(ProtocolId id, global::ProtoBuf.IExtensible data)
        {
            HttpRequest ret = new HttpRequest(id, data);
            return ret;
        }

        public static HttpRequest createRequest(ProtocolId id)
        {
            HttpRequest ret = new HttpRequest(id);
            return ret;
        }

        ////////////////////////////////////////////////

        //协议ID
        public ProtocolId _protocolId { get; set; }

        //请求类型
        public HttpRequestType _requestType { get; set; }

        //服务器url
        public string _serverUrl { get; set; }

        //是否阻塞列表
        public bool _isBlock { get; set; }

        //是否阻塞操作
        public bool _isBlockStartOpera { get; set; }

        //是否阻塞操作
        public bool _isBlockEndOpera { get; set; }

        //是否超时重链
        public bool _isReconnect { get; set; }

        //是否错误
        public bool _isHanldeError { get; set; }

        //当前请求Id
        public static int _curRequestId { get; set; }

        // 协议数据
        public global::ProtoBuf.IExtensible _po { get; set; }


        public HttpRequest(ProtocolId id, global::ProtoBuf.IExtensible data)
        {
            this._protocolId = id;
            this._requestType = HttpRequestType.POST;
            this._serverUrl = HttpProtocolId.getURL(id);
            this._isHanldeError = true;
            this._isBlock = true;
            this._isBlockStartOpera = true;
            this._isBlockEndOpera = true;
            _po = data;
        }

        public HttpRequest(ProtocolId id)
        {
            this._protocolId = id;
            this._requestType = HttpRequestType.GET;
            this._serverUrl = HttpProtocolId.getURL(id);
            this._isHanldeError = true;
            this._isBlock = true;
            this._isBlockStartOpera = true;
            this._isBlockEndOpera = true;
        }

        public byte[] getSendBytes()
        {
            byte[] ret = null;
            if (_po != null)
            {
                MemoryStream s = new MemoryStream();
                ProtoBuf.Serializer.Serialize(s, _po);
                ret = s.ToArray();
            }
            return ret;
        }

        public static byte[] getByte<T>(T t)
        {
            MemoryStream s = new MemoryStream();
            ProtoBuf.Serializer.Serialize(s, t);
            return s.ToArray();
        }
    }
}