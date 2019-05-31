using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBot : MonoBehaviour {

    public List<AudioClip> music;
    AudioSource sounds;

	// Use this for initialization
	void Start () {
        sounds = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!sounds.isPlaying)
        {
            sounds.PlayOneShot(music[Random.Range(0,3)]);
        }
		
	}
}
