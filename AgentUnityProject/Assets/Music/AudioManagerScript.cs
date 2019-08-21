using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    // As a list cannot be used in the Inspector easily, this array catches
    // the pool of sound clips we need
    public Sound[] Sounds;

    [HideInInspector]
    public Sound CurrentlyPlayingSound;

    // This is the queue that is updated at runtime so that we can 
    // queue up the voice over clips should one already be active
    // when another attempts to play.
    [HideInInspector]
    public List<Sound> SoundList;

    [HideInInspector]
    public bool ListIsPlaying = false;

    // Create Audio Sources for each of the clips defined tine the 
    // array and play anything that is defined as 'play on start'
    private void Awake()
    {
        foreach(Sound s in Sounds)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;

            if(s.PlayOnStart)
            { 
                Play(s.Name);
            }
        }
    }

    public void Update()
    {
        // Check that its time for the next sound in the list
        if (ListIsPlaying)
        {
            if (!CurrentlyPlayingSound.Source.isPlaying)
            {
                PlayNextListItem();
            }
        }
    }

    public void AddArrayToList(Sound[] SoundArray)
    {
        foreach (Sound s in SoundArray)
        {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;

            if (s.PlayOnStart)
            {
                Play(s.Name);
            }
        }
    }

    public void AddSoundToList(Sound sound)
    {
        SoundList.Add(sound);
    }

    public void AddSoundToList(string Name)
    {
        // find the clip in the array and add it to the list
        SoundList.Add(Array.Find(Sounds, Element => Element.Name == Name));
    }

    public void PlayNextListItem()
    {
        if (SoundList.Count > 0)
        {
            SoundList.RemoveAt(0);

            if (SoundList.Count > 0)
            {
                CurrentlyPlayingSound = SoundList[0];
                CurrentlyPlayingSound.Source.Play();
            }
            else
            {
                ListIsPlaying = false;
                Debug.Log("End of list");
            }
        }
        else
        {
            Debug.Log("End of list");
            ListIsPlaying = false;
        }
    }

    public void RemoveFromList(Sound sound)
    {
        SoundList.Remove(sound);
    }

    // This is the method that should be called by a designer and should be able to check
    // the status of the list of sounds and respond. 
    public void Play(string Name)
    {

        Sound SelectedSound = Array.Find(Sounds, Element => Element.Name == Name);

        // The supplied name does not find a sound 
        if (SelectedSound == null)
        {
            Debug.Log("Selected Sound is not valid from AudioManagerScript/Play()");
            return;
        }

        // If there are no other sounds playing, set the new sound
        if(CurrentlyPlayingSound.Clip == null)
        {
            AddSoundToList(SelectedSound);
            CurrentlyPlayingSound = SelectedSound;
            CurrentlyPlayingSound.Source.Play();
            ListIsPlaying = true;
        }
        else if(SelectedSound.CanInturrupt)
        {
            if(CurrentlyPlayingSound.Interruptable)
            {
                CurrentlyPlayingSound.Source.Stop();
                RemoveFromList(CurrentlyPlayingSound);
                CurrentlyPlayingSound = SelectedSound;
                SelectedSound = null;
                CurrentlyPlayingSound.Source.Play();
                ListIsPlaying = true;
            }
        }
        else
        {
            // put the clip in the queue
            AddSoundToList(SelectedSound);  
        }
    }

    public void Play(Sound SoundToPlay)
    {
        // The supplied name does not find a sound 
        if (SoundToPlay == null)
        {
            Debug.Log("Selected Sound is not valid from AudioManagerScript/Play()");
            return;
        }

        // If there are no other sounds playing, set the new sound
        if (CurrentlyPlayingSound.Clip == null)
        {
            AddSoundToList(SoundToPlay);
            CurrentlyPlayingSound = SoundToPlay;
            CurrentlyPlayingSound.Source.Play();
            ListIsPlaying = true;
        }
        else if (SoundToPlay.CanInturrupt)
        {
            if (CurrentlyPlayingSound.Interruptable)
            {
                CurrentlyPlayingSound.Source.Stop();
                RemoveFromList(CurrentlyPlayingSound);
                CurrentlyPlayingSound = SoundToPlay;
                CurrentlyPlayingSound.Source.Play();
                ListIsPlaying = true;
            }
        }
        else
        {
            // put the clip in the queue
            AddSoundToList(SoundToPlay);
        }
    }
}
