using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Chengzi
{
    public class PopBoxView
    {
        private DialogType _dialogType;
        private Button _sureBtn;
        private Button _cancelBtn;
        private Text _contentText;
        private RectTransform _viewObj;
        private Button _closeBtn;
        /// <summary>
        /// 按钮回调委托
        /// </summary>
        public delegate void DialogDelegate(PopBoxView sender);
        public delegate void DialogDelegateWithData(PopBoxView sender, Bundle bundleData);



        public enum DialogType
        {
            UNKNOWN = 0,
            /// <summary>
            /// 单按钮确认对话框
            /// </summary>
            SURE = 1,
            /// <summary>
            /// 双按钮，确认&取消对话框
            /// </summary>
            SUER_OR_CANCEL = 2
        }

        public static PopBoxView create(string content)
        {
            PopBoxView view = new PopBoxView();
            view.init(content);
            return view;
        }

        public static PopBoxView create(string content, DialogType type)
        {
            PopBoxView view = new PopBoxView();
            view.init(content, type);
            return view;
        }

        private void init(string content)
        {
            _dialogType = DialogType.SURE;
            this.init(content, DialogType.SURE);
        }

        private void init(string content, DialogType type)
        {
            _dialogType = type;
            this.initView();
            this.initContent(content);
            this.initButton(type);
        }

        private void initView()
        {
            _viewObj = PrefabPool.Instance.getObject("UI/Prefab/Common/CommonPopView").GetComponent<RectTransform>();
            _viewObj.parent = CommonViewManager.Instance._popViewNode;
            _viewObj.localScale = Vector3.one;
            _viewObj.localPosition = Vector3.zero;
            _viewObj.localRotation = Quaternion.identity;
            _sureBtn = _viewObj.transform.Find("Bg/ConfirmBtn").GetComponent<Button>();
            _cancelBtn = _viewObj.transform.Find("Bg/CancelBtn").GetComponent<Button>();
            _contentText = _viewObj.transform.Find("Bg/Content").GetComponent<Text>();
            _closeBtn = _viewObj.transform.Find("Bg/CloseBtn").GetComponent<Button>();
        }

        private void initButton(DialogType type)
        {
            switch (type)
            {
                case DialogType.UNKNOWN:
                    break;
                case DialogType.SURE:
                    _cancelBtn.transform.gameObject.SetActive(false);
                    _sureBtn.transform.gameObject.SetActive(true);
                    _sureBtn.gameObject.GetComponent<RectTransform>().localPosition = new Vector3(0, -70, 0);
                    _sureBtn.addListener(close);
                    _closeBtn.addListener(close);
                    break;
                case DialogType.SUER_OR_CANCEL:
                    _cancelBtn.transform.gameObject.SetActive(true);
                    _sureBtn.transform.gameObject.SetActive(true);
                    _cancelBtn.addListener(close);
                    _sureBtn.addListener(close);
                    _closeBtn.addListener(close);
                    break;
            }
        }

        /// <summary> 初始化内容/// </summary>

        private void initContent(string content)
        {
            this._contentText.text = content;
        }


        /// <summary> 设置确定按钮回调 </summary>
        public void setSureButtonCallback(DialogDelegate del)
        {
            _sureBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
            {
                del(this);
            });

            if (_dialogType == DialogType.SURE)
            {
                _closeBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
                {
                    del(this);
                });
            }
        }
        /// <summary> 设置确定按钮回调</summary>
        public void setSureButtonCallback(DialogDelegateWithData del, Bundle bundleData)
        {
            _sureBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
            {
                del(this, bundleData);
            });
            if (_dialogType == DialogType.SURE)
            {
                _closeBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
                {
                    del(this, bundleData);
                });
            }
        }

        /// <summary>设置取消按钮回调 </summary>

        public void setCancelButtonCallback(DialogDelegate del)
        {
            _sureBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
            {
                del(this);
            });

            if (_dialogType == DialogType.SUER_OR_CANCEL)
            {
                _closeBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
                {
                    del(this);
                });
            }
        }

        /// <summary>设置取消按钮回调</summary>
        public void SetCancelButtonCallback(DialogDelegateWithData del, Bundle bundleData)
        {
            _sureBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
            {
                del(this, bundleData);
            });

            if (_dialogType == DialogType.SUER_OR_CANCEL)
            {
                _closeBtn.BindButtonEvent(EventTriggerType.PointerClick, (e) =>
                {
                    del(this, bundleData);
                });
            }
        }

        /// <summary> 关闭对话框 </summary>
        public void close()
        {
            if (_viewObj != null)
            {
                GameObject.Destroy(_viewObj.gameObject);
                CommonViewManager.Instance.setPopViewIsShow(false);
            }
        }

    }
}

