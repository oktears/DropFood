using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{

    /// <summary>
    /// RGB分裂
    /// </summary>
    public interface IRGBSplitFilter
    {

        void setEnable(bool isEnable);

        bool isEnable();

        float getAmount();

        void setAmount(float amount);

        void setAngle(float angle);
    }
}
