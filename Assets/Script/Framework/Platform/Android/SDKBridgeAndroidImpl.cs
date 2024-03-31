using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{

#if UNITY_ANDROID || UNITY_STANDALONE_WIN

    public class SDKBridgeAndroidImpl : AndroidBridge, ISDKBridge
    {
        protected override string javaClassName
        {
            get { return "com.chengzi.unitybase.platform.JavaCSharpBridge"; }
        }

        public void quitApp()
        {
        }

        public string getMsg(BaseDto dto)
        {
            string json = JsonConvert.SerializeObject(dto);
            return json;
        }

        public void initSDK()
        {
        }

        public void destorySDK()
        {
        }


    }

#endif

}
