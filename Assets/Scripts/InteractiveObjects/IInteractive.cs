using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractive
{

    string Annotation { get; }
    
    void Interact();
    void StopInteraction();

    event Action OnStopInteraction;
}
