using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
#if UNITY_IPHONE || UNITY_STANDALONE_OSX

    public class SDKBridgeiOSImpl : ISDKBridge
    {
        private bool isFirstShow = true;


        public void quitApp()
        {
        }

        public string getMsg(BaseDto dto)
        {
            return "";
        }

        public void initSDK()
        {
            isFirstShow = true;
        }


        public void destorySDK()
        {
        }
    }
#endif
}
