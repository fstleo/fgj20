using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryCameraMover : MonoBehaviour
{
    private Transform _transform;

    private Vector3 _startPos;
    private Quaternion _startRotation;
    private Transform _targetTransform;

    private float _timer = 0.5f;
    [SerializeField]
    private float _lookTime = 0.5f;
    
    private void Awake()
    {
        _transform = transform;
    }

    public void SetTarget(Transform target)
    {
        _startPos = _transform.position;
        _startRotation = _transform.rotation;
        _timer = _lookTime;
        _targetTransform = target;
    }

    private void LateUpdate()
    {
        _transform.position = Vector3.Lerp(_startPos, _targetTransform.position, 1 - _timer / _lookTime);
        _transform.rotation = Quaternion.Lerp(_startRotation, _targetTransform.rotation, 1 - _timer / _lookTime);        
        _timer -= Time.deltaTime;
        
        if (Input.GetMouseButton(1))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            if ((_targetTransform.localEulerAngles.x > 330 && y < 0) ||
                (_targetTransform.localEulerAngles.x > 180 && y > 0) ||
                (_targetTransform.localEulerAngles.x < 180 && y < 0) || 
                (_targetTransform.localEulerAngles.x < 30 && y > 0))
            {
                _targetTransform.RotateAround(_targetTransform.position + _targetTransform.forward, _targetTransform.right, y);
            }

            if ((_targetTransform.localEulerAngles.y > 315 && x < 0) ||
                (_targetTransform.localEulerAngles.y > 180 && x > 0) ||
                (_targetTransform.localEulerAngles.y < 180 && x < 0) ||
                (_targetTransform.localEulerAngles.y < 45 && x > 0))
            {
                _targetTransform.RotateAround(_targetTransform.position + _targetTransform.forward, Vector3.up, x);
            }

        }
    }
}
