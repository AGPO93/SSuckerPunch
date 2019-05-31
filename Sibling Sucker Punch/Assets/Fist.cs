using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : MonoBehaviour {

    bool active = false;
    public bool second_fist = false;
    PlayerController controller;
    AudioSource audio_player;
    public AudioClip[] punch_clips;
    float punch_volume = .6f;

    Camera cam;

    // Use this for initialization
    void Start () {
        audio_player = GetComponentInParent<AudioSource>();
        controller = transform.parent.GetComponent<PlayerController>();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        if (!second_fist)
        {
            active = controller.punching;
        }
        else
        {
            active = controller.punching_behind;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != gameObject.tag && active)
        {
            //Destroy(col.gameObject);
            Vector2 dir = (col.transform.position - transform.parent.transform.position).normalized;

            if (col.gameObject.GetComponent<Health>())
            {
                col.gameObject.GetComponent<Health>().SendMessage("TakeDamageLight", dir);
                audio_player.volume = punch_volume;
                audio_player.PlayOneShot(punch_clips[Random.Range(0, 4)]);
                StartCoroutine(cam.GetComponent<CameraShake>().Shake(.05f, .05f));

                if (!second_fist)
                {
                    controller.punching = false;
                }
                else
                {
                    controller.punching_behind = false;
                }
            }
            //Debug.Log("Hit");
        }
    }

}
