using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureVaporize : MonoBehaviour
{
    public MusicShuffler shuffler;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("PlaceableFurniture"))
        {
            collision.GetComponent<FurnitureController>().RemoveFurniture(new UnityEngine.InputSystem.InputAction.CallbackContext());
        }

        if (collision.CompareTag("Player"))
        {
            shuffler.StopCustomSong();
        }
    }

}
