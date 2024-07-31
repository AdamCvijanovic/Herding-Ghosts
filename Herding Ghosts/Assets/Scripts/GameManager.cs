using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager instance { get { return _instance; } }


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        if (GetComponent<SaveItem>().numbersToSave.Count == 0) { 
            GetComponent<SaveItem>().numbersToSave.Add("satisfiedCustomers", 0);
            GetComponent<SaveItem>().numbersToSave.Add("disastisfiedCustomers", 0);
            GetComponent<SaveItem>().numbersToSave.Add("points", 0);
            GetComponent<SaveItem>().numbersToSave.Add("day", 0);
        }

        DontDestroyOnLoad(gameObject);
        // If the instance reference has not been set, yet, 
    }

    private void Start()
    {
        
    }

    public void ResetDailyCounters()
    {
    }

    public void UpdateCustomerCounter(int i)
    {
        GetComponent<SaveItem>().numbersToSave["satisfiedCustomers"] += i;

        GetComponent<SaveItem>().numbersToSave["points"] += i * 250;


        if (FindObjectOfType<LevelManager>().maxCustomerCount <= GetComponent<SaveItem>().numbersToSave["satisfiedCustomers"])
        {
            FindObjectOfType<LevelManager>().AllCustomersServedForTheDay();
        }

    }

    public void UpdateDisatisfiedCustomerCounter(int i)
    {
        GetComponent<SaveItem>().numbersToSave["disastisfiedCustomers"] += i;
    }

    public void UpdatePoints(int i)
    {
        GetComponent<SaveItem>().numbersToSave["points"] += i;
    }

    public void IncrementDay()
    {
        GetComponent<SaveItem>().numbersToSave["day"]++;
    }

    public void ClearAllFields()
    {
        GetComponent<SaveItem>().numbersToSave["satisfiedCustomers"] = 0;
        GetComponent<SaveItem>().numbersToSave["disastisfiedCustomers"] = 0;
        GetComponent<SaveItem>().numbersToSave["points"] = 0;
        GetComponent<SaveItem>().numbersToSave["day"] = 0;
    }

    public int GetSatisifed()
    {
        return GetComponent<SaveItem>().numbersToSave["satisfiedCustomers"];
    }

    public int GetDisatisfied()
    {
        return GetComponent<SaveItem>().numbersToSave["disastisfiedCustomers"];
    }

    public int GetPoints()
    {
        return GetComponent<SaveItem>().numbersToSave["points"];
    }

    public int GetDay()
    {
        return GetComponent<SaveItem>().numbersToSave["day"];
    }

}
