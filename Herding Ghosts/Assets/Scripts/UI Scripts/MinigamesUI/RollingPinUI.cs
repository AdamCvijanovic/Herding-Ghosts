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

    public int flatness;

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
        //doughObj.UpdateDoughFlatness();
        CalculateDoughFlatness();
        ResetColBools();
    }

    public void CalculateDoughFlatness()
    {
        flatness = doughObj.flatnessLevel;
        Sprite newSprite = doughObj.doughDefault;

        if (flatness < 2)
        {
            flatness++;
        }
        else
        {
            //flatnessLevel = 0;
        }

        switch (flatness)
        {
            case 0:
                newSprite = doughObj.doughDefault;
                break;
            case 1:
                newSprite = doughObj.doughHalfFlat;
                break;
            case 2:
                newSprite = doughObj.doughFlat;
                break;

        }

        doughObj.UpdateDoughFlatness(newSprite, flatness);
    }

    public void ResetColBools()
    {
        upper = false;
        middle = false;
        lower = false;
    }

}
