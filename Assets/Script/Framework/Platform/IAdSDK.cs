using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{

    /// <summary>
    /// 广告SDK接口
    /// </summary>
    public class IADSDK
    {

        public ADConstant.VideoADPos _lastVideoPos { get; protected set; }

        public int _itemId { get; protected set; }

        public virtual void initAD() { }

        public virtual void preloadVideoAD(ADConstant.VideoADPos videoId) { }

        public virtual void showVideoAD(ADConstant.VideoADPos videoId) { }

        public virtual void showVideoAD(ADConstant.VideoADPos videoId, int itemId) { }


        public virtual void showBannerAD(bool isShow) { }

        public virtual void showInterstitial() { }
    }
}
