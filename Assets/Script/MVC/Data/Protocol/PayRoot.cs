using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{
    /// <summary>支付下行root</summary>
    public class PayRoot
    {
        /// <summary></summary>
        public int ret { get; set; }

        /// <summary></summary>
        public int paytype { get; set; }

        /// <summary></summary>
        public string appId { get; set; }

        /// <summary></summary>
        public string machid { get; set; }

        /// <summary></summary>
        public string orderId { get; set; }

        /// <summary></summary>
        public string prepayid { get; set; }

        /// <summary></summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int timeStamp { get; set; }

        /// <summary></summary>
        public string packageValue { get; set; }

        /// <summary></summary>
        public string sign { get; set; }

        /// <summary></summary>
        public string alipayinfo { get; set; }
    }
}
