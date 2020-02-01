using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Vector3 _diff;
    private Vector3 _lastMousePos;

    private Transform _cameraTform;
    
    void Awake()
    {
        _cameraTform = transform;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && !Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition)))
        {
            _diff = Input.mousePosition - _lastMousePos;
            _lastMousePos = Input.mousePosition;
        }
        else
        {
            _diff = Vector3.zero;           
        }
    }
   
    void LateUpdate()
    {
        _cameraTform.Rotate(Vector3.up, _diff.x);        
    }
}
