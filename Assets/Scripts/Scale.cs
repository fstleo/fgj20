using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    [SerializeField]
    private float _duration;
    
    [SerializeField]
    private float _startScale;
    
    [SerializeField]
    private float _finalScale;

    private float _timer;

    private Vector3 _finalScaleVector;
    private Vector3 _startScaleVector;
    

    private Transform _transform;
    
    void Awake()
    {
        _transform = transform;
        _startScaleVector = new Vector3(_startScale, _startScale,_startScale);
        _finalScaleVector = new Vector3(_finalScale, _finalScale, _finalScale);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _transform.localScale = Vector3.Lerp(_startScaleVector, _finalScaleVector, _timer/_duration);
    }
}
