using UnityEngine;

public class SoundEffectsController : MonoBehaviour
{
    private static AudioSource soundEffects;

    void Start ()
    {
        soundEffects = GetComponent<AudioSource>();
    }

    public static void PlaySoundEffect(AudioClip clip)
    {
        soundEffects.clip = clip;
        soundEffects.Play();
    }
}