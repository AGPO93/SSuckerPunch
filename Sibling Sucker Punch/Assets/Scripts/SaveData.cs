using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    //TODO - fill this out with gameSave relevant data!
    public int currentSceneID = 0;

    
    public int GetCurrentSceneID()
    {
        return currentSceneID;
    }

    public void ResetData()
    {
        currentSceneID = 0;
    }

    public void SetCurrentSceneID(int _sceneID)
    {
        currentSceneID = _sceneID;
    }

}
