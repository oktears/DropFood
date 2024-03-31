
using UnityEngine;
using UnityEngine.UI;

namespace Chengzi
{

    /// <summary>
    /// 语言界面
    /// </summary>
    public class LanguageView : PopUpBaseView
    {
        //关闭按钮（空白处）
        private Button _closeBtn;
        //返回按钮
        private Button _backBtn;

        //简体中文
        private Button _cnBtn;
        //繁体中文
        private Button _twBtn;
        //英文
        private Button _enBtn;
        //对号
        private Transform _maker;

        public override void init()
        {
            base.init();
            _viewID = ViewConstant.ViewId.LANGUAGE;

            bindTarget("Prefab/UI/Setting/LanguageView", ViewManager.Instance._UIRoot2D);

            _closeBtn = _viewObj.Find("CloseBtn").GetComponent<Button>();
            _closeBtn.addListener(clickBackBtn);

            _backBtn = _viewObj.Find("Mask/Group/BackBtn").GetComponent<Button>();
            _backBtn.addListener(clickBackBtn);

            _cnBtn = _viewObj.Find("Mask/Group/CnBtn").GetComponent<Button>();
            _cnBtn.addListener(clickCNBtn);

            _enBtn = _viewObj.Find("Mask/Group/EnBtn").GetComponent<Button>();
            _enBtn.addListener(clickENBtn);

            _twBtn = _viewObj.Find("Mask/Group/TwBtn").GetComponent<Button>();
            _twBtn.addListener(clickTWBtn);

            _maker = _viewObj.Find("Mask/Group/Maker");

            updateTextI18N();
            updateMaker();

            _viewObj.Find("Mask/Group").GetComponent<TipShowAnimation>().PlayAnimation(null);
        }

        private void updateMaker()
        {
            if (EntityManager.Instance._userEntity._curLanguage == SystemLanguage.ChineseSimplified
                || EntityManager.Instance._userEntity._curLanguage == SystemLanguage.Chinese)
            {
                //简体中文
                _maker.transform.localPosition = new Vector2(_maker.localPosition.x, _cnBtn.transform.localPosition.y);
            }
            else if (EntityManager.Instance._userEntity._curLanguage == SystemLanguage.ChineseTraditional)
            {
                //繁体
                _maker.transform.localPosition = new Vector2(_maker.localPosition.x, _twBtn.transform.localPosition.y);
            }
            else
            {
                //英文
                _maker.transform.localPosition = new Vector2(_maker.localPosition.x, _enBtn.transform.localPosition.y);
            }
        }

        private void updateTextI18N()
        {
            _viewObj.Find("Mask/Group/Title/Text").GetComponent<Text>().text = BusinessManager.Instance._userBiz.getTextI18N(GameConstant.TEXT_LANGUAGE);

            if (!BusinessManager.Instance._userBiz.isChinese())
            {
                Vector2 titlePos = _viewObj.Find("Mask/Group/Title/Text").localPosition;
                _viewObj.Find("Mask/Group/Title/Text").localPosition = new Vector2(titlePos.x, titlePos.y + 8.0f);
            }
        }

        private void clickBackBtn()
        {
            _viewObj.Find("Mask/Group").GetComponent<TipHideAnimation>().PlayAnimation(close);
        }

        private void clickCNBtn()
        {
            EntityManager.Instance._userEntity._curLanguage = SystemLanguage.ChineseSimplified;
            DaoManager.Instance._userDao.saveLanguage();
            chooseLangague();
        }

        private void clickENBtn()
        {
            EntityManager.Instance._userEntity._curLanguage = SystemLanguage.English;
            DaoManager.Instance._userDao.saveLanguage();
            chooseLangague();
        }

        private void clickTWBtn()
        {
            EntityManager.Instance._userEntity._curLanguage = SystemLanguage.ChineseTraditional;
            DaoManager.Instance._userDao.saveLanguage();
            chooseLangague();
        }

        private void chooseLangague()
        {
            updateMaker();
            clickBackBtn();

            ViewEvent i18nEvent = new ViewEvent(ViewConstant.ViewId.MAIN, ViewEventConstant.EVENT_MAIN_VIEW_UPDATE_I18N);
            EventCenter.Instance.send(i18nEvent);

            ViewEvent i18nEvent2 = new ViewEvent(ViewConstant.ViewId.SETTING, ViewEventConstant.EVENT_SETTING_VIEW_UPDATE_I18N);
            EventCenter.Instance.send(i18nEvent2);
        }

    }
}
