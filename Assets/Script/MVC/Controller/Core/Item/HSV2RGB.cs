using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{

    [ExecuteInEditMode]
    public class HSV2RGB : MonoBehaviour
    {
        public float _hue;
        public float _saturation;
        public float _value;
        public SpriteRenderer _render;

        void Update()
        {
            _render.material.color = Color.HSVToRGB(_hue, _saturation, _value);
        }
    }
}