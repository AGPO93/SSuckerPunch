using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthToUI : MonoBehaviour {

    public Slider health;
    public Health hp;
	// Use this for initialization
	void Start ()
    {
        health = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        health.value = Mathf.MoveTowards(health.value, hp.health / hp.maxHealth, 0.3f * Time.deltaTime);
    }
}
