using System.Collections;
using UnityEngine;

namespace Chengzi
{
    public class BaseScene
    {
        public SceneType _type { get; protected set; }
        ///进入场景		
        public virtual void onEnter()
        {
            //Debug.Log("进入场景");
        }

        ///退出场景
        public virtual void onExit()
        {
            ViewManager.Instance._firstViewManager.closeView();
            ViewManager.Instance._popUpViewManager.closeAll();
            //Debug.Log("退出场景");
        }
    }
}