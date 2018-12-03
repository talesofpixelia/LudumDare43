using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindPlayerCtrl : MonoBehaviour {

    public Rewired.Player rePlayer;
    public Player player;
    public GameObject PersonalScore;
    private bool upPressed = false;
    private bool downPressed = false;
    private bool leftPressed = false;
    private bool rightPressed = false;
    public CommandController commandController;
    public ParticleSystem oPart;
    public ParticleSystem xPart;
    public ParticleSystem yPart;
    public ParticleSystem aPart;
    public ParticleSystem bPart;
   


    // Use this for initialization
    void Start () {
        rePlayer = Rewired.ReInput.players.GetPlayer(player.Id);
        oPart = this.transform.Find("O_parts").gameObject.GetComponent<ParticleSystem>();
        xPart = this.transform.Find("X_parts").gameObject.GetComponent<ParticleSystem>();
        yPart = this.transform.Find("Y_parts").gameObject.GetComponent<ParticleSystem>();
        aPart = this.transform.Find("A_parts").gameObject.GetComponent<ParticleSystem>();
        bPart = this.transform.Find("B_parts").gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
		if (rePlayer.GetButtonDown("Action1"))
        {
            Debug.Log(player.Id + " A1");
            int result = commandController.DoTask(0, "Action1");
            if (result > 0) aPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Action2"))
        {
            Debug.Log(player.Id + " A2");
            int result = commandController.DoTask(0, "Action2");
            if (result > 0) bPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Action3"))
        {
            Debug.Log(player.Id + " A3");
            int result = commandController.DoTask(0, "Action3");
            if (result > 0) xPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Action4"))
        {
            Debug.Log(player.Id + " A4");
            int result = commandController.DoTask(0, "Action4");
            if (result > 0) yPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Right"))
        {
            Debug.Log(player.Id + " R");
            int result = commandController.DoTask(0, "Right");
            if (result > 0) oPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Left"))
        {
            Debug.Log(player.Id + " L");
            int result = commandController.DoTask(0, "Left");
            if (result > 0) oPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Up"))
        {
            Debug.Log(player.Id + " U");
            int result = commandController.DoTask(0, "Up");
            if (result > 0) oPart.Emit(6);
            player.Score += result;
        }
        if (rePlayer.GetButtonDown("Down"))
        {
            Debug.Log(player.Id + " D");
            int result = commandController.DoTask(0, "Down");
            if (result > 0) oPart.Emit(6);
            player.Score += result;
        }
	}
}
