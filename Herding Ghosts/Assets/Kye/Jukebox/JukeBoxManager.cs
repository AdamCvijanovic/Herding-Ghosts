using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class JukeBoxManager : MonoBehaviour
{
    public Transform content;
    public MusicShuffler allsongs;
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {

        foreach (var song in allsongs.pattisserieSongList)
        {
            var tempButton = Instantiate(button, content);
            tempButton.GetComponentInChildren<TextMeshProUGUI>().text = song.clip.name;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
