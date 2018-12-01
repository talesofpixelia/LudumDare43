using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDetector : MonoBehaviour {

    public PlayerController controller;
    public CircleCollider2D collider;
    public bool isInAir = true;

	void Start () {
        collider = GetComponent<CircleCollider2D>();
        controller = transform.parent.GetComponent<PlayerController>();
	}
	
	void Update () {
        Debug.Log(isInAir);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInAir = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInAir = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.hasTouchedDown();
    }
}
