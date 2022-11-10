using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{


    public int maxItems = 3;

    public List<Item> _items = new List<Item>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //we could do with an inventory script that handles available space and inventory slots seperately
    public bool HasInventorySpace()
    {
        return _items.Count <= maxItems - 1;
    }

    public virtual void AddItemToList(Item item)
    {
        //if(item.GetFoodType() == FoodItem.FoodType.Carrot)
        _items.Add(item);

    }

    public void RemoveItemFromList()
    {
        _items.Remove(_items[0]);
    }

    public void RemoveItemFromList(int i)
    {
        _items.Remove(_items[i]);
    }

    public void RemoveItemFromList(Item item)
    {
        if (_items.Contains(item))
            _items.Remove(item);
    }
}
