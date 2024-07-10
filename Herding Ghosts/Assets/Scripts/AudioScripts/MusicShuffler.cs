using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicShuffler : MonoBehaviour
{
    public List<AudioSource> pattisserieSongList;
    int currentTrack = 0;
    private bool isGamePaused = false;
   
    private bool isMusicPlaying;

    private int prevTrack;

    private int customTrack = -1;


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
        if (((!pattisserieSongList[currentTrack].isPlaying) || (customTrack != -1 && pattisserieSongList[customTrack].isPlaying)) && !isGamePaused)
            isMusicPlaying = false;


        if (!isMusicPlaying && customTrack == -1)
        {   
            do
            {
                currentTrack = Random.Range(0, pattisserieSongList.Count);
            } while (currentTrack == prevTrack);

            pattisserieSongList[currentTrack].Play();
            prevTrack = currentTrack;
            isMusicPlaying = true;
        }
    }


    public void StartCustomSong(string name)
    {
        var index = pattisserieSongList.FindIndex(x => x.clip.name == name);

        if (customTrack != -1)
            pattisserieSongList[customTrack].Stop();

        else
            pattisserieSongList[currentTrack].Stop();


        pattisserieSongList[index].loop = true;
        pattisserieSongList[index].Play();


        customTrack = index;

    }

    public void StopCustomSong()
    {
        if (customTrack != -1)
        {
            pattisserieSongList[customTrack].loop = false;
            pattisserieSongList[customTrack].Stop();


            customTrack = -1;
        }
    }
}
