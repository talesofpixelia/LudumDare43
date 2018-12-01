using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");
        bool bt1 = Input.GetButton("Action1");
        Debug.Log(vAxis + " - " + hAxis + " - " + bt1 + " - ");

    }
}
