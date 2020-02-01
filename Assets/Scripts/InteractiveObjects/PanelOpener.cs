using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour, IInteractive
{
    public string Annotation => "Open";
       
    private Transform _cameraTform;

    [SerializeField]
    private Vector3 _cameraOffset;
    
    [SerializeField] 
    private float _cameraAnimationTime = 0.5f;
    
    private Animator _animator;

    private void Awake()
    {
        _cameraTform = Camera.main.transform;
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        _animator.SetBool("Opened", true);
        StartCoroutine(PutCameraBefore());
    }

    public void StopInteraction()
    {
        _animator.SetBool("Opened", false);
        OnStopInteraction?.Invoke();
    }

    public event Action OnStopInteraction;

    private IEnumerator PutCameraBefore()
    {
        Vector3 cameraStartPos = _cameraTform.position;
        float timer = _cameraAnimationTime;
        while (timer > 0)
        {
            _cameraTform.position = Vector3.Lerp(cameraStartPos, transform.position + transform.rotation * _cameraOffset,
                1 - timer / _cameraAnimationTime);
            _cameraTform.LookAt(transform);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
