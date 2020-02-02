using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleStuff : MonoBehaviour
{

    private float _startScale;
    private Vector3 _startPosition;

    private Transform _transform;
    
    void Awake()
    {
        _transform = transform;
        _startScale = _transform.localScale.x;
        _startPosition = _transform.position;
    }
   
    private void Update()
    {
        float newScale = _startScale + Mathf.Sin(Time.time) / 20f;
        _transform.localScale = new Vector3(newScale, newScale, newScale);
        _transform.position =  _startPosition + Vector3.up * Mathf.Sin(Time.time) / 50f;
    }
}
