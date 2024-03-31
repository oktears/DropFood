using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    /// <summary>
    /// 消息通知类
    /// </summary>
    public class NotificationCenter
    {
        public delegate bool func_notify(int a, object o);
        private static NotificationCenter s_instance = null;

        protected object _lock = new object();

        public static NotificationCenter getInstance()
        {
            if (s_instance == null)
            {
                s_instance = new NotificationCenter();
            }
            return s_instance;
        }


        Dictionary<int, List<func_notify>> _notifyMap;
        List<KeyValuePair<int, object>> _postList;

        private NotificationCenter()
        {
            _notifyMap = new Dictionary<int, List<func_notify>>(10);
            _postList = new List<KeyValuePair<int, object>>(10);
        }
        private bool remove_lock = false;
        public bool notify(int e, object o)
        {
            bool ret = false;
            do
            {
                lock (_lock)
                {
                    if (_notifyMap == null) break;
                    List<func_notify> l = _notifyMap.ContainsKey(e) ? _notifyMap[e] : null; ;
                    if (l == null) break;

                    int count = l.Count;
                    //remove_lock = true;
                    for (int i = 0; i < count; i++)
                    {
                        func_notify func = l[i];
                        if (func(e, o))
                        {
                            ret = true;
                            break;
                        }
                    }
                    //remove_lock = false;
                    ret = true;
                }
            } while (false);
            return ret;
        }

        public bool regNotify(int e, func_notify func)
        {
            bool ret = false;
            do
            {
                lock (_lock)
                {
                    if (_notifyMap == null) break;

                    List<func_notify> l = _notifyMap.ContainsKey(e) ? _notifyMap[e] : null;

                    if (l == null)
                    {
                        l = new List<func_notify>();
                        _notifyMap.Add(e, l);
                    }
                    else if (l.Contains(func))
                    {
                        break;
                    }
                    l.Add(func);
                    ret = true;
                }
            } while (false);
            return ret;
        }

        public bool unregNotify(int e, func_notify func)
        {
            bool ret = false;
            do
            {
                lock (_lock)
                {
                    if (remove_lock)
                    {
                        UnityEngine.Debug.LogError("NotificationCenter.unregNotify,消息队列已锁，不允许删除");
                        break;
                    }
                    if (_notifyMap == null) break;
                    List<func_notify> l = _notifyMap.ContainsKey(e) ? _notifyMap[e] : null;
                    if (l == null) break;

                    ret = l.Remove(func);
                    if (l.Count == 0)
                    {
                        _notifyMap.Remove(e);
                    }
                }
            } while (false);
            return ret;
        }

        /// <summary>线程不安全的</summary>
        public void postMessage(int e, object msg)
        {
            _postList.Add(new KeyValuePair<int, object>(e, msg));
        }

        public object recvMessage(int e)
        {
            int count = _postList.Count;
            for (int i = 0; i < count; i++)
            {
                KeyValuePair<int, object> o = _postList[i];
                if (o.Key == e)
                {
                    _postList.RemoveAt(i);
                    return o.Value;
                }
            }
            return null;
        }

        public void clear()
        {
            if (_notifyMap != null)
            {
                _notifyMap.Clear();
            }
            if (_postList != null)
            {
                _postList.Clear();
            }
        }

        public void destroy()
        {
            clear();
            s_instance = null;
        }
    }
}
