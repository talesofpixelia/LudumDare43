using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AiGrindPlayer : MonoBehaviour{
    public int difficulty = 0;
    public float[] reactionTimes = new float[3];
    public float[] mistakeProba = new float[3];
    public float PassedTime = 0;
    private bool waiting;
    public string visibleAction;
    public string input;

    private void Update()
    {
        if (waiting)
        {
            PassedTime += Time.deltaTime;
        }

        if (waiting && PassedTime > reactionTimes[difficulty] + UnityEngine.Random.Range(-0.05f, 0.1f))
        {
            if (UnityEngine.Random.value > mistakeProba[difficulty])
            {
                waiting = false;
                input = visibleAction;
            }
            else
            {
                waiting = false;
                input = "ERROR";
            }
        } else
        {
            input = null;
        }
        
    }

    public string GetInput ()
    {
        return input;
    }

    public void ShowAction (string action)
    {
        visibleAction = action;
        waiting = true;
        PassedTime = 0;
    }
}
