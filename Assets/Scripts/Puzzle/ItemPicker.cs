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
            Debug.Log("Mouse down");
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickableItem")))
            {
                _pickedItem = hit.transform.GetComponent<PickableItem>();
                if (_pickedItem != null)
                {
                    _pickedItem.OnPicked();
                }
            }
        }

        if (Input.GetMouseButtonUp(0) && _pickedItem != null)
        {
            Debug.Log("Mouse up");
            _pickedItem.OnReleased();
            _pickedItem = null;
        }

        if (_pickedItem != null && (!Mathf.Approximately(Input.GetAxis("Mouse X"), 0f) || !Mathf.Approximately(Input.GetAxis("Mouse Y"), 0f)))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, LayerMask.GetMask("PickedItemsDragPlane")))
            {
                _pickedItem.transform.position = hit.point;
            }
        }
    }
}