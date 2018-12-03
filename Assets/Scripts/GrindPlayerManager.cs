using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrindPlayerManager : MonoBehaviour {

    public CommandController commandController;
    public Player[] Players = new Player[4];
    
    // Use this for initialization
    void Start () {
		if(commandController)
        {
             Players.CopyTo(commandController.Players,0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        
        //PersonalScore.GetComponent<TextMeshPro>().text = Players[0].Score.ToString("D5");
    }
}
