using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class HandlerManager : MonoBehaviour
{
    public AudioManagerScript AudioManager;

    [HideInInspector]
    public List<Sound> SoundList;

    public Sound[] MainMenuPool;
    public Sound[] NewOpPool;
    public Sound[] AgentProfilePool;
    public Sound[] NewAgentPool;
    public Sound[] DLCPool;
    public Sound[] SettingsPool;
    public Sound[] MainGameBeginsPool;
    public Sound[] MainGameMiddlePool;
    public Sound[] MainGameEndWinPool;
    public Sound[] MainGameEndLosePool;
    public Sound[] MakeDropPool;
    public Sound[] SecurityClearancePool;

    private void Awake()
    {
        AddArrayToList(MainMenuPool); 
        AddArrayToList(NewOpPool); 
        AddArrayToList(AgentProfilePool);
        AddArrayToList(NewAgentPool);
        AddArrayToList(DLCPool);
        AddArrayToList(SettingsPool);
        AddArrayToList(MainGameBeginsPool);
        AddArrayToList(MainGameMiddlePool);
        AddArrayToList(MainGameEndWinPool);
        AddArrayToList(MainGameEndLosePool);
        AddArrayToList(MakeDropPool);
        AddArrayToList(SecurityClearancePool);

        PlayFromStart(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayRandomClipFromPool(MainMenuPool);
    }

    public void PlayRandomClipFromPool(Sound[] ClipPool)
    {
        int RandomElement = Mathf.RoundToInt(UnityEngine.Random.Range(0, ClipPool.Length));
        Play(ClipPool[RandomElement]);  
    }

    public void PlayFromStart()
    {
        foreach(Sound sound in SoundList)
        {
            if(sound.PlayOnStart)
            {
                Play(sound);
            }
        }
    }

    public void Play(Sound SoundToPlay)
    {
        AudioManager.Play(SoundToPlay);
    }

    public void HandlerSay(string ClipName)
    {
        AudioManager.Play(ClipName);
    }

    public void AddArrayToList(Sound[] SoundArray)
    {
        foreach(Sound sound in SoundArray)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.loop = sound.Loop;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;

            if (sound.PlayOnStart)
            {
                // you need to sort out a play method
            }

            SoundList.Add(sound);
        }
    }
}
