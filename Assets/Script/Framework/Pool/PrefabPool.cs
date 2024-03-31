using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Chengzi
{
    public class PrefabPool : MonoSingleton<PrefabPool>
    {

        private Dictionary<string, GameObject> _pool = new Dictionary<string, GameObject>();
        private Dictionary<string, Object> _poolObj = new Dictionary<string, Object>();

        public GameObject getPrefab(string path)
        {
            if (_pool.ContainsKey(path))
            {
                //Debug.LogError("***Prefab has contain:" + path);
                return _pool[path];
            }
            else
            {
                GameObject obj = Utilities.GetPrefab(path);
                if (obj != null)
                {
                    _pool[path] = obj;
                }
                else
                {
                    Debug.LogError("[PrefabPool] can not find prefab:" + path);
                }
                return obj;
            }
        }

        public T getPrefab<T>(string path) where T : Object
        {
            //T ret = Resources.Load<T>(path);
            //_poolObj.Add(path, ret);
            return Resources.Load<T>(path);
        }

        public bool removePrefab(string path)
        {
            if (_pool.ContainsKey(path))
            {
                return _pool.Remove(path);
            }
            else
            {
                return false;
            }
        }

        public void Clear()
        {
            _pool.Clear();
        }

        public GameObject getObject(string path)
        {
            GameObject ret = null;
            GameObject prefab = getPrefab(path);
            do
            {
                if (prefab == null) break;
                //_poolObj.Add(path, prefab);
                ret = getObject(prefab);
            } while (false);
            return ret;
        }

        public GameObject getObject(GameObject prefab)
        {
            GameObject clone = null;
            do
            {
                if (prefab == null) break;
                clone = GameObject.Instantiate(prefab) as GameObject;
                // QualityManager.Instance.setParticleLevel(clone);
                clone.name = prefab.name;
            } while (false);
            return clone;
        }

        public GameObject getPrefabCloneNoOpera(string path)
        {
            GameObject prefab = getPrefab(path);
            if (prefab != null)
            {
                GameObject clone = GameObject.Instantiate(prefab) as GameObject;
                clone.name = prefab.name;
                return clone;
            }
            else
            {
                return null;
            }
        }

        public void loadPrefabByScene(SceneType sceneType)
        {
            switch (sceneType)
            {
                case SceneType.UNKNOWN:
                    break;
                case SceneType.MAIN:
                    break;
                case SceneType.GAME:
                    break;
            }
        }
    }
}
