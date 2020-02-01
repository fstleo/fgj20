using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlace : MonoBehaviour
{
    public event Action OnFix;

    [SerializeField] private Transform _puzzlePoint;

    [SerializeField] private GameObject _indicator;

    [SerializeField]
    private PanelOpener _interactive;
    
    void Awake()
    {        
        _indicator.SetActive(false);      
    }

    public void InitWithPuzzle(Puzzle puzzle)
    {
        GameObject puzzleObject = puzzle.gameObject;
        puzzleObject.transform.SetParent(_puzzlePoint);
        puzzleObject.transform.localPosition = Vector3.zero;        
        puzzleObject.transform.localRotation = Quaternion.identity;        
        puzzleObject.transform.Rotate(90, 0, 0);
        puzzleObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        _puzzlePoint.gameObject.SetActive(false);
    }

    public void Break()
    {
        _puzzlePoint.GetComponentInChildren<Puzzle>().StartPuzzle(Fix);
        _indicator.SetActive(true);
        _puzzlePoint.gameObject.SetActive(true);
        _interactive.CanInteract = true;
    }

    private void Fix()
    {
        _interactive.CanInteract = false;
        _puzzlePoint.gameObject.SetActive(false);
        _indicator.SetActive(false);
        OnFix?.Invoke();
        _interactive.StopInteraction();        
    }
}
