using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RollingPinUI : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler*/
{
    public DoughUI doughObj;
    public Scrollbar scrollbar;

    public Vector3 rollingPinPosition = new Vector3();

    public GameObject upperCol;
    public GameObject middleCol;
    public GameObject lowerCol;

    public bool upper;
    public bool middle;
    public bool lower;

    // Start is called before the first frame update
    void Start()
    {
        rollingPinPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetRollingPin()
    {
        ResetColBools();
        scrollbar.value = 0;
    }

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    //Debug.Log("START DRAG ROLLING PIN");
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    //UpdateRollingPinPosition(eventData);
    //}

    public void UpdateRollingPinPosition(PointerEventData eventData)
    {
        rollingPinPosition.y = eventData.position.y;

        transform.position = rollingPinPosition;
    }

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("END DRAG ROLLING PIN");
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION ENTERED " + collision.gameObject.name);

        if(collision.gameObject == upperCol)
        {
            upper = true;
        }

        if (collision.gameObject == middleCol)
        {
            middle = true;
        }

        if (collision.gameObject == lowerCol)
        {
            lower = true;
        }

        if(upper && middle && lower)
        {
            UpdateDough();
        }

    }

    public void UpdateDough()
    {
        doughObj.UpdateDoughFlatness();
        ResetColBools();
    }

    

    public void ResetColBools()
    {
        upper = false;
        middle = false;
        lower = false;
    }

}
