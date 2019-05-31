using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroy : MonoBehaviour {

    Health hp;
    AudioSource sound;
    public ObjectDestroyedCount count;
    public CameraShake camShake;
    public ParticleSystem particles;

    public bool slideable = false; 
    bool destroyed = false;

	// Use this for initialization
	void Start () {
        hp = GetComponent<Health>();
        sound = GetComponent<AudioSource>();
	}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "PlayerOne" || collision.gameObject.tag == "PlayerTwo") && destroyed == false)
        {
            if(collision.gameObject.GetComponent<PlayerController>().knocked)
            {
                collision.gameObject.GetComponent<PlayerController>().knocked = false;
                if (hp.TakeDamage())
                {
                    GetComponent<Animator>().Play("Dead");
                    destroyed = true;
                    if(slideable)
                    {

                    }
                }
                if(!sound.isPlaying)
                {
                    sound.Play();
                }
                count.object_destroyed++;
                if(particles)
                    particles.Play();
                StartCoroutine(camShake.Shake(.1f, .2f));
            }
        }
    }
}
