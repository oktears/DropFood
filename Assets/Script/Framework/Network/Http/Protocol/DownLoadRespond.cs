using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    public abstract class DownLoadRespond
    {
        public WWW www;

        public DownLoadType type;

        public abstract void update();
    }
}
