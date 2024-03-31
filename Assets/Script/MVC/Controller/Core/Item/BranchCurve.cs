using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    public class BranchCurve : MonoBehaviour
    {
        //操作1曲线
        public AnimationCurve _operate1Curve;
        //转向时速度变化曲线
        //public AnimationCurve _turnDirSpeedCurve;
        //转向时，速度方向变化率曲线
        public AnimationCurve _speedDirRateCurve_Whell;
        public AnimationCurve _speedDirRateCurve_Joystick;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void FixedUpdate()
        {
            PlayerMovePosition();
        }

        void PlayerMovePosition()
        {
            //GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position);
        }
    }
}