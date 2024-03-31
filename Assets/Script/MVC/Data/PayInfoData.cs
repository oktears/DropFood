using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{




    /// <summary>
    /// 支付信息
    /// </summary>
    public class PayInfoData
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public PayConstant.ProductId _id { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public float _price { get; set; }

        /// <summary>
        /// 金币数
        /// </summary>
        public int _gold { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        public string _name { get; set; }

        /// <summary>
        /// 商品名（繁体）
        /// </summary>
        public string _nameTW { get; set; }

        /// <summary>
        /// 商品名（英文）
        /// </summary>
        public string _nameEN { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string _desc { get; set; }

        /// <summary>
        /// 描述（繁体）
        /// </summary>
        public string _descTW { get; set; }

        /// <summary>
        /// 描述（英文）
        /// </summary>
        public string _descEN { get; set; }

        /// <summary>
        /// GooglePlay商品Id
        /// </summary>
        public string _googlePlayId { get; set; }
    }


}
