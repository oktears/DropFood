using UnityEngine;
using UnityEngine.UI;

#if !UNITY_WEBGL

namespace Chengzi
{


    public class PingCounter : MonoBehaviour
    {

        private Ping ping;
        private float delayTime = 0;
        public Text _text;

        void Start()
        {
            SendPing();
        }

        void Update()
        {

            if (_text != null && null != ping && ping.isDone)
            {
                _text.text = "ping: " + delayTime.ToString() + "ms";
                delayTime = ping.time;
                ping.DestroyPing();
                ping = null;
                Invoke("SendPing", 1.0f);//每秒Ping一次
            }
        }

        void SendPing()
        {
            ping = new Ping(HttpProtocolId.game_Url);
        }
    }
}

#endif