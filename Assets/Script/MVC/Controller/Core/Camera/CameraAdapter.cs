
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 摄像机适配
    /// </summary>
    public class CameraAdapter : MonoBehaviour
    {

        //基于iPhone6比例的单位高度
        public readonly static float DESIGN_HEIGHT = 1334.0f;
        //基于iPhone6比例的单位宽度
        public readonly static float DESIGN_WIDTH = 750.0f;
        //2d主摄像机
        public Camera _camera;

        void Start()
        {
            //设计分辨率比例
            //16:9=1.777的
            float s1 = DESIGN_WIDTH;
            //实际分辨率比例
            float s2 = Screen.width * 1.0f / Screen.height * DESIGN_HEIGHT;
            float aspectRatio = s2 / s1;
            _camera.orthographicSize = _camera.orthographicSize * aspectRatio;

            //相机起始坐标调整
            RectTransform trans = _camera.transform.GetComponent<RectTransform>();
            float y = trans.localPosition.y - (aspectRatio - 1) * DESIGN_HEIGHT / 2;
            trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
        }
    }
}