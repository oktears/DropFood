using Chengzi;
using System.Collections;
using UnityEngine;

namespace Chengzi
{
    public class PopUpBaseView : BaseView
    {
        public override void close()
        {
            remList();
            base.close();
        }

        public virtual void remList()
        {
            ViewManager.Instance._popUpViewManager.removeView(this);
        }

        public void delayClose(float time)
        {
            startCoroutine(deleyClose(time));
        }

        private IEnumerator deleyClose(float time)
        {
            yield return new WaitForSeconds(time);
            close();
        }
    }
}
