using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyForward : MonoBehaviour
{
    [SerializeField]
    private Transform _to;

    private Vector3 _startPosition;
    [SerializeField]
    private float _time;
    
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(_startPosition, _to.position, _timer / _time);
        _timer += Time.deltaTime;
    }
}
