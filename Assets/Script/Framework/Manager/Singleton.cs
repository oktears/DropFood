using UnityEngine;
using System.Collections;

public class Singleton<T> where T : new()
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}


public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    protected static T _instance;

    private static object _lock = new object();

    private static bool applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                 "' already destroyed on application quit." +
                                 " Won't create again - returning null.");
                return null;
            }
            lock (_lock)
            {
                if (_instance == null)
                {
                    //find first active
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                                       " - there should never be more than 1 singleton!" +
                                       " Reopenning the scene might fix it.");
                        return _instance;
                    }
                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(Singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                        singleton.transform.parent = SingletonParent.Instance.transform;
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                                  _instance.gameObject.name);
                    }
                }
                return _instance;
            }
        }
    }

    public virtual void onDestory()
    {
        applicationIsQuitting = true;
    }

    public static void destroySingleton()
    {
        if (Instance != null)
        {
            Debug.Log("DestroySinglton" + _instance.gameObject.name);
            Destroy(_instance.gameObject);
            applicationIsQuitting = true;
            _instance = null;
        }
    }
}

public class SingletonParent : MonoSingleton<SingletonParent>
{

}

