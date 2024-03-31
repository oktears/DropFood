
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 游戏业务逻辑
    /// </summary>
    public class GameBiz
    {
        /// <summary>
        /// 暂停状态
        /// </summary>
        public bool _isPause { get; set; }

        /// <summary>
        /// 游戏是否开始
        /// </summary>
        public bool _isGameStart { get; set; }

        /// <summary>
        /// 游戏是否结束
        /// </summary>
        public bool _isGameOver { get; set; }

        /// <summary>
        /// 场景预设路径  
        /// </summary>
        public string _scenePath { get; set; }

        /// <summary>
        /// 时间缩放
        /// </summary>
        public float _timeScale { get; set; }

        /// <summary>
        /// 获得的金币
        /// </summary>
        public int _gainGold { get; set; }

        /// <summary>
        /// 下一个道具Id
        /// </summary>
        public int _nextItemId { get; set; }

        /// <summary>
        /// 激活的收藏品
        /// </summary>
        public Dictionary<int, bool> _activeCollection = new Dictionary<int, bool>();

        /// <summary>
        /// 引导步骤
        /// </summary>
        public int _guideStep { get; set; }

        /// <summary>
        /// 是否正在引导
        /// </summary>
        public bool _isGuiding { get; set; }

        /// <summary>
        /// 新记录
        /// </summary>
        public bool _isNewRecord { get; set; }

        /// <summary>
        /// 游戏总时间
        /// </summary>
        public float _eclipseTime { get; set; }

        /// <summary>
        /// 分数
        /// </summary>
        public int _score { get; set; }

        /// <summary>
        /// 摄像机上移次数
        /// </summary>
        public int _cameraMoveTimes { get; set; }

        /// <summary>
        /// 是否mspo超时
        /// </summary>
        public bool _isMspoTimeOut { get; set; }

        /// <summary>
        /// 是否从收藏品界面回到主界面
        /// </summary>
        public bool _isBackHomeInCollectionView { get; set; }

        /// <summary>
        /// 记录点的分数
        /// </summary>
        public int _recordScore { get; set; }

        /// <summary>
        /// 记录主摄像机位置
        /// </summary>
        public float _recordMainCameraY { get; set; }

        /// <summary>
        /// 记录背景摄像机位置
        /// </summary>
        public float _recordBgCameraY { get; set; }

        /// <summary>
        /// 记录点的下落个数
        /// </summary>
        public int _recordDropCount { get; set; }

        /// <summary>
        /// 记录点的下一个掉落物品Id
        /// </summary>
        public int _recordNextItemId { get; set; }

        /// <summary>
        /// 剩余复活次数
        /// </summary>
        public int _reliveTimes { get; set; }

        public void init()
        {
            reset();
        }

        public void reset()
        {
            _isPause = true;
            _isGameOver = false;
            _gainGold = 0;
            _isGameStart = false;
            _nextItemId = 0;
            _guideStep = 0;
            _isGuiding = false;
            _isNewRecord = false;
            _eclipseTime = 0;
            _score = 0;
            _cameraMoveTimes = 0;
            //_isMspoMode = false;
            _isMspoTimeOut = true;
            _isBackHomeInCollectionView = false;
            _recordScore = 0;
            _reliveTimes = 0;

            _activeCollection.Clear();
            foreach (var item in DaoManager.Instance._gameDao._itemDataDict)
            {
                _activeCollection.Add(item.Value._itemId, false);
            }
        }

        /// <summary>
        /// 激活收藏品
        /// </summary>
        /// <param name="itemId"></param>
        public void activeCollection(int itemId)
        {
            if (!BusinessManager.Instance._userBiz.isOwnCollection(itemId))
            {
                _activeCollection[itemId] = true;
            }
        }

    }
}
