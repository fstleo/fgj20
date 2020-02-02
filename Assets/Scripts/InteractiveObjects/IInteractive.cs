using System;
using UnityEngine;

public interface IInteractive
{
    bool CanInteract { get; set; }
    Transform CameraPosition { get; }
    
    string Annotation { get; }
    
    void Interact();
    void StopInteraction();

    event Action OnStopInteraction;
}
