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
                PickableItem pickedItem = hit.transform.parent.GetComponent<PickableItem>();
                if (!CheckCoveringItems(pickedItem))
                {
                    _pickedItem = pickedItem;
                    if (_pickedItem != null)
                    {
                        _pickedItem.OnPicked();
                        _pickedItem.SetPositionAnimated(GetPosOnPickedPlane(_pickedItem));
                        if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.OnInitialPosition
                            || _pickedItem.initialPositionState == PickableItem.InitialPositionState.MovingToInitialPosition)
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
            bool interactedWithGhost = false;
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickableItemGhost")))
            {
                PickableItemGhost ghost = hit.transform.parent.GetComponent<PickableItemGhost>();
                if (!CheckCoveringItems(ghost.transform.GetComponent<PickableItem>()))
                {
                    if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.CanBeReturnedToInitialPosition)
                    {
                        _pickedItem.SetPositionAnimated(ghost.transform.position, pickedItem =>
                        {
                            pickedItem.initialPositionState = PickableItem.InitialPositionState.OnInitialPosition;
                            pickedItem.isBroken = false;
                        });
                        _pickedItem.initialPositionState = PickableItem.InitialPositionState.MovingToInitialPosition;
                    }
                    else if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.PickedFromInitialPosition)
                    {
                        _pickedItem.SetPosition(GetPosOnPickedPlane(_pickedItem));
                    }

                    interactedWithGhost = true;
                }
            }

            if (!interactedWithGhost)
            {
                if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.PickedFromInitialPosition)
                {
                    _pickedItem.SetPositionAnimated(GetPosOnPickedPlane(_pickedItem));
                    _pickedItem.initialPositionState = PickableItem.InitialPositionState.CanBeReturnedToInitialPosition;
                }
                else
                {
                    _pickedItem.SetPosition(GetPosOnPickedPlane(_pickedItem));
                    if (_pickedItem.initialPositionState == PickableItem.InitialPositionState.OnInitialPosition
                        || _pickedItem.initialPositionState == PickableItem.InitialPositionState.MovingToInitialPosition)
                    {
                        _pickedItem.initialPositionState = PickableItem.InitialPositionState.CanBeReturnedToInitialPosition;
                    }
                }
            }
        }
    }

    private Vector3 GetPosOnPickedPlane(PickableItem item)
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickedItemsDragPlane")))
        {
            float xOffset = item.collider.transform.position.x - item.transform.position.x;
            float zOffset = item.collider.transform.position.z - item.transform.position.z;
            return hit.point - Vector3.right * xOffset - Vector3.forward * zOffset;
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
                PickableItem coveringItem = hit.transform.parent.GetComponent<PickableItem>();
                if (coveringItem.initialPositionState == PickableItem.InitialPositionState.OnInitialPosition)
                {
                    return true;
                }
            }
        }

        return false;
    }
}