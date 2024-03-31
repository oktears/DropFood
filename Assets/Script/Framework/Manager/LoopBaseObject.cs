using System.Collections;
using UnityEngine;

namespace Chengzi
{
    public abstract class LoopBaseObject
    {
        public LoopBaseObject()
        {
            MainLoop.Instance.addRaceObject(this);
        }

        private bool _bActive;

        protected virtual bool isActive
        {
            get
            {
                return _bActive;
            }
            set
            {
                _bActive = value;
            }
        }

        protected bool _bStarted; 

        //executing on first update frame
        protected virtual void onStart()
        {
            _bStarted = true;
        }

        //executing on every frame
        public virtual void onUpdate()
        {
            if (!_bStarted)
            {
                _bStarted = true;
                onStart();
            }
        }

        public virtual void onFixedUpdate()
        {
            if (!_bStarted)
            {
                _bStarted = true;
                onStart();
            }
        }

        public virtual void onLateUpdate()
        {
            if (!_bStarted)
            {
                _bStarted = true;
                onStart();
            }
        }

        //destory it
        public virtual void recycle()
        {
            //data reset here
            _bStarted = false;
            MainLoop.Instance.removeRaceObject(this);
        }

        /////Coroutine

        public void startCoroutine(IEnumerator routine)
        {
            MainLoop.Instance.mono.StartCoroutine(routine);
        }

        public void startCoroutine(IEnumerator routine, float waitTime)
        {
            startCoroutine(coroutineWaitTime(routine, waitTime));
        }

        private IEnumerator coroutineWaitTime(IEnumerator routine, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            startCoroutine(routine);
        }

        public void stopCoroutine(string name)
        {
            MainLoop.Instance.mono.StopCoroutine(name);
        }

        public void stopCoroutine(IEnumerator routine)
        {
            MainLoop.Instance.mono.StopCoroutine(routine);
        }

        public void stopAllCoroutines()
        {
            MainLoop.Instance.mono.StopAllCoroutines();
        }
    }
}