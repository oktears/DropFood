using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chengzi
{
    public interface IGroundGlassFilter
    {

        void setEnable(bool isEnable);

        void setCallback(ShaderManager.TextureCallback callback);

        void setSupport(bool isSupport);

    }
}
