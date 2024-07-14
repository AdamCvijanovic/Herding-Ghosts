using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public IngredientScriptableObject ingScrptObj;
    public IngredientItem item;

    public Image image;

    public Transform currentParent;
    public Transform parentAfterDrag;

    public InventorySlot currentSlot;



    public void Start()
    {
        currentParent = transform.parent;
        UpdateCurrentSlot();

        if (ingScrptObj != null)
        {
            item = ingScrptObj.itemPrefab.GetComponent<IngredientItem>();
            if(item!=null && item.itemSprite != null)
            {
                image.sprite = item.itemSprite;
            }
        }
    }

    private void Update()
    {
        UpdateItemImage();
    }

    public void UpdateItemImage()
    {
        if (ingScrptObj != null)
        {
            item = ingScrptObj.itemPrefab.GetComponent<IngredientItem>();
            if (item!= null && item.itemSprite != null)
            {
                image.sprite = item.itemSprite;
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;

        //need to do a null check here
        if(currentSlot != null)
        {
            currentSlot.RemoveItemFromSlot();
            currentSlot = null;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //transform.position = eventData.position;
        transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            Debug.Log("Raycast Out Of Bounds, Revert Item to Original Position");
        }
        else if (eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>())
        {
            parentAfterDrag = eventData.pointerCurrentRaycast.gameObject.transform;
            //We should update the inventory now

        }
        else if(eventData.pointerCurrentRaycast.gameObject.GetComponent<FoodPrepPanelUI>())
        {
            eventData.pointerCurrentRaycast.gameObject.GetComponent<FoodPrepPanelUI>().SetParentAfterDrag(this);
        }
        

        //parent is set in the inventory slot???
        transform.SetParent(parentAfterDrag);
        currentParent = transform.parent;
        UpdateCurrentSlot();
        image.raycastTarget = true;
    }

    public void UpdateCurrentSlot()
    {
        if (currentParent.GetComponent<InventorySlot>() != null)
        {
            currentSlot = currentParent.GetComponent<InventorySlot>();
            if(item != null)
            {
                currentSlot.AddItemToSlot(this.gameObject);
            }
        }
    }
}
