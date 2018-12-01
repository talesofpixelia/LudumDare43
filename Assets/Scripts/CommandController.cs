using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int PlayerCount;
    public Player[] Players = new Player[4];
    public Task[] ActiveTasks = new Task[4];
    
    const int CHANGING = 0;
    const int READY = 1;


	// Use this for initialization
	void Start () {
        State = READY;
        TopRow.SetTasks(RollNewTasks());
        ActiveTasks = RollNewTasks();
        BotRow.SetTasks(ActiveTasks);
        TimeLeft = TimePerTask;
    }
	
	// Update is called once per frame
	void Update () {
		if (READY == State && TimeLeft < 0)
        {
            StateToChanging();
        } else if (CHANGING == State && TopRow.transform.position == MidPosition.position)
        {
            StateToReady(); ;
        }
        if (READY == State) 
        {
            TimeLeft -= Time.deltaTime;
        } else
        {
            MoveOverSpeed(TopRow.gameObject, MidPosition.position, ChangeSpeed);
            MoveOverSpeed(BotRow.gameObject, BotPosition.position, ChangeSpeed);
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
        for (int i =0; i < Players.Length; i++)
        {
            int id = Random.Range(0, SymbolSprites.Length);
            sprites[i] = SymbolSprites[id];
            names[i] = TaskNames[id];
        }

        for (int i = Players.Length; i < 4; i++)
        {
            sprites[i] = EmptySymbol;
            names[i] = "";
        }
        Players.Shuffle();
        for(int i = 0; i < 4; i++)
        {
            tasks[i] = new Task(Players[i], sprites[i], names[i]);
        }

        return tasks;
    }

    public int DoTask (int player, string action)
    {
        return 0;
    }

}
