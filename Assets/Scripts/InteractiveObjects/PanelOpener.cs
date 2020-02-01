using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour, IInteractive
{
    public string Annotation => "Open";
       
    private Transform _cameraTform;

    [SerializeField]
    private Transform _cameraPlace;
    
    [SerializeField] 
    private float _cameraAnimationTime = 0.5f;
    
    private Animator _animator;

    private ItemPicker _itemPicker;

    private void Awake()
    {
        _cameraTform = Camera.main.transform;
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        _animator.SetBool("Opened", true);
        StartCoroutine(PutCameraBefore());
        _itemPicker = gameObject.AddComponent<ItemPicker>();
        _cameraTform.GetComponent<CameraShakes>().Fixed = true;
    }

    public void StopInteraction()
    {
        _animator.SetBool("Opened", false);
        OnStopInteraction?.Invoke();
        _cameraTform.GetComponent<CameraShakes>().Fixed = false;
        Destroy(_itemPicker);
    }

    public event Action OnStopInteraction;

    private IEnumerator PutCameraBefore()
    {
        Vector3 cameraStartPos = _cameraTform.position;
        float timer = _cameraAnimationTime;
        while (timer > 0)
        {
            _cameraTform.position = Vector3.Lerp(cameraStartPos, _cameraPlace.position,
                1 - timer / _cameraAnimationTime);
            _cameraTform.LookAt(transform);
            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
