using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour {

    public bool isActive = false;
    Transform child;
    public GameObject[] trailSprites;
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
        hitTick = 0;
        isActive = true;
        return true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if (hitTick < len)
        {
            for (int i = 0; i < len; i++)
            {
                trailSprites[i].SetActive(false);
            }
            trailSprites[hitTick].SetActive(true);
            hitBox.SetActive(true);
            hitTick++;
        }
        else
        {
            for (int i = 0; i < len; i++)
                trailSprites[i].SetActive(false);
            hitBox.SetActive(false);
            isActive = false;
        }
    }
}
