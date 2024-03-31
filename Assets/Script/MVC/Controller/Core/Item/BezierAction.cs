
using UnityEngine;

namespace Chengzi
{

    public class BezierAction : MonoBehaviour
    {

        //起始点
        public Vector2 _startPosition;
        //控制点
        public Vector2 _controlPoint_1;
        public Vector2 _controlPoint_2;
        //结束点
        public Vector2 _endPosition;

        private float _elapsed;
        public float _duration;

        public Transform _target;

        private bool _isRunning = false;
        public bool _isRunFinish = false;

        public void runAction()
        {
            _isRunning = true;
        }

        public void stopAction()
        {
            _isRunning = false;
            _isRunFinish = true;
        }

        public void fixedUpdate(float dt)
        {
            updateStep(dt);
        }

        private void updateStep(float dt)
        {
            if (_isRunning)
            {
                _elapsed += dt;
                float time = Mathf.Clamp01(_elapsed / _duration);
                updateMove(time);
                if (_elapsed >= _duration)
                {
                    _elapsed = 0;
                    stopAction();
                }
            }
        }

        private void updateMove(float time)
        {
            float xa = _startPosition.x;
            float xb = _controlPoint_1.x;
            float xc = _controlPoint_2.x;
            float xd = _endPosition.x;

            float ya = _startPosition.y;
            float yb = _controlPoint_1.y;
            float yc = _controlPoint_2.y;
            float yd = _endPosition.y;

            float x = bezierat(xa, xb, xc, xd, time);
            float y = bezierat(ya, yb, yc, yd, time);

            _target.localPosition = new Vector2(x, y);
        }

        // Bezier cubic formula:
        //    ((1 - t) + t)3 = 1 
        // Expands to ...
        //   (1 - t)3 + 3t(1-t)2 + 3t2(1 - t) + t3 = 1 
        private float bezierat(float a, float b, float c, float d, float t)
        {
            return (Mathf.Pow(1 - t, 3) * a +
                    3 * t * (Mathf.Pow(1 - t, 2)) * b +
                    3 * Mathf.Pow(t, 2) * (1 - t) * c +
                    Mathf.Pow(t, 3) * d);
        }

    }
}
