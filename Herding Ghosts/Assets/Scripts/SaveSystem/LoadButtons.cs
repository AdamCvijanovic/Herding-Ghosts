using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtons : MonoBehaviour
{
    public int slot = 0;
   // Start is called before the first frame update
    void Start()
    {
        if(!SaveSystem.ManagerState.CheckAvailable(slot))
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }

}
