
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 游戏控制器 基类
    /// </summary>
    public class GameControllerBase
    {

        //游戏时间
        public float _eclipseTime = 0;
        private float _timer = 0;
        //游戏时间：秒
        private int _gameTime = 60;
        //加载步骤
        public int _step = 1;

        public SceneLoader _sceneLoader { get; protected set; }
        public CameraController _cameraController { get; protected set; }
        public ItemManager _item { get; protected set; }
        public TimeManager _timeManager { get; set; }
        public EffectManager _effectManager { get; set; }

        // 分步初始化
        public virtual int stepInit()
        {
            do
            {
                switch (_step)
                {
                    case 1: { if (initScene()) { _step++; } break; }
                    case 2: { if (initItem()) { _step++; } break; }
                    case 3: { if (initMap()) { _step++; } break; }
                    case 4: { if (initBranch()) { _step++; } break; }
                    case 5: { if (initCamera()) { _step++; } break; }
                    case 6: { if (initView()) { _step++; } break; }
                }
            } while (false);
            return _step;
        }

        public virtual bool initScene()
        {
            NotificationCenter.getInstance().regNotify(Event.EVENT_FINISH_GAME, finishGame);

            return false;
        }

        public virtual bool initBranch()
        {
            return false;
        }

        public virtual bool initCamera()
        {
            return false;
        }

        public virtual bool initMap()
        {
            return false;
        }

        public virtual bool initItem()
        {
            return false;
        }

        public virtual bool initView()
        {
            return false;
        }

        /// <summary>
        /// 是否初始化完毕
        /// </summary>
        /// <returns></returns>
        public virtual bool initFinish()
        {
            return false;
        }

        /// <summary>暂停游戏</summary>
        public virtual void pause()
        {
            BusinessManager.Instance._gameBiz._isPause = true;
            // BusinessManager.Instance._gameBiz._timeScale = 0;
        }

        /// <summary>恢复游戏</summary>
        public virtual void resume()
        {
            BusinessManager.Instance._gameBiz._isPause = false;
            //BusinessManager.Instance._gameBiz._timeScale = 1.0f;
        }

        /// <summary>开始 </summary>
        public virtual void start()
        {
            BusinessManager.Instance._gameBiz._isGameStart = true;
            _item.startGame();
        }

        public bool finishGame(int e, object o)
        {
            if (BusinessManager.Instance._gameBiz._isPause
                || BusinessManager.Instance._gameBiz._isGameOver)
            {
                return false;
            }
            AudioManager.Instance.play(AudioManager.SOUND_SFX_FAIL);
            _item.scatteringAnim();
            _item.gameOverFlyAnim();
            _cameraController.changeFinishCamera();

            float delayTime = 3.0f + BusinessManager.Instance._gameBiz._cameraMoveTimes * 0.1f + 1.0f;
            vp_Timer.In(delayTime, new vp_Timer.Callback(() =>
            {
                //PlatformManager.Instance.runOnUIThread().showSimpleDialog("", "-----" + BusinessManager.Instance._gameBiz._totalScore, "ok");
                BusinessManager.Instance._gameBiz._timeScale = 1;
                BusinessManager.Instance._gameBiz.reset();
                //BusinessManager.Instance._gameBiz._isMspoMode = true;
                AudioManager.Instance.stopBGM();
                SceneManager.Instance.LoadScene(SceneType.MAIN, false);
            }));


            return false;
        }

        /// <summary>
        /// 结束游戏
        /// </summary>
        public virtual void finishGame()
        {
            if (BusinessManager.Instance._gameBiz._isPause
                || BusinessManager.Instance._gameBiz._isGameOver)
            {
                return;
            }

            AudioManager.Instance.fadeOutBg(1.0f);
            AudioManager.Instance.play(AudioManager.SOUND_SFX_FAIL);
            _item.scatteringAnim();
            _item.gameOverFlyAnim();
            _cameraController.changeFinishCamera();

            BusinessManager.Instance._userBiz.updateHistoryScore(BusinessManager.Instance._gameBiz._score);
            BusinessManager.Instance._userBiz.addGold(BusinessManager.Instance._gameBiz._gainGold);
            BusinessManager.Instance._userBiz.updateCollection(BusinessManager.Instance._gameBiz._activeCollection);

            BusinessManager.Instance._gameBiz._isGameOver = true;
            BusinessManager.Instance._gameBiz._isPause = true;

            ViewManager.Instance.getView(ViewConstant.ViewId.GAME_RESULT);

            // GooglePlayManager.Instance.postScore(BusinessManager.Instance._gameBiz._score);
        }

        public virtual void readyStart()
        {
            BusinessManager.Instance._gameBiz._isPause = false;
            start();
        }

        public virtual void update(float dt)
        {
            checkInput();
            //checkGameEnd();
            ///////////////
            if (!BusinessManager.Instance._gameBiz._isPause
                && !BusinessManager.Instance._gameBiz._isGameOver
                && BusinessManager.Instance._gameBiz._isGameStart)
            {
                _eclipseTime += dt;
                _timer += dt;
                if (_timer >= 1)
                {
                    _timer = 0;
                    --_gameTime;
                    if (_gameTime <= 0)
                    {
                        //finishGame();
                    }
                    // (ViewManager.Instance._firstViewManager._curView as GameView).updateGameTime(_gameTime);
                }
                BusinessManager.Instance._gameBiz._eclipseTime = _eclipseTime;
            }
        }

        public virtual void fixedUpdate(float dt)
        {
        }

        public virtual void lateUpdate(float dt)
        {
        }

        public virtual void onGUI(float dt)
        {
        }

        public virtual bool checkGameEnd()
        {

            return false;
        }

        public virtual bool checkGameStart()
        {
            if (BusinessManager.Instance._gameBiz._isGameStart)
                return true;

            return false;
        }

        /// <summary>
        /// 检测输入
        /// </summary>
        protected virtual void checkInput()
        {
            if (BusinessManager.Instance._gameBiz._isPause)
            {
                pause();
            }
            else
            {
                resume();
            }
        }

        /// <summary>
        /// 加载完成
        /// </summary>
        public virtual void onLoadingFinish()
        {
            readyStart();
            //发送消息给Loading界面，让其关闭
            ViewEvent ve = new ViewEvent(ViewConstant.ViewId.GAME_LOADING,
                ViewEventConstant.EVENT_GAME_LOADING_VIEW_LOAD_FINISH);
            EventCenter.Instance.send(ve);
        }

        public virtual void destroy()
        {
            NotificationCenter.getInstance().unregNotify(Event.EVENT_FINISH_GAME, finishGame);
            _item.destory();
            _sceneLoader.destory();
            PrefabPool.Instance.Clear();
        }

        public void readyFinish()
        {
            _sceneLoader.hideBlackLayer();
            ViewEvent hideEvent = new ViewEvent(ViewConstant.ViewId.GAME, ViewEventConstant.EVENT_GAME_VIEW_HIDE_BLACK_LAYER);
            EventCenter.Instance.send(hideEvent);
        }

        public void addGameTime(int time)
        {
            _gameTime += time;
        }
    }
}
