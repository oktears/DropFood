using System.Collections;
using UnityEditor;
using UnityEngine;

public class BaseEditor<T> : Editor where T : MonoBehaviour
{
    protected T TargetMonoScript;

    void OnEnable()
    {
        TargetMonoScript = target as T;
    }
}