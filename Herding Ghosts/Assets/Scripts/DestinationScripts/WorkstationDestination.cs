using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkstationDestination : Destination
{

    public int maxItems = 3;

    //take the inventory out
    //should reference a seperate inventory componenet
    //public List<IngredientItem> _items = new List<IngredientItem>();

    public WorkstationInventory _inventory;

    public enum WorkstationType {Cauldron, Oven};

    public List<Transform> _itemPositions = new List<Transform>();

    public Transform finishedItemPosition;

    //_recipe list shoudl be the only relevant thing here
    public List<RecipeObject> _recipeList = new List<RecipeObject>();

    //Accepted Items

    //Recipe Outputs


    //Interaction system
    public SpriteRenderer sprRndr;

    public Material defaultMaterial;
    public Material highlightMaterial;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Interaction Systems
    public void WorkStationHighlight()
    {
        if (highlightMaterial != null)
            sprRndr.material = highlightMaterial;
    }

    public void WorkstationUnHighlight()
    {
        if (defaultMaterial != null)
        {
            sprRndr.material = defaultMaterial;
        }
        //if item highlighted, unhiglight

    }


    //we could do with an inventory script that handles available space and inventory slots seperately
    public bool HasInventorySpace()
    {

        Debug.Log("ITEM LIST SIZE == " + _inventory._items.Count);

        return _inventory._items.Count <= maxItems-1;
    }

    public void AddItemToList(IngredientItem item)
    {
        _inventory._items.Add(item);
        MoveItemsToSlotPosition(item);


        InventoryCheck();

        

    }

    public void MoveItemsToSlotPosition(IngredientItem item)
    {
        if(_inventory._items.Count > 0)
        {
            int index = _inventory._items.IndexOf(item);
            item.transform.position = _itemPositions[index].transform.position;
        }
    }


    public void ClearInventory()
    {


        _inventory.ClearInventory();


    }
    public void InventoryCheck()
    {
        RecipeObject tempRecipe = null;

        if (_inventory._items.Count >= 3)
            if (_recipeList.Count > 0)
            {
                foreach (RecipeObject recipe in _recipeList)
                {
                    if (RecipeCheck(recipe))
                    {
                        tempRecipe = recipe;
                    }
                }

                if (tempRecipe != null)
                {
                    RecipeCook(tempRecipe);
                }
            }
    }

    //Scan through existing recipes
    //See if items at index's 0-2 match
    //consume items
    //output prefab
    public bool RecipeCheck(RecipeObject recipeIn)
    {


        //bool recipeTrue = false;
        bool[] ingredient = new bool[3] { false, false, false } ;



        List<Item> tempInventory = new List<Item>(_inventory._items);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < tempInventory.Count; j++)
            {
                if (tempInventory[j].GetIngredientType() == recipeIn.ingredient[i])
                {
                    ingredient[i] = true;
                    tempInventory.RemoveAt(j);
                    break;
                }
            }
        }


        return (ingredient[0] && ingredient[1] && ingredient[2]);
        
    }

    public virtual void RecipeCook(RecipeObject recipeIn)
    {
        //temp store
        GameObject ingredient0 = _inventory._items[0].gameObject;
        GameObject ingredient1 = _inventory._items[1].gameObject;
        GameObject ingredient2 = _inventory._items[2].gameObject;

        ConsumeItems(ingredient0, ingredient1, ingredient2);

        Instantiate(recipeIn.recipePrefab, finishedItemPosition.position, Quaternion.identity);
    }


    public void RemoveItemFromList()
    {
        _inventory._items.Remove(_inventory._items[0]);
    }

    public void RemoveItemFromList(int i)
    {
        _inventory._items.Remove(_inventory._items[i]);
    }

    public void RemoveItemFromList(IngredientItem item)
    {
        if (_inventory._items.Contains(item))
            _inventory._items.Remove(item);
    }

    public void ConsumeItems(GameObject ingredient0, GameObject ingredient1, GameObject ingredient2)
    {
        RemoveItemFromList(ingredient0.GetComponent<IngredientItem>());
        RemoveItemFromList(ingredient1.GetComponent<IngredientItem>());
        RemoveItemFromList(ingredient2.GetComponent<IngredientItem>());

        Destroy(ingredient0);
        Destroy(ingredient1);
        Destroy(ingredient2);

        _inventory.ClearInventory();
    }
}