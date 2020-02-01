using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    private Light _light;
    
    private float _timer;
    [SerializeField]
    private float _blinkTime;
    
    private void Awake()
    {
        _light = GetComponent<Light>();       
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            _timer = _blinkTime;
            _light.enabled = !_light.enabled;
        }
    }
}
