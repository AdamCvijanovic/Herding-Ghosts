using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{

    //**acvija This reference really needs to be something else more secure
    //this is bound to cause an error
    public List<InventorySlot> inventorySlotList;
    public int index;
    public PlayerInventoryUI _playerInventoryUI;

    public Transform childTransform;
    public DraggableItem currentItem;
    public int quantity;

    public DoughUI doughUI;

    public void OnDrop(PointerEventData eventData)
    {
        if(currentItem == null && eventData.pointerDrag.GetComponent<DraggableItem>())
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            draggableItem.transform.position = dropped.transform.position;
            //draggableItem.parentAfterDrag = transform;
            currentItem = draggableItem;
        }
    }

    public void AddItemToSlot(GameObject itemObj)
    {
        itemObj.transform.position = this.transform.position;
        itemObj.transform.SetParent(this.transform);
        currentItem = itemObj.GetComponent<DraggableItem>();
        if (currentItem != null && _playerInventoryUI != null )
        {
            _playerInventoryUI.UpdatePlayerInventoryFromUI(index, currentItem.item);
        }

        //**acvija: I hate this, there must be a better way
        if(currentItem != null && doughUI != null)
        {
            doughUI.UpdateCurrentIngredient();
        }
    }

    public void RemoveItemFromSlot()
    {
        currentItem = null;
        //**acvija: we really should call a method in PlayerInventoryUI 
        //inventorySlotList.RemoveAt(index);
        if(_playerInventoryUI != null)
        {
            _playerInventoryUI.UpdatePlayerInventoryFromUI(index, null);
        }

        if (doughUI)
        {
            doughUI.UpdateCurrentIngredient();
        }

    }

    public void CreateDraggableItem(GameObject draggableItemPrefab)
    {

    }

    public void ConsumeItem()
    {
        if(currentItem != null)
        {
            GameObject tempItem = (currentItem.gameObject);
            RemoveItemFromSlot();

            Destroy(tempItem);
        }
    }


}
