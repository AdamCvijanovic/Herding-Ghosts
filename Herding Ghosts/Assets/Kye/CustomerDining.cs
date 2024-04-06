using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerDining : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToDining()
    {
        var fManager = GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<FurnitureManager>();
        var runs = 0;
        
        while (runs < 10) {
            int randomNumber = Random.Range(0, fManager.m_createdFurniture.Count);
            var furniture = fManager.m_createdFurniture[randomNumber].GetComponent<FurnitureController>();

            if (furniture.ghostUse && !furniture.inUse)
            {
                GetComponent<NavMeshAgent>().enabled = false;
                transform.GetChild(2).gameObject.SetActive(false);
                runs = 11;

                var step = 1.5F * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, fManager.m_createdFurniture[randomNumber].transform.position, step);

                if (Vector3.Distance(transform.position, fManager.m_createdFurniture[randomNumber].transform.position) < 0.2f)
                {
                    furniture.inUse = true;
                    GetComponent<YSorter>().modifier += 1;
                   
                    
                }
            }

            runs++;
        }

    }

}
