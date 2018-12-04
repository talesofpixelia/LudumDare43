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
    public AiGrindPlayer aiGrindPlayer;
    public ParticleSystem oPart;
    public ParticleSystem xPart;
    public ParticleSystem yPart;
    public ParticleSystem aPart;
    public ParticleSystem bPart;
   


    // Use this for initialization
    void Start () {
        if (!player.isBot)
        {
            rePlayer = Rewired.ReInput.players.GetPlayer(player.Id);
        }
        oPart = this.transform.Find("O_parts").gameObject.GetComponent<ParticleSystem>();
        xPart = this.transform.Find("X_parts").gameObject.GetComponent<ParticleSystem>();
        yPart = this.transform.Find("Y_parts").gameObject.GetComponent<ParticleSystem>();
        aPart = this.transform.Find("A_parts").gameObject.GetComponent<ParticleSystem>();
        bPart = this.transform.Find("B_parts").gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isAlive)
        {
            if (!player.isBot)
            {
                if (rePlayer.GetButtonDown("Action1"))
                {
                    Debug.Log(player.Id + " A1");
                    int result = commandController.DoTask(player.Id, "Action1");
                    if (result > 0) aPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Action2"))
                {
                    Debug.Log(player.Id + " A2");
                    int result = commandController.DoTask(player.Id, "Action2");
                    if (result > 0) bPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Action3"))
                {
                    Debug.Log(player.Id + " A3");
                    int result = commandController.DoTask(player.Id, "Action3");
                    if (result > 0) xPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Action4"))
                {
                    Debug.Log(player.Id + " A4");
                    int result = commandController.DoTask(player.Id, "Action4");
                    if (result > 0) yPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Right"))
                {
                    Debug.Log(player.Id + " R");
                    int result = commandController.DoTask(player.Id, "Right");
                    if (result > 0) oPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Left"))
                {
                    Debug.Log(player.Id + " L");
                    int result = commandController.DoTask(player.Id, "Left");
                    if (result > 0) oPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Up"))
                {
                    Debug.Log(player.Id + " U");
                    int result = commandController.DoTask(player.Id, "Up");
                    if (result > 0) oPart.Emit(10);
                    player.Score += result;
                }
                if (rePlayer.GetButtonDown("Down"))
                {
                    Debug.Log(player.Id + " D");
                    int result = commandController.DoTask(player.Id, "Down");
                    if (result > 0) oPart.Emit(10);
                    player.Score += result;
                }
            }
            else
            {
                string aiInput = aiGrindPlayer.GetInput();
                if (aiInput != null)
                {
                    int result = commandController.DoTask(player.Id, aiInput);
                    player.Score += result;
                    if (result > 0)
                    {
                        switch (aiInput)
                        {
                            case "Action1":
                                aPart.Emit(10);
                                break;
                            case "Action2":
                                bPart.Emit(10);
                                break;
                            case "Action3":
                                xPart.Emit(10);
                                break;
                            case "Action4":
                                yPart.Emit(10);
                                break;
                            default:
                                oPart.Emit(10);
                                break;
                        }
                    }
                }
            }
        }
    }

    public void ShowAction(string action)
    {
        Debug.Log(action);
        if(player.isBot)
        {
            aiGrindPlayer.ShowAction(action);
        }
    }

}
