using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontDIsplayIfEmptyText : MonoBehaviour {

    public Text text;
    Image thisImage;
    Color transparent, nonTransparent;

    // Use this for initialization
    void Start()
    {
        thisImage = GetComponent<Image>();
        transparent = new Color(0, 0, 0, 0);
        nonTransparent = new Color(0, 0, 0, 0.5f);
    }

        // Update is called once per frame
    void Update () {
        if (text.text == "")
            thisImage.color = transparent;
        else
            thisImage.color = nonTransparent;

    }
}
