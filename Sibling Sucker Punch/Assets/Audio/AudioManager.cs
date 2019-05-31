using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.soundClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void Play(string audioName)
    {
        foreach(Sound s in sounds)
        {
            if (s.name == audioName)
            {
                s.source.Play();
                return;
            }
        }
        Debug.Log("Sound not found, check sound name matches function argument");
    }

    public void Play(string audioName, bool _loop)
    {
        foreach (Sound s in sounds)
        {
            if (s.name == audioName)
            {
                s.source.Play();
                s.source.loop = _loop;
                return;
            }
        }
        Debug.Log("Sound not found, check sound name matches function argument");
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

}
