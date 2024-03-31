using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{
    public static class NetConfig
    {
        //开发服务器地址
        public const string DEVELOP_URL = "";


        public static Dictionary<Channel, NetData> getNetConfig()
        {
            Dictionary<Channel, NetData> ret = new Dictionary<Channel, NetData>();

            //添加数据
            ret.Add(Channel.TEST, new NetData(DEVELOP_URL));
    
            return ret;
        }
    }
}