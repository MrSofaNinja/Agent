using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string Name;
    public AudioClip Clip;

    [Range(0f,1f)]
    public float Volume = 1;

    [Range(.1f, 2f)]
    public float Pitch = 1;

    public bool Loop;
    public bool PlayOnStart;
    public bool Interruptable;
    public bool CanInturrupt;

    [HideInInspector]
    public AudioSource Source;

    // might want a 'played' bool in here too.
    [HideInInspector]
    public bool HasPlayed;

}
