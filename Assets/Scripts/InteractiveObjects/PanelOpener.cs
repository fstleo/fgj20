using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelOpener : MonoBehaviour, IInteractive
{
    public bool CanInteract { get; set; }
    public Transform CameraPosition => _cameraPlace;
    public string Annotation => "Open";
       
    [SerializeField]
    private Transform _cameraPlace;
    
    private Animator _animator;

    private ItemPicker _itemPicker;

    [SerializeField] private GameObject _effect;

    private void Awake()
    {        
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        _animator.SetBool("Opened", true);
        _itemPicker = gameObject.AddComponent<ItemPicker>();      
        _effect.SetActive(false);
    }

    public void StopInteraction()
    {
        _animator.SetBool("Opened", false);
        OnStopInteraction?.Invoke();        
        Destroy(_itemPicker);
    }

    public event Action OnStopInteraction;

}
