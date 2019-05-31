using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyedCount : MonoBehaviour {

    public int object_destroyed = 0;

    public Health player_one;
    public Health player_two;

    private void FixedUpdate()
    {
        if(player_one.health <= 0)
        {
            GameData.instance.winner = 2;
            GameData.instance.sceneManager.LoadLevel(3);
        }
        if (player_two.health <= 0)
        {
            GameData.instance.winner = 1;
            GameData.instance.sceneManager.LoadLevel(3);
        }
    }
}
