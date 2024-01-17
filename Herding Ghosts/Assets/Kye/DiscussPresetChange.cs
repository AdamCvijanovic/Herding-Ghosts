using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscussPresetChange : MonoBehaviour
{
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnablePresetChange()
    {

        canvas.SetActive(true);
    }

    public void DisablePresetChange()
    {
        canvas.SetActive(false);

    }
}
