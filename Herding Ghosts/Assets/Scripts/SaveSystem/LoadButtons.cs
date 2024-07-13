using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadButtons : MonoBehaviour
{
    public int slot = 0;

   // Start is called before the first frame update
    void Start()
    {
        SetupButton();


    }

    public void SetupButton()
    {
        if (!SaveSystem.ManagerState.CheckAvailable(slot))
        {
            gameObject.GetComponent<Button>().interactable = false;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Save Slot {slot + 1}";
        }
        else
        {
            var time = SaveSystem.ManagerState.GetSaveTime(slot);

            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Save Slot {slot + 1}: {time}";
        }
    }

}
