using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public GameObject[] m_allRoomUpgrades;
    int m_currentRoom = 0;
    
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
        if (m_currentRoom < m_allRoomUpgrades.Length)
        {
            m_allRoomUpgrades[m_currentRoom].SetActive(false);
            m_currentRoom++;
            m_allRoomUpgrades[m_currentRoom].SetActive(true);
           
        }

     

    }






}
