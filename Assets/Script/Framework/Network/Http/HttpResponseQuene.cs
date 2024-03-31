using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{
    public class HttpResponseQuene
    {
        private List<HttpResponse> _responseList;

        public HttpResponseQuene()
        {
            _responseList = new List<HttpResponse>();
        }

        public void addRespond(HttpResponse respond)
        {
            _responseList.Add(respond);
        }

        public void update()
        {
            if (_responseList.Count > 0)
            {
                for (int i = _responseList.Count - 1; i >= 0; i++)
                {
                    HttpResponse response = _responseList[i];
                    _responseList.Remove(response);
                }
            }
        }
    }
}