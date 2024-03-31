using UnityEngine;
using System.Collections;

namespace Chengzi
{
    public class ProtocolControllerManager : Singleton<ProtocolControllerManager>
    {

        public CommonProtocolController _commonProtocolController { get; private set; }


        public void init()
        {
            _commonProtocolController = new CommonProtocolController();
        }

    }
}