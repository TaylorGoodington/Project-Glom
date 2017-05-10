using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    private static GameControl _thisObject;

    public static AudioSource audioSource;

    void Awake()
    {
        if (_thisObject != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        _thisObject = this;
        DontDestroyOnLoad(transform.root.gameObject);
    }

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void GoToLevel (string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public static void PlaySoundEffect (AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}