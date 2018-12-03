using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandController : MonoBehaviour {

    public Sprite[] SymbolSprites = new Sprite[4];
    public string[] TaskNames = new string[4];
    public Sprite EmptySymbol;
    public TaskRow TopRow;
    public TaskRow BotRow;
    public Transform TopPosition;
    public Transform MidPosition;
    public Transform BotPosition;
    public float TimePerTask;
    public float TimeLeft;
    public float ChangeSpeed;
    public int State;
    public Player[] Players;
    public Task[] ActiveTasks = new Task[4];
    public int PlayerOrder = 0;
    public int TasksPerPlayer;
    public int TasksRemaining;
    public Animator ScoreAnimator;
    public float SceneChangeTimer;
    
    
    const int CHANGING = 0;
    const int READY = 1;
    const int SCORE = 2;


	// Use this for initialization
	void Start () {
        State = READY;
        TopRow.SetTasks(RollNewTasks());
        ActiveTasks = EmptyTasks();
        BotRow.SetTasks(ActiveTasks);
        TimeLeft = TimePerTask;
        TasksRemaining = TasksPerPlayer;
    }
	
	// Update is called once per frame
	void Update () {
        if (SCORE == State)
        {
            SceneChangeTimer -= Time.deltaTime;
            if (SceneChangeTimer < 0)
            {
                SceneManager.LoadScene("brawl");
            }
        }
        if (TasksRemaining > 0)
        {
            if (READY == State && TimeLeft < 0)
            {
                StateToChanging();
            }
            else if (CHANGING == State && TopRow.transform.position == MidPosition.position)
            {
                StateToReady();
            }
            if (READY == State)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                MoveOverSpeed(TopRow.gameObject, MidPosition.position, ChangeSpeed);
                MoveOverSpeed(BotRow.gameObject, BotPosition.position, ChangeSpeed);
            }
        } else
        {
            StateToScore();
        }
	}

    private void StateToChanging()
    {
        State = CHANGING;
    }

    
  public void MoveOverSpeed(GameObject objectToMove, Vector3 target, float speed)
    {
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, target, speed * Time.deltaTime);
    }

    private void StateToReady()
    {
        TaskRow topRow = TopRow;
        BotRow.transform.Translate(0, 2, 0);
        TopRow = BotRow;
        BotRow = topRow;
        TimeLeft = TimePerTask;
        ActiveTasks = BotRow.GetTasks();
        State = READY;
        TopRow.SetTasks(RollNewTasks());
    }

    private Task[] RollNewTasks()
    {
        Task[] tasks = new Task[4];
        Sprite[] sprites = new Sprite[4];
        string[] names = new string[4];
        for (int i = 0; i < tasks.Length; i++)
        {
            int id = Random.Range(0, SymbolSprites.Length);
            sprites[i] = SymbolSprites[id];
            names[i] = TaskNames[id];
        }
        
        Players.Shuffle();
        for(int i = 0; i < 4; i++)
        {
            Sprite s = sprites[i];
            string n = names[i];
            if (!Players[i].isAlive)
            {
                s = EmptySymbol;
                n = "";
            }
            tasks[i] = new Task(Players[i],s ,n );
        }
        TasksRemaining--;
        PlayerOrder = 0;
        return tasks;
    }

    private Task[] EmptyTasks()
    {
        Task[] tasks = new Task[4];
        
        for (int i = 0; i < 4; i++)
        {
            tasks[i] = new Task(Players[i], EmptySymbol, "");
        }
        return tasks;
    }

    public int DoTask (int playerId, string action)
    {
        if (State == CHANGING) return 0;
        int score = -50;
        for(int i = 0; i < ActiveTasks.Length; i++)
        {
            if (ActiveTasks[i].Player.Id == playerId && ActiveTasks[i].Name == action && !ActiveTasks[i].done)
            {
                score = 4 - PlayerOrder;
                ActiveTasks[i].done = true;
                PlayerOrder++;
            }
        }
        return score;
    }

    private void StateToScore()
    {
        State = SCORE;
        ScoreAnimator.SetBool("IsScore", true);
    }

}
