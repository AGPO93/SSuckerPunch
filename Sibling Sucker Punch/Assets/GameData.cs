using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour {


    public static GameData instance;


    public SceneManagerController sceneManager;
    public SaveLoadManager saveLoadManager;

    public int winner;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        sceneManager = GetComponentInChildren<SceneManagerController>();
        saveLoadManager = GetComponentInChildren<SaveLoadManager>();
    }



}
