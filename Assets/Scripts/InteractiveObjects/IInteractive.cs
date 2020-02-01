using System;

public interface IInteractive
{
    bool CanInteract { get; set; }
    
    string Annotation { get; }
    
    void Interact();
    void StopInteraction();

    event Action OnStopInteraction;
}
