using System;

public interface IInteractive
{
    string Annotation { get; }
    
    void Interact();
    void StopInteraction();

    event Action OnStopInteraction;
}
