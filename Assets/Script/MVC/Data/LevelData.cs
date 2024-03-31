using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 关卡类型
    /// </summary>
    public enum LevelType
    {
        /// <summary>
        /// 普通关
        /// </summary>
        NORMAL = 1,
        /// <summary>
        /// 结束关
        /// </summary>
        END = 2,
        /// <summary>
        /// 支线关
        /// </summary>
        BRANCH = 3,
    }

    /// <summary>
    /// 关卡数据
    /// </summary>
    public class LevelData
    {

        /// <summary>
        /// 关卡Id
        /// </summary>
        public byte _id { get; set; }

        /// <summary>
        /// 章节Id
        /// </summary>
        public byte _chapterId { get; set; }

        /// <summary>
        /// 关卡名
        /// </summary>
        public string _name { get; set; }

        /// <summary>
        /// 关卡类型
        /// </summary>
        public LevelType _levelType { get; set; }

        /// <summary>
        /// 解锁关卡
        /// </summary>
        public string _unlockLevels { get; set; }

        /// <summary>
        /// 解锁关卡列表
        /// </summary>
        public List<byte> _unlockLevelList { get; set; }

        /// <summary>
        /// 预设
        /// </summary>
        public string _prefab { get; set; }

        /// <summary>
        /// 初始河流速度
        /// </summary>
        public float _riverMaxSpeed { get; set; }

        /// <summary>
        /// 背景音乐-入场
        /// </summary>
        public int _bgmIn { get; set; }

        /// <summary>
        /// 背景音乐-中段循环
        /// </summary>
        public int _bgmLoop { get; set; }

        /// <summary>
        /// 背景音乐-收尾
        /// </summary>
        public int _bgmOut { get; set; }

        /// <summary>
        /// 是否开启泛光
        /// </summary>
        public bool _isOpenBloom { get; set; }

        /// <summary>
        /// 是否开启黑角
        /// </summary>
        public bool _isOpenLomo { get; set; }

        /// <summary>
        /// 是否开启偏轴模糊
        /// </summary>
        public bool _isOpenTiltShift { get; set; }
    }

}
