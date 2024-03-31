using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chengzi
{
    public class ItemSeqData
    {

        /// <summary>
        /// 组Id
        /// </summary>
        public int _groupId { get; set; }

        /// <summary>
        /// 概率
        /// </summary>
        public string _prob { get; set; }

        /// <summary>
        /// 道具概率
        /// </summary>
        public Dictionary<int[], float> _itemPropDict { get; set; }

        /// <summary>
        /// 道具概率数组
        /// </summary>
        public float[] _itemPropArr { get; set; }
    }
}
