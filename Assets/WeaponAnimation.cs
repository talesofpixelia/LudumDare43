using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour {

    public bool isActive = false;
    public float rotation = 0;
    Transform child;
	// Use this for initialization
	void Awake () {
        child = transform.GetChild(0);
	}

    public void StartHit()
    {
        if (!isActive)
        {
            isActive = true;
            rotation = -50;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if (isActive)
        {
            rotation += Time.deltaTime * 600f;
            if (rotation >= 50f)
                isActive = false;
        }
        transform.localRotation = Quaternion.Euler(0, 0, rotation);
        if (rotation >= 50 || isActive == false)
            child.gameObject.SetActive(false);
        else
            child.gameObject.SetActive(true);
    }
}
