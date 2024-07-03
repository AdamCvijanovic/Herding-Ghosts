using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BakeryWorkbenchUI : MonoBehaviour
{
    public BakeryWorkstation _bakeryWorksation;

    [Header("UI Elements")]
    public FoodPrepPanelUI foodPrepPanel;
    public GameObject itemGrid;
    public GameObject workbenchPanel;
    public GameObject IngInventoryPanel;
    public GameObject cookButton;
    public Canvas canvas;

    [Header("Child References")]
    public DoughUI doughUI;
    public RollingPinUI rollingPinUI;

    public int baseDoughIndex = 0;

    public List<FoodScriptableObject> doughTypes = new List<FoodScriptableObject>();

    public List<Button> bakerySelectButtons = new List<Button>();


    public List<FoodScriptableObject> cookedFoodPrefabList;


    

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        _bakeryWorksation = FindObjectOfType<BakeryWorkstation>();
        //foodPrepPanel = GetComponentInChildren<FoodPrepPanelUI>();

        GetDough();

        //UpdateDoughTypeImage(IngredientItem.IngredientType.None);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetDough()
    {
        if (doughUI == null)
        {
            doughUI = GetComponentInChildren<DoughUI>();
            doughUI._bakeryWorkbenchUI = this;
        }
        else
        {
            doughUI._bakeryWorkbenchUI = this;
        }
    }

    public void SetWindowWorkstation(BakeryWorkstation bakeryWorkstationIn)
    {
        _bakeryWorksation = bakeryWorkstationIn;
    }

    public void ActivatePanel()
    {
        workbenchPanel.SetActive(true);
        FillInventory();
        IngInventoryPanel.GetComponent<PlayerInventoryUI>().UpdateInventory();

    }

    public void DeActivatePanel()
    {
        workbenchPanel.SetActive(false);
    }

    public void ActivateInventoryPanel()
    {
        if(IngInventoryPanel != null)
        {
            //IngInventoryPanel.SetActive(true);
            IngInventoryPanel.GetComponent<PlayerInventoryUI>().ToggleInventory();
        }
    }

    public void DeActivateInventoryPanel()
    {
        if(IngInventoryPanel != null)
        {
            IngInventoryPanel.SetActive(false);
        }
    }

    public void ActivateRollingPin()
    {
        if (rollingPinUI != null)
        {
            rollingPinUI.gameObject.SetActive(true);
        }
    }

    public void DeActivateRollingPin()
    {
        if (rollingPinUI != null)
        {
            rollingPinUI.gameObject.SetActive(false);
        }
    }

    public void ActivateCookButton()
    {
        if (cookButton != null)
        {
            cookButton.SetActive(true);
        }
    }

    public void DeActivateCookButton()
    {
        if (cookButton != null)
        {
            cookButton.SetActive(false);
        }
    }

    public void ResetMinigameUI()
    {
        doughUI.ResetDough();

        DeActivateInventoryPanel();
        ActivateRollingPin();
        rollingPinUI.ResetRollingPin();
    }

    public void SetInventoryMode()
    {
        DeActivateRollingPin();
        ActivateInventoryPanel();
        ActivateCookButton();
    }

    public void SetRollingMode()
    {
        ActivateRollingPin();
        DeActivateInventoryPanel();
        DeActivateCookButton();
    }

    public void PressCookButton()
    {

        GameObject cookedFoodPrefab = null;

        if (doughUI.ingType == IngredientItem.IngredientType.Apple)
        {
            cookedFoodPrefab = cookedFoodPrefabList[0].itemPrefab;
        }
        
        if (doughUI.ingType == IngredientItem.IngredientType.Carrot)
        {
            cookedFoodPrefab = cookedFoodPrefabList[1].itemPrefab;
        }
        
        if (doughUI.ingType == IngredientItem.IngredientType.Strawberry)
        {
            cookedFoodPrefab = cookedFoodPrefabList[2].itemPrefab;
        }
        
        if (doughUI.ingType == IngredientItem.IngredientType.None)
        {
            cookedFoodPrefab = cookedFoodPrefabList[3].itemPrefab;
        }

        //Consume The Ingredient
        ConsumeDoughItem();
        //Make the Food
        _bakeryWorksation.ProcessFood(cookedFoodPrefab);



        //Once food Is cooked we reset
        ResetMinigameUI();

    }

    public void ConsumeDoughItem()
    {
        doughUI.ConsumeCurrentItem();
    }

    public void UpdateDragItem(Item item)
    {
        //**acvija change to enum dough image list
        doughUI.UpdateImage(item.sprRndr.sprite);
    }

    public void UpdateDoughTypeImage(IngredientItem.IngredientType ingType)
    {
        doughUI.UpdateDough(ingType);
    }

    //**Acvija, Do we use this? can we delete?
    public void ActivateBaseIngredientButton(GameObject baseIngdntPrefab)
    {
        // Activate Image in centre, Use sprite assigned to ingredient
        if(foodPrepPanel != null)
        {
            if (foodPrepPanel.ingredientBase != null)
            {
                foodPrepPanel.ingredientBase.gameObject.SetActive(true);
                foodPrepPanel.ingredientBase.GetComponent<Image>().sprite = baseIngdntPrefab.GetComponent<FoodItem>().foodSprite;

                //cookedFoodPrefab = baseIngdntPrefab;
            }
        }
        else
        {
            Debug.LogError("No foodPrepPanel Found");
        }
    }

    public void ActivateLeftCycleButton()
    {
        Debug.Log("Left Button pressed");
    }

    public void ActivateRightCycleButton()
    {
        Debug.Log("Right Button pressed");
    }

    public void FillInventory()
    {
        Debug.Log(_bakeryWorksation.GetInventoryItems());
    }

}
