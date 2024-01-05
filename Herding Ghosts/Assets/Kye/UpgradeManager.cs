using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public GameObject[] m_allRoomUpgrades;
    GameObject m_currentRoomUpgrade = null;
    int m_currentRoom = -1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpgradeRoom()
    {
        Debug.Log("hit");
        if (m_currentRoom < m_allRoomUpgrades.Length)
        {
            if(m_currentRoom != -1)
                m_allRoomUpgrades[m_currentRoom].SetActive(false);
            m_currentRoom++;
            m_allRoomUpgrades[m_currentRoom].SetActive(true);
           
        }

     

    }






}
