using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneManagerController : MonoBehaviour
{
    //Required to not be null for it to work.
    public CanvasGroup loadingScreenCanvasGroup;
    public Canvas loadingScreenCanvas;
    public Slider loadingBarSlider;


    //Declare actions for use in unity event system. You can subscribe to any of these actions.
    //They are completed on sceneLoad.
    public Action myAction;
    public Action<string> stringAction;
    public Action<bool> boolAction;
    public Action<float> floatAction;
    public Action<int> intAction;

    //test for callback function on load complete.
    private bool loadComplete = false;


    void Start()
    {
        //subscribe callbacks to actions
        myAction += Callback;
        stringAction += CallbackString;
        boolAction += CallbackBool;

        //subscribe to the Scenemanager's built in delegate
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //The below is an example function.
        NewSceneLoaded();
    }

    public void NewSceneLoaded()
    {
        //Do stuff that needs to be done ONLY when the scene has been loaded.
        //Like initialise certain values n stuff?
        Debug.Log("Scene Loaded!");
    }


    //TODO - remove, testing only.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadLevel(1);
        }
    }

    #region Action Callbacks
    void Callback()
    {

    }

    void CallbackString(string aStringParam)
    {

    }
    
    void CallbackBool(bool _val)
    {
        loadComplete = _val;
        //do anything that needs to be done on scene load completion.
    }
    #endregion




    #region new game

    public void NewGame(string newGameFirstSceneName)
    {
        //TODO opening cinematic, Load start scene then begin play.
        
        LoadLevel("scene2");
    }
    public void NewGame(int newGameFirstSceneIndex)
    {
        //TODO opening cinematic, Load start scene then begin play.

        LoadLevel(newGameFirstSceneIndex);
    }

    #endregion

    #region LoadLevel

    public void LoadLevel(int sceneIndex)
    {
        //loadingScreenCanvasGroup.alpha = 1.0f;
        StartCoroutine(LoadLevelBySceneIndexAsync(sceneIndex));
    }

    public void LoadLevel(string sceneName)
    {
        //loadingScreenCanvasGroup.alpha = 1.0f;
        StartCoroutine(LoadLevelBySceneNameAsync(sceneName));
    }


    IEnumerator LoadLevelBySceneIndexAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log("Progress: " + progress);
            //loadingBarSlider.value = progress;
            yield return null;
        }
        if (loadingScreenCanvasGroup)
            loadingScreenCanvasGroup.alpha = 0.0f;
    }

    IEnumerator LoadLevelBySceneNameAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarSlider.value = progress;
            yield return null;
        }
        if(loadingScreenCanvasGroup)
        loadingScreenCanvasGroup.alpha = 0.0f;
    }

    #endregion


}
