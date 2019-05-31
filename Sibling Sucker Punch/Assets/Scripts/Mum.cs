using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mum : MonoBehaviour
{
    public Health player_one;
    public Health player_two;

    // timer
    public Timer timer;
    private float timerMinutes;
    private float timerSeconds;

    //dialogue
    public Text dialogueText;
    public Image bubble;
    private bool dialogueEnabled = false;

    // room dmg
    private float destroyedCount;
    private bool doneFirst = false;
    private bool doneSecond = false;
    private bool doneThird = false;
    public Animator doorOpen;

    void Start ()
    {
        bubble.gameObject.SetActive(false);
    }
	
	void Update ()
    {
		timerMinutes = timer.GetComponent<Timer>().minutes;
        timerSeconds = Mathf.RoundToInt(timer.GetComponent<Timer>().seconds);
        destroyedCount = this.GetComponent<ObjectDestroyedCount>().object_destroyed;
        TimeEvents();
        RoomDmgEvents();
    }

    private void TimeEvents()
    {
        if (!dialogueEnabled)
        {
            if (timerMinutes == 2 && timerSeconds == 10)
            {
                MumSay("Dinner will be served soon!");
                Debug.Log("dINNER BITCHES");
                dialogueEnabled = true;
            }
            else if (timerMinutes == 1 && timerSeconds == 20)
            {
                MumSay("Dinner's ready!!");
                dialogueEnabled = true;
            }
            else if (timerMinutes == 0 && timerSeconds == 40)
            {
                MumSay("Dinner's getting cold!!!");
                dialogueEnabled = true;
            }
        }
    }

    private void RoomDmgEvents()
    {
        if (destroyedCount == 23)
        {
            doorOpen.Play("Open");
            Invoke("WinState", 3);
        }

        if (!dialogueEnabled)
        {
            if(destroyedCount == 8 && !doneFirst)
            {
                MumSay("What was that?");
                dialogueEnabled = true;
                doneFirst = true;
            }
            else if(destroyedCount == 16 && !doneSecond)
            {
                MumSay("i'll count to three!!");
                dialogueEnabled = true;
                doneSecond = true;
            }
            else if(destroyedCount == 21 && !doneThird)
            {
                MumSay("i'm coming up there!!!");
                dialogueEnabled = true;
                doneThird = true;
            }
        }
    }

    private void MumSay(string dialogue)
    {
        dialogueText.gameObject.SetActive(true);
        bubble.gameObject.SetActive(true);
        dialogueText.text = dialogue;
        Invoke("ClearText", 3);
    }

    private void ClearText()
    {
        dialogueText.gameObject.SetActive(false);
        bubble.gameObject.SetActive(false);
        dialogueEnabled = false;
        CancelInvoke("ClearText");
    }

    private void WinState()
    {
        CancelInvoke("WinState");
        GameData.instance.winner = player_one.health < player_two.health ? 1 : 2;
        GameData.instance.sceneManager.LoadLevel(3);
    }
}