using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 数学工具类
    /// </summary>
    public class MathUtil
    {

        /// <summary>
        /// 两个浮点数是否近似相等
        /// </summary>
        /// <param name="value1">浮点数1</param>
        /// <param name="value2">浮点数2</param>
        /// <param name="epsilon">近似值</param>
        /// <returns></returns>
        public static bool NearlyEqual(float value1, float value2)
        {
            float epsilon = 0.0001f;
            float absA = Math.Abs(value1);
            float absB = Math.Abs(value2);
            float diff = Math.Abs(value1 - value2);

            // shortcut, handles infinities
            if (value1 == value2)
            {
                return true;
            }
            else if (value1 == 0 || value2 == 0 || diff < float.Epsilon)
            {
                // a or b is zero or both are extremely close to it
                // relative error is less meaningful here
                return diff < (epsilon * float.Epsilon);
            }
            else
            {
                // use relative error
                return diff / (absA + absB) < epsilon;
            }
        }

        /// <summary>
        /// 求两向量角度
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float angle360(Vector3 from, Vector3 to)
        {
            //两点的x、y值
            float x = from.x - to.x;
            float y = from.y - to.y;

            //斜边长度
            float hypotenuse = Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));

            //求出弧度
            float cos = x / hypotenuse;
            float radian = Mathf.Acos(cos);

            //用弧度算出角度    
            float angle = 180 / (Mathf.PI / radian);

            if (y < 0)
            {
                angle = -angle;
            }
            else if ((y == 0) && (x < 0))
            {
                angle = 180;
            }
            return angle;
        }

        /// <summary>
        /// 某坐标点绕某方向旋转后的坐标
        /// </summary>
        /// <param name="position"></param>
        /// <param name="center"></param>
        /// <param name="axis"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector3 rotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
        {
            Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
            Vector3 newPos = center + point;
            return newPos;
        }

        /// <summary>
        /// 某方向向量旋转后的方向向量
        /// </summary>
        /// <param name="v">需要旋转的向量</param>
        /// <param name="angle">旋转的角度</param>
        /// <returns>旋转后的向量</returns>
        public static Vector2 rotateMatrix(Vector2 v, float angle)
        {
            var x = v.x;
            var y = v.y;
            var sin = Math.Sin(Math.PI * angle / 180);
            var cos = Math.Cos(Math.PI * angle / 180);
            var newX = x * cos + y * sin;
            var newY = x * -sin + y * cos;
            return new Vector2((float)newX, (float)newY);
        }
    }

}
