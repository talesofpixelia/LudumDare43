﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class ReinitPlayerData : MonoBehaviour
{
    public GameObject[] toJoinText;
    public GameObject[] joinedText;

    public IList<Rewired.Player> rePlayerList;

    ActivePlayerList activePlayerList;
    int playerJoinedAmount = 0;
    // Use this for initialization
    void Start () {
        activePlayerList = GameObject.Find("_PlayerData_").GetComponent<ActivePlayerList>();
        for (int i = 0; i < 4; i++)
        {
            var player = activePlayerList.Players[i];
            player.isAlive = true;
            player.Score = 0;
            player.Id = i;
        }
        rePlayerList = ReInput.players.AllPlayers;
    }

    // Update is called once per frame
    void Update () {
        int i = 0;
        foreach(var player in rePlayerList)
        {
            if (player.GetButtonDown("joinGame"))
            {
                var newPlayer = activePlayerList.Players[i - 1];
                newPlayer.isBot = false;
                newPlayer.rePlayerID = newPlayer.isBot ? string.Format("Bot {0}", i) : string.Format("Player {0}", i);
                toJoinText[i - 1].SetActive(false);
                joinedText[i - 1].SetActive(true);
            }
            i++;
            if (player.GetButtonDown("startGame"))
            {
                Debug.Log("startGame");
                SceneManager.LoadScene(1);
            }
        }
    }
}
