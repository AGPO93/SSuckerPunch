using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZOrdering : MonoBehaviour {

    SpriteRenderer rend;
	// Use this for initialization
	void Start () {
        rend = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        int sorting_order = (int)Map(transform.position.y, -3.8f, 1.5f, 100, 0);
        rend.sortingOrder = sorting_order;
	}

    float Map(float value, float min1, float max1, float min2, float max2)
    {
        return (value - min1) / (max1 - min1) * (max2 - min2) + min2;
    }
}
