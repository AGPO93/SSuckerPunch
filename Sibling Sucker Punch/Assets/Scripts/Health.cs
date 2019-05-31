using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHealth = 1.0f;
    public float health = 0.0f;

    private bool do_once = true;

    public ParticleSystem deathParticle;
    public ParticleSystem hitParticle;
    public AudioClip playerDamageSound;
    Rigidbody2D rb;
    Vector2 knock_dir;

    AudioSource audio_player;
    public AudioClip[] injury_sounds;
    float injury_volume = .8f;

    public bool TakeDamage()
    {
        health -= 1;
        return health <= 0;
    }

    public bool TakeDamageLight(Vector2 hit_dir)
    {
        health -= 1;
        StartCoroutine(Knockback(hit_dir, 1));
        hitParticle.Play();

        audio_player.volume = injury_volume;
        audio_player.PlayOneShot(injury_sounds[Random.Range(0, 5)]);
        return health <= 0; 
    }

    public bool TakeDamageHeavy(Vector2 hit_dir)
    {
        health -= 5;
        StartCoroutine(Knockback(hit_dir, 5));

        audio_player.volume = injury_volume;
        audio_player.PlayOneShot(injury_sounds[Random.Range(0, 5)]);
        hitParticle.Play();
        return health <= 0;
    }

    //void Knockback(Vector2 hit_dir, float magnitude)
    //{
    //    GetComponent<PlayerController>().knocked = true;
    //    hit_dir *= 50;
    //    rb.velocity += hit_dir;
    //    rb.AddForce(knock_dir, ForceMode2D.Force);
    //    GetComponent<PlayerController>().knocked = false;

    //}

    IEnumerator Knockback(Vector2 hit_dir, float magnitude)
    {
        GetComponent<PlayerController>().knocked = true;
        float mag = 5*(1.0f - 0.5f * Mathf.Pow(1.03f, health - 100));
        hit_dir *= (magnitude * mag);
        rb.velocity += hit_dir;
        rb.AddForce(knock_dir, ForceMode2D.Force);
        yield return new WaitForSeconds(.1f);
        GetComponent<PlayerController>().knocked = false;
    }

    float Map(float value, float min1, float max1, float min2, float max2)
    {
        return (value - min1) / (max1 - min1) * (max2 - min2) + min2;
    }

    public bool IsDead()
    {
        if (health <= 0 && deathParticle && do_once)
        {
            do_once = !do_once;
            Instantiate(deathParticle, transform.position, Quaternion.identity);
        }
        return health <= 0;
    }

    // Use this for initialization
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        audio_player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        
    }
}