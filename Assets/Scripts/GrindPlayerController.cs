using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrindPlayerController : MonoBehaviour {

    public CommandController commandController;
    public Player[] Players = new Player[4];
    public GameObject PersonalScore;

    // Use this for initialization
    void Start () {
		if(commandController)
        {
             Players.CopyTo(commandController.Players,0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Action1"))
        {
            Players[0].Score += commandController.DoTask(0, "Action1");
        }
        if (Input.GetButtonDown("Action2"))
        {
            Players[0].Score += commandController.DoTask(0, "Action2");
        }
        if (Input.GetButtonDown("Action3"))
        {
            Players[0].Score += commandController.DoTask(0, "Action3");
        }
        if (Input.GetButtonDown("Action4"))
        {
            Players[0].Score += commandController.DoTask(0, "Action4");
        }
        PersonalScore.GetComponent<TextMeshPro>().text = ("" + Players[0].Score).PadLeft(5, '0');
    }
}
