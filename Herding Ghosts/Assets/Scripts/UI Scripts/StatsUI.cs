using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI dayBanner;
    public TextMeshProUGUI textMeshProUGUI;
    public string bannertext;
    public string statsString;
    public int satisfiedCustomers;
    public int disastisfiedCustomers;
    public int points;

    // Start is called before the first frame update
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        UpdateStatsText();
        //IncrementDay();
        UpdateBannerText();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStatsText()
    {
        if(GameManager.instance != null)
        {
            satisfiedCustomers = GameManager.instance.GetSatisifed();
            disastisfiedCustomers = GameManager.instance.GetDisatisfied();
            points = GameManager.instance.GetPoints();

            statsString = "Satisfied Customers " + satisfiedCustomers + "\n\n Disatisfied Customers " + disastisfiedCustomers /*+ "\n\n Points " + points*/;

            textMeshProUGUI.text = statsString;
        }
    }

    public void UpdateBannerText()
    {
        switch(GameManager.instance.GetDay())
        {
            case 3:
                bannertext = "First Day Completed!";
            break;
            case 6:
                bannertext = "Second Day Completed!";
            break;
            case 9:
                bannertext = "Third Day Completed!";
            break;
            default:
                bannertext = "Day Completed!";
            break;
        }

        
        dayBanner.text = bannertext;
    }

    void IncrementDay()
    {
        GameManager.instance.IncrementDay();
    }
}
