
using UnityEngine;

namespace Chengzi
{
    public class DebugConfig : MonoBehaviour
    {

        /// <summary>
        /// 是否显示设备信息
        /// </summary>
        public static bool s_isShowDeviceDialog = false;

        /// <summary>
        /// 是否为调试模式
        /// </summary>
        public static bool s_isDebugMode = false;

        /// <summary>
        /// 是否无限复活
        /// </summary>
        public static bool s_isUnlimitRelive = false;

        /// <summary>
        /// 是否显示调试日志
        /// </summary>
        public static bool s_isOpenDebugLog = false;

        /// <summary>
        /// 道具质量倍数
        /// </summary>
        public float _itemMassRatio = 1.0f;

        public static float s_itemMassRatio;

        /// <summary>
        /// 道具默认摩擦力和弹力
        /// </summary>
        public float _itemDefaultFriction = 0.2f;
        public float _itemDefaultFrictionMspo = 0.4f;
        public float _itemDefaultBounciness = 0;
        public static float s_itemDefaultFriction;
        public static float s_itemDefaultBounciness;
        public static float s_itemDefaultFrictionMspo;

        /// <summary>
        /// 桌子的默认摩擦力和阻力
        /// </summary>
        public float _tableFriction = 0.5f;
        public float _tableBounciness = 0f;
        public static float s_tableFriction;
        public static float s_tableBounciness;

        public float _blockFrictionRatio = 1.0f;
        public float _blockBouncinessRatio = 1.0f;

        /// <summary>
        /// 板糖下落后所有道具的摩擦力和弹性变化系数
        /// </summary>
        public static float s_blockFrictionRatio;
        public static float s_blockBouncinessRatio;

        private void Start()
        {
            s_itemMassRatio = _itemMassRatio;
            s_blockBouncinessRatio = _blockBouncinessRatio;
            s_blockFrictionRatio = _blockFrictionRatio;
            s_itemDefaultBounciness = _itemDefaultBounciness;
            s_itemDefaultFriction = _itemDefaultFriction;
            s_tableBounciness = _tableBounciness;
            s_tableFriction = _tableFriction;
            s_itemDefaultFrictionMspo = _itemDefaultFrictionMspo;
        }
    }
}
