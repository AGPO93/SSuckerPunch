using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip soundClip;
    [Range(0.0f, 1.0f)]
    public float volume;
    [Range(0.1f, 2.0f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
