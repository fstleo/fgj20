using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakes : MonoBehaviour
{

    private Transform _transform;
    
    [SerializeField]
    private float _shakeDuration = 0f;	
    
    [SerializeField]
    private float _shakeAmount = 0.7f;
    
    [SerializeField]
    private float _decreaseFactor = 1.0f;       
        	
    private void Awake()
    {
        _transform = transform;
    }
    
      public void Shake(float duration, float amount)
    {
        _shakeAmount = amount;
        _shakeDuration = duration;
        SoundPlayer.Play("explosion", amount * 4f);
        enabled = true;
    }
    
    private void LateUpdate()
    {

        if (_shakeDuration > 0)
        {
            _transform.localPosition = Random.insideUnitSphere * _shakeAmount;
			
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
        }
        else
        {
            _shakeDuration = 0f;
            enabled = false;
            _transform.localPosition = Vector3.zero;
        }
    }
}
