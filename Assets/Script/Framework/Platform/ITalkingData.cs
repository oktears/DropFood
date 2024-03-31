using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// TalkingData接口
    /// </summary>
    public class ITalkingData
    {

        public void onStart()
        {
            //TalkingDataGA.OnStart("9D6E78D4584D4435A8B2BFE4B9810744", PlatformManager.Instance._deviceInfo.getChannelId());
        }

        public void onEnd()
        {
            //TalkingDataGA.OnEnd();
        }

        public void initAccount()
        {
            //TalkingDataGA.OnStart("9D6E78D4584D4435A8B2BFE4B9810744", PlatformManager.Instance._deviceInfo.getChannelId());
            //TDGAAccount account = TDGAAccount.SetAccount(PlatformManager.Instance._deviceInfo.getDeviceId());
            //account.SetAccountName(EntityManager.Instance._userEntity._name);
            //account.SetGameServer(PlatformManager.Instance._deviceInfo.getAppVersionName());   
        }

        public void requestPay(string orderId, string pname, float price, string payType)
        {
            //TDGAVirtualCurrency.OnChargeRequest(orderId, pname, price, "CNY", 0, payType);
        }

        public void paySuccess(string orderId)
        {
            //TDGAVirtualCurrency.OnChargeSuccess(orderId);
        }
    }
}
