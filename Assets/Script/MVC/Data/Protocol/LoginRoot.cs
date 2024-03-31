using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{
    /// <summary>登录下行root</summary>
    public class LoginRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public int ret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Passport passport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string session_key { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int dayrolltimes { get; set; }

    }
}
