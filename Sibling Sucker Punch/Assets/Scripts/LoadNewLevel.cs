using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewLevel : MonoBehaviour {

    public int level = 0;

    public AudioClip clip;

    public void StartGameNow()
    {
        GetComponentInParent<AudioSource>().PlayOneShot(clip);
        GameData.instance.sceneManager.LoadLevel(level);
    }
}
