using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 游戏时间管理器 
    /// </summary>
    public class TimeManager
    {

        public void init()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1.0f;
        }

        public void updateTimeScale(float timescale)
        {
            BusinessManager.Instance._gameBiz._timeScale = timescale;
            AudioManager.Instance.updatePitch(timescale);
        }

        public void fixedUpdate(float dt)
        {
        }

        public void update(float dt)
        {
            Time.timeScale = BusinessManager.Instance._gameBiz._timeScale;
        }

    }
}
