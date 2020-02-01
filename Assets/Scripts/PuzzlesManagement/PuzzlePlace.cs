using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePlace : MonoBehaviour//, IInteractive
{
    public event Action OnFix;
    
    [SerializeField]
    private GameObject _puzzlePrefab;

    [SerializeField] private GameObject _indicator;
    
    void Awake()
    {
        _indicator.SetActive(false);
        var puzzle = Instantiate(_puzzlePrefab, transform);
        puzzle.transform.Rotate(-90, 0, 0);
        puzzle.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        puzzle.transform.localPosition += 2f * transform.forward / 3f;
//        puzzle.GetComponent<PuzzleView>().OnFix += Fix;
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
}
