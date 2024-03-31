using UnityEngine;
using System.Collections;

namespace Chengzi
{
    public class MainScene : BaseScene
    {
        public override void onEnter()
        {
            _type = SceneType.MAIN;
            base.onEnter();
            ViewManager.Instance.getView(ViewConstant.ViewId.MAIN);
            LifeCycleManager.Instance._isEnterMain = true;
        }

        public override void onExit()
        {
            base.onExit();
        }
    }
}
