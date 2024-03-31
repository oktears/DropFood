using UnityEngine;
using System.Collections;
using BestHTTP;
using Newtonsoft.Json;
using LitJson;
using System;

namespace Chengzi
{
    public class BaseProtocolController : LoopBaseObject
    {
        /// <summary>获得验证码
        public const string YanZhengMa = "GameMember.getValid";
        /// <summary>新用户注册
        public const string NewPlayerRegist = "GameMember.registerUser";
        /// <summary>用户登陆
        public const string PlayerLoginIn = "GameMember.login";
        //修改密码
        public const string ChangePassWord = "GameMember.forupdatePwd";
        //忘记密码
        public const string ForgetPassWord = "GameMember.forgetPwd";
        //第三方登陆
        public const string OtherLogin = "GameMember.otherLogin";
        //商城
        public const string ShopPrice = "GameOrder.getGoodPrice";
        //购买物品
        public const string BuyGoodsItem = "GameOrder.prepay";
        //请求版本号
        public const string GetVersion = "GameTools.GetVersion";


        public delegate void downCall1<T>(T data, int god);

        public BaseProtocolController()
        {
            init();
        }

        private void init()
        {
            NetworkManager.Instance._http._responseDelegate += httpResponseDelegate;
        }

        public virtual void httpResponseDelegate(HttpResponse response)
        {
        }

        private void destroy()
        {
            NetworkManager.Instance._http._responseDelegate -= httpResponseDelegate;
        }


    }
}
