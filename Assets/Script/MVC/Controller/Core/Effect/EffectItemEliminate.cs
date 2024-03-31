
using System.Linq;
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 道具消失特效
    /// </summary>
    public class EffectItemEliminate : EffectBase
    {

        public EffectItemEliminate(Transform trans) : base(trans)
        {
            _effectId = EffectType.ITEM_ELIMINATE;
            _root = PrefabPool.Instance.getObject("Prefab/Effect/effect_Glow_01");
            //_root.transform.SetParent(trans);
            _root.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            _root.transform.localPosition = new Vector3(0, 0, 1);
            _particle = _root.GetComponentsInChildren<ParticleSystem>().ToList();
            _root.SetActive(false);
        }

    }
}
