using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Song
{
    Africa,
    Russia,
    Ireland,
    Greenland,
    America
};

public class MusicPlayer : MonoBehaviour {

    public Song song;
    private string[] song_names = { "Africa", "Russia", "Ireland", "Greenland", "America" };
	// Use this for initialization
	void Start ()
    {
        AudioManager.instance.Play(song_names[(int)song], true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
