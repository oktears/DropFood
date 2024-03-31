using UnityEngine;
using System.Collections;
using Chengzi;

namespace Chengzi
{
    public class LogicSceneManager : Singleton<LogicSceneManager>
    {
        public BaseScene _curScene { get; private set; }

        public SceneType _type { get; set; }

        public void init()
        {
            _curScene = null;
            SceneManager.Instance.setDelegate(enterScene, exitScene);
        }

        public void enterScene(SceneType type) 
        {
            switch (type)
            {
                case SceneType.UNKNOWN:
                    {
                        _curScene = null;
                        _type = SceneType.UNKNOWN;
                        break;
                    }
                case SceneType.MAIN:
                    {
                        _curScene = new MainScene();
                        _type = SceneType.MAIN;
                        break;
                    }
                case SceneType.GAME:
                    {
                        _curScene = new GameScene();
                        _type = SceneType.GAME;
                        break;
                    }
                case SceneType.LAUNCH:
                    {
                        _curScene = new LaunchScene();
                        _type = SceneType.LAUNCH;
                        break;
                    }
            }
            if (_curScene != null) _curScene.onEnter();
        }

        public void exitScene(SceneType type)
        {
            if (_curScene != null) _curScene.onExit();
        }
    }


}