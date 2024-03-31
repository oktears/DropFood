using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Linq;

namespace Chengzi
{
    public class Loom : MonoBehaviour
    {
        public static int maxThreads = 8;
        static int numThreads;

        private static Loom _current;
        private int _count;
        public static Loom Current
        {
            get
            {
                Initialize();
                return _current;
            }
        }

        void Awake()
        {
            _current = this;
            initialized = true;
        }

        public static bool initialized;

        static void Initialize()
        {
            if (!initialized)
            {

                if (!Application.isPlaying)
                    return;
                initialized = true;
                var g = new GameObject("Loom");
                _current = g.AddComponent<Loom>();
                DontDestroyOnLoad(g);
            }

        }

        private List<Action> _actions = new List<Action>();
        public struct DelayedQueueItem
        {
            public float time;
            public Action action;
        }
        private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

        List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();

        public static void QueueOnMainThread(Action action)
        {
            Debug.Log("QueueOnMainThread---1----" + "  ");
            QueueOnMainThread(action, 0f);
        }
        public static void QueueOnMainThread(Action action, float time)
        {

            Debug.Log("QueueOnMainThread---2----" + "  ");
            if (time != 0)
            {
                Debug.Log("QueueOnMainThread--3----" + "  ");
                lock (Current._delayed)
                {
                    Debug.Log("QueueOnMainThread--4----" + "  ");
                    Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action });
                }
            }
            else
            {
                Debug.Log("QueueOnMainThread--5----" + "  ");
                lock (Current._actions)
                {
                    Debug.Log("QueueOnMainThread--6----" + "  ");
                    Current._actions.Add(action);
                }
            }
        }

        public static Thread RunAsync(Action a)
        {
            Initialize();
            while (numThreads >= maxThreads)
            {
                Thread.Sleep(1);
            }
            Interlocked.Increment(ref numThreads);
            System.Threading.ThreadPool.QueueUserWorkItem(RunAction, a);
            return null;
        }

        private static void RunAction(object action)
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
                initialized = false;
            }
        }



        // Use this for initialization
        void Start()
        {

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

        public void ClearYinFun()
        {
            if (_actions != null)
            {
                _actions.Clear();
            }
        }
    }
}