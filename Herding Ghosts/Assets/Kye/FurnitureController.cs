using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FurnitureController : MonoBehaviour
{

    private FurnitureManager furnitureManager;
    
    public InputAction clickAction;

    public InputAction rotateAction;

    public InputAction placeAction;

    public InputAction removeAction;

    public bool selected = false;

    private bool m_putDownProtection = false;

    public int layer = 0;

    private void OnEnable()
    {
        furnitureManager = GameObject.FindGameObjectWithTag("FurnitureManager").GetComponent<FurnitureManager>();
     

        clickAction.Enable();
        clickAction.performed += ClickFurniture;
        GetComponent<YSorter>().modifier += layer;

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
        if (!furnitureManager.holdingObject)
        {
            furnitureManager.holdingObject = true;
            GetComponent<Interactable>().enabled = false;
            GetComponent<BoxCollider2D>().isTrigger = true;

            if (!selected)
                furnitureManager.CreateFurnitureInScene(gameObject);

            selected = true;



            rotateAction.Enable();
            rotateAction.performed += RotateFurniture;

            placeAction.Enable();
            placeAction.performed += PlaceFurniture;

            removeAction.Enable();
            removeAction.performed += RemoveFurniture;

            var player = GameObject.FindGameObjectWithTag("Player").transform;

            transform.parent = player.GetChild(0);
            transform.position = new Vector3(player.GetChild(0).transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x, player.GetChild(0).transform.position.y, player.GetChild(0).transform.position.z);



            furnitureManager.ClearFurniture();
            clickAction.performed -= ClickFurniture;
        }
    }

   private void RotateFurniture(InputAction.CallbackContext context)
    {
        gameObject.transform.Rotate(0, 0, 90);

    }

    private void PlaceFurniture(InputAction.CallbackContext context)
    {
        if (!m_putDownProtection)
        {
            transform.parent = furnitureManager.presets[furnitureManager.currentPreset].transform;
            rotateAction.Disable();
            rotateAction.performed -= RotateFurniture;

            placeAction.Disable();
            placeAction.performed -= PlaceFurniture;

            removeAction.Disable();
            placeAction.performed -= RemoveFurniture;

            GetComponent<BoxCollider2D>().isTrigger = false;

            GetComponent<Interactable>().enabled = true;

            m_putDownProtection = true;
            furnitureManager.holdingObject = false;
        }

        else
            m_putDownProtection = false;
    }


    public void RemoveFurniture(InputAction.CallbackContext context)
    {
        rotateAction.Disable();
        rotateAction.performed -= RotateFurniture;

        placeAction.Disable();
        placeAction.performed -= PlaceFurniture;

        removeAction.Disable();
        placeAction.performed -= RemoveFurniture;

        furnitureManager.RemoveFurnitureInScene(gameObject);
        Destroy(gameObject);

    }
}
