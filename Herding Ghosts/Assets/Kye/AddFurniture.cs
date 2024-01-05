using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFurniture : MonoBehaviour
{
    public GameObject m_rewardFurniture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRewardFurniture()
    {
        GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<FurnitureManager>().AddFurniture(m_rewardFurniture);
        Destroy(this);
    }

}
