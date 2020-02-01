using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveObjectsTracker : MonoBehaviour
{
    private readonly RaycastHit [] _raycastHits = new RaycastHit[5];

    private Transform _transform;
    
    [SerializeField]
    private PlayerMove _playerMover;

    [SerializeField]
    private TextMeshProUGUI _annotationLabel;

    private IInteractive _currentInteractive = null;

    private void Awake()
    {
        _transform = transform;
    }
        
    private void Update()
    {
        _annotationLabel.text = "";
        if (_currentInteractive == null)
        {
            for (int i = 0; i < Physics.RaycastNonAlloc(_transform.position, _transform.forward, _raycastHits, 2); i++)
            {
                var interactive = _raycastHits[i].transform.GetComponent<IInteractive>();
                if (interactive != null && interactive.CanInteract)
                {
                    _annotationLabel.text = interactive.Annotation + " (E)";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        _currentInteractive = interactive;
                        _currentInteractive.OnStopInteraction += StopInteraction;
                        _playerMover.enabled = false;
                        _currentInteractive.Interact();                        
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _currentInteractive.StopInteraction();
            }
        }
    }       

    private void StopInteraction()
    {
        _currentInteractive.OnStopInteraction -= StopInteraction;
        _playerMover.enabled = true;
        _currentInteractive = null;
    }
}
