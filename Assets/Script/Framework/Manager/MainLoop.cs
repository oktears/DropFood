using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Chengzi
{
    /// <summary>
    /// 主循环函数
    /// </summary>
    public class MainLoop : Singleton<MainLoop>
    {
        public List<LoopBaseObject> _loopBaseObject = new List<LoopBaseObject>();
        private List<LoopBaseObject> _listPendingAdd = new List<LoopBaseObject>();
        private List<LoopBaseObject> _listPendingRemove = new List<LoopBaseObject>();

        public MonoBehaviour mono { get; private set; }

        public void init(MonoBehaviour mono)
        {
            this.mono = mono;
        }

        public void addRaceObject(LoopBaseObject obj)
        {
            _listPendingAdd.Add(obj);
        }

        public void removeRaceObject(LoopBaseObject obj)
        {
            _listPendingRemove.Add(obj);
        }

        private void addObject(LoopBaseObject obj)
        {
            _loopBaseObject.Add(obj);
        }

        private void removeObject(LoopBaseObject obj)
        {
            _loopBaseObject.Remove(obj);
        }

        public void gameUpdate()
        {
            int count = _listPendingAdd.Count;
            for (int i = 0; i < count; i++)
            {
                addObject(_listPendingAdd[i]);
            }

            _listPendingAdd.Clear();

            count = _listPendingRemove.Count;
            for (int i = 0; i < count; i++)
            {
                removeObject(_listPendingRemove[i]);
            }

            _listPendingRemove.Clear();

            count = _loopBaseObject.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    _loopBaseObject[i].onUpdate();
                }
                catch (UnityException e)
                {
                    Debug.Log(e.Message);
                }
            }
        }

        public void gameFixedUpdate()
        {
            int count = _listPendingAdd.Count;
            for (int i = 0; i < count; i++)
            {
                addObject(_listPendingAdd[i]);
            }

            _listPendingAdd.Clear();

            count = _listPendingRemove.Count;
            for (int i = 0; i < count; i++)
            {
                removeObject(_listPendingRemove[i]);
            }

            _listPendingRemove.Clear();

            count = _loopBaseObject.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    _loopBaseObject[i].onFixedUpdate();
                }
                catch (UnityException e)
                {
                    Debug.Log(e.Message);
                }
            }
        }

        public void gameLateUpdate()
        {
            int count = _listPendingAdd.Count;
            for (int i = 0; i < count; i++)
            {
                addObject(_listPendingAdd[i]);
            }

            _listPendingAdd.Clear();

            count = _listPendingRemove.Count;
            for (int i = 0; i < count; i++)
            {
                removeObject(_listPendingRemove[i]);
            }

            _listPendingRemove.Clear();

            count = _loopBaseObject.Count;
            for (int i = 0; i < count; i++)
            {
                try
                {
                    _loopBaseObject[i].onLateUpdate();
                }
                catch (UnityException e)
                {
                    Debug.Log(e.Message);
                }
            }
        }

        public void clear()
        {
            int count = _listPendingRemove.Count;
            for (int i = 0; i < count; i++)
            {
                removeObject(_listPendingRemove[i]);
            }

            _listPendingRemove.Clear();

            _listPendingAdd.Clear();
        }

    }
}