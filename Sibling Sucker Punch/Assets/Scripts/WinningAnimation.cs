using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningAnimation : MonoBehaviour {

    public int id;
    public Text text;
    public string name;

	// Use this for initialization
	void Start ()
    {
        if (GameData.instance.winner == id)
        {
            GetComponent<Animator>().Play("Victory");
            text.text = name + " wins!";
        }
        else
        {
            GetComponent<Animator>().Play("Defeat");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    	
	}
}
