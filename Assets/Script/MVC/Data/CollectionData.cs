using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Chengzi
{

    /// <summary>
    /// 收藏品数据
    /// </summary>
    public class CollectionData
    {

        /// <summary>
        /// 编号
        /// </summary>
        public short _id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string _icon { get; set; }

        /// <summary>
        /// 收藏品名 中
        /// </summary>
        public string _nameCN { get; set; }

        /// <summary>
        /// 收藏品信息 英
        /// </summary>
        public string _nameEN { get; set; }

        /// <summary>
        /// 收藏品信息 中
        /// </summary>
        public string _infoCN { get; set; }

        /// <summary>
        /// 收藏品信息 英
        /// </summary>
        public string _infoENG { get; set; }


    }
}
