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

    private void Awake()
    {        
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        _animator.SetBool("Opened", true);
        _itemPicker = gameObject.AddComponent<ItemPicker>();             
        SoundPlayer.Play("door_open");
    }

    public void StopInteraction()
    {
        SoundPlayer.Play("door_open");
        _animator.SetBool("Opened", false);
        OnStopInteraction?.Invoke();        
        Destroy(_itemPicker);
    }

    public event Action OnStopInteraction;

}
