using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlace : MonoBehaviour//, IInteractive
{
    public event Action OnFix;
    
//    [SerializeField]
//    private GameObject _prefab;

    [SerializeField] private GameObject _indicator;
    
    void Awake()
    {
        _indicator.SetActive(false);

    }

    public void Break()
    {
         _indicator.SetActive(true);
    }

    public void Fix()
    {
        _indicator.SetActive(false);
        OnFix?.Invoke();
        GetComponentInChildren<IInteractive>().StopInteraction();        
    }

    private void OnMouseDown()
    {
        Fix();
    }

//    public string Annotation => "Fix";
//    
//    public void Interact()
//    {        
//        StopInteraction();
//    }
//
//    public void StopInteraction()
//    {
//        OnStopInteraction?.Invoke();
//    }
//
//    public event Action OnStopInteraction;
}
