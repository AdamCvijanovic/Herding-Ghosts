using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDay : MonoBehaviour
{
    public LevelManager levelManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDaySaved(int slot)
    {
        SaveSystem.ManagerState.SetSaveSlot(slot);
        SaveSystem.ManagerState.SaveItems();

        levelManager.RepeatDay();
    }
}
