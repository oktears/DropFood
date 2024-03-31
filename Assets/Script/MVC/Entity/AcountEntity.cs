using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{


    public class AccountEntity
    {
        public const string UID_NAME = "VX_UID";
        public const string PSW_NAME = "VX_PASSWORD";
        public const string VX_ROOT = "VX_ROOT";

        /// <summary>微信usid</summary>
        public string _openId { get; set; }

        /// <summary>微信token</summary>
        public string _token { get; set; }

        /// <summary>下行信息</summary>
        public LoginRoot _root { get; set; }

        /// <summary>
        /// 服务器时间
        /// </summary>
        public ulong _serverTime { get; set; }

        /// <summary>
        /// 是否鉴权
        /// </summary>
        public bool _isAuth { get; set; }


        /// <summary>保存vechat uid token</summary>
        /// <param name="uid"></param>
        /// <param name="psw"></param>
        public void save(string uid, string psw)
        {
            if (uid != PlayerPrefsHelper.Get<string>(UID_NAME))
            {
                this._openId = uid;
                PlayerPrefsHelper.Save<string>(UID_NAME, uid);
            }
            if (psw != PlayerPrefsHelper.Get<string>(PSW_NAME))
            {
                this._token = psw;
                PlayerPrefsHelper.Save<string>(PSW_NAME, psw);
            }
        }

        public void load()
        {
            _openId = PlayerPrefsHelper.Get<string>(UID_NAME);
            _token = PlayerPrefsHelper.Get<string>(PSW_NAME);
        }
    }
}
