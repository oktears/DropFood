
using UnityEngine;

namespace Chengzi
{
    public class Just4Test : MonoBehaviour
    {

        // Use this for initialization
        GameControllerBase controller = null;
        delegate void Function_Delegate(float dt);

        Function_Delegate update_func = null;
        Function_Delegate fixed_update_func = null;
        Function_Delegate late_update_func = null;
        Function_Delegate gui_func = null;

        void Start()
        {
            BusinessManager.Instance._gameBiz._timeScale = 1.0f;
            update_func = prepare;
            Debug.Log("Just4Test->Start()");
        }

        float _emptyLoopTime = 0;
        float _maxEmptyLoopTime = 0.24f;
        // Update is called once per frame
        void Update()
        {
            if (update_func != null)
            {
                update_func(Time.deltaTime);
            }
        }

        protected void prepare(float dt)
        {
            if (controller == null)
            {
                controller = new GameControllerNormal();
                update_func = init;
            }
        }

        protected void init(float dt)
        {
            if (!controller.initFinish())
            {
                if (_emptyLoopTime >= _maxEmptyLoopTime)
                {
                    int step = controller.stepInit();

                    //ViewEvent viewEvent = new ViewEvent(ViewConstant.ViewId.LOADING, ViewEventConstant.LOAD_PROGRESS);
                    //viewEvent._bundleData.PutFloat(EventDataKey.LAODING_PROGRESS, (float)step * 10);
                    //EventCenter.Instance.send(viewEvent);

#if DEBUG
                    Debug.Log("初始化完成步骤" + step);
#endif
                    _emptyLoopTime = 0;
                }
            }
            else
            {
                if (_emptyLoopTime >= _maxEmptyLoopTime)
                {
                    controller.onLoadingFinish();
                    update_func = controller.update;
                    fixed_update_func = controller.fixedUpdate;
                    late_update_func = controller.lateUpdate;
                    gui_func = controller.onGUI;
                }
            }
            _emptyLoopTime += Time.deltaTime;
        }

        void FixedUpdate()
        {
            if (fixed_update_func != null)
            {
                fixed_update_func(Time.fixedDeltaTime);
            }
        }

        void LateUpdate()
        {
            if (late_update_func != null)
            {
                late_update_func(Time.fixedDeltaTime);
            }
        }

        void OnGUI()
        {
            if (gui_func != null)
            {
                gui_func(Time.fixedDeltaTime);
            }
        }

        void OnDestroy()
        {
            update_func = null;
            fixed_update_func = null;
            gui_func = null;
            if (controller != null)
            {
                controller.destroy();
            }
            controller = null;
        }
    }
}
