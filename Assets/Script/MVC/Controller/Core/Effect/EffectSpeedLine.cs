
using System.Linq;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 速度线
    /// </summary>
    public class EffectSpeedLine : EffectBase
    {
        private int _playCount = 0;

        public EffectSpeedLine(Transform trans) : base(trans)
        {
            _effectId = EffectType.SPEED_LINE;
            _root = PrefabPool.Instance.getObject("Prefab/Effect/Speed_Line/Effect_SpeedLine");
            //_root.transform.SetParent(trans);
            _root.transform.localScale = Vector3.one;
            _root.transform.localPosition = new Vector3(0, 0, 1);
            _particle = _root.GetComponentsInChildren<ParticleSystem>().ToList();
            _root.SetActive(false);
            _root.transform.parent = Camera.main.gameObject.transform;
            //restTransform(_root.transform);
            //_root.transform.localPosition = new Vector3(0, -2, 8.5f);
        }
    }
}
