using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    //关卡模式
    public enum LevelMode
    {
        //关卡编辑模式(对应Launcher上的TestScene, 编辑关卡时测试当前编辑的关卡时使用)
        EDIT = 1,
        //关卡测试模式(对应Laucher上的TestSceneList, 方便测试多个关卡时使用，此模式所有关卡默认解锁)
        TEST = 2,
        //配置表模式(正式模式，读取system/level.xls下配置的关卡数据，此模式关卡走正常解锁逻辑)
        CONFIG = 3,
    }

    public class Launcher : MonoBehaviour
    {

        public List<string> _testSceneList;
        public static List<string> _testScenePathList = new List<string>();

        public string _testScene;
        public static string _testScenePath;

        ////关卡编辑模式
        //public bool _isEditMode;
        //public static bool _isEdit;

        /// <summary>
        /// 关卡模式
        /// </summary>
        public LevelMode _levelMode = LevelMode.TEST;
        public static LevelMode s_levelMode;

        //测试无限地图
        public static bool _isTestUnlimitMap = false;
        //开启反向
        public static bool _isOpenReverse = true;

        public void Awake()
        {
            _testScenePathList = _testSceneList;
            _testScenePath = _testScene;

            s_levelMode = _levelMode;

            DontDestroyOnLoad(this.gameObject);

            //监听平台Native消息
            //PlatformContext.ListenMessage();

            MainLoop.Instance.init(this);
            LogicSceneManager.Instance.init();
            LogicSceneManager.Instance.enterScene(SceneType.LAUNCH);
        }

        public void Update()
        {
            MainLoop.Instance.gameUpdate();
            NetworkManager.Instance.update();
            EventCenter.Instance.update(Time.deltaTime);

            // if (Application.platform == RuntimePlatform.Android
            //     && (Input.GetKeyDown(KeyCode.Escape)))
            // {
            //     PlatformManager.Instance._sdkBridge.quitApp();
            // }

        }

        public void FixedUpdate()
        {
            MainLoop.Instance.gameFixedUpdate();
        }

        public void LateUpdate()
        {
            MainLoop.Instance.gameLateUpdate();
        }

        public void OnDestroy()
        {

        }

        void OnApplicationPause(bool isPause)
        {
            LifeCycleManager.Instance.onApplicationPause(isPause);
        }

        void OnApplicationQuit()
        {
            LifeCycleManager.Instance.onQuit();
        }

        /// <summary>
        /// 接收Native层消息
        /// </summary>
        /// <param name="msg"></param>
        public void receiveMsg(string msg)
        {
            string[] arr = XDParseUtil.parseStringArray(msg, ';');
            DtType type = GenericityUtil.convertType<DtType>(arr[0]);

            switch (type)
            {
                default:
                    break;
            }
        }
    }
}