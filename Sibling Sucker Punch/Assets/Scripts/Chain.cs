using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour {

    float hit_volume = .6f;
    public AudioClip hit_sound;
    float whip_volume = .4f;
    public AudioClip whip_sound;
    AudioSource audio_player;
    float lifetime = .2f;
    bool active = true;
    Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
        audio_player = GetComponent<AudioSource>();
        audio_player.volume = whip_volume;
        audio_player.PlayOneShot(whip_sound);
	}
	
	// Update is called once per frame
	void Update () {

        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "PlayerTwo" && active)
        {
            //Debug.Log(col.gameObject);
            //Destroy(col.gameObject);
            Vector2 dir = (col.transform.position - transform.position).normalized;
            col.gameObject.GetComponent<Health>().SendMessage("TakeDamageHeavy", dir);
            StartCoroutine(cam.GetComponent<CameraShake>().Shake(.1f, .2f));
            audio_player.volume = hit_volume;
            audio_player.PlayOneShot(hit_sound);
            active = false;
        }
    }


}
