using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FurnitureController : MonoBehaviour
{
    public InputAction clickAction;

    public InputAction rotateAction;

    public InputAction placeAction;
    
    public bool selected = false;

    private bool m_putDownProtection = false;

    private void OnEnable()
    {
        clickAction.Enable();
        clickAction.performed += ClickFurniture;
    }

    private void OnDisable()
    {
        clickAction.Disable();
        clickAction.performed -= ClickFurniture;
        rotateAction.Disable();
    }

    private void Update()
    {
    }

    private void ClickFurniture(InputAction.CallbackContext context)
    {
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(mousePos), Vector2.zero);

        if (hit.collider != null && hit.collider.transform == transform)
        {
            PickUpFurniture();
        }
    }

    public void PickUpFurniture()
    {
        GetComponent<Interactable>().enabled = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        selected = true;
        rotateAction.Enable();
        rotateAction.performed += RotateFurniture;

        placeAction.Enable();
        placeAction.performed += PlaceFurniture;

        var player = GameObject.FindGameObjectWithTag("Player").transform;

        transform.parent = player.GetChild(0);
        transform.position = new Vector3(player.GetChild(0).transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x, player.GetChild(0).transform.position.y, player.GetChild(0).transform.position.z);



        GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<FurnitureManager>().ClearFurniture();
        clickAction.performed -= ClickFurniture;
    }

   private void RotateFurniture(InputAction.CallbackContext context)
    {
        gameObject.transform.Rotate(0, 0, 90);

    }

    private void PlaceFurniture(InputAction.CallbackContext context)
    {
        if (!m_putDownProtection)
        {
            transform.parent = null;
            rotateAction.Disable();
            rotateAction.performed -= RotateFurniture;

            placeAction.Disable();
            placeAction.performed -= PlaceFurniture;

            GetComponent<BoxCollider2D>().isTrigger = false;

            GetComponent<Interactable>().enabled = true;

            m_putDownProtection = true;
        }

        else
            m_putDownProtection = false;
    }
}
