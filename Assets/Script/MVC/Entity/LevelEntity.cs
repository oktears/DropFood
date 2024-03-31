using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// 关卡实体
    /// </summary>
    public class LevelEntity
    {
        /// <summary>
        /// 关卡Id
        /// </summary>
        public byte _id { get; set; }

        /// <summary>
        /// 是否解锁
        /// </summary>
        public bool _isUnlock { get; set; }

        /// <summary>
        /// 是否为新解锁的关卡
        /// </summary>
        public bool _isNew { get; set; }
    }
}
