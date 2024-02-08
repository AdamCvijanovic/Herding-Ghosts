using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableGrindstone : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler

{
    GrindstoneWorkbenchUI _grindstoneWorkbenchUI;

    public float dragSpeed = 2f;

    // This event echoes the new local angle to which we have been dragged
    //public event Action<Quaternion> OnAngleChanged = null;

    public Quaternion currentDragAngle;
    public Quaternion angleChange;
    public Quaternion dragStartRotation;
    public Quaternion dragStartInverseRotation;


    private void Awake()
    {
        // As an example: rotate the attached object
        //OnAngleChanged += (rotation) => transform.localRotation = rotation;
        _grindstoneWorkbenchUI = GetComponentInParent<GrindstoneWorkbenchUI>();
    }

    private void Update()
    {
        //this.transform.Rotate(new Vector3(0, 0, 1), Mathf.Lerp(this.transform.rotation.eulerAngles.z, angleChange.eulerAngles.z, dragSpeed * Time.deltaTime));

    }

    // This detects the starting point of the drag more accurately than OnBeginDrag,
    // because OnBeginDrag won't fire until the mouse has moved from the point of mousedown
    public void OnPointerDown(PointerEventData eventData)
    {
        dragStartRotation = transform.localRotation;
        Vector3 worldPoint;
        if (DragWorldPoint(eventData, out worldPoint))
        {
            // We use Vector3.forward as the "up" vector because we assume we're working in a Canvas
            // and so mostly care about rotation around the Z axis
            dragStartInverseRotation = Quaternion.Inverse(Quaternion.LookRotation(worldPoint - transform.position, Vector3.forward));
        }
        else
        {
            Debug.LogWarning("Couldn't get drag start world point");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Do nothing (but this has to exist or OnDrag won't work)
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Do nothing (but this has to exist or OnDrag won't work)
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 worldPoint;
        if (DragWorldPoint(eventData, out worldPoint))
        {
            currentDragAngle = Quaternion.LookRotation(worldPoint - transform.position, Vector3.forward);
            //if (OnAngleChanged != null)
            {
                angleChange = (currentDragAngle * dragStartInverseRotation * dragStartRotation);
                //this.transform.Rotate(new Vector3(0, 0, 1), Mathf.Lerp(this.transform.rotation.eulerAngles.z, angleChange.eulerAngles.z, dragSpeed * Time.deltaTime));
                this.transform.Rotate(Vector3.forward, dragSpeed);

                if(_grindstoneWorkbenchUI != null)
                {
                    _grindstoneWorkbenchUI.ProgressGrindSlider();
                }
            }
        }
    }


    // Gets the point in worldspace corresponding to where the mouse is
    private bool DragWorldPoint(PointerEventData eventData, out Vector3 worldPoint)
    {
        return RectTransformUtility.ScreenPointToWorldPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out worldPoint);
    }

}
