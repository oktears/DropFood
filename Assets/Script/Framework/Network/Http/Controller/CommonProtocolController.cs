using LitJson;
using UnityEngine;

namespace Chengzi
{
    public class CommonProtocolController : BaseProtocolController
    {
        public CommonProtocolController()
        {
        }

        /// <summary>获取端口号和网址及更新</summary>
        public void GetLoginTypeFun<T>(Http.downCall<T> t)
        {
            var body = new JsonData();

            body["method"] = GetVersion;
            JsonData paramData = new JsonData();

            //"1.0.36"
            // string tmpBanBen = PlatformManager.Instance._deviceInfo.getAppVersionName().Replace(".", "");
            // paramData["clientVer"] = tmpBanBen;

            paramData["timestamp"] = Time.time;
#if UNITY_ANDROID || UNITY_STANDALONE_WIN
            //如果是安卓的话提示更新
            paramData["platform"] = "Android";
#elif UNITY_IPHONE
			paramData["platform"] = "IOS";
#endif
            body["param"] = paramData;
            CommonRequest req = new CommonRequest();
            req.type = NetTipType.Login;
            req.data = body;
            Debug.Log(body.ToString());
            NetworkManager.Instance._http.send<T>(req, t, HttpProtocolId.START_URL);
        }
    }
}