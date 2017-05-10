using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> tracks;
    private AudioSource player;
    private bool switchTracks;
    private int trackNumber = 0;

	void Start ()
    {
        player = GetComponent<AudioSource>();
        player.clip = tracks[trackNumber];
        player.Play();
        StartCoroutine("TrackIsDone", player.clip.length);
	}

    void Update ()
    {
        if (switchTracks)
        {
            SwitchTrack();
        }
    }

    public void SwitchTrack ()
    {
        switchTracks = false;

        if (trackNumber < tracks.Count - 1)
        {
            trackNumber++;
        }
        else
        {
            trackNumber = 0;
        }

        player.clip = tracks[trackNumber];
        player.Play();
        StartCoroutine("TrackIsDone", player.clip.length);
    }

    public IEnumerator TrackIsDone (float length)
    {
        yield return new WaitForSeconds(length);
        switchTracks = true;
    }
}