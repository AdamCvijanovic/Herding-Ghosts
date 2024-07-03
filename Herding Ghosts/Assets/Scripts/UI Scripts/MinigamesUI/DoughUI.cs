using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoughUI : MonoBehaviour
{
    public BakeryWorkbenchUI _bakeryWorkbenchUI;

    public Image doughImage;
    public InventorySlot inventorySlot;

    public IngredientScriptableObject currentIngrdnt;

    public IngredientItem.IngredientType ingType;

    public DoughType doughType;

    //currentDoughType

    //DoughTypeEnum

    //DoughFlatness
    public int flatnessLevel;
    public Sprite doughDefault;
    public Sprite doughHalfFlat;
    public Sprite doughFlat;


    private void Update()
    {

    }

    public void ResetDough()
    {
        EmptyDoughIngredient();
        flatnessLevel = 0;
        UpdateImage(doughDefault);
    }

    public void UpdateCurrentIngredient()
    {
        if(inventorySlot.currentItem != null)
        {
            currentIngrdnt = inventorySlot.currentItem.ingScrptObj;
            UpdateDoughIngredient(currentIngrdnt);
        }
        else
        {
            currentIngrdnt = null;
            EmptyDoughIngredient();
        }
    }

    public void UpdateDoughIngredient(IngredientScriptableObject ingScrptObj)
    {
        currentIngrdnt = ingScrptObj;
        IngredientItem.IngredientType ingType = ingScrptObj.itemPrefab.GetComponent<IngredientItem>().GetIngredientType();

        Debug.Log("Update DoughIngredient Here: " + ingScrptObj.name);
        UpdateDough(ingType);
    }

    public void EmptyDoughIngredient()
    {
        currentIngrdnt = null;
        IngredientItem.IngredientType ingType = IngredientItem.IngredientType.None;
        //Should we delete the Slot Draggable Item?

        Debug.Log("Update Dough Ingredient To Plain: ");
        UpdateDough(ingType);
    }

    public void UpdateDough(IngredientItem.IngredientType ingTypeIn)
    {
        ingType = ingTypeIn;
        Debug.Log("Update Dough Here: " + ingType);
        switch (ingType)
        {
            case IngredientItem.IngredientType.Apple:
                UpdateImage(doughType.appleDough);
                break;
            case IngredientItem.IngredientType.Strawberry:
                UpdateImage(doughType.strawberryDough);
                break;
            case IngredientItem.IngredientType.Carrot:
                UpdateImage(doughType.carrotDough);
                break;
            default:
                UpdateImage(doughType.plainDough);
                ingType = IngredientItem.IngredientType.None;
                break;
        }
    }
    public void UpdateImage(Sprite sprite)
    {
        doughImage.sprite = sprite;
    }

    public void UpdateDoughFlatness()
    {
        if(flatnessLevel < 2)
        {
            flatnessLevel++;
        }
        else
        {
            //flatnessLevel = 0;
        }

        switch (flatnessLevel)
        {
            case 0:
                doughImage.sprite = doughDefault;
                break;
            case 1:
                doughImage.sprite = doughHalfFlat;
                break;
            case 2:
                doughImage.sprite = doughFlat;
                _bakeryWorkbenchUI.SetInventoryMode();
                break;

        }
    }

    public void ConsumeCurrentItem()
    {
        inventorySlot.ConsumeItem();
    }
}



[System.Serializable]
public struct DoughType
{
    public Sprite plainDough;
    public Sprite appleDough;
    public Sprite strawberryDough;
    public Sprite carrotDough;
}
