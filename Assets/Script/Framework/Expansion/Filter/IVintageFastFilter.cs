using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    /// <summary>
    /// 校色滤镜
    /// </summary>
    public interface IVintageFastFilter
    {
        void setEnable(bool isEnable);

        void setAmount(float amount);
    }
}
