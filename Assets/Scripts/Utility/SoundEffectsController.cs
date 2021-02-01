using System;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class SoundEffectsController : MonoBehaviour
{
    public static SoundEffectsController Instance;

    private AudioSource audioSource;
    public List<SoundEffect> soundEffects;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
        }
    }


    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(Utility.SoundEffect effect)
    {
        AudioClip clip = null;

        foreach (SoundEffect soundEffect in soundEffects)
        {
            if (soundEffect.effect == effect)
            {
                clip = soundEffect.clip;
                break;
            }
        }

        audioSource.clip = clip;
        audioSource.Play();
    }
}

[Serializable]
public class SoundEffect
{
    public Utility.SoundEffect effect;
    public AudioClip clip;

    public SoundEffect(Utility.SoundEffect effect, AudioClip clip)
    {
        this.effect = effect;
        this.clip = clip;
    }
}