
using Colorful;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace Chengzi
{

    public class CameraPoint
    {
        public enum PointType
        {
            //固定 
            Fixed,
            //横坐标跟随
            FollowHorizontal,
            //纵坐标跟随
            FollowVertical,
        }

        //控制点的类型
        private PointType type = PointType.Fixed;
        //控制范围
        private Vector2 fixedSize = Vector2.one * 20f;
        //横向移动速度
        private float horizontalSpeed = 5f;
        //纵向移动速度
        private float verticalSpeed = 5f;

        public Transform transform;


        public PointType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public Vector2 FixedSize
        {
            get
            {
                return fixedSize;
            }

            set
            {
                fixedSize = value;
            }
        }

        public float HorizontalSpeed
        {
            get
            {
                return horizontalSpeed;
            }

            set
            {
                horizontalSpeed = value;
            }
        }

        public float VerticalSpeed
        {
            get
            {
                return verticalSpeed;
            }

            set
            {
                verticalSpeed = value;
            }
        }
    }


    /// <summary>
    /// 摄像机控制器
    /// </summary>
    public class CameraController
    {
        //跟随目标
        public RectTransform _target;
        //主摄像机
        public GameObject _mainCameraObj { get; private set; }
        //背景摄像机
        public GameObject _bgCameraObj { get; private set; }
        //默认跟随速度
        public float _followSpeed = 1.0f;
        //摄像头坐标微调
        public Vector2 _deltaPositon = Vector2.up * 0.5f;
        //所有摄像头控制点的父对象
        public GameObject _eventPoints;
        //所有控制点
        private CameraPoint[] _points;
        //是否跟随
        private bool _isFollowBranch = false;

        //主相机
        private Camera _mainCamera;
        //是否开启滤镜
        private bool _isOpenFilter = false;

        //lomo滤镜
        private FastVignette _fastVignetteFilter;
        //泛光滤镜
        private BloomOptimized _bloomFilter;
        //移轴摄影滤镜
        private TiltShift _tiltShiftFilter;

        //基于iPhone6比例的单位高度
        public readonly static float DESIGN_HEIGHT = 1334.0f;
        //基于iPhone6比例的单位宽度
        public readonly static float DESIGN_WIDTH = 750.0f;

        public float _mainCameraInitY { get; set; }
        public float _bgCameraInitY { get; set; }

        private GameControllerBase _ctrl;

        //初始化
        public void init(RectTransform target, GameControllerBase ctrl)
        {
            this._ctrl = ctrl;
            this._target = target;
            //CameraPoint point = new CameraPoint();
            ////获取所有控制点
            _points = new CameraPoint[0];
            //_points[0] = point;
            //Camera.main.orthographicSize = 6.0f;
            _mainCamera = Camera.main;
            _mainCameraObj = _mainCamera.gameObject;
            _bgCameraObj = _ctrl._sceneLoader._bgCamera;
            _bgCameraInitY = _mainCameraObj.transform.localPosition.y;

            //_isOpenFilter = QualityManager.Instance._isOpenFilter;
            //ShaderManager.Instance._isOpenLomoFilter = false;
            //ShaderManager.Instance._isOpenTiltShiftFilter = _isOpenFilter;

            //_fastVignetteFilter = _mainCamera.GetComponent<FastVignette>();
            //_bloomFilter = _mainCamera.GetComponent<BloomOptimized>();
            //_tiltShiftFilter = _mainCamera.GetComponent<TiltShift>();

            //_fastVignetteFilter.enabled = ShaderManager.Instance._isOpenLomoFilter;
            //_bloomFilter.enabled = ShaderManager.Instance._isOpenBloomFilter;
            //_tiltShiftFilter.enabled = ShaderManager.Instance._isOpenTiltShiftFilter;

            //NotificationCenter.getInstance().regNotify(Event.EVENT_MODIFY_FILTER, modifyCameraFilter);

            adapter();
        }

        private void adapter()
        {
            //设计分辨率比例
            //16:9=1.777的
            float s1 = DESIGN_WIDTH;
            //实际分辨率比例
            float s2 = Screen.width * 1.0f / Screen.height * DESIGN_HEIGHT;
            float ratio = s2 / s1;
            _mainCamera.orthographicSize = _mainCamera.orthographicSize * ratio;

            //相机起始坐标调整，只针对比16：9长的屏幕
            float aspectRatio = Screen.height * 1.0f / Screen.width;
            if (aspectRatio > 1.7f)
            {
                RectTransform trans = _mainCamera.transform.GetComponent<RectTransform>();
                _mainCameraInitY = trans.localPosition.y - (ratio - 1) * DESIGN_HEIGHT / 2;
                trans.localPosition = new Vector3(trans.localPosition.x, _mainCameraInitY, trans.localPosition.z);
            }
            else if (aspectRatio < 1.6f)
            {
                _ctrl._sceneLoader._root.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
                ViewManager.Instance._UIRoot2D.GetComponent<CanvasScaler>().matchWidthOrHeight = 1;
            }
        }

        //public void destory()
        //{
        //    NotificationCenter.getInstance().unregNotify(Event.EVENT_MODIFY_FILTER, modifyCameraFilter);
        //}

        public void moveByCameraPosY(float posY)
        {
            _mainCameraObj.transform.DOBlendableLocalMoveBy(new Vector3(0, posY, 0), 0.5f).SetEase(Ease.Linear);
            _bgCameraObj.transform.DOBlendableLocalMoveBy(new Vector3(0, posY, 0), 0.5f).SetEase(Ease.Linear);
        }

        public void moveToCameraPosY(float mainPosY, float bgPosY)
        {
            _mainCameraObj.transform.DOLocalMoveY(mainPosY, 0.5f).SetEase(Ease.Linear);
            _bgCameraObj.transform.DOLocalMoveY(bgPosY, 0.5f).SetEase(Ease.Linear);
        }

        ///// <summary>
        ///// 修改滤镜
        ///// </summary>
        ///// <param name="e"></param>
        ///// <param name="o"></param>
        ///// <returns></returns>
        //private bool modifyCameraFilter(int e, object o)
        //{
        //    _fastVignetteFilter.enabled = ShaderManager.Instance._isOpenLomoFilter;
        //    _bloomFilter.enabled = ShaderManager.Instance._isOpenBloomFilter;
        //    _tiltShiftFilter.enabled = ShaderManager.Instance._isOpenTiltShiftFilter;

        //    NotificationCenter.getInstance().notify(Event.EVENT_CHANGE_CTRL, 0);
        //    return false;
        //}

        /// <summary>
        /// 切换到结束摄像机
        /// </summary>
        public void changeFinishCamera()
        {
            _isFollowBranch = false;
            _mainCameraObj.transform.DOLocalMoveY(_mainCameraInitY, 3.0f).SetEase(Ease.Linear);
            _bgCameraObj.transform.DOLocalMoveY(_bgCameraInitY, 3.0f).SetEase(Ease.Linear);
        }

        //private void cameraMoveFinish()
        //{
        //    //_isFollowBranch = true;
        //}

        //public void fixedUpdate(float dt)
        //{

        //    if (!_isFollowBranch)
        //    {
        //        return;
        //    }

        //    //没有摄像头控制点则一默认跟随方式跟随
        //    if (_points.Length < 1)
        //    {
        //        methodFollowDefault();
        //    }
        //    //记录是否执行过其它跟随方式，若没有执行，则以默认跟随方式跟随
        //    int Count = 0;

        //    foreach (var item in _points)
        //    {
        //        switch (item.Type)
        //        {
        //            case CameraPoint.PointType.Fixed:
        //                if (methodFixed(item))
        //                    Count++;
        //                break;
        //            case CameraPoint.PointType.FollowHorizontal:
        //                if (methodHorizontal(item))
        //                    Count++;
        //                break;
        //            case CameraPoint.PointType.FollowVertical:
        //                if (methodVertical(item))
        //                    Count++;
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    //没有执行其它跟随方式，以默认跟随方式跟随
        //    if (Count < 1)
        //        methodFollowDefault();
        //}

        ////纵向跟随
        //public bool methodVertical(CameraPoint Point)
        //{
        //    //检测是否在控制范围
        //    if (_target.transform.position.x > Point.transform.position.x - Point.FixedSize.x &&
        //        _target.transform.position.x < Point.transform.position.x + Point.FixedSize.x)
        //    {
        //        //跟随控制点的 X 坐标，跟随目标的 Y 坐标， 摄像头本身的 Z 坐标
        //        Vector3 Destination = new Vector3(Point.transform.position.x, _target.transform.position.y + _deltaPositon.y, _mainCameraObj.transform.position.z);
        //        //平滑移动(线性插值)
        //        Vector3 PositionNow = Vector3.Lerp(_mainCameraObj.transform.position, Destination, _followSpeed * Time.deltaTime);

        //        _mainCameraObj.transform.position = new Vector3(PositionNow.x, PositionNow.y, PositionNow.z);
        //        return true;
        //    }

        //    return false;
        //}

        ////横向跟随
        //public bool methodHorizontal(CameraPoint Point)
        //{
        //    //检测是否在控制范围
        //    if (_target.transform.position.x > Point.transform.position.x - Point.FixedSize.x &&
        //        _target.transform.position.x < Point.transform.position.x + Point.FixedSize.x)
        //    {
        //        //跟随目标的 X 坐标，控制点的 Y 坐标， 摄像头本身的 Z 坐标
        //        Vector3 Destination = new Vector3(_target.transform.position.x + _deltaPositon.x, Point.transform.position.y, _mainCameraObj.transform.position.z);
        //        //平滑移动(线性插值)
        //        Vector3 PositionNow = Vector3.Lerp(_mainCameraObj.transform.position, Destination, _followSpeed * Time.deltaTime);

        //        _mainCameraObj.transform.position = new Vector3(PositionNow.x, PositionNow.y, PositionNow.z);
        //        return true;
        //    }
        //    return false;
        //}

        ////固定摄像机模式
        //public bool methodFixed(CameraPoint Point)
        //{
        //    //检测是否在控制范围
        //    if (_target.transform.position.x > Point.transform.position.x - Point.FixedSize.x &&
        //        _target.transform.position.x < Point.transform.position.x + Point.FixedSize.x &&
        //        _target.transform.position.y > Point.transform.position.y - Point.FixedSize.y &&
        //        _target.transform.position.y < Point.transform.position.y + Point.FixedSize.y)
        //    {
        //        //跟随控制点的 X 坐标，控制点的 Y 坐标， 摄像头本身的 Z 坐标
        //        Vector3 Destination = new Vector3(Point.transform.position.x,
        //            Point.transform.position.y,
        //            _mainCameraObj.transform.position.z);
        //        //平滑移动(线性插值)
        //        Vector3 PositionNow = Vector3.Lerp(_mainCameraObj.transform.position, Destination, _followSpeed * Time.deltaTime);

        //        _mainCameraObj.transform.position = new Vector3(PositionNow.x, PositionNow.y, PositionNow.z);
        //        return true;
        //    }
        //    return false;
        //}

        ////默认跟随方式
        //public void methodFollowDefault()
        //{
        //    //跟随目标的 Y 坐标， 摄像机本身的 X, Z坐标锁定
        //    Vector3 destination = new Vector3(
        //         _mainCameraObj.transform.localPosition.x,
        //        //_target.transform.localPosition.x,
        //        _target.transform.localPosition.y,
        //        _mainCameraObj.transform.localPosition.z);

        //    //枝头未生长到中间位置
        //    if (_target.transform.localPosition.y < _cameraInitPos.y)
        //    {
        //        destination.Set(destination.x, _cameraInitPos.y, destination.z);
        //    }

        //    //平滑移动(线性插值)
        //    Vector3 positionNow = Vector3.Lerp(_mainCameraObj.transform.localPosition, destination, _followSpeed * Time.deltaTime);
        //    _mainCameraObj.transform.localPosition = new Vector3(positionNow.x, positionNow.y, _mainCameraObj.transform.localPosition.z);
        //}
    }


}
