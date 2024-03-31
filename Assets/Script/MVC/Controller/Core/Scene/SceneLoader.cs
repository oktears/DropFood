
using System.Collections.Generic;
using UnityEngine;

namespace Chengzi
{
    /// <summary>
    /// 场景加载器
    /// </summary>
    public class SceneLoader
    {
        public GameObject _root { get; set; }

        public ItemManager _item { get; set; }

        public GameControllerBase _ctrl { get; private set; }

        private Transform _blackLayer;

        private GameObject _bgRoot;
        public List<Transform> _bgList = new List<Transform>();
        public GameObject _bgCamera { get; set; }

        public void init(GameControllerBase ctrl)
        {
            _root = PrefabPool.Instance.getObject("Prefab/Game/GameRoot2D");
            //_blackLayer = _root.transform.Find("BlackLayer");
            _ctrl = ctrl;
            _bgRoot = PrefabPool.Instance.getObject("Prefab/Game/BgRoot2D");
            _bgCamera = _bgRoot.transform.Find("BgCamera").gameObject;
            NotificationCenter.getInstance().regNotify(Event.EVENT_SHOW_BLACK_LAYER, showBlackLayer);
            initBg();
        }

        private void initBg()
        {
            //初始化生成7张背景
            for (int i = 0; i < 7; i++)
            {
                Transform bg = PrefabPool.Instance.getObject("Prefab/Game/Bg").transform;
                bg.setParent(_bgRoot.transform);
                bg.localPosition = new Vector2(0, 1334 / 2 + 1520 / 2 + i * 1520);
                bg.name = "bg_" + _bgList.Count;
                _bgList.Add(bg);
            }
        }

        public void addBg()
        {
            Transform topTrans = _bgList[_bgList.Count - 1];
            for (int i = 0; i < 6; i++)
            {
                Transform bg = PrefabPool.Instance.getObject("Prefab/Game/Bg").transform;
                bg.setParent(_bgRoot.transform);
                bg.localPosition = new Vector2(0, topTrans.localPosition.y + (i + 1) * 1520);
                bg.name = "bg_" + _bgList.Count;
                _bgList.Add(bg);
            }
        }

        public ItemManager loadItem()
        {
            _item = new ItemManager();
            _item.init(_root.transform, _ctrl);
            return _item;
        }

        public void hideBlackLayer()
        {
            //_blackLayer.gameObject.SetActive(false);
        }

        public bool showBlackLayer(int e, object o)
        {
            //_blackLayer.gameObject.SetActive(true);
            _root.SetActive(false);
            _bgRoot.SetActive(false);
            return false;
        }

        public void destory()
        {
            NotificationCenter.getInstance().unregNotify(Event.EVENT_SHOW_BLACK_LAYER, showBlackLayer);
        }
    }
}
