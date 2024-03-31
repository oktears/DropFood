using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{

    /// <summary>
    /// 音效实体类
    /// </summary>
    public class SoundData
    {

        /// <summary>
        /// 音效Id
        /// </summary>
        public short _soundId { get; set; }

        /// <summary>
        /// 音效路径
        /// </summary>
        public string _soundPath { get; set; }

        /// <summary>
        /// 是否循环播放
        /// </summary>
        public bool _isLoop { get; set; }

        /// <summary>
        /// 音量
        /// </summary>
        public float _volume { get; set; }

        /// <summary>
        /// 音效数据初始化
        /// </summary>
        /// <param name="SoundId">音效ID</param>
        /// <param name="SoundPath">路径</param>
        /// <param name="IsLoop">是否循环</param>
        /// <param name="Volume">音量</param>
        public SoundData(short soundId, string soundPath, bool isLoop, float volume)
        {
            this._soundId = soundId;
            this._soundPath = soundPath;
            this._isLoop = isLoop;
            this._volume = volume;
        }

        public SoundData()
        {

        }

    }


}
