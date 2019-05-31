using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField]
    private List<SaveData> saveFiles = new List<SaveData>();
    [SerializeField]
    private SaveData currentSaveData = null;   //When loading a game, this keeps track of the currentSaveData by ref. as things change, when saved it outputs this data.
    [SerializeField]
    private GameSettings currentSettings = null;   //Loaded in from the debug mode premade file on awake. settings file overrides. A "set to default()" for settings may call the debug function at the bottom to default it, then LoadSettings() again.


    public bool isDebug = false;

    #region Awake

    // Use this for initialization
    void Awake()
    {
        PopulateSavesList();
        LoadSettings();
    }

    #endregion

    public SaveData GetCurrentSaveData()
    {
        return currentSaveData;
    }

    public GameSettings GetCurrentSettings()
    {
        return currentSettings;
    }

    public void SaveGame(int _saveSlot)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Saves/saveData_" + _saveSlot.ToString() + ".dat");

        SaveData saveData = currentSaveData;

        binaryFormatter.Serialize(file, saveData);
        file.Close();

        Debug.Log("Save complete");
    }

    public void LoadGame(int _saveSlot)
    {
        if (File.Exists(Application.dataPath + "/Saves/saveData_" + _saveSlot.ToString() + ".dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Saves/saveData_" + _saveSlot.ToString() + ".dat", FileMode.Open);
            currentSaveData = (SaveData)binaryFormatter.Deserialize(file);

            file.Close();
            Debug.Log("Load game successful");

            //Remember, don't open the scene here. Get this script's "currentSaveData.getcurrentSceneID()" and open the scene. Minimise coupling. 

            return;
        }
        Debug.Log("Error: Load game failed");
    }

    public void NewGame()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.dataPath + "/Saves/saveData_NewGame.dat", FileMode.Open);

        currentSaveData = (SaveData)binaryFormatter.Deserialize(file);

        file.Close();
        Debug.Log("New Game Started");

        //Remember, don't open the scene here. Get this script's "currentSaveData.getcurrentSceneID()" and open the scene. Minimise coupling. 

        return;
    }

    private void SaveSettings()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Saves/settingsData.dat");

        binaryFormatter.Serialize(file, currentSettings);
        file.Close();

        Debug.Log("Save complete");
    }

    private void LoadSettings()
    {
        if (File.Exists(Application.dataPath + "/Saves/settingsData.dat"))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/Saves/settingsData.dat", FileMode.Open);
            currentSettings = (GameSettings)binaryFormatter.Deserialize(file);

            file.Close();
            Debug.Log("Load settings successful");
            return;
        }
        Debug.Log("Failed to load settings file");
    }

    public void UpdateSettings()
    {
        //TODO make settings take effect
        SaveSettings();
    }

    public List<SaveData> GetSavesList()
    {
        return saveFiles;
    }

    //Attempt to find all previous save files and make a list of them
    private void PopulateSavesList()
    {
        for (int index = 0; index < 10; index++)
        {
            if (File.Exists(Application.dataPath + "/Saves/saveData_" + index.ToString() + ".dat"))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream file = File.Open(Application.dataPath + "/Saves/saveData_" + index.ToString() + ".dat", FileMode.Open);

                SaveData saveData = (SaveData)binaryFormatter.Deserialize(file);

                saveFiles.Add(saveData);

                file.Close();
                Debug.Log("Populated list with save file saveData_" + index.ToString() + ".dat");
            }
            else
            {
                Debug.Log("Failed to find save file 'saveData_" + index.ToString() + ".dat'");
            }
        }
    }

    //In these functions, we can make new saves for settings and new game, useful for a game that may be dependant on "new game" being a load of a binary file rather than a hard coded thing in all the code elsewhere.
    //Could also be duplicated and modified for making a "new game +" feature.
    public void DebugOnly_MakeNewSettingsFile()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Saves/settingsData.dat");

        //TODO - Initialise desired default settings
        GameSettings set = new GameSettings
        {
            difficulty = GameSettings.Difficulty.Medium,
            resolutionHeight = 1080,
            resolutionWidth = 1920
        };

        binaryFormatter.Serialize(file, set);
        file.Close();
        Debug.Log("DEBUG: Generated new settings file");
        return;
    }

    public void DebugOnly_MakeNewGameFile()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/Saves/saveData_NewGame.dat");
        SaveData newData = new SaveData();

        //TODO - Once starting location, position and playerstate etc are confirmed. Put them here.
        newData.SetCurrentSceneID(1);

        binaryFormatter.Serialize(file, newData);

        file.Close();
        Debug.Log("DEBUG: Generated new start file");
        return;
    }

}
