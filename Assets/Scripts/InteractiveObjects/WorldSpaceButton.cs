using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceButton : MonoBehaviour, IInteractive
{
    
    [SerializeField]
    private string _annotation;

    public string Annotation => _annotation;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void Interact()
    {
        _animator.SetInteger("Pressed", 1);    
    }           
}
