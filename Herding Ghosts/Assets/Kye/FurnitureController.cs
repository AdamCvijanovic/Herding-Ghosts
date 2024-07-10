using Cinemachine;
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

    public InputAction movement;


    public bool selected = false;

    private bool m_putDownProtection = false;

    public int layer = 0;

    public int tracker;

    public Sprite[] rotationSprites;

    public int rotationTracker = 0;


    public bool ghostUse = false;

    public bool inUse = false;

    private bool startLineRenderer = false;

    private bool preventPlace = false;

    private bool isBeingHeld = false;

    private Vector2 currentDirection = Vector2.zero;
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
        if (startLineRenderer)
        {
            //SetupLineRenderer();
            DisplayCollider();

        }
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
            isBeingHeld = true;

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
            transform.parent = null;


            player.GetChild(0).gameObject.GetComponent<PlayerInput>().enabled = false;
            player.GetChild(1).GetComponent<CinemachineVirtualCamera>().Follow = transform;

            movement.Enable();
            movement.performed += MovedObject;
            SetupLineRenderer();

            GetComponent<Interactable>().UnHighlight();
            GetComponent<Interactable>().enabled = false;


            furnitureManager.ClearFurniture();
            clickAction.performed -= ClickFurniture;
        }
    }

    public void MovedObject(InputAction.CallbackContext context)
    {
        var buttonPress = context.ReadValue<Vector2>();
        currentDirection = buttonPress * -1;
        var combo = (buttonPress);


        switch (combo)
        {
            case Vector2 when combo.Equals(Vector2.up):
                transform.position += Vector3.up / 4;
                break;
            case Vector2 when combo.Equals(Vector2.down):
                transform.position += Vector3.down / 4;
                break;
            case Vector2 when combo.Equals(Vector2.left):
                transform.position += Vector3.left / 4;
                break;

            case Vector2 when combo.Equals(Vector2.right):
                transform.position += Vector3.right / 4;
                break;


        }
    }

        public void ManualRotate(int direction)
    {

            switch (direction)
            {
                case 0:
                    this.GetComponent<SpriteRenderer>().sprite = rotationSprites[0];
                    break;
                case 90:
                    this.GetComponent<SpriteRenderer>().sprite = rotationSprites[1];
                    break;
                case 180:
                    this.GetComponent<SpriteRenderer>().sprite = rotationSprites[2];
                    break;
                case 270:
                    this.GetComponent<SpriteRenderer>().sprite = rotationSprites[3];
                    break;
                default:
                    this.GetComponent<SpriteRenderer>().sprite = rotationSprites[0];
                    break;
            }

            rotationTracker = direction;
    }

   private void RotateFurniture(InputAction.CallbackContext context)
    {
        var col = GetComponent<BoxCollider2D>();

        if (col != null)
        {
            col.size = new Vector2(col.size.y, col.size.x);
            col.size = new Vector2(Mathf.Ceil(col.size.x * 2) / 2.0f, Mathf.Ceil(col.size.y * 2) / 2.0f);
        }

        if ((rotationSprites.Length == 2 || rotationSprites.Length == 4) && rotationTracker == 0)
        {
            rotationTracker = 90;
            this.GetComponent<SpriteRenderer>().sprite = rotationSprites[1];
        }

        else if (( rotationSprites.Length == 4) && rotationTracker == 90)
        {
            rotationTracker = 180;
            this.GetComponent<SpriteRenderer>().sprite = rotationSprites[2];
        }

        else if ((rotationSprites.Length == 4) && rotationTracker == 180)
        {
            rotationTracker = 270;
            this.GetComponent<SpriteRenderer>().sprite = rotationSprites[3];
        }

        else if ((rotationSprites.Length == 2 && rotationTracker == 90) || (rotationSprites.Length == 4 && rotationTracker == 270))
        {
            rotationTracker = 0;
            this.GetComponent<SpriteRenderer>().sprite = rotationSprites[0];
        }

    }

    public void PlaceFurniture(InputAction.CallbackContext context)
    {
        if (!m_putDownProtection && !preventPlace)
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

            if (startLineRenderer)
            {
                var player = GameObject.FindGameObjectWithTag("Player").transform;
                player.GetChild(0).gameObject.GetComponent<PlayerInput>().enabled = true;
                player.GetChild(1).GetComponent<CinemachineVirtualCamera>().Follow = player.GetChild(0);
                movement.Disable();
                movement.performed -= MovedObject;
                startLineRenderer = false;
                GetComponent<LineRenderer>().enabled = false;
            }

            m_putDownProtection = true;
            furnitureManager.holdingObject = false;
            isBeingHeld = false;
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

        if (startLineRenderer)
        {
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            player.GetChild(0).gameObject.GetComponent<PlayerMove>().enabled = true;
            player.GetChild(0).gameObject.GetComponent<PlayerInput>().enabled = true;
            player.GetChild(1).GetComponent<CinemachineVirtualCamera>().Follow = player.GetChild(0);
            movement.Disable();
            movement.performed -= MovedObject;
            startLineRenderer = false;
            GetComponent<LineRenderer>().enabled = false;
        }

        furnitureManager.RemoveFurnitureInScene(gameObject);
        Destroy(gameObject);

        furnitureManager.holdingObject = false;
    }


    void SetupLineRenderer()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        var lineRenderer = GetComponent<LineRenderer>();


        boxCollider.size = new Vector2(Mathf.Ceil(boxCollider.size.x * 2) / 2.0f, Mathf.Ceil(boxCollider.size.y * 2) / 2.0f);
        transform.position = new Vector3(Mathf.Ceil(transform.position.x * 2) / 2.0f, Mathf.Ceil(transform.position.y * 2) / 2.0f, 0);

        lineRenderer.enabled = true;
        lineRenderer.positionCount = 5;
        lineRenderer.loop = true;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.magenta;
        lineRenderer.endColor = Color.magenta;

        startLineRenderer = true;

       
       


    }

    void DisplayCollider()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        var lineRenderer = GetComponent<LineRenderer>();

        Vector2 size = boxCollider.size;
        Vector2 offset = boxCollider.offset;
        Vector2 position = transform.position;


        Vector3[] corners = new Vector3[5];
        corners[0] = new Vector3(position.x + offset.x - size.x / 2, position.y + offset.y - size.y / 2, 0); 
        corners[1] = new Vector3(position.x + offset.x + size.x / 2, position.y + offset.y - size.y / 2, 0); 
        corners[2] = new Vector3(position.x + offset.x + size.x / 2, position.y + offset.y + size.y / 2, 0); 
        corners[3] = new Vector3(position.x + offset.x - size.x / 2, position.y + offset.y + size.y / 2, 0); 
        corners[4] = corners[0];
      

        lineRenderer.SetPositions(corners);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (furnitureManager.holdingObject && isBeingHeld)
        {
            if(collision.gameObject.CompareTag("Perimeter"))
            {
                transform.position += new Vector3 (currentDirection.x, currentDirection.y, 0) / 2;

            }

            if (collision.gameObject.CompareTag("PlaceableFurniture") || collision.gameObject.CompareTag("Player"))
            {
                AntiPlacement(Color.red, true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (furnitureManager.holdingObject && isBeingHeld)
        {
            if (collision.gameObject.CompareTag("PlaceableFurniture") || collision.gameObject.CompareTag("Player"))
            {
                AntiPlacement(Color.red, true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (furnitureManager.holdingObject && isBeingHeld)
        {

                AntiPlacement(Color.magenta, false);

        }
    }


    private void AntiPlacement(Color color, bool enter)
    {
        preventPlace = enter;

        var lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;

        DisplayCollider();

    }
}
