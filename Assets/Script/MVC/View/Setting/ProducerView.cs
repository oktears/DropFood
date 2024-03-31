using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 制作人界面
    /// </summary>
    public class ProducerView : PopUpBaseView
    {

        //返回按钮
        private Button _backBtn;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.PRODUCER;

            bindTarget("Prefab/UI/Setting/ProducerView", ViewManager.Instance._UIRoot2D);

            _backBtn = _viewObj.Find("Bg/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);
        }

        private void clickBackBtn()
        {
            close();
        }

    }
}
