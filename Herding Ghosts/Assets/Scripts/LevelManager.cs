using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;


public class LevelManager : MonoBehaviour
{

    public CanvasManager canvasManager;
    public UIFadeInOut uiFadeInOut;

    public Light2D globalLight;
    public Color dayColor;
    public Color nightColor;
    public float colorTime = 0;
    float colorIncrement = 0.2f;

    public BedScript bed;
    public Granny_Anim granny;

    public int maxCustomerCount;
    public int dayNumber;

    public float remainingCustomers;

    public bool isDayOne;
    public bool isStoreOpen;
    public bool minCustomersServed;

    //end cutscene event
    public UnityEvent _ActivateEvent = new UnityEvent();


    private void Awake()
    {
        bed = FindObjectOfType<BedScript>();
        BedToggle(false);


        granny = FindObjectOfType<Granny_Anim>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        uiFadeInOut = FindObjectOfType<UIFadeInOut>();
        SetMaxCustomers();
        ResetLightingTimer();
    }

    // Update is called once per frame
    void Update()
    {
       

        ChangeLighting();

    }

    public void StartDay()
    {
        if(uiFadeInOut != null)
            uiFadeInOut.FadeIn();
    }

    public void EndDay()
    {
        if (uiFadeInOut != null)
            uiFadeInOut.FadeOut();
    }

    public void OpenStore()
    {
        isStoreOpen = true;
        GrannyToggle(false);
    }

    public void ChangeLighting()
    {
        if(globalLight != null)
        {
            if (minCustomersServed)
            {
                globalLight.color = Color.Lerp(dayColor, nightColor, colorTime);
            }
            else
            {
                globalLight.color = Color.Lerp(nightColor, dayColor, colorTime);
            }

            if (colorTime < 1)
            {
                colorTime += Time.deltaTime * colorIncrement;
            }
        }
        
    }

    public void ResetLightingTimer()
    {
        colorTime = 0;
    }

    public void RepeatDay()
    {
        if (GameManager.instance.GetDay() >= 6)
        {
            Debug.Log("Third Day passed");
            RollCredits();
        }
        else
        {
            Debug.Log("A new day begins");
            SceneManager.LoadScene("Drink_Minigame Lily1", LoadSceneMode.Single);
        }
    }

    public void RollCredits()
    {
        SceneManager.LoadScene("Game_Outro_Cutscene", LoadSceneMode.Single);
    }

    public void RollCredits2()
    {
        SceneManager.LoadScene("Game_Credits", LoadSceneMode.Single);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu NEW", LoadSceneMode.Single);
    }

    public void NightDayScene()
    {
        GameManager.instance.IncrementDay();

        Debug.Log("Third Day passed");
        SceneManager.LoadScene("NightToDayCutscene", LoadSceneMode.Single);
    }

    public void ActivateEventAfterFade()
    {
        Debug.Log("ACTIVATE");
        _ActivateEvent.Invoke();
    }

    public void AllCustomersServedForTheDay()
    {
        ResetLightingTimer();
        minCustomersServed = true;
        BedToggle(true);
    }

    public void BedToggle(bool state)
    {
        if(bed != null)
            bed.GetComponent<Interactable>().enabled = state;
    }

    public void GrannyToggle(bool state)
    {
        if (granny != null)
            granny.gameObject.SetActive(state);
    }

    public void SetMaxCustomers()
    {
        switch (GameManager.instance.GetDay())
        {
            case 0:
                maxCustomerCount = 0;
                dayNumber = 1;
                isDayOne = true;
                GrannyToggle(true);
                break;
            case 3:
                maxCustomerCount = 2;
                dayNumber = 2;
                isDayOne = false;
                GrannyToggle(false);
                break;
            case 6:
                maxCustomerCount = 3;
                dayNumber = 3;
                isDayOne = false;
                GrannyToggle(false);
                break;
            default:
                maxCustomerCount = 1;
                break;
        }
    }

}
