using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRow : MonoBehaviour {

    public SpriteRenderer[] Sprites = new SpriteRenderer[4];
    public Task[] Tasks = new Task[4];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Task[] GetTasks()
    {
        return Tasks;
    }

    public void SetTasks(Task[] newTasks)
    {
        Tasks = newTasks;
        for (int i = 0; i < newTasks.Length; i++)
        {
            Sprites[i].sprite = newTasks[i].Icon;
            Sprites[i].color = newTasks[i].Player.PlayerColor;
        }
    }
}
