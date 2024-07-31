using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerLogic : MonoBehaviour
{
    [SerializeField]
    private Customer _customer;

    public enum CustomerState {Shopping,Leaving};
    public CustomerState currentState;

    DestinationManager _destMngr;
    public CounterDestination counter;
    public Destination customerSpawner;


    public float minDst;
    public float dst;
    public float despawnTime;

    //audio
    public AudioSource audioGhostSatisfied;


    // Start is called before the first frame update
    void Start()
    {
        _customer = GetComponent<Customer>();
        _destMngr = FindObjectOfType<DestinationManager>();
        FetchDestination();
        currentState = CustomerState.Shopping;
    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        dst = Vector3.Distance(_customer.GetNavigator().GetDestination(), transform.position);
    }

    public void SetCustomer(Customer customer)
    {
        _customer = customer;
    }

    public void FetchDestination()
    {
        counter = _destMngr.GetDestinationOfType(Destination.DestinationType.Counter).GetComponent<CounterDestination>();
        customerSpawner = _destMngr.GetDestinationOfType(Destination.DestinationType.CustomerSpawner);
        _customer.GetNavigator().SetDestination(counter.queuePositions[0].transform.position);
    }

    public void SetState(CustomerState state)
    {
        if(state == CustomerState.Leaving)
        {
            _customer.GetNavigator().SetDestination(customerSpawner.transform);
            dst = Vector3.Distance(customerSpawner.transform.position, transform.position);
            _customer.GetCustomerManager().ResetPortraitUI();
            FindObjectOfType<CanvasManager>().DeActivateTimer();

        }

        currentState = state;

    }

    public void CheckState()
    {


        if (currentState == CustomerState.Shopping)
        {
            if (dst < minDst)
            {
                //_customer.UpdateUI();

                if (counter.GetCounterInventory()._items.Count > 0)
                {
                    if (DesiredItemFound())
                    {
                        GrabItem();
                        _customer.GetNavigator().SetDestination(customerSpawner.transform);
                        dst = Vector3.Distance(customerSpawner.transform.position, transform.position);
                        currentState = CustomerState.Leaving;
                    }
                    else
                    {
                        //Debug.Log("No item found");
                    }
                }
            }
        }

        if (currentState == CustomerState.Leaving)
        {
            //remove from list if it exists in it
            if (FindObjectOfType<PlayerPickup>()._nearbyinteractables.Contains(this.GetComponentInChildren<Interactable>()))
            {
                FindObjectOfType<PlayerPickup>()._nearbyinteractables.Remove(this.GetComponentInChildren<Interactable>());
            }
            _customer.GetConversationTrigger().enabled = false;

            if (dst < minDst)
            {

                _customer.GetNavigator().SetDestination(customerSpawner.transform);
                dst = Vector3.Distance(_customer.GetNavigator().GetDestination(), transform.position);
                RemoveCustomer();

                //This is moved to when the customer picks up the desired item
                //if (_customer.isSatisfied)
                //{
                //    //_customer.AmSatisfied();
                //}
            }
        }       
    }

    public void RunShopping()
    {

    }


    public void RemoveCustomer()
    {
        Debug.Log("Remove customer");

        

        _customer.GetCustomerManager().RemoveCustomer(this.GetComponent<Customer>());
        
        Destroy(this.gameObject, despawnTime);
    }

    public bool DesiredItemFound()
    { 
        return counter.GetCounterInventory().SearchForFoodType(_customer._desiredFood);
    }

    public void GrabItem()
    {
        Item desiredFoodItem = counter.GetCounterInventory().GrabFoodOfType(_customer._desiredFood).GetComponent<Item>();
        _customer.GetPickup().PickupItem(desiredFoodItem);

        

        _customer.isSatisfied = true;

        

    }

    public void CustomerSatisfied()
    {
        //play audio
        
        audioGhostSatisfied.Play(0);
        //Reset Timer
        //FindObjectOfType<CanvasManager>().timerUI.GetComponent<UITImerClock>().ResetClock();
        Debug.Log("TIMER " + FindObjectOfType<CanvasManager>().timerUI.GetComponent<UITImerClock>().isCounting);
        FindObjectOfType<CanvasManager>().RestartTimer();
        Debug.Log("TIMER AFTER " + FindObjectOfType<CanvasManager>().timerUI.GetComponent<UITImerClock>().isCounting);
        _customer.isSatisfied = true;
        _customer.GetCustomerManager().IncrementCounter();
    }

    public void CustomerDisatisfied()
    {
        SetState(CustomerLogic.CustomerState.Leaving);
        GameManager.instance.UpdateDisatisfiedCustomerCounter(1);
    }
}
