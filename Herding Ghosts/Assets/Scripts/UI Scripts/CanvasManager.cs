using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    public GameObject introductionMenu;
    public GameObject menuElements;
    //public GameObject pauseMenu;
    public GameObject winMenu;
    //public GameObject loseMenu;
    //public GameObject controlsMenu;
    
    public GameObject pantryUI;
    public GameObject seedBarrelUI;
    public GameObject timerUI;

    [Header("Minigame UI Panels")]

    public GameObject bakingMinigameUI;
    public GameObject grindStoneMinigameUI;

    [Header("Grimoire and Inventory Screens")]
    public GrimoireUI grimoireUI;
    public GameObject playerInventoryUI;

    public UIFadeInOut levelChanger;
    
    //public Image playerIMG;
    public Image playerIMG;
    public Image customerIMG;

    public bool youWin = false;

    //audio
    public AudioSource audioGrimoireEnter;
    public AudioSource audioGrimoireExit;

    // Start is called before the first frame update
    void Start()
    {
        //populate vars
        levelChanger = GetComponentInChildren<UIFadeInOut>();

        //set active or not
        introductionMenu.SetActive(true);
        //pauseMenu.SetActive(false);
        //loseMenu.SetActive(false);
        winMenu.SetActive(false);
        //controlsMenu.SetActive(false);

        GrimoireDeactivate();

    }

    // Update is called once per frame
    void Update()
    {
        if(introductionMenu.activeInHierarchy ){Pause();}

        if(youWin == true)
        {
            WinMenu();
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Resume()
    {   
        Time.timeScale = 1f;
    }

    /* public void PauseMenu()
    {
        //Disable Other Menus
        DisableOtherUIElements();

        if (pauseMenu.activeSelf)
        {
            menuElements.SetActive(false);
            pauseMenu.SetActive(false);
            Resume();
        }
        else if(controlsMenu.activeInHierarchy == false && loseMenu.activeInHierarchy == false && winMenu.activeInHierarchy == false && introductionMenu.activeInHierarchy == false && pauseMenu.activeInHierarchy == false)
        {
            Pause();
            menuElements.SetActive(true);
            pauseMenu.SetActive(true);
        }
    }  */

/*
    public void LoseMenu()
    {
        menuElements.SetActive(true);
        loseMenu.SetActive(true);
    }
*/
    public void WinMenu()
    {
        /* if(controlsMenu.activeInHierarchy == false && loseMenu.activeInHierarchy == false && winMenu.activeInHierarchy == false && introductionMenu.activeInHierarchy == false && pauseMenu.activeInHierarchy == false )
        {
            Pause();
            menuElements.SetActive(true);
            winMenu.SetActive(true);
        } */
            Pause();
            menuElements.SetActive(true);
            winMenu.SetActive(true);
    }
    
    public void GrimoireToggle()
    {

        if (!grimoireUI.isActive)
        {
            GrimoireActivate();
        }
        else
        {
            GrimoireDeactivate();
        }
    }

    public void GrimoireActivate()
    {
        grimoireUI.gameObject.SetActive(true);
        grimoireUI.Activate();
        //audioGrimoireEnter.Play(0);
    }

    public void GrimoireDeactivate()
    {
        grimoireUI.Deactivate();
        //audioGrimoireExit.Play(0);
    }
    public void PantryActivate()
    {
        pantryUI.SetActive(true);
        pantryUI.GetComponent<PantryUI>();
    }

    public void PantryDeactivate()
    {
        pantryUI.GetComponent<PantryUI>();
        pantryUI.SetActive(false);
    }

    public void SeedBarrelActivate()
    {
        seedBarrelUI.SetActive(true);
        seedBarrelUI.GetComponent<SeedButtonUI>();
    }

    public void SeedBarrelDeactivate()
    {
        seedBarrelUI.GetComponent<SeedButtonUI>();
        seedBarrelUI.SetActive(false);
    }

    public void ToggleInventory()
    {
        Debug.Log("Toggle activate");

        if (playerInventoryUI.activeInHierarchy == false)
        {
            playerInventoryUI.SetActive(true);
            //Should have an activate function in the class itself instead of this search and call here
            playerInventoryUI.GetComponent<PlayerInventoryUI>().UpdateInventory();
        }
        else if (playerInventoryUI.activeInHierarchy == true)
        {
            playerInventoryUI.SetActive(false);
        }
    }
    public void ToggleMinigameInventory()
    {
        Debug.Log("Toggle activate");

        if (playerInventoryUI.activeInHierarchy == false)
        {
            playerInventoryUI.SetActive(true);
            playerInventoryUI.GetComponent<PlayerInventoryUI>().UpdateInventory();
        }
        else if (playerInventoryUI.activeInHierarchy == true)
        {
            playerInventoryUI.SetActive(false);
        }
    }
    public void ActivateTimer()
    {
        timerUI.GetComponent<UITImerClock>().ResetClock();
        timerUI.GetComponent<UITImerClock>().StartClock();
        UpdateTimerGFX();
    }

    public void DeActivateTimer()
    {
        timerUI.GetComponent<UITImerClock>().StopClock();
    }

    public void RestartTimer()
    {
        timerUI.GetComponent<UITImerClock>().ResetClock();
    }

    public void UpdateTimerGFX()
    {
        //old timer
        //timerUI.GetComponent<UITimer>().UpdateTimerImg(FindObjectOfType<CustomerManager>().GetCurrentCustomer()._customerAppearance);

        //clock timer


    }

    public void DisableOtherUIElements()
    {
        if (pantryUI.activeInHierarchy)
        {
            PantryDeactivate();
        }

        grimoireUI.tooltipAnimator.SetBool("isActive", false);
        grimoireUI.grimoireAnimator.SetBool("isActive", false);
    }

    public void EnableUIElements()
    {
        grimoireUI.tooltipAnimator.SetBool("isActive", true);
    }

}
