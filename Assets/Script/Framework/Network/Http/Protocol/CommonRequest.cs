using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Chengzi
{

    public class CommonRequest
    {
        public NetTipType type { get; set; }

        public JsonData data { get; set; }

        public CommonRequest()
        {
            data = new JsonData();
            type = NetTipType.Common;
        }
    }
}
