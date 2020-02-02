using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _charController;

    [SerializeField] private Transform _cameraTform;
    [SerializeField] private Vector3 _cameraOffset;
        
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rotatingSpeed = 1f;
    
    private Transform _transform;    
    
    private void Awake()
    {
        _transform = transform;
        _charController = GetComponent<CharacterController>();       
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;        
    }

    private void OnDisable()
    {        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void Update()
    {
        _charController.Move(_speed * _transform.forward * Input.GetAxis("Vertical") * Time.deltaTime);
        _charController.Move(_speed * _transform.right * Input.GetAxis("Horizontal") * Time.deltaTime);              

        _transform.rotation = Quaternion.Euler(_transform.rotation.eulerAngles.x, _transform.rotation.eulerAngles.y + Input.GetAxis("Mouse X") * _rotatingSpeed, transform.rotation.eulerAngles.z);                
    }
    
    private void LateUpdate()
    {                
        _cameraTform.position = _transform.position + _cameraOffset;
        var rotationY = _cameraTform.rotation.eulerAngles.x - Input.GetAxis("Mouse Y") * _rotatingSpeed;
        rotationY = rotationY > 280 || rotationY < 80 ? rotationY : (rotationY > 180 ? 280 : 80);
        _cameraTform.rotation = Quaternion.Euler(rotationY, 
            _transform.rotation.eulerAngles.y, _transform.rotation.eulerAngles.z);
    }
}
