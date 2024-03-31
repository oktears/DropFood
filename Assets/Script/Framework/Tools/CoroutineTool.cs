using UnityEngine;
using System.Collections;

namespace Chengzi
{
    public static class CoroutineTool
    {
        /////Coroutine

        public static void startCoroutine(IEnumerator routine)
        {
            MainLoop.Instance.mono.StartCoroutine(routine);
        }

        public static void startCoroutine(string routine)
        {
            MainLoop.Instance.mono.StartCoroutine(routine);
        }

        public static void startCoroutine(IEnumerator routine, float waitTime)
        {
            startCoroutine(coroutineWaitTime(routine, waitTime));
        }

        private static IEnumerator coroutineWaitTime(IEnumerator routine, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            startCoroutine(routine);
        }

        public static void stopCoroutine(string name)
        {
            MainLoop.Instance.mono.StopCoroutine(name);
        }

        public static void stopCoroutine(IEnumerator routine)
        {
            MainLoop.Instance.mono.StopCoroutine(routine);
        }

        public static void stopAllCoroutines()
        {
            MainLoop.Instance.mono.StopAllCoroutines();
        }
    }
}

