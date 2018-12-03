using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void GenericCall();

public class NaIA : MonoBehaviour
{
    PlayerController controller;
    List<PlayerController> opponnents;
    PlayerController nearestOpponent = null;
    float hitDelay = 0.3f, lastHit = 0;
    // Use this for initialization
    void Start ()
    {
        controller = GetComponent<PlayerController>();
        controller.isBot = true;
        opponnents = new List<PlayerController>();
        foreach (var p in BrawlCore.Instance.players)
        {
            if (p != controller)
            {
                opponnents.Add(p);
            }
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (BrawlCore.Instance.isRunning == false)
            return;
        if (controller.playerId == 0)
        {
            controller.isBot = false;
            GameObject.Destroy(this);
        }
        getNearestPlayer();
        lastHit += Time.deltaTime;
        if (nearestOpponent != null)
        {
            if (nearestOpponent.transform.position.x > controller.transform.position.x)
                controller.hAxis = 1;
            else
                controller.hAxis = -1;
            if (nearestOpponent.transform.position.y > controller.transform.position.y)
            {
                if (nearestOpponent.transform.position.y - controller.transform.position.y >
                    Mathf.Abs(nearestOpponent.transform.position.x - controller.transform.position.x))
                {
                    StartCoroutine(delayedCall(controller.jump, 25, 50));
                }
            }
            if (Vector3.Distance(nearestOpponent.transform.position, controller.transform.position) < 1 &&
                lastHit > hitDelay)
            {
                StartCoroutine(delayedCall(controller.hit, 50, 100));
                lastHit = 0;
            }
        }
    }

    IEnumerator delayedCall(GenericCall func, float minReactionTime, float maxReactionTime)
    {
        yield return new WaitForSeconds(Random.Range(minReactionTime / 1000f, maxReactionTime / 1000f));
        func();
    }

    void getNearestPlayer()
    {
        float distance = float.MaxValue;
        nearestOpponent = null;
        foreach (var p in opponnents)
        {
            float newDist = Vector3.Distance(controller.transform.position, p.transform.position);
            if (newDist < distance)
            {
                nearestOpponent = p;
                distance = newDist;
            }
        }
    }
}
