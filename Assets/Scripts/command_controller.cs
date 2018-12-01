using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class command_controller : MonoBehaviour {

    public Sprite[] SymbolSprites = new Sprite[4];
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
    public Color[] PlayerColor = new Color[4];
    const int CHANGING = 0;
    const int READY = 1;


	// Use this for initialization
	void Start () {
        State = READY;
        TopRow.SetSymbols(RollNewTasks(PlayerCount));
        TopRow.SetColors(PlayerColor.Shuffle());
        BotRow.SetSymbols(RollNewTasks(PlayerCount));
        BotRow.SetColors(PlayerColor.Shuffle());
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
        State = READY;
        TopRow.SetSymbols(RollNewTasks(PlayerCount));
        TopRow.SetColors(PlayerColor.Shuffle());
        
    }

    private Sprite[] RollNewTasks(int taskCount)
    {
        Sprite[] newTasks = new Sprite[4];
        for (int i =0; i < taskCount; i++)
        {
            newTasks[i] = SymbolSprites[Random.Range(0, SymbolSprites.Length)];
        }

        for (int i = taskCount; i < 4; i++)
        {
            newTasks[i] = EmptySymbol;
        }

        return newTasks;
    }

}
