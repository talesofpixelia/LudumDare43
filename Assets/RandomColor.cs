using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

    public Color[] colors;
    public static int id = 0;
	// Use this for initialization
	void Start () {
        Debug.Log(id);
        GetComponent<SpriteRenderer>().color = colors[id++];
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
