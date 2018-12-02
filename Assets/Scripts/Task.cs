using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Task {
    public Player Player;
    public string Name;
    public Sprite Icon;
    public bool done = false;

    public Task(Player player, Sprite sprite, string name)
    {
        Player = player;
        Icon = sprite;
        Name = name;
    }
}
