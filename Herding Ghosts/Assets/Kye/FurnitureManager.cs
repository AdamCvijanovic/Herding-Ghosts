using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FurnitureManager : MonoBehaviour
{

    List<GameObject> m_createdFurniture = new List<GameObject>();
    List<GameObject> m_availableFurniture = new List<GameObject>();
    public GameObject[] m_furnitureSelector;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void AddFurniture(GameObject furniture)
    {
        GameObject[] tempList = new GameObject[m_furnitureSelector.Length + 1];
        m_furnitureSelector.CopyTo(tempList, 0);
        tempList[m_furnitureSelector.Length] = furniture;
        m_furnitureSelector = tempList;
    }

    public void FurnitureAppear()
    {
        var position = transform.position + new Vector3(-5, -3, 0);


        foreach (GameObject furniture in m_furnitureSelector)
        {

            m_availableFurniture.Add(Instantiate(furniture, position, Quaternion.identity));

            position += new Vector3(2, 0, 0);

        }

    }

    public void ClearFurniture()
    {
        foreach (GameObject furniture in m_availableFurniture)
        {
            if(!furniture.GetComponent<FurnitureController>().selected )
                Destroy(furniture);
        }
        m_availableFurniture.Clear();
    }

    public void CreateFurnitureInScene(GameObject furniture)
    {
        m_createdFurniture.Add(furniture);
        Debug.Log(m_createdFurniture.Count);
    }

    public void RemoveFurnitureInScene(GameObject furniture)
    {
        
        m_createdFurniture.Remove(furniture);
        Debug.Log("Removed");
    }

    public void RemoveAllFurniture()
    {

    }

}
