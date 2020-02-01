using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    private Camera _camera;
    private PickableItem _pickedItem;

    private void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickableItem")))
            {
                PickableItem pickedItem = hit.transform.GetComponent<PickableItem>();
                if (!CheckCoveringItems(pickedItem))
                {
                    _pickedItem = pickedItem;
                    if (_pickedItem != null)
                    {
                        _pickedItem.OnPicked();
                        _pickedItem.SetPositionAnimated(GetPosOnPickedPlane());
                        if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.OnInitialPosition)
                        {
                            _pickedItem.initialPositionState = PickableItem.InitialPositionState.PickedFromInitialPosition;
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && _pickedItem != null)
        {
            _pickedItem.OnReleased();
            _pickedItem = null;
        }

        if (_pickedItem != null && (!Mathf.Approximately(Input.GetAxis("Mouse X"), 0f) || !Mathf.Approximately(Input.GetAxis("Mouse Y"), 0f)))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickableItemGhost")))
            {
                PickableItemGhost ghost = hit.transform.GetComponent<PickableItemGhost>();
                if (CheckCoveringItems(ghost.GetComponent<PickableItem>()))
                {
                    if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.PickedFromInitialPosition)
                    {
                        _pickedItem.SetPositionAnimated(GetPosOnPickedPlane());
                        _pickedItem.initialPositionState = PickableItem.InitialPositionState.CanBeReturnedToInitialPosition;
                    }
                    else
                    {
                        _pickedItem.SetPosition(GetPosOnPickedPlane());
                    }
                }
                else
                {
                    if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.CanBeReturnedToInitialPosition)
                    {
                        _pickedItem.SetPositionAnimated(ghost.transform.position,
                            pickedItem => pickedItem.initialPositionState = PickableItem.InitialPositionState.OnInitialPosition);
                    }
                    else if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.OnInitialPosition)
                    {
                        _pickedItem.SetPositionAnimated(GetPosOnPickedPlane());
                        _pickedItem.initialPositionState = PickableItem.InitialPositionState.PickedFromInitialPosition;
                    }
                    else if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.PickedFromInitialPosition)
                    {
                        _pickedItem.SetPosition(GetPosOnPickedPlane());
                    }
                }
            }
            else
            {
                if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.PickedFromInitialPosition)
                {
                    _pickedItem.SetPositionAnimated(GetPosOnPickedPlane());
                    _pickedItem.initialPositionState = PickableItem.InitialPositionState.CanBeReturnedToInitialPosition;
                }
                else
                {
                    _pickedItem.SetPosition(GetPosOnPickedPlane());
                }
            }
        }
    }

    private Vector3 GetPosOnPickedPlane()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickedItemsDragPlane")))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    private bool CheckCoveringItems(PickableItem item)
    {
        foreach (Transform point in item.sectionPoints)
        {
            Ray ray = new Ray(point.position, item.transform.up);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickableItem")))
            {
                PickableItem coveringItem = hit.transform.GetComponent<PickableItem>();
                if (coveringItem.initialPositionState == PickableItem.InitialPositionState.OnInitialPosition)
                {
                    return true;
                }
            }
        }

        return false;
    }
}