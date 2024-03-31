using Chengzi;

namespace Chengzi
{
    /// <summary>
    /// 界面事件
    /// </summary>
    public class ViewEvent
    {
        public ViewEvent(ViewConstant.ViewId viewId, int eventType)
        {
            this._viewID = viewId;
            this._eventType = eventType;
            this._bundleData = new Bundle();
        }

        public ViewEvent(ViewConstant.ViewId viewId, int eventType, Bundle bundleData)
        {
            this._viewID = viewId;
            this._eventType = eventType;
            this._bundleData = bundleData;
        }

        /// <summary>
        /// 界面Id
        /// </summary>
        public ViewConstant.ViewId _viewID { get; set; }

        /// <summary>
        /// 事件类型
        /// </summary>
        public int _eventType { get; set; }

        /// <summary>
        /// 绑定数据
        /// </summary>
        public Bundle _bundleData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float _delayTime { get; set; }
    }
}