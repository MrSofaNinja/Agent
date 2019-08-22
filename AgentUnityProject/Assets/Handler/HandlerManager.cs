using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
        if (SceneManager.GetActiveScene().name == "MainMenuScreen")
        {
            PlayRandomClipFromPool(MainMenuPool);
        }
        else if (SceneManager.GetActiveScene().name == "AgentCreationScreen")
        {
            PlayRandomClipFromPool(NewAgentPool);
        }
        else if (SceneManager.GetActiveScene().name == "AgentGameScreen")
        {
            PlayRandomClipFromPool(MainGameBeginsPool);
        }
        else if (SceneManager.GetActiveScene().name == "OpDebriefScreen")
        {
            PlayRandomClipFromPool(SecurityClearancePool);
        }
        else if (SceneManager.GetActiveScene().name == "SecurityClearanceReview")
        {
            PlayRandomClipFromPool(SecurityClearancePool);
        }
    }

    public void PlayFromMainMenuPool()
    {
        PlayRandomClipFromPool(MainMenuPool);
    }

    public void PlayFromNewOpPool()
    {
        PlayRandomClipFromPool(NewOpPool);
    }

    public void PlayFromAgentProfilePool()
    {
        PlayRandomClipFromPool(AgentProfilePool);
    }

    public void PlayFromNewAgentPool()
    {
        PlayRandomClipFromPool(NewAgentPool);
    }

    public void PlayFromDLCPool()
    {
        PlayRandomClipFromPool(DLCPool);
    }

    public void PlayFromSettingsPool()
    {
        PlayRandomClipFromPool(SettingsPool);
    }

    public void PlayFromMainGameBeginsPool()
    {
        PlayRandomClipFromPool(MainGameBeginsPool);
    }

    public void PlayFromMainGameMiddlePool()
    {
        PlayRandomClipFromPool(MainGameMiddlePool);
    }

    public void PlayFromMainGameEndWinPool()
    {
        PlayRandomClipFromPool(MainGameEndWinPool);
    }

    public void PlayFromMainGameEndLosePool()
    {
        PlayRandomClipFromPool(MainGameEndLosePool);
    }

    public void PlayFromMakeDropPool()
    {
        PlayRandomClipFromPool(MakeDropPool);
    }

    public void PlayFromSecurityClearancePool()
    {
        PlayRandomClipFromPool(SecurityClearancePool);
    }

    public void PlayRandomClipFromPool(Sound[] ClipPool)
    {
        //int RandomElement = Mathf.RoundToInt(UnityEngine.Random.Range(0, ClipPool.Length));
        //Play(ClipPool[RandomElement]);  

        //create a new list
        List<Sound> TempSoundlist = new List<Sound>();

        // go through the array and copy anything that has not played into the list
        foreach(Sound sound in ClipPool)
        {
            if(!sound.HasPlayed)
            {
                TempSoundlist.Add(sound);
            }
        }
        
        // test the list is more than 0
        if(TempSoundlist.Count > 0)
        {
            // if so, pick from 0 to 'count' and play clip
            int RandomElement = Mathf.RoundToInt(UnityEngine.Random.Range(0, TempSoundlist.Count));
            Play(TempSoundlist[RandomElement]); 

        }
        else
        {
            // if not, loop through the array switching them all back to false
            foreach (Sound sound in ClipPool)
            {
                sound.HasPlayed = false;
            }

            // Pick one from the list at random and play
            int RandomElement = Mathf.RoundToInt(UnityEngine.Random.Range(0, ClipPool.Length));
            Play(ClipPool[RandomElement]);
        }
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
