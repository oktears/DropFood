using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 启动场景
    /// </summary>
    public class LaunchScene : BaseScene
    {

        public override void onEnter()
        {
            _type = SceneType.LAUNCH;
            base.onEnter();
            ViewManager.Instance.init();
            ViewManager.Instance.getView(ViewConstant.ViewId.LAUNCH_LOADING);
        }

        public override void onExit()
        {
            base.onExit();
        }
    }
}
