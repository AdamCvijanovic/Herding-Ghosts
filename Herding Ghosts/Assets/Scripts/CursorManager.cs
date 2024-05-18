using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D[] cursors;
    
    // Start is called before the first frame update
    void Start()
    {
        var i = Random.Range(0, 4);
        Cursor.SetCursor(cursors[i], Vector2.zero,CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
