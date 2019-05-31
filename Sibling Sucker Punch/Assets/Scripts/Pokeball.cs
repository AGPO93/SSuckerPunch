using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour {

    float drop_range = .5f;

    float min_y;
    float start_y;
    float bounce_amount = 4f;
    bool active = true;

    float lifetime = 3;

    AudioSource audio_player;
    public AudioClip throw_sound;
    public AudioClip[] bounce_sounds;
    public AudioClip hit_sound;
    float bounce_volume = .5f;
    float throw_volume = .4f;
    float hit_volume = .6f;

    Camera cam;

    bool play_once = true;

    // Use this for initialization
    void Start () {
        audio_player = GetComponent<AudioSource>();
        cam = Camera.main;
        min_y = -9999;
        start_y = transform.position.y;
        audio_player.volume = throw_volume;
        audio_player.PlayOneShot(throw_sound);
    }
	
	// Update is called once per frame
	void Update () {
        Bounce();
        //lifetime -= Time.deltaTime;
        //if (lifetime <= 0)
        //{
        //    Destroy(gameObject);
        //}
	}

    //void LateUpate()
    //{
    //    Bounce();
    //}

    void Bounce()
    {
        if (bounce_amount <= .3f)
        {
            float clamped_y = Mathf.Clamp(transform.position.y, min_y, 9999);
            transform.position = new Vector3(transform.position.x, clamped_y, transform.position.z);
            if (play_once)
            {
                audio_player.volume = bounce_volume;
                audio_player.PlayOneShot(bounce_sounds[3]);
                play_once = false;
            }
        }
        else if (transform.position.y <= min_y && bounce_amount > .3f)
        {

            audio_player.volume = bounce_volume;
            audio_player.PlayOneShot(bounce_sounds[Random.Range(0, 3)]);

            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, bounce_amount), ForceMode2D.Impulse);
            bounce_amount *= .7f;
            active = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.tag == col.gameObject.tag && !active)
        {
            col.GetComponent<PlayerController>().has_pokeball = true;
            Destroy(gameObject);
        }

        if (col.gameObject.tag != gameObject.tag && active)
        {
            //Destroy(col.gameObject);
            Vector2 dir = (col.transform.position - transform.position).normalized;

            if (col.gameObject.GetComponent<Health>())
            {
                col.gameObject.GetComponent<Health>().SendMessage("TakeDamageHeavy", dir);
                StartCoroutine(cam.GetComponent<CameraShake>().Shake(.1f, .2f));

                active = false;
            }
            //Debug.Log("Hit");
            min_y = transform.position.y - drop_range;

            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            audio_player.volume = hit_volume;
            audio_player.PlayOneShot(hit_sound);
        }
        else if (col.gameObject.tag != gameObject.tag && active)
        {
            min_y = transform.position.y - drop_range;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            audio_player.volume = hit_volume;
            audio_player.PlayOneShot(hit_sound);
        }
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
