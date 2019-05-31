using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timer_text;
    public float seconds = 0;
    public float minutes = 3;

    public Health player_one;
    public Health player_two;

    void Update ()
    {
        seconds -= Time.deltaTime;
        if (seconds < 0)
        {
            if (minutes > 0)
            {
                --minutes;
                seconds = 60;
            }
            else
            {
                seconds = 0;
                GameData.instance.winner = player_one.health > player_two.health ? 1 : 2;
                GameData.instance.sceneManager.LoadLevel(3);
            }
        }
        if (seconds < 10)
        {
            timer_text.text = ((int)minutes) + ":0" + ((int)seconds);
        }
        else
        {
            timer_text.text = ((int)minutes) + ":" + ((int)seconds);
        }
    }
}
