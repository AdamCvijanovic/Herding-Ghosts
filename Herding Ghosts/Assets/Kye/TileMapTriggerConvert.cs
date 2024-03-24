using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapTriggerConvert : MonoBehaviour
{
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
        if(collision.CompareTag("PlaceableFurniture"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
