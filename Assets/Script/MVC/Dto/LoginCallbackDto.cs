using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    public class LoginCallbackDto : BaseDto
    {
        /// <summary>
        /// 1.免授权登录验证成功，直接调用登录接口
        /// 2.免授权登录验证失败，抹掉token和openId
        /// 3.微信授权登录成功
        /// 4.微信登录授权失败
        /// </summary>                                                                                                                                 
        public int _type { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string _openId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string _token { get; set; }
    }
}
