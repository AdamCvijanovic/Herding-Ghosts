using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CauldronDestination : WorkstationDestination
{
    //Derive from Worstation Destination
    
    [SerializeField]
    private UnityEvent _OnHasItems;

    public UnityEvent OnHasItems
    {
        get
        {
            return this._OnHasItems;
        }
    }
    
    [SerializeField]
    private UnityEvent _onHasntItems;

    public UnityEvent OnHasntItems
    {
        get
        {
            return this._onHasntItems;
        }
    }

    [SerializeField]
    private UnityEvent _onPurge;

    public UnityEvent OnPurge
    {
        get
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Drain");
            return this._onPurge;
        }
    }

    [SerializeField]
    private UnityEvent _onSuccess;

    public UnityEvent OnSuccess
    {
        get
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Flush");
            return this._onSuccess;
        }
    }

    public enum ItemState
    {
        HasItems, HasntItems
    }

    private ItemState _currentState;

    public ItemState CurrentState
    {
        get
        {
            return _currentState;
        }
        private set
        {
            if (this._currentState == value)
            {
                return;
            }

            this._currentState = value;

            switch (this._currentState)
            {
                case ItemState.HasItems:

                    this.OnHasItems.Invoke();

                    break;

                case ItemState.HasntItems:

                    //this.OnHasntItems.Invoke();
                
                break;
            }
        }
    }

    public void Update()
    {
        AnimationCheck();
    }

    public void AnimationCheck()
    {
        if (ItemCheck())
        {
            CurrentState = ItemState.HasItems;
        }
        else
        {
            CurrentState = ItemState.HasntItems;
        }

    }

    public bool ItemCheck()
    {
        return _inventory._items.Count > 0;
    }

    public override void RecipeCook(RecipeObject recipeIn)
    {
        this.OnSuccess.Invoke();
        base.RecipeCook(recipeIn);
    }

    public void PurgeCauldron()
    {
        ClearInventory();
        this.OnPurge.Invoke();
    }

}
