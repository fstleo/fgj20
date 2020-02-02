using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinEffectEgg : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _normal;
    
    [SerializeField]
    private GameObject _egg;
    
    
    private bool _enabled = false;
    private float _timer = 10f;
    
    // Update is called once per frame
    void Update()
    {
        if (Time.time < 30f)
            return;
        if (Random.Range(0, 1f) > 0.9999f)
        {
            _enabled = true;
            _egg.SetActive(true);
            _normal.SetActive(false);
        }

        if (_enabled)
        {
            _timer -= Time.deltaTime;
            if (_timer < 0)
            {
                _egg.SetActive(false);
                _normal.SetActive(true);
                _timer = 10f;
                _enabled = false;
            }                
        }
        
    }
}
