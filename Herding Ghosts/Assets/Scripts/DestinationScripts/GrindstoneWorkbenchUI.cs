using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GrindstoneWorkbenchUI : MonoBehaviour
{
    public GrindstoneWorkstation _grindstoneWorksation;

    public GameObject workbenchPanel;

    public Canvas canvas;

    public DraggableGrindstone draggableGrindstoneHandle;

    public GameObject milledFlourImg;

    public Slider grindSlider;
    public float incrementValue;
    public float flourScaleFactor;

    public Button flourButton;

    public InventorySlot wheatInventorySlot;
    public DraggableItem dragItem;

    public GameObject wheatPrefab;
    public GameObject flourPrefab;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetWindowWorkstation(GrindstoneWorkstation _grindstoneWorksationIn)
    {
        _grindstoneWorksation = _grindstoneWorksationIn;
    }

    public void ActivatePanel()
    {
        if(workbenchPanel.activeInHierarchy != true)
        {
            workbenchPanel.SetActive(true);
            draggableGrindstoneHandle.transform.rotation = Quaternion.Euler(0, 0, 0);
            milledFlourImg.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            grindSlider.value = grindSlider.minValue;
        }
    }

    public void DeActivatePanel()
    {
        workbenchPanel.SetActive(false);
    }

    public void ActivateButton()
    {
        //dragItem.GetComponent<Image>().sprite = cookedIngredientSprite;
        _grindstoneWorksation.ProcessFood();
    }

    public void UpdateDragItem(Item item)
    {
        dragItem.image.sprite = item.sprRndr.sprite;
        dragItem.UpdateItemImage();
    }

    public void ProgressGrindSlider()
    {
        if(grindSlider.value >= grindSlider.maxValue)
        {
            FullGrindSlider();
        }
        else
        {
            grindSlider.value += (grindSlider.maxValue / incrementValue);


            //Flour Scale logic
            float range = grindSlider.maxValue - grindSlider.minValue;
            float currentPercent = (grindSlider.value / range); 
            milledFlourImg.GetComponent<RectTransform>().localScale = new Vector3(1,1,1) * ((currentPercent + 1) * flourScaleFactor);
            DisableFlourButton();
        }

    }

    private void FullGrindSlider()
    {
        dragItem.item = flourPrefab.GetComponent<IngredientItem>();
        EnableFlourButton();
    }

    public void ResetGrindSlider()
    {
        grindSlider.value = 0;
    }

    public void SpawnFlourButton()
    {
        ResetGrindSlider();
        DisableFlourButton();
        GameObject newFlour = _grindstoneWorksation.SpawnFlour();

        //change flour back to wheat
        dragItem.item = wheatPrefab.GetComponent<IngredientItem>();
        dragItem.UpdateItemImage();

        //Should Disable Canvas and pickup Flour
        DeActivatePanel();
        //**Acvija: Change this
        FindObjectOfType<PlayerPickup>().Pickup(newFlour.GetComponent<IngredientItem>());
    }

    public void EnableFlourButton()
    {
        flourButton.gameObject.SetActive(true);
    }

    public void DisableFlourButton()
    {
        flourButton.gameObject.SetActive(false);
    }

}

