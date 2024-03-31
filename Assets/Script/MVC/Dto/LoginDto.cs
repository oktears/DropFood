using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    public class LoginDto : BaseDto
    {

        //1.请求微信免授权登录  
        //2.请求微信正常登录
        public int _loginType { get; set; }

        //uid
        public string _openId { get; set; }

        //pwd
        public string _token { get; set; }
    }
}
