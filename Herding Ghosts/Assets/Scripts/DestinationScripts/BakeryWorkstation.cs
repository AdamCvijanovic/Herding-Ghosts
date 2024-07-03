using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BakeryWorkstation : MonoBehaviour
{
    public Interactable interactable;

    //inventory references a seperate inventory componenet
    public WorkstationInventory _inventory;

    public BakeryWorkbenchUI _bakeryWorkbenchUI;

    public Transform itemSpawnPosition;

    public Item currentItem;

    public Sprite tempCookedIngSprite;

    public float maxDistance;
    public float dstToPlayer;

    public Item ingdntBase;
    public Item ingdntOne;
    public Item ingdntTwo;


    // Start is called before the first frame update
    void Start()
    {
        interactable = GetComponent<Interactable>();

        _inventory = GetComponent<WorkstationInventory>();



        if (_bakeryWorkbenchUI == null)
        {
            _bakeryWorkbenchUI = FindObjectOfType<BakeryWorkbenchUI>();
            _bakeryWorkbenchUI.SetWindowWorkstation(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerDistanceDisableUI();
    }


    public void UseWorkbench()
    {
        Debug.Log("Using Workbench");
        _bakeryWorkbenchUI.ActivatePanel();
        LinkInventoryToPlayer();

        WorkstationInventory wkstnInventory = GetComponent<WorkstationInventory>();

    }

    public void PlayerDistanceDisableUI()
    {
        dstToPlayer = Vector3.Distance(FindObjectOfType<Player>().transform.position, transform.position);
        if (dstToPlayer >= maxDistance)
        {
            _bakeryWorkbenchUI.DeActivatePanel();
        }
    }

    public void UpdateCurrentItem(Item item)
    {
        //**acvija: what are we doing here again?
        currentItem = item;
        _bakeryWorkbenchUI.UpdateDragItem(item);
    }

    public List<Item> GetInventoryItems()
    {
        return _inventory._items;
    }

    public void LinkInventoryToPlayer()
    {
        _inventory._items = FindObjectOfType<PlayerInventoryUI>().playerInventory._items;
    }

    public void InventoryCheck(Item item)
    {
        //RecipeObject tempRecipe = null;
        Debug.Log("The Item is " + item.gameObject.name);

        if (_inventory._items.Count > 0 && _inventory._items.Count >= _inventory.maxItems)
        {
            UpdateCurrentItem(item);
        }
    }

    public void ProcessFood(GameObject baseIngdntPrefab)
    {
        //if(currentItem != null)
        //{
        //    currentItem.sprRndr.sprite = tempCookedIngSprite;
        //    if (currentItem.GetComponent<IngredientProperties>() != null)
        //    {
        //        currentItem.GetComponent<IngredientProperties>().AddProperty(IngredientProperties.IngredientGroup.Chopped);
        //    }
        //}

        //Consume Item

        Debug.Log("Create Cooked Item");
        GameObject newFood = Instantiate(baseIngdntPrefab, itemSpawnPosition);

        _bakeryWorkbenchUI.DeActivatePanel();
        FindObjectOfType<PlayerPickup>().Pickup(newFood.GetComponent<FoodItem>());
        _bakeryWorkbenchUI.ResetMinigameUI();

    }
}
