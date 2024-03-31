using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    public class BezierMove : MonoBehaviour
    {
        public List<Transform> _ctrlPointList;
        public GameObject _moveObject;
        public AnimationCurve _curve = AnimationCurve.Linear(0, 0, 1, 1);

        private List<Vector3> _pahtPointList = new List<Vector3>();

        //段数
        public int _sectionDetail = 100;
        //总移动时间
        public float _totalTime = 3.0f;
        //单段移动时间
        private float _singleTime = 0;
        private float _totalTimer = 0;
        private float _singleTimer = 0;
        private int _pathCounter = 1;

        //是否开始执行动作
        public bool _isMove = false;
        //是否移动完成
        public bool _isMoveFinish = false;
        //是否在世界左边系下变换
        private bool _isWorldSpace = true;

        void Start()
        {
            _singleTime = _totalTime / _sectionDetail;
            if (_ctrlPointList != null && _ctrlPointList.Count > 0)
            {
                for (int j = 0; j < _sectionDetail; j++)
                {
                    float node = (float)j / _sectionDetail;
                    _pahtPointList.Add(Cala(node,
                        _ctrlPointList[0].position,
                        _ctrlPointList[1].position,
                        _ctrlPointList[2].position,
                        _ctrlPointList[3].position));
                }
            }
        }

        public void init()
        {
            _singleTime = _totalTime / _sectionDetail;
            if (_ctrlPointList != null && _ctrlPointList.Count > 0)
            {
                for (int j = 0; j < _sectionDetail; j++)
                {
                    float node = (float)j / _sectionDetail;
                    _pahtPointList.Add(Cala(node,
                        _ctrlPointList[0].position,
                        _ctrlPointList[1].position,
                        _ctrlPointList[2].position,
                        _ctrlPointList[3].position));
                }
            }
            _totalTimer = _totalTime;
        }

        public void init(Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Vector4 p4)
        {
            _singleTime = _totalTime / _sectionDetail;
            for (int j = 0; j < _sectionDetail; j++)
            {
                float node = (float)j / _sectionDetail;
                _pahtPointList.Add(Cala(node,
                    p1,
                    p2,
                    p3,
                    p4));
            }
            _totalTimer = _totalTime;
        }

        public void initBezier(Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            Vector4 p4,
            float totalTime,
            int sectionDetail,
            GameObject moveObject,
            bool isWorldSpace)
        {
            _curve = AnimationCurve.Linear(1, 1, 1, 1);
            _moveObject = moveObject;
            _totalTime = totalTime;
            _sectionDetail = sectionDetail;
            _singleTime = _totalTime / _sectionDetail;
            _isWorldSpace = isWorldSpace;
            for (int j = 0; j < _sectionDetail; j++)
            {
                float node = (float)j / _sectionDetail;
                _pahtPointList.Add(Cala(node,
                    p1,
                    p2,
                    p3,
                    p4));
            }
        }

        public void startMove()
        {
            _isMove = true;
        }

        public void stopMove()
        {
            _isMove = false;
            _isMoveFinish = true;
            //_moveObject.SetActive(false);
        }

        public void fixedUpdate(float dt)
        {
            if (!_isMove)
            {
                return;
            }

            if (_sectionDetail < 2)
            {
                _sectionDetail = 2;
            }

            //float rate = _curve.Evaluate(_totalTimer / _totalTime);

            _totalTimer += 0.002f;
            _singleTimer += 0.002f;

            if (_singleTimer >= _singleTime)
            {
                _singleTimer = 0;

                if (_isWorldSpace)
                {
                    _moveObject.transform.position = Vector3.Lerp(_pahtPointList[_pathCounter - 1], _pahtPointList[_pathCounter], 1.0f);
                    _moveObject.transform.position = _pahtPointList[_pathCounter - 1];
                }
                else
                {
                    _moveObject.transform.localPosition = Vector3.Lerp(_pahtPointList[_pathCounter - 1], _pahtPointList[_pathCounter], 1.0f);
                    //_moveObject.transform.localPosition = _pahtPointList[_pathCounter - 1];
                }
                _pathCounter++;

                if (_pathCounter >= _pahtPointList.Count)
                {
                    //结束
                    _pathCounter = 1;
                    stopMove();
                }
            }
        }

        /// <summary>
        /// 线性的贝塞尔曲线
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <returns></returns>
        Vector3 Cala(float t, Vector3 p0, Vector3 p1)
        {//P = P0+ t (P1 - P0)，0 <= t <= 1
            return p0 + t * (p1 - p0);
        }

        /// <summary>
        /// 二次贝塞尔曲线动
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        Vector3 Cala(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            //  B(t) = (1-t)^2P0+ 2(1-t)tP1 + t^2P2,   0 <= t <= 1
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;
            return p;
        }

        /// <summary>
        /// 三次贝塞尔曲线
        /// </summary>
        /// <param name="t"></param>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        Vector3 Cala(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * t;

            Vector3 p = uuu * p0;
            p += 3 * uu * t * p1;
            p += 3 * u * tt * p2;
            p += ttt * p3;

            return p;
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