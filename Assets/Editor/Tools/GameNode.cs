using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameNode : IComparable<GameNode>
{

    public GameObject obj;
    public string FullPath = string.Empty;
    public string RelPath = string.Empty;
    public string name = string.Empty;
    public Transform transform;

    #region IComparable implementation
    public int CompareTo(GameNode other)
    {
        return this.FullPath.CompareTo(other.FullPath);
    }
    #endregion
}

