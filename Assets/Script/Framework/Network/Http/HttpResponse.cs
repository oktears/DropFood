using UnityEngine;
using System.Collections;
using System.IO;
using ProtoBuf;
using Newtonsoft.Json;

namespace Chengzi
{
    public class HttpResponse
    {
        ////////////////////////////////////////////
        ///static

        public static HttpResponse createResponse(ProtocolId protocoId, byte[] data, HttpRequest req)
        {
            HttpResponse response = new HttpResponse(protocoId, data, req);
            return response;
        }

        public static DownLoadRespond createResponse(DownLoadRequest req, WWW www)
        {
            DownLoadRespond ret = null;
            switch (req.type)
            {
                case DownLoadType.Head:

                    break;
                case DownLoadType.PostVoice:

                    break;
                case DownLoadType.GetVoice:

                    break;
            }
            if (ret != null)
            {
                ret.type = req.type;
                ret.www = www;
            }
            return ret;
        }

        /////////////////////////////////////////////

        public byte[] _byte { get; private set; }

        public ProtocolId _protocoId { get; private set; }

        public HttpRequest _request { get; private set; }

        public HttpResponse(ProtocolId protocoId, byte[] data, HttpRequest req)
        {
            _protocoId = protocoId;
            _byte = data;
            _request = req;
        }

        public T deserialize<T>()
        {
            MemoryStream stream = new MemoryStream(_byte);
            T ret = Serializer.Deserialize<T>(stream);
            return ret;
        }

        public static T deserialize<T>(byte[] byteArray)
        {
            MemoryStream stream = new MemoryStream(byteArray);
            T ret = Serializer.Deserialize<T>(stream);
            return ret;
        }

        public T deserializeJson<T>()
        {
            return JsonConvert.DeserializeObject<T>(System.Text.Encoding.Default.GetString(_byte));
        }
    }
}