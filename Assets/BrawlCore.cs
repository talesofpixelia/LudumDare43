using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrawlCore : Singleton<BrawlCore> {

    public PlayerController[] players;
    public PlayerController playerTemplate;

	void Start () {
        players = new PlayerController[4];
        for (int i = 0; i < 4; i++)
        {
            PlayerController player = GameObject.Instantiate(playerTemplate, this.transform.parent, false) as PlayerController;
            player.playerId = i;
            players[i] = player;
        }
        playerTemplate.gameObject.SetActive(false);
	}
	
	void Update () {
		
	}
}
