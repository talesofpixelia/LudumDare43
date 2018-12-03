using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrawlCore : Singleton<BrawlCore> {

    public PlayerController[] players;
    public GameObject[] spawns;
    public PlayerController playerTemplate;
    public Text mainText;
    public bool isRunning = false;
    public float timer = 0;

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
        timer += Time.deltaTime;
        if (timer < 2)
        {
            mainText.text = "3";
            for (int i = 0; i < 4; i++)
                players[i].transform.position = spawns[i].transform.position;
        }
        else if (timer < 3f)
            mainText.text = "2";
        else if (timer < 4)
            mainText.text = "1";
        else if (timer < 5f)
            mainText.text = "Let's brawl !";
        else if (timer < 6f)
        {
            mainText.text = "";
            isRunning = true;
        }
    }
}
