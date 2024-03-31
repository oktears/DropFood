using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 游戏控制器：正常关卡模式
    /// </summary>
    public class GameControllerNormal : GameControllerBase
    {

        public override bool initScene()
        {
            base.initScene();
            _timeManager = new TimeManager();
            _timeManager.init();

            _sceneLoader = new SceneLoader();
            _sceneLoader.init(this);

            Debug.Log("初始化场景");
            return true;
        }

        public override bool initBranch()
        {

            return true;
        }

        public override bool initView()
        {
            ViewManager.Instance.getView(ViewConstant.ViewId.GAME);

            Debug.Log("初始化游戏UI");
            return true;
        }

        public override bool initCamera()
        {
            _cameraController = new CameraController();
            _cameraController.init(null, this);

            Debug.Log("初始化相机");
            return true;
        }

        public override bool initItem()
        {
            _item = _sceneLoader.loadItem();
            _effectManager = new EffectManager();

            Debug.Log("初始化道具");
            return true;
        }

        public override bool initMap()
        {

            return true;
        }

        /// <summary>
        /// 是否初始化完毕
        /// </summary>
        /// <returns></returns>
        public override bool initFinish()
        {
            return _step > 6;
        }

        public override void fixedUpdate(float dt)
        {
            base.fixedUpdate(dt);

            if (BusinessManager.Instance._gameBiz._isPause)
            {
                return;
            }

            //_cameraController.fixedUpdate(dt);
            _sceneLoader._item.fixedUpdate(dt);
            _effectManager.fixedUpdate(dt);
            _timeManager.fixedUpdate(dt);
        }

        public override void update(float dt)
        {
            base.update(dt);
            _timeManager.update(dt);

            if (BusinessManager.Instance._gameBiz._isPause
                || BusinessManager.Instance._gameBiz._isGameOver
                || !BusinessManager.Instance._gameBiz._isGameStart)
            {
                return;
            }
            _sceneLoader._item.update(dt);
        }

        public override void lateUpdate(float dt)
        {
            base.lateUpdate(dt);

            if (BusinessManager.Instance._gameBiz._isPause
                || BusinessManager.Instance._gameBiz._isGameOver
                || !BusinessManager.Instance._gameBiz._isGameStart)
            {
                return;
            }
        }

        public override void onGUI(float dt)
        {
            base.onGUI(dt);

            if (BusinessManager.Instance._gameBiz._isPause
                || BusinessManager.Instance._gameBiz._isGameOver
                || !BusinessManager.Instance._gameBiz._isGameStart)
            {
                return;
            }

            _item.onGUI(dt);
        }

        /// <summary>
        /// 检测输入
        /// </summary>
        protected override void checkInput()
        {
            base.checkInput();
        }

        public override void destroy()
        {
            base.destroy();
            //_cameraController.destory();
        }

    }
}