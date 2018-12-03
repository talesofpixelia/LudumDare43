using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class ReinitPlayerData : MonoBehaviour
{
    public GameObject[] toJoinText;
    public GameObject[] joinedText;

    IList<Rewired.Player> rePlayerList;
    ActivePlayerList activePlayerList;
    int playerJoinedAmount = 0;
    // Use this for initialization
    void Start () {
        activePlayerList = GameObject.Find("_PlayerData_").GetComponent<ActivePlayerList>();
        activePlayerList.Players = new List<Player>();
        for (int i = 0; i < 4; i++)
        {
            var player = new Player();
            player.isBot = true;
            player.isAlive = true;
            player.Score = 0;
            player.Id = i;
            activePlayerList.Players.Add(player);
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
                toJoinText[i - 1].SetActive(false);
                joinedText[i - 1].SetActive(true);
            }
            i++;
            if (player.GetButtonDown("startGame"))
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
