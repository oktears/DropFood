using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Chengzi
{

    public class Table : MonoBehaviour
    {

        public Rigidbody2D _rigidbody;
        public BoxCollider2D _collider;

        public void Start()
        {
            this._rigidbody.sharedMaterial.bounciness = DebugConfig.s_tableBounciness;
            this._rigidbody.sharedMaterial.friction = DebugConfig.s_tableFriction;
        }

    }
}