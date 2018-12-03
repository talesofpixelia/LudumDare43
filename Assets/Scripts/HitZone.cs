using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZone : MonoBehaviour
{
    float power = 10;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.gameObject != transform.parent.parent.gameObject)
        {
            Debug.Log("HIT !!!!!!");
            collision.GetComponent<PlayerController>().getRekt(this.transform.parent.position);
        }
    }
}
