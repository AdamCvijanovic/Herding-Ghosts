using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    public static SaveSystem ManagerState;

    private int currentSaveSelection = 0;


    private void Awake()
    {

        if (ManagerState != null)
        {
            Destroy(gameObject);
            return;
        }

        ManagerState = this;
        DontDestroyOnLoad(gameObject);
    }
}
