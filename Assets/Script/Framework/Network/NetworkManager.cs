using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace Chengzi
{
    /// <summary>
    /// 网络管理器
    /// </summary>
    public class NetworkManager : Singleton<NetworkManager>
    {
        public Http _http { get; private set; }

        public Tcp _tcp { get; private set; }

        public void init()
        {
            _http = new Http();
            _http.init();

            _tcp = new Tcp();
            _tcp.init();
        }

        public void update()
        {
            if (_http != null)
            {
                _http.update();
            }

            if (_tcp != null)
            {
                _tcp.update();
            }
        }
    }
}