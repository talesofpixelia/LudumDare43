using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDetector : MonoBehaviour {

    public PlayerController controller;
    public CircleCollider2D collider;
    public bool isInTheAir = true;

	void Start () {
        collider = GetComponent<CircleCollider2D>();
        controller = transform.parent.GetComponent<PlayerController>();
	}
	
	void Update () {
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        isInTheAir = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInTheAir = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (controller != null)
            controller.hasTouchedDown();
    }
}
