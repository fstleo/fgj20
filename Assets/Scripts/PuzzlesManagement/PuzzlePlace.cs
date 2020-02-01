using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlace : MonoBehaviour//, IInteractive
{
    public event Action OnFix;
    
    [SerializeField]
    private GameObject _puzzlePrefab;

    [SerializeField] private Transform _puzzlePoint;

    [SerializeField] private GameObject _indicator;
    
    void Awake()
    {        
        _indicator.SetActive(false);
        var puzzle = Instantiate(_puzzlePrefab, _puzzlePoint);
        puzzle.transform.localRotation = Quaternion.identity;        
        puzzle.transform.Rotate(90, 0, 0);
        puzzle.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        
        _puzzlePoint.gameObject.SetActive(false);
//        puzzle.transform.localPosition += 2f * transform.forward / 3f;
//        puzzle.GetComponent<PuzzleView>().OnFix += Fix;
    }

    public void Break()
    {
         _indicator.SetActive(true);
        _puzzlePoint.gameObject.SetActive(true);
    }

    public void Fix()
    {
        _puzzlePoint.gameObject.SetActive(false);
        _indicator.SetActive(false);
        OnFix?.Invoke();
        GetComponentInChildren<IInteractive>().StopInteraction();        
    }
}
