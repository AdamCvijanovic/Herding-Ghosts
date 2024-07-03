using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private int number = -1;
    private bool start = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (start)
        {

            if (GetComponent<SaveItem>().numbersToSave.Count == 0)
            {
                GetComponent<SaveItem>().numbersToSave.Add("testnumber", 2);

                SaveSystem.ManagerState.SetSaveSlot(0);

                SaveSystem.ManagerState.SetSaveItems();

                SaveSystem.ManagerState.SaveItems();

                gameObject.SetActive(false);
            }

            else
                number = GetComponent<SaveItem>().numbersToSave["testnumber"];

            start = false;
        }

    }
}
