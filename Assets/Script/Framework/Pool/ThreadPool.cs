using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

namespace Chengzi
{

    public class ThreadPool : MonoSingleton<ThreadPool>
    {
        public static int maxThreads = 8;
        static int numThreads;

        private ThreadPool _current;
        public ThreadPool Current
        {
            get
            {
                return _current;
            }
        }

        private List<Action> _actions = new List<Action>();
        private List<Action> _lateActions = new List<Action>();
        public struct DelayedQueueItem
        {
            public float time;
            public Action action;
        }
        private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

        List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();

        public void init()
        {
            _current = this;
        }

        /// <summary>
        /// 切换到Unity OpenGL/DX渲染线程执行
        /// </summary>
        /// <param name="action"></param>
        public void runOnRenderThread(Action action)
        {
            runOnRenderThread(action, 0f);
        }

        public void runOnRenderThread(Action action, float time)
        {
            if (time != 0)
            {
                if (Current != null)
                {
                    lock (Current._delayed)
                    {
                        Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                    }
                }
            }
            else
            {
                if (Current != null)
                {
                    lock (Current._actions)
                    {
                        Current._actions.Add(action);
                    }
                }
            }
        }

        public void runOnRenderThreadLate(Action action)
        {
            if (Current != null)
            {
                lock (Current._actions)
                {
                    Current._lateActions.Add(action);
                }
            }
        }

        /// <summary>
        /// 启动异步线程池执行
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public Thread RunAsync(Action a)
        {
            while (numThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            System.Threading.ThreadPool.QueueUserWorkItem(RunAction, a);
            return null;
        }

        private void RunAction(object action)
        {
            try
            {
                ((Action)action)();
            }
            catch
            {
            }
            finally
            {
                Interlocked.Decrement(ref numThreads);
            }

        }

        void OnDisable()
        {
            if (_current == this)
            {

                _current = null;
            }
        }


        // Use this for initialization  
        void Start()
        {
            _current = this;
        }

        List<Action> _currentActions = new List<Action>();

        // Update is called once per frame  
        void Update()
        {
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_actions);
                _actions.Clear();
            }
            foreach (var a in _currentActions)
            {
                a();
            }
            lock (_delayed)
            {
                _currentDelayed.Clear();
                _currentDelayed.AddRange(_delayed.Where(d => d.time <= Time.time));
                foreach (var item in _currentDelayed)
                    _delayed.Remove(item);
            }
            foreach (var delayed in _currentDelayed)
            {
                delayed.action();
            }
        }

        void LateUpdate()
        {
            lock (_actions)
            {
                _currentActions.Clear();
                _currentActions.AddRange(_lateActions);
                _lateActions.Clear();
            }
            foreach (var a in _currentActions)
            {
                a();
            }
            _currentActions.Clear();
        }
    }
}