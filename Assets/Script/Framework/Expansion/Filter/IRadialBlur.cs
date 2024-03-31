using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{
    public interface IRadialBlur
    {
        void setStrength(float strength);

        void setEnable(bool isEnable);
    }
}
