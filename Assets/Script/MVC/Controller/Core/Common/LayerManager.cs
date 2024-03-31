
using UnityEngine;

namespace Chengzi
{

    /// <summary>
    /// 层级管理器
    /// </summary>
    public static class LayerManager
    {

        public const string NAME_WORLD = "World";
        public const string NAME_GAMESCENE = "GAMESCENE";
        public const string NAME_ROCK = "Rock";

        public static int LAYER_GAMESCENE { get; private set; }
        public static int LAYER_WORLD { get; private set; }
        public static int LAYER_ROCK { get; private set; }

        public static int MASK_WORLD { get; private set; }
        public static int MASK_GAMESCENE { get; private set; } 
        public static int MASK_ROCK { get; private set; }

        private static bool FLAG_INIT = false;

        //渲染排序层级
        //地图
        public const string SORTING_LAYER_MAP = "Map";
        //速度变换区
        public const string SORTING_LAYER_SPEED_AREA = "SpeedArea";
        //风区域
        public const string SORTING_LAYER_WIND_AREA = "WindArea";
        //河流
        public const string SORTING_LAYER_BRANCH = "Branch";
        //岩石
        public const string SORTING_LAYER_ROCK = "Rock";
        //雾
        public const string SORTING_LAYER_FOG = "Fog";
        //机关门
        public const string SORTING_LAYER_DOOR_SWITCH = "DoorSwitch";
        //圆环
        public const string SORTING_LAYER_ROUND = "Round";
        //弹簧
        public const string SORTING_LAYER_ELASTIC = "Elastic";
        //三角
        public const string SORTING_LAYER_TRIANGLE = "Triangle";
        //树
        public const string SORTING_LAYER_TREE = "Tree";
        //回溯
        public const string SORTING_LAYER_REDO = "Redo";
        //路点光
        public const string SORTING_LAYER_LIGHT_WP = "LightWP";
        //Mp
        public const string SORTING_LAYER_MP = "Mp";
        //陨石
        public const string SORTING_LAYER_AEROLITE = "Aerolite";
        //移动光
        public const string SORTING_LAYER_LIGHT_MOVE = "LightMove";
        //逸散
        public const string SORTING_LAYER_LIGHT_TAP = "LightTap";
        //光
        public const string SORTING_LAYER_LIGHT = "Light";
        //云
        public const string SORTING_LAYER_CLOUD = "Cloud";
        //黑暗层
        public const string SORTING_LAYER_DARKNESS = "Darkness";
        //夜光
        public const string SORTING_LAYER_NIGHT_LIGHT = "NightLight";
        //UI
        public const string SORTING_LAYER_UI = "UI";

        public static void init()
        {
            if (FLAG_INIT) return;
            // 初始化

            LAYER_WORLD = LayerMask.NameToLayer(NAME_WORLD);
            MASK_WORLD = LayerMask.GetMask(NAME_WORLD);

            LAYER_GAMESCENE = LayerMask.NameToLayer(NAME_GAMESCENE);
            MASK_GAMESCENE = LayerMask.GetMask(NAME_GAMESCENE);

            LAYER_ROCK = LayerMask.NameToLayer(NAME_ROCK);
            MASK_ROCK = LayerMask.GetMask(NAME_ROCK);

            FLAG_INIT = true;
        }

    }
}
