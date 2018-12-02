using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrindPlayerController : MonoBehaviour {

    public CommandController commandController;
    public Player[] Players = new Player[4];
    public GameObject PersonalScore;
    private bool upPressed = false;
    private bool downPressed = false;
    private bool leftPressed = false;
    private bool rightPressed = false;
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
        if (Input.GetAxis("Horizontal") > 0 && !rightPressed)
        {
            Players[0].Score += commandController.DoTask(0, "Right");
            rightPressed = true;
        }
        if (Input.GetAxis("Horizontal") < 0 && !leftPressed)
        {
            Players[0].Score += commandController.DoTask(0, "Left");
            leftPressed = true;
        }
        if (Input.GetAxis("Horizontal") == 0)
        {
            leftPressed = false;
            rightPressed = false;
        }
        if (Input.GetAxis("Vertical") > 0 && !upPressed)
        {
            Players[0].Score += commandController.DoTask(0, "Up");
            upPressed = true;
        }
        if (Input.GetAxis("Vertical") < 0 && !downPressed)
        {
            Players[0].Score += commandController.DoTask(0, "Down");
            downPressed = true;
        }
        if(Input.GetAxis("Vertical") == 0)
        {
            upPressed = false;
            downPressed = false;
        }
        PersonalScore.GetComponent<TextMeshPro>().text = Players[0].Score.ToString("D5");
    }
}
