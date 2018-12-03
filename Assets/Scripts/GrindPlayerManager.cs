using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrindPlayerManager : MonoBehaviour {

    public CommandController commandController;
    public GameObject[] PlayerGOs = new GameObject[4];
    public Player[] Players = new Player[4];
    public Sprite[] AvailableWeapons = new Sprite[4];
    public int ScytheScore;
    public int StaffScore;
    public int DaggerScore;
    public GrindResults ResultsObject;

    // Use this for initialization
    void Start () {
		if(commandController)
        {
            for(int i = 0; i < 4; i++)
            {
                Player p = PlayerGOs[i].GetComponentInChildren<GrindPlayerCtrl>().player;
                Players[i] = p;
                commandController.Players[i] = p;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
               
    }

    public void ShowWeapons()
    {
        for (int i = 0; i < PlayerGOs.Length; i++) {
            int score = PlayerGOs[i].GetComponentInChildren<GrindPlayerCtrl>().player.Score;
            if (score > ScytheScore)
            {
                PlayerGOs[i].transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = AvailableWeapons[3];
                Players[i].Weapon = 3;
            }
            else if (score > StaffScore)
            {
                PlayerGOs[i].transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = AvailableWeapons[2];
                Players[i].Weapon = 2;
            }
            else if (score > DaggerScore)
            {
                PlayerGOs[i].transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = AvailableWeapons[1];
                Players[i].Weapon = 1;
            }
            else
            {
                PlayerGOs[i].transform.Find("Weapon").GetComponent<SpriteRenderer>().sprite = AvailableWeapons[0];
                Players[i].Weapon = 0;
            }
        }

        ResultsObject.Players = Players;
    }
    
}
