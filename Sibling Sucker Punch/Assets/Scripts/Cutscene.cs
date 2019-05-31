using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Image bigBubble, lilBubble;
    public Text bigText, lilText;
    public AudioSource dialogueAudio;
    public List<AudioClip> clips;

    public float timer;
    private bool firstDialogue = false, secondDialogue = false, thirdDialogue = false;
    bool doOnce = false;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > .5f && !firstDialogue)
        {
            LilSay("Get off, it's my turn!");
            dialogueAudio.PlayOneShot(clips[0]);
            firstDialogue = true;
        }
        else if (timer > 1.5f && !secondDialogue)
        { 
            BigSay("Get lost loser!");
            dialogueAudio.PlayOneShot(clips[1]);
            secondDialogue = true;
        }
        else if (timer > 3.5f && !thirdDialogue)
        {
            BigSay("Stop biting me!!");
            dialogueAudio.PlayOneShot(clips[2]);
            thirdDialogue = true;
        }
        else if (timer > 7.0f)
        {
            if (!doOnce)
            {
                doOnce = true;
                GameData.instance.sceneManager.LoadLevel(2);
            }
        }
    }

    private void BigSay(string dialogue)
    {
        bigText.gameObject.SetActive(true);
        bigBubble.gameObject.SetActive(true);
        bigText.text = dialogue;
        // play sound
        Invoke("ClearTextBig", .9f);
    }

    private void LilSay(string dialogue)
    {
        lilText.gameObject.SetActive(true);
        lilBubble.gameObject.SetActive(true);
        lilText.text = dialogue;
        // play sound
        Invoke("ClearTextLil", .9f);
    }

    private void ClearTextBig()
    {
        bigText.gameObject.SetActive(false);
        bigBubble.gameObject.SetActive(false);
        CancelInvoke("ClearTextBig");
    }

    private void ClearTextLil()
    {
        lilText.gameObject.SetActive(false);
        lilBubble.gameObject.SetActive(false);
        CancelInvoke("ClearTextLil");
    }

}
