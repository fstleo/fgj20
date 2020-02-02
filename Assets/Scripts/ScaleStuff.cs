using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleStuff : MonoBehaviour
{
    private float _startScale;

    private void Awake()
    {
        _startScale = transform.localScale.x;
    }
    
    private void Update()
    {
        float fx = _startScale + Mathf.Sin(Time.time)/20f;
        transform.localScale = new Vector3(fx, fx, fx);
    }
}
