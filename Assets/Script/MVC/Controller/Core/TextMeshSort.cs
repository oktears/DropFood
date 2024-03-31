using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMeshSort : MonoBehaviour {

    public MeshRenderer _renderer;

	// Use this for initialization
	void Start () {
        _renderer.sortingLayerName = "Effect";
        _renderer.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
