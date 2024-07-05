using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndDay : MonoBehaviour
{
    public GameObject parent;
    public LevelManager levelManager;
    public int slot;

    // Start is called before the first frame update
    void Start()
    {
        if (SaveSystem.ManagerState.CheckAvailable(slot))
        {
            var time = SaveSystem.ManagerState.GetSaveTime(slot);

            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Save Slot {slot}: {time}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDaySaved(int slot)
    {
        SaveSystem.ManagerState.SetSaveSlot(slot);
        SaveSystem.ManagerState.SaveItems();
        parent.SetActive(false);
        levelManager.RepeatDay();
    }
}
