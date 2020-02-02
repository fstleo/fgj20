using System;
using UnityEngine;

public class RotateStuff : MonoBehaviour
{
    [SerializeField]
    private float _speed = 30;

    [SerializeField]
    private Vector3 _axis = Vector3.up;

    [SerializeField]
    private Transform _targetTransform;

    private void Awake()
    {
        if (_targetTransform == null)
        {
            _targetTransform = transform;
        }
    }

    private void Update()
    {
        _targetTransform.Rotate(_axis, _speed * Time.deltaTime);
    }
}