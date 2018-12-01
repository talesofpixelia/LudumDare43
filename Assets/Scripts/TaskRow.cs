using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRow : MonoBehaviour {

    public SpriteRenderer[] Sprites = new SpriteRenderer[4];


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSymbols(Sprite[] newSprites)
    {
        for(int i = 0; i < newSprites.Length; i++)
        {
            Sprites[i].sprite = newSprites[i];
        }
    }

    internal void SetColors(IList<Color> colors)
    {
        for(int i = 0; i < colors.Count; i++)
        {
            Sprites[i].color = colors[i];
        }
    }
}
