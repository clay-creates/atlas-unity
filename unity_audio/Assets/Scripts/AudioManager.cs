using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource runningSource;
    public AudioSource landingSource;
    public AudioSource victorySource;

    [Header("Audio Clips")]
    public AudioClip footstepClip;
    public AudioClip landingClip;
    public AudioClip victoryClip;

    [Header("Audio Mixers")]
    public AudioMixer audioMixer;

    [Header("Mixer Snapshots")]
    public AudioMixerSnapshot normalSnapshot;
    public AudioMixerSnapshot pausedSnapshot;

    private void Awake()
    {
        // Singleton Pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayFootstep()
    {
        runningSource.clip = footstepClip;
        runningSource.Play();
    }

    public void StopFootstep()
    {
        runningSource.Stop();
    }

    public void PlayLanding()
    {
        landingSource.clip = landingClip;
        landingSource.Play();
    }

    public void StopLanding()
    {
        landingSource.Stop();
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        bgmSource.clip = bgmClip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlayVictory()
    {
        victorySource.clip = victoryClip;
        victorySource.Play();
    }

    public void ApplyPausedSnapshot()
    {
        pausedSnapshot.TransitionTo(0.1f);
    }

    public void ApplyNormalSnapshot()
    {
        normalSnapshot.TransitionTo(0.1f);
    }
}
