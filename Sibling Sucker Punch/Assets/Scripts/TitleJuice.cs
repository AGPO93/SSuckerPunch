using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleJuice : MonoBehaviour {

    public Text title;
    public Text start_text;
    public Text text;

    public GameObject big_brother;
    public GameObject little_brother;

    public Vector3 target_big;
    public Vector3 target_little;

    private Vector3 target = new Vector3(1,1,1);
    private float speed = 1.5f;
    private bool walk_in = false;

    private bool increase_once = false;

    // Use this for initialization
    void Start () {
        title.transform.localScale = new Vector3(0, 0, 0);
        start_text.transform.localScale = new Vector3(0,0,0);
        text.transform.localScale = new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        title.transform.localScale = Vector3.MoveTowards(title.transform.localScale, new Vector3(1,1,1), speed * Time.deltaTime);
        start_text.transform.localScale = Vector3.MoveTowards(start_text.transform.localScale, target, speed * Time.deltaTime);
        if(text)
            text.transform.localScale = Vector3.MoveTowards(text.transform.localScale, target, speed * Time.deltaTime);

        if (start_text.transform.localScale.x > 1.05f)
        {
            target = new Vector3(0.8f, 0.8f, 0.8f);
            speed = 0.3f;
            walk_in = true;
        }
        else if (start_text.transform.localScale.x < 0.95f)
        {
            target = new Vector3(1.2f, 1.2f, 1.2f);
        }

        if(walk_in)
        {
            if(big_brother)
                big_brother.transform.position = Vector3.Lerp(big_brother.transform.position, target_big, 2 * Time.deltaTime);
            if(little_brother)
                little_brother.transform.position = Vector3.Lerp(little_brother.transform.position, target_little, 2 * Time.deltaTime);
        }
    }
}
