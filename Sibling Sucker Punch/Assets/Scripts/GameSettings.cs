using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettings
{
    //TODO - fill this out with settings relevant information!
    public int resolutionWidth = 0;
    public int resolutionHeight = 0;

    public Difficulty difficulty = Difficulty.Medium;


    public enum Difficulty
    {
        Easy = 0,
        Medium,
        Hard,
    };

    //Use this for initialization
   void Awake()
    {

       Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height);
        }
        Screen.SetResolution(resolutions[0].width, resolutions[0].height, true);
    }

}