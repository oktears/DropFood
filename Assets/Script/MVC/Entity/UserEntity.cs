using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// 用户实体
    /// </summary>
    public class UserEntity
    {

        /// <summary>是否不是第一次游戏</summary>
        public bool _isOpenedGame { get; set; }

        /// <summary>是否开启音效</summary>
        public bool _isOpenSfx { get; set; }

        /// <summary>是否开启音乐</summary>
        public bool _isOpenBgm { get; set; }

        /// <summary>
        /// 是否购买了去广告
        /// </summary>
        public bool _isRemovedAD { get; set; }

        /// <summary>
        /// 拥有的收藏品
        /// </summary>
        public List<int> _ownCollection { get; set; }

        /// <summary>
        /// 首次游戏的时间
        /// </summary>
        public long _firstGameTimestamp { get; set; }

        /// <summary>
        /// 游戏分数
        /// </summary>
        public int _gameScore { get; set; }

        /// <summary>
        /// 金币数
        /// </summary>
        public int _goldCount { get; set; }

        /// <summary>
        /// 是否检测引导
        /// </summary>
        public bool _isCheckGuide { get; set; }

        /// <summary>
        /// 当前语言
        /// </summary>
        public SystemLanguage _curLanguage { get; set; }

        public UserEntity()
        {

        }

    }
}