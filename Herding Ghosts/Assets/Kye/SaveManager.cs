using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    string m_saveLocation;

    // Start is called before the first frame update
    void Start()
    {
        m_saveLocation = Application.persistentDataPath + "/" + gameObject.scene.name +".json";
        Debug.Log(m_saveLocation);
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
