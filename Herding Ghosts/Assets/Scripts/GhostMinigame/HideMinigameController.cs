using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMinigameController : MonoBehaviour
{

    List<HideLocation> m_HideLocations = new List<HideLocation>();
    int m_selectedItem = -1;

    delegate void NewHideLocationEvent();

    event NewHideLocationEvent  newLocation;
    // Start is called before the first frame update
    void Start()
    {
        m_selectedItem = Random.Range(0, m_HideLocations.Count);

        newLocation = AssignNewHideLocation;

        //m_HideLocations[m_selectedItem].SelectLocation(newLocation);

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignNewHideLocation()
    {
        m_HideLocations[m_selectedItem].DeSelectLocation();
    }
}
