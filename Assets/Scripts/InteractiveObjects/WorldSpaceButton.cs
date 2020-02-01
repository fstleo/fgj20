using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceButton : MonoBehaviour, IInteractive
{
    
    [SerializeField]
    private string _annotation;

    public bool CanInteract { get; set; }
    public string Annotation => _annotation;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        _animator.SetTrigger("Pressed");
        StopInteraction();
    }

    public void StopInteraction()
    {
        OnStopInteraction?.Invoke();
    }

    public event Action OnStopInteraction;
}
