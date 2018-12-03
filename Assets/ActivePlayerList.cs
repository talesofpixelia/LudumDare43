using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayerList : MonoBehaviour {

    public List<Player> Players = new List<Player>(4);

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
