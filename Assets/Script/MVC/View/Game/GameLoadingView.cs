using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{
    /// <summary>
    /// 游戏加载界面
    /// </summary>
    public class GameLoadingView : PopUpBaseView
    {

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.GAME_LOADING;
            bindTarget("Prefab/UI/Game/GameLoadingView", ViewManager.Instance._UIRoot2D);

            float s1 = 16.0f / 9.0f;
            //实际分辨率比例
            float s2 = Screen.height * 1.0f / Screen.width;
            if (s2 > s1)
            {
                float aspectRatio = s2 / s1;
                _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }
            else
            {
                //float aspectRatio = s1 / s2;
                // _viewObj.Find("Bg").localScale = new UnityEngine.Vector3(1 * aspectRatio, 1 * aspectRatio, 1);
            }

            int idx = new System.Random().Next(0, DaoManager.Instance._commonDao._tipList.Count);

            Text smallTip = this._viewObj.Find("TipBg/SmallTip").GetComponent<Text>();
            smallTip.text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_TIPS);

            Text tipContent = this._viewObj.Find("TipBg/Tip").GetComponent<Text>();
            tipContent.text = BusinessManager.Instance._userBiz.getTipI18N(DaoManager.Instance._commonDao._tipList[idx]);
            //tipContent.text = "披薩可以阻斷對下層受力哦";
            //tipContent.text = "利用好具有消除功能的美食可以讓游戲更輕松哦";

            if (EntityManager.Instance._userEntity._curLanguage == SystemLanguage.Chinese
                || EntityManager.Instance._userEntity._curLanguage == SystemLanguage.ChineseSimplified
                 || EntityManager.Instance._userEntity._curLanguage == SystemLanguage.ChineseTraditional)
            {
                if (tipContent.text.Length > 12)
                {
                    //左上对齐
                    smallTip.alignment = TextAnchor.UpperLeft;
                }
                else
                {
                    //左中对齐
                    smallTip.alignment = TextAnchor.MiddleLeft;
                }
            }

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                smallTip.transform.localPosition = new Vector2(smallTip.transform.localPosition.x + 30, smallTip.transform.localPosition.y);
                tipContent.transform.localPosition = new Vector2(tipContent.transform.localPosition.x - 20, tipContent.transform.localPosition.y);
            }
        }


        public override void onReceive(ViewEvent e)
        {
            base.onReceive(e);
            if (e._viewID == _viewID)
            {
                if (e._eventType == ViewEventConstant.EVENT_GAME_LOADING_VIEW_LOAD_FINISH)
                {
                    close();
                }
            }

        }


    }
}
