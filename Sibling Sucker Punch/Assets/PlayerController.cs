using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour {

    Rigidbody2D rb;
    Sprite sprite;
    Animator anim;
    SpriteRenderer sprite_renderer;
    SpriteRenderer[] sprite_renderer_fists;
    Collider2D[] colliders;
    public Text paused_text;
    
    public AudioSource audio_player;
    bool paused = false;
    //audio 
    float punch_miss_volume = .4f;
    public AudioClip[] punch_miss_clips;

    public GameObject pokeball;
    public GameObject chain;
    GameObject chain_obj;
    float max_y = 4.5f;
    float min_y = -4.5f;
    float sprite_max = 1.1f;
    float sprite_min = 0.6f;

    //combat
    float punch_speed = 10;
    float punch_range_y = .6f;
    float punch_range_x = 1f;
    float punch_idle_range = .5f;
    float heavy_cooldown = 0;
    float cooldown_poke = 5;
    float cooldown_chain = 3;

    public bool has_pokeball = true;

    public bool punching = false;
    public bool punching_behind = false;

    public bool heavy_hitting = false;

    public GameObject fist;
    public GameObject fist_behind;

    Vector3 punch_target;

    Vector3 punch_idle;
    Vector3 punch_idle_behind;

    public bool knocked = false;
    public Transform nearest_target;

    public float fist_pos = 0;
    public float fist_pos_behind = 0;

    public int current_fist = 0;




    //Movement
    float vertical_speed = 1;
    float horizontal_speed = 2;
    float speed = 5;

    //inputs
    float input_vertical = 0;
    float input_horizontal = 0;
    bool light_hit;
    bool heavy_hit;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>().sprite;
        sprite_renderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        sprite_renderer_fists = GetComponentsInChildren<SpriteRenderer>();
        colliders = GetComponents<Collider2D>();
        fist_behind.GetComponent<Fist>().second_fist = true;
        audio_player = GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update () {
        if (GetComponent<Health>().health == 0)
        {
            rb.velocity = Vector3.zero;
            foreach(Collider2D col in colliders)
            {
                col.enabled = false;
            }
            anim.Play("Defeat");
            return;
        }
        GetInputs();
        Move();
        SpriteScaling();
        Punch();
        Targeting();
        Cooldown();
    }

    void Move()
    {
        if (knocked)
        {
            anim.Play("Hit");
            return;
        }
        anim.SetBool("Running", (input_horizontal != 0 || input_vertical != 0));
        Vector2 velocity = new Vector2(input_horizontal * horizontal_speed, input_vertical * vertical_speed) * Time.deltaTime;
        rb.velocity = velocity.normalized * speed;

        if (chain_obj)
        {
            chain_obj.transform.position = fist_behind.transform.position;
        }
    }

    void Cooldown()
    {
        heavy_cooldown -= Time.deltaTime;
        heavy_cooldown = Mathf.Clamp(heavy_cooldown, 0, 5);
    }

    void Punch()
    {
        if (!punching)
        {
            fist_pos -= Time.deltaTime * punch_speed;
        }
        else
        {
            anim.Play("Attack");
            fist_pos += Time.deltaTime * punch_speed;
            if (fist_pos >= 1)
            {
                audio_player.volume = punch_miss_volume;
                audio_player.PlayOneShot(punch_miss_clips[Random.Range(0, 3)]);
                punching = false;
            }
        }
        fist_pos = Mathf.Clamp(fist_pos, 0, 1);
        fist.transform.position = Vector2.Lerp(punch_idle, punch_target, fist_pos);

        if (!punching_behind)
        {
            fist_pos_behind -= Time.deltaTime * punch_speed;
        }
        else
        {
            anim.Play("Attack");
            fist_pos_behind += Time.deltaTime * punch_speed;
            if (fist_pos_behind >= 1)
            {
                audio_player.volume = punch_miss_volume;
                audio_player.PlayOneShot(punch_miss_clips[Random.Range(0, 3)]);
                punching_behind = false;
            }
        }
        fist_pos_behind = Mathf.Clamp(fist_pos_behind, 0, 1);
        fist_behind.transform.position = Vector2.Lerp(punch_idle_behind, punch_target, fist_pos_behind);
    }

    void HeavyHit()
    {
        if (heavy_cooldown > 0) return;
        switch (gameObject.tag)
        {
            case "PlayerOne":
                ChainWhip();
                break;
            case "PlayerTwo":
                ThrowPokeball();
                break;
        }
        
    }

    void ChainWhip()
    {
        heavy_cooldown += cooldown_chain;

        Vector3 diff = nearest_target.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.Euler(0f, 0f, rot_z - 90);

        //GameObject whip = Instantiate(chain, fist_behind.transform.position, rot, transform);
        chain_obj = Instantiate(chain, fist_behind.transform.position, rot);
        anim.Play("Special");

    }

    void ThrowPokeball()
    {
        if (!has_pokeball) return;
        heavy_cooldown += cooldown_poke;
        GameObject poke = Instantiate(pokeball, transform.position, Quaternion.identity);
        Vector2 dir = (nearest_target.position - transform.position).normalized;
        poke.GetComponent<Rigidbody2D>().AddForce(dir * 20, ForceMode2D.Impulse);
        has_pokeball = false;
        anim.Play("Special");
    }

    void Targeting()
    {
        punch_target = (nearest_target.transform.position - transform.position).normalized;
        punch_idle = transform.position + (punch_target * punch_idle_range);
        punch_target.y *= punch_range_y;
        punch_target.x *= punch_range_x;
        punch_target = punch_idle + punch_target;

        Vector3 direction = (punch_target - punch_idle).normalized;
        punch_idle_behind = Vector3.Cross(direction, -Vector3.forward) * .3f + punch_idle;
    }

    void SpriteScaling()
    {
        float scale = Map(transform.position.y, -4f, 4f, sprite_max, sprite_min);
        scale = Mathf.Clamp(scale, sprite_min, sprite_max);
        transform.localScale = new Vector3(scale, scale, scale);

        sprite_renderer.flipX = (nearest_target.transform.position.x < transform.position.x);
        foreach (SpriteRenderer rend in sprite_renderer_fists)
        {
            rend.flipX = (nearest_target.transform.position.x < transform.position.x);
        }
    }

    void GetInputs()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }
        paused_text.enabled = paused;
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (paused) return;

        input_horizontal = Input.GetAxisRaw(gameObject.tag + "Horizontal");
        input_vertical = Input.GetAxisRaw(gameObject.tag + "Vertical");
        light_hit = Input.GetButtonDown(gameObject.tag + "Light");
        heavy_hit = Input.GetButtonDown(gameObject.tag + "Heavy");
        

        if (light_hit == true && !heavy_hit)
        {
            if (current_fist == 0)
            {
                current_fist = 1;
                punching = true;

            }
            else
            {
                current_fist = 0;
                punching_behind = true;

            }

        }
        if (heavy_hit == true && !light_hit)
        {
            HeavyHit();
        } 
    }

    float Map(float value, float min1, float max1, float min2, float max2)
    {
        return (value - min1) / (max1 - min1) * (max2 - min2) + min2;
    }
}
