using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 记录道具的状态
    /// </summary>
    public class RecordItemData
    {
        public Vector3 _pos { get; set; }

        public Quaternion _rotation { get; set; }

        public long _instanceId { get; set; }
    }
}
