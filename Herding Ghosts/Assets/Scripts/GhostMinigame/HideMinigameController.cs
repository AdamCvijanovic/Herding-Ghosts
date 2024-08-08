using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMinigameController : MonoBehaviour
{

    List<HideLocation> m_HideLocations = new List<HideLocation>();
    int m_selectedItem = -1;

    public delegate void NewHideLocationEvent();

    public static NewHideLocationEvent  hiderEvent;

    public GameObject m_hidingGhost;
    // Start is called before the first frame update
    
    void Start()
    {
        m_selectedItem = Random.Range(0, m_HideLocations.Count);


        AssignNewHideLocation();
        hiderEvent += AssignNewHideLocation;
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignNewHideLocation()
    {
        m_selectedItem = Random.Range(0, m_HideLocations.Count);
        m_HideLocations[m_selectedItem].SelectLocation();

        var hiding = Instantiate(m_hidingGhost,transform.position, transform.rotation);
       
    }



}
