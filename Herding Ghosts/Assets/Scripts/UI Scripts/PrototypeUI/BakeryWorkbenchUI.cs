using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BakeryWorkbenchUI : MonoBehaviour
{
    public BakeryWorkstation _bakeryWorksation;

    public GameObject workbenchPanel;

    public Canvas canvas;

    public DraggableItem baseIngItem;

    public Sprite cookedIngredientSprite;

    public FoodPrepPanelUI foodPrepPanel;

    public GameObject itemGrid;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        _bakeryWorksation = FindObjectOfType<BakeryWorkstation>();
        foodPrepPanel = GetComponentInChildren<FoodPrepPanelUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWindowWorkstation(BakeryWorkstation bakeryWorkstationIn)
    {
        _bakeryWorksation = bakeryWorkstationIn;
    }

    public void ActivatePanel()
    {
        workbenchPanel.SetActive(true);
        FillInventory();
    }

    public void DeActivatePanel()
    {
        workbenchPanel.SetActive(false);
    }

    public void ActivateCookButton()
    {
        baseIngItem.GetComponent<Image>().sprite = cookedIngredientSprite;
        _bakeryWorksation.ProcessFood();
    }

    public void UpdateDragItem(Item item)
    {
        baseIngItem.image.sprite = item.sprRndr.sprite;
    }

    public void FillInventory()
    {
        Debug.Log(_bakeryWorksation.GetInventoryItems());
    }

}
