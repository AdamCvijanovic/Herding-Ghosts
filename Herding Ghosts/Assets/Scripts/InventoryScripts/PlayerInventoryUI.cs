using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInventoryUI : MonoBehaviour
{
    public GameObject draggableItemPrefab;

    public GameObject _playerInventoryPanel;

    public GridLayoutGroup _gridLayoutGroup;

    public List<InventorySlot> _inventorySlotList = new List<InventorySlot>();

    public Inventory playerInventory;

    // Start is called before the first frame update
    void Awake()
    {
        if(playerInventory == null)
        {
            playerInventory = FindObjectOfType<Player>().gameObject.GetComponent<Inventory>();
        }

        //_playerInventoryPanel.SetActive(false);
        _gridLayoutGroup = _playerInventoryPanel.GetComponentInChildren<GridLayoutGroup>();

        _gridLayoutGroup.GetComponentsInChildren<InventorySlot>(true, _inventorySlotList);
        
        //assign index and reference to each slot
        foreach(InventorySlot inv in _inventorySlotList)
        {
            inv._playerInventoryUI = this;
            inv.inventorySlotList = _inventorySlotList;
            inv.index = _inventorySlotList.IndexOf(inv);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ToggleInventory()
    {
        Debug.Log("Toggle activate");

        if (_playerInventoryPanel.activeInHierarchy == false)
        {
            _playerInventoryPanel.SetActive(true);
            UpdateInventory();
        }
        else if(_playerInventoryPanel.activeInHierarchy == true)
        {
            _playerInventoryPanel.SetActive(false);
        }
    }
    public void ToggleMinigameInventory()
    {
        Debug.Log("Toggle activate");

        if (_playerInventoryPanel.activeInHierarchy == false)
        {
            _playerInventoryPanel.SetActive(true);
            UpdateInventory();
        }
        else if (_playerInventoryPanel.activeInHierarchy == true)
        {
            _playerInventoryPanel.SetActive(false);
        }
    }

    public void UpdatePlayerInventoryFromUI(int index, Item item)
    {
        playerInventory._items[index] = item;
    }

    public void UpdateInventory()
    {
        //get number of items
        int numSlots = _inventorySlotList.Count;

        for (int i = 0; i < numSlots; i++)
        {
            if(playerInventory._items[i] == null)
            {
                _inventorySlotList[i].RemoveItemFromSlot();
                //Destroy(_inventorySlotList[i].currentItem.gameObject);
            }
            //Check if index item is the same as UI item 
            else if(playerInventory._items[i] != _inventorySlotList[i].currentItem)
            {
                Debug.Log("PLAYER INVENTORY" + playerInventory._items[i].name);
                // check if it's an ingredient item
                if (playerInventory._items[i].GetComponent<IngredientItem>().ingScrptobj)
                {
                    IngredientScriptableObject scrObj = playerInventory._items[i].GetComponent<IngredientItem>().ingScrptobj;

                    //if the inventory slot is empty
                    if (_inventorySlotList[i].currentItem == null)
                    {
                        ////////////////////Actually all of this could be in the Slot Script with the draggable prefab passaed as a parameter

                        //spawn draggable item
                        GameObject newDraggable = Instantiate(draggableItemPrefab, this.gameObject.transform);
                        newDraggable.transform.SetParent(_inventorySlotList[i].transform);
                        //assign Parent & Child
                        _inventorySlotList[i].currentItem = newDraggable.GetComponent<DraggableItem>();
                        newDraggable.GetComponent<DraggableItem>().currentParent = _inventorySlotList[i].transform;
                        //Update Object and Slot
                        _inventorySlotList[i].currentItem.ingScrptObj = scrObj;
                        _inventorySlotList[i].currentItem.UpdateCurrentSlot();
                        //update player inventory
                    }
                    //if inventory slot is not empty
                    else
                    {
                        //just update the draggable item
                        _inventorySlotList[i].currentItem.ingScrptObj = scrObj;
                        _inventorySlotList[i].currentItem.UpdateCurrentSlot();
                    }
                }
            }
        }
    }
}
