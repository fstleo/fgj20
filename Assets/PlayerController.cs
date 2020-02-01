using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _charController;

    [SerializeField] private Transform _cameraTform;
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _rotatingSpeed = 1f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _charController.Move(_speed * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.fixedDeltaTime);
        _charController.transform.Rotate(Vector3.up);
    }
}
