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

    private Vector3 _direction;
    // Start is called before the first frame update
    void Start()
    {
        _direction = Random.onUnitSphere;
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_to == null)
        {
            transform.position +=  _direction * 5 * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(Random.Range(0,5f),Random.Range(0,5f),Random.Range(0,5f));
        }
        else
        {
            transform.position = Vector3.Lerp(_startPosition, _to.position, _timer / _time);
            _timer += Time.deltaTime;
        }
    }
}
