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
                _pickedItem = hit.transform.GetComponent<PickableItem>();
                if (_pickedItem != null)
                {
                    _pickedItem.OnPicked();
                    _pickedItem.SetPositionAnimated(GetPosOnPickedPlane());
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
                _pickedItem.SetPositionAnimated(ghost.transform.position);
            }
            else
            {
                _pickedItem.SetPosition(GetPosOnPickedPlane());                
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
}