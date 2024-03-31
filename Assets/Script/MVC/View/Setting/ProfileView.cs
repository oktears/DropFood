using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 说明界面
    /// </summary>
    public class ProfileView : PopUpBaseView
    {

        //返回按钮
        private Button _backBtn;
        //重置游戏按钮
        private Button _resetBtn;
        //关于按钮
        private Button _aboutBtn;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.PROFILE;

            bindTarget("Prefab/UI/Setting/ProfileView", ViewManager.Instance._UIRoot2D);

            _backBtn = _viewObj.Find("Adapter/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);

            _resetBtn = _viewObj.Find("Adapter/ResetBtn").GetComponent<Button>();
            _resetBtn.addListener(clickResetBtn);

            float s1 = 16.0f / 9.0f;
            //实际分辨率比例
            float s2 = Screen.height * 1.0f / Screen.width;
            if (s2 > s1)
            {
                float aspectRatio = s2 / s1;
                _viewObj.Find("Bg").localScale = new Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }
            else
            {
                //float aspectRatio = s1 / s2;
                // _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }
        }

        private void clickBackBtn()
        {
            close();
        }

        private void clickResetBtn()
        {
            //清存档
            EntityManager.Instance._userEntity._isCheckGuide = true;
            DaoManager.Instance._userDao.saveGuide();
            CommonViewManager.Instance.showTip("guide clear");
        }
    }
}
