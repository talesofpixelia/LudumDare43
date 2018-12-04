using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BrawlCore : Singleton<BrawlCore> {

    public PlayerController[] players;
    public GameObject[] spawns;
    public PlayerController playerTemplate;
    public Text mainText;
    public bool isRunning = false;
    public float countDownTimer = 0;
    public CharacterInfoCard[] InfoCards;
    public Text timerText;
    int playerAmount = 4;
    ActivePlayerList activePlayerList;

    public float timer;
    private bool finished;
    public bool doesUpdate = true;

    void Start () {
        activePlayerList = GameObject.Find("_PlayerData_").GetComponent<ActivePlayerList>();
        for (int i = 0; i < 4; i++)
        {
            InfoCards[i].gameObject.SetActive(false);
            if (i < playerAmount && activePlayerList.Players[i].isAlive == true)
                InfoCards[i].gameObject.SetActive(true);
        }
        players = new PlayerController[playerAmount];
        for (int i = 0; i < playerAmount; i++)
        {
            if (activePlayerList.Players[i].isAlive)
            {
                PlayerController player = GameObject.Instantiate(playerTemplate, this.transform.parent, false) as PlayerController;
                player.playerId = i;
                players[i] = player;
                player.weaponId = activePlayerList.Players[i].Weapon;
                player.playerName = activePlayerList.Players[i].rePlayerID;
                if (activePlayerList.Players[i].isBot)
                {
                    player.gameObject.AddComponent<NaIA>();
                    player.isBot = true;
                }
            }
        }
        playerTemplate.gameObject.SetActive(false);
        finished = false;
	}
	
	void Update () {
        if (!doesUpdate)
            return;
        if (!isRunning && !finished)
        {
            timer = 60;
            countDownTimer += Time.deltaTime;
            if (countDownTimer < 2)
            {
                mainText.text = "3";
                for (int i = 0; i < 4; i++)
                    if (players[i] != null)
                    {
                        players[i].transform.position = spawns[i].transform.position;
                    }
            }
            else if (countDownTimer < 3f)
                mainText.text = "2";
            else if (countDownTimer < 4)
                mainText.text = "1";
            else if (countDownTimer < 5f)
                mainText.text = "Let's brawl !";
            else
            {
                mainText.text = "";
                isRunning = true;
            }
        }

        if (isRunning && !finished)
        {
            timer -= Time.deltaTime;
            timerText.text = ((int)timer).ToString();
            if (timer <= 0)
                stopGame();
        }
        if (finished)
        {
            bool someoneHasBeenSacrificed = false;
            doesUpdate = false;
            for (int i = 0; i < playerAmount; i++)
            {
                if (players[i] == null)
                    continue;
                if (players[i].lives < 0)
                {
                    mainText.text = activePlayerList.Players[i].rePlayerID + " has been SACRIFICED";
                    activePlayerList.Players[i].isAlive = false;
                    someoneHasBeenSacrificed = true;
                }
                players[i].rb.simulated = false;
                players[i].transform.position = new Vector3(0, 100 + i * 10);
            }
            if (someoneHasBeenSacrificed)
                StartCoroutine(goToGrind());
            else
                StartCoroutine(goToGrindAdterSacrificingRandom());
        }
    }

    IEnumerator goToGrind()
    {
        
        yield return new WaitForSeconds(2);
        int playersAliveAmount = 0;
        int nonBotPlayersAlive = 0;
        for (int i = 0; i < 4; i++)
        {
            if (activePlayerList.Players[i].isAlive && !activePlayerList.Players[i].isBot)
            {
                playersAliveAmount++;
            }
            else if (activePlayerList.Players[i].isAlive && activePlayerList.Players[i].isBot)
            {
                nonBotPlayersAlive++;
            }
        }
        if (playersAliveAmount < 2)
            SceneManager.LoadScene(3);
        else
            SceneManager.LoadScene(1);
    }

    IEnumerator goToGrindAdterSacrificingRandom()
    {
        mainText.text = "MEDIOCRE !!";
        yield return new WaitForSeconds(1);
        mainText.text = "Someone has to be sacrificed!\nand it will be...";
        yield return new WaitForSeconds(1.5f);
        bool hasBeenSacrificed = false;
        int sacreficedId = 0;
        while (!hasBeenSacrificed)
        {
            sacreficedId = Random.Range(0, 3);
            if (!activePlayerList.Players[sacreficedId].isAlive)
                continue;
            activePlayerList.Players[sacreficedId].isAlive = false;
            hasBeenSacrificed = true;
        }
        mainText.text = activePlayerList.Players[sacreficedId].rePlayerID + " !!";
        int playersAliveAmount = 0;
        int nonBotPlayersAlive = 0;
        for (int i = 0; i < 4; i++)
        {
            if (activePlayerList.Players[i].isAlive && !activePlayerList.Players[i].isBot)
            {
                playersAliveAmount++;
            } else if (activePlayerList.Players[i].isAlive && activePlayerList.Players[i].isBot)
            {
                nonBotPlayersAlive++;
            }
        }
        yield return new WaitForSeconds(2);
        Debug.Log("azfazf");

        if (playersAliveAmount < 2)
            SceneManager.LoadScene(3);
        else
            SceneManager.LoadScene(1);
    }


    public void stopGame()
    {
        finished = true;
        isRunning = false;
    }
}
