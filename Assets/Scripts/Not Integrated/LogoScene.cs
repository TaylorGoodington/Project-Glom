using UnityEngine;

public class LogoScene : MonoBehaviour
{
    public AudioSource splashSound;

    public void LoadStartMenu ()
    {
        //LevelManager.levelManager.LoadLevel("Start");
    }

    public void StartSoundClip ()
    {
        splashSound.Play();
    }
}
