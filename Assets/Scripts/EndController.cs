using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour {

    public float Duration = 5;

    public GameObject[] particles;

    public GameObject congratz;
    public GameObject sacrifice;

    // Use this for initialization
    void Start () {

        congratz.SetActive(true);
        sacrifice.SetActive(false);
    }
    void Awake()
    {

        congratz.SetActive(true);
        sacrifice.SetActive(false);
    }


    // Update is called once per frame
    void Update () {

        StartCoroutine(Sleep());
	}

    public IEnumerator Sleep()
    {
        for (int i = 0; i < Duration; i++)
        {
            Debug.Log(i);

            if (i == 1 || i == 4)
            {
                foreach (var particle in particles)
                {
                    particle.SetActive(true);
                }
            }

            if (i == 4)
            {
                congratz.SetActive(false);
                sacrifice.SetActive(true);
            }
            yield return new WaitForSeconds(1);
        }

        SceneManager.LoadScene(0);
    }
}
