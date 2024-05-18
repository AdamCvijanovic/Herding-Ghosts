using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickup : Pickup
{
    public Player _player;

    //audio
    public AudioSource audioPickup;
    private float minPitch;
    private float maxPitch;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Player>();
        minPitch = 0.9f;
        maxPitch = 1.1f;
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTutorialText();
        if(GetNearestInteractable() != null)
            GetNearestInteractable().UpdateHelpTextPickup();
    }

    public void PickupDropAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_isHolding)
            {
                Drop();
            }
            else
            {
                Pickup();
            }
        }   
    }

    public void Pickup()
    {
        
        //if (!_isHolding)
        {
            Debug.Log("Pickup");

            //get nearby objects
            //parent to transform
            if (_nearbyItems.Count > 0)
            {
                Item nearestItem = GetNearestItem();

               // if (removecloseItem(nearestItem))
                //{
                    _currentItem = nearestItem;
                    _nearbyItems.Remove(nearestItem);


                    _isHolding = true;
                    _currentItem.OnPickup(this);
                    UpdateHelpTextUse(nearestItem);
                    nearestItem.GetComponentInChildren<SpriteRenderer>().sortingOrder = 7;
                //}
            }
        }


    }


    private void OnTriggerStay2D(Collider2D collision)
    {
            if (collision.gameObject.GetComponent<Item>())
            {
                Item collidedItem = collision.gameObject.GetComponent<Item>();
                
                if(!_nearbyItems.Contains(collidedItem))
                 _nearbyItems.Add(collidedItem);

                UpdateHelpTextPickup(collidedItem);
                collidedItem.ItemHighlight();

            }

            if (collision.gameObject.GetComponent<WorkstationDestination>())
            {
                PlayerNearWorkstation(collision.gameObject);
            }

            if (collision.gameObject.GetComponent<Inventory>())
            {
                nearInventory = collision.gameObject.GetComponent<Inventory>();
            }

            if (collision.gameObject.GetComponent<Interactable>() && collision.gameObject.GetComponent<Interactable>().enabled)
            {
                if (!_nearbyinteractables.Contains(collision.gameObject.GetComponent<Interactable>()))
                {
                    _nearbyinteractables.Add(collision.gameObject.GetComponent<Interactable>());
                    PlayerNearInteractable(collision.gameObject);
                }
            }

            if (collision.gameObject.GetComponent<InteractableText>() && collision.gameObject.GetComponent<InteractableText>().enabled)
            {
                //if (!_nearbyinteractables.Contains(collision.gameObject.GetComponent<InteractableText>()))
                {
                    //_nearbyinteractables.Add(collision.gameObject.GetComponent<InteractableText>());
                    //PlayerNearInteractableText(collision.gameObject);
                }
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>())
        {
            Item collidedItem = collision.gameObject.GetComponent<Item>();
            _nearbyItems.Remove(collidedItem);

            DisableHelpTextPickup();

            collidedItem.ItemUnHighlight();
        }

        if (collision.gameObject.GetComponent<WorkstationDestination>())
        {
            if (nearWorkstation != null)
            {
                
                PlayerLeaveWorkstation(collision.gameObject);

                nearWorkstation = null;
            }


        }

        if (collision.gameObject.GetComponent<Inventory>())
        {
            if (nearInventory != null)
            {
                nearInventory = null;
            }
        }

        if (collision.gameObject.GetComponent<Interactable>())
        {
            _nearbyinteractables.Remove(collision.gameObject.GetComponent<Interactable>());
            PlayerLeaveInteractable(collision.gameObject);
            DisableHelpTextPickup();
        }

        if (collision.gameObject.GetComponent<InteractableText>())
        {
            //PlayerLeaveInteractable(collision.gameObject);
            //DisableHelpTextPickup();
        }


    }

    public void PlayerNearWorkstation(GameObject go)
    {
        nearWorkstation = go.GetComponent<WorkstationDestination>();
        nearWorkstation.WorkStationHighlight();
    }

    public void PlayerLeaveWorkstation(GameObject go)
    {
        nearWorkstation = go.GetComponent<WorkstationDestination>();
        nearWorkstation.WorkstationUnHighlight();
    }

    public void PlayerNearInteractable(GameObject go)
    {
        nearInteractable = go.GetComponent<Interactable>();
        go.GetComponent<Interactable>().Highlight();
    }

    public void PlayerNearInteractableText(GameObject go)
    {
        InteractableText nearInteractableText = go.GetComponent<InteractableText>();
        nearInteractableText.Highlight();
    }

    public void PlayerLeaveInteractableText(GameObject go)
    {
        InteractableText nearInteractableText = go.GetComponent<InteractableText>();
        go.GetComponent<Interactable>().UnHighlight();
    }

    public void PlayerLeaveInteractable(GameObject go)
    {
        nearInteractable = null;
        go.GetComponent<Interactable>().UnHighlight();
    }

    private Item GetNearestItem()
    {
        //I do this here because this method should never be called if there are no items nearby
        Item nearestItem = null;
        if (_nearbyItems.Count > 0)
        {
             nearestItem = _nearbyItems[0];
            float smallestDist = Vector2.Distance(transform.position, _nearbyItems[0].gameObject.transform.position);


            //we only really need to do this if there is more than one item
            foreach (Item i in _nearbyItems)
            {
                float currentDist = Vector2.Distance(transform.position, i.gameObject.transform.position);
                if (currentDist < smallestDist)
                {
                    smallestDist = currentDist;
                    nearestItem = i;
                    //nearestItem.ItemHighlight();
                }
            }
        }
        audioPickup.pitch = Random.Range(minPitch, maxPitch);
        audioPickup.Play();
        return nearestItem;
    }


    private bool removecloseItem(Item item)
    {

        float currentDist = Vector2.Distance(transform.position, item.gameObject.transform.position);
        if (currentDist > 1)
        {
            _nearbyItems.Clear();
            return false;
        }

        return true;

    }


   public void ActivateItem(InputAction.CallbackContext context)
   {

        if (_currentItem != null && context.performed)
        {
            _currentItem.Activate();
        }
   }

    public void ActivateInteractable(InputAction.CallbackContext context)
    {
        if(_nearbyinteractables.Count > 0)
        {
            Interactable nearestInteractable = GetNearestInteractable();

            if (nearestInteractable != null && context.performed)
            {
                nearestInteractable.Activate(context);
            }
        }
    }

    public void UpdateHelpTextPickup(Item item)
    {
        //if (_player.helpText != null)
            _player.helpText.UpdateTextPickup(item.pickupString);
    }

    public void DisableHelpTextPickup()
    {
        if (_player.helpText != null)
            _player.helpText.DisableText();
    }

    public void UpdateHelpTextUse(Item item)
    {
        if (_player.helpText != null)
            _player.helpText.UpdateTextUse(item.useString);
        //FadeOutTextAfterTime();
    }

    public void DisableHelpTextUse()
    {
        if (_player.helpText != null)
            _player.helpText.DisableText();
    }

    public void FadeOutTextAfterTime()
    {
        if (_player.helpText != null)
            Invoke("DisableHelpTextUse", 1.2f);
    }

}
