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
    int playerAmount = 3;

    public float timer;
    private bool finished;
    public bool doesUpdate = true;

    void Start () {
        if (!doesUpdate)
            return;
        for (int i = 0; i < 4; i++)
        {
            InfoCards[i].gameObject.SetActive(false);
            if (i < playerAmount)
                InfoCards[i].gameObject.SetActive(true);
        }
        players = new PlayerController[playerAmount];
        for (int i = 0; i < playerAmount; i++)
        {
            PlayerController player = GameObject.Instantiate(playerTemplate, this.transform.parent, false) as PlayerController;
            player.playerId = i;
            players[i] = player;
        }
        playerTemplate.gameObject.SetActive(false);
        finished = false;
	}
	
	void Update () {
        if (!isRunning && !finished)
        {
            timer = 60;
            countDownTimer += Time.deltaTime;
            if (countDownTimer < 2)
            {
                mainText.text = "3";
                for (int i = 0; i < 4; i++)
                    players[i].transform.position = spawns[i].transform.position;
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
            doesUpdate = false;
            for (int i = 0; i < playerAmount; i++)
            {
                if (players[i].lives < 0)
                {
                    mainText.text = players[i].player.name + " has been SACRIFICED";
                }
                players[i].rb.isKinematic = true;
                players[i].transform.position = new Vector3(0, 100 + i * 10);
            }
            StartCoroutine(goToGrind());
        }
    }

    IEnumerator goToGrind()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    public void stopGame()
    {
        finished = true;
        isRunning = false;
    }
}
