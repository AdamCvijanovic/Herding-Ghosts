using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomReset : MonoBehaviour
{
    FurnitureManager furnitureManager;

    // Start is called before the first frame update
    private void OnEnable()
    {
        furnitureManager = GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<FurnitureManager>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetRoom()
    {
        furnitureManager.RemoveAllFurniture();
    }
}
