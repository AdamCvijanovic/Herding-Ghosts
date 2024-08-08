
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class HideLocation : MonoBehaviour
{
    bool m_hideEvent_Activated = false;
    


    // Start is called before the first frame update
    void Start()
    {
        HideMinigameController.hiderEvent += DeSelectLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLocation()
    {
        m_hideEvent_Activated = true;

    }


    public void DeSelectLocation()
    {
        if (m_hideEvent_Activated)
        {
            m_hideEvent_Activated = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
            HideMinigameController.hiderEvent?.Invoke();
    }


}
