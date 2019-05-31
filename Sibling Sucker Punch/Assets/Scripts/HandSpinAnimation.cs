using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSpinAnimation : MonoBehaviour {

    public GameObject left_hand;
    public GameObject right_hand;

    public Vector3 centre_left;
    public Vector3 centre_right;

    public float radius;
    public float speed;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        left_hand.transform.localPosition = new Vector3(radius * 0.5f * Mathf.Sin(Time.time* speed), radius * Mathf.Cos(Time.time* speed), 0) + centre_left;
        right_hand.transform.localPosition = new Vector3(radius * 0.5f * Mathf.Sin(Time.time* speed + Mathf.PI), radius * Mathf.Cos(Time.time* speed + Mathf.PI), 0) + centre_right;
    }
}
