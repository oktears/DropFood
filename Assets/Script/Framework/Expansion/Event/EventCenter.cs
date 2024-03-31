using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Chengzi;

namespace Chengzi
{
    public class EventCenter : Singleton<EventCenter>
    {
        public delegate void _event(ViewEvent e);

        private Dictionary<ViewConstant.ViewId, _event> _eventDic = new Dictionary<ViewConstant.ViewId, _event>();

        private Dictionary<ViewEvent, float> _dic = new Dictionary<ViewEvent, float>();
        private List<ViewEvent> _delayMap = new List<ViewEvent>();
        public bool regEvent(ViewConstant.ViewId ID, _event e)
        {
            bool ret = false;
            do
            {
                if (!_eventDic.ContainsKey(ID)) _eventDic.Add(ID, e);
                else { _eventDic[ID] += e; }
                ret = true;
            } while (false);
            return ret;
        }

        public bool unregEvent(ViewConstant.ViewId ID, _event e)
        {
            bool ret = false;
            do
            {
                if (!_eventDic.ContainsKey(ID)) break;
                _eventDic[ID] -= e;
                ret = true;
            } while (false);
            return ret;
        }

        public void send(ViewEvent e)
        {
            do
            {
                if (e == null || !_eventDic.ContainsKey(e._viewID) || _eventDic[e._viewID] == null) break;
                _eventDic[e._viewID](e);
            } while (false);
        }


        public void update(float dt)
        {
            if (_delayMap.Count > 0)
            {
                for (int i = _delayMap.Count - 1; i >= 0; i--)
                {
                    _delayMap[i]._delayTime -= dt;
                    if (_delayMap[i]._delayTime < 0)
                    {
                        send(_delayMap[i]);
                        _delayMap.Remove(_delayMap[i]);
                    }
                }
            }
        }

        ///延迟的发送消息
        public bool delaySend(ViewEvent e, float delayTime)
        {
            bool ret = false;
            e._delayTime = delayTime;
            _delayMap.Add(e);
            return ret;
        }

        ///延迟的发送消息
        public bool delaySend(ViewEvent e)
        {
            bool ret = false;
            _delayMap.Add(e);
            return ret;
        }
    }
}