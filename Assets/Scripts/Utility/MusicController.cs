using UnityEngine;
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> tracks;

    private static List<AudioClip> staticTracks;
    private static AudioSource player;

	void Start ()
    {
        player = GetComponent<AudioSource>();
        staticTracks = new List<AudioClip>();
        staticTracks = tracks;
	}

    public static void PlayTrack (int track)
    {
        player.clip = staticTracks[track];
        player.Play();
    }
}