using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JukeBoxButton : MonoBehaviour
{
    private MusicShuffler allsongs;
    public TextMeshProUGUI textItem;
    // Start is called before the first frame update
    void Start()
    {
        allsongs = GameObject.FindGameObjectWithTag("Audio").GetComponent<MusicShuffler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSong()
    {
        allsongs.StartCustomSong(textItem.text);
    }
}
