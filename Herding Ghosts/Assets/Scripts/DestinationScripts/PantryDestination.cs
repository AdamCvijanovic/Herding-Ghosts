using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantryDestination : Destination
{
    public Interactable interactable;

    public GameObject UIPanel;

    public Transform spawnPosition;

    public List<GameObject> ingredientprefabs = new List<GameObject>();

    public float maxDistance;
    public float dstToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIPanel.activeInHierarchy)
            PlayerDistanceDisableUI();
    }

    public void ActivatePantryUI()
    {
        FindObjectOfType<CanvasManager>().PantryActivate();
    }

    public void SpawnIngredient(IngredientItem.IngredientType ingredientType)
    {
        Debug.Log("IngredientType: " + ingredientType );

        foreach(GameObject i in ingredientprefabs)
        {
            if(i.GetComponent<IngredientItem>().GetIngredientType() == ingredientType)
            {
                GameObject newItem = Instantiate(i, spawnPosition.transform.position, Quaternion.identity);
                FindObjectOfType<PlayerPickup>().PickupItem(newItem.GetComponent<Item>());
            }
        }
    }

    public void PlayerDistanceDisableUI()
    {
        dstToPlayer = Vector3.Distance(FindObjectOfType<Player>().transform.position, transform.position);
        if (dstToPlayer >= maxDistance)
        {
            FindObjectOfType<PantryUI>().Deactivate();
        }
    }
}
