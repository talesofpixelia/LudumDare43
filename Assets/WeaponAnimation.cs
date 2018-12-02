using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour {

    public bool isActive = false;
    Transform child;
    public Sprite[] trailSprites;
    public GameObject hitBox;
    int len;
	// Use this for initialization
	void Awake () {
        child = transform.GetChild(0);
        len = trailSprites.Length;
	}
    public int hitTick = 0;

    public bool StartHit()
    {
        if (hitTick >= 4)
        {
            isActive = true;
            return true;
        }
        return false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hitTick < 4)
        {
        }
        else
        {

        }
        hitTick++;
    }
}
