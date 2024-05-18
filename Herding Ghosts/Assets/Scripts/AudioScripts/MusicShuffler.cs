using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicShuffler : MonoBehaviour
{
    public AudioSource[] pattisserieSongList;
    int currentTrack = 0;
    private bool isGamePaused = false;
   
    private bool isMusicPlaying;

    private int prevTrack;

    void Start()
    {
        isMusicPlaying = false;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isGamePaused = pauseStatus;
    }

    void Update()
    {
        if (!pattisserieSongList[currentTrack].isPlaying && !isGamePaused)
            isMusicPlaying = false;


        if (!isMusicPlaying)
        {   
            do
            {
                currentTrack = Random.Range(0, pattisserieSongList.Length);
            } while (currentTrack == prevTrack);

            pattisserieSongList[currentTrack].Play();
            prevTrack = currentTrack;
            isMusicPlaying = true;
        }
    }
}
