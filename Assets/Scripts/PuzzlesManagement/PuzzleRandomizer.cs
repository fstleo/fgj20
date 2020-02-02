using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PuzzleRandomizer : MonoBehaviour
{
    public event Action OnBreak;
    public event Action OnPuzzleFix;
    
    [SerializeField] private PuzzlePlace[] _puzzles;

    private readonly Queue<PuzzlePlace> _puzzlesQueue = new Queue<PuzzlePlace>();
    private readonly List<PuzzlePlace> _solvedPuzzles = new List<PuzzlePlace>();

    private PuzzlePlace _currentPuzzle;

    [SerializeField]
    private float _minTime = 2f;
    
    [SerializeField]
    private float _maxTime = 5f;
    
    private void Start()
    {
        var puzzleGen = GetComponent<PuzzleGenerator>();
        _solvedPuzzles.AddRange(_puzzles);
        foreach (var puzzle in _solvedPuzzles)
        {
            puzzle.InitWithPuzzle(puzzleGen.GeneratePuzzle(3, 3, 2));
        }
        StartNextPuzzle();
    }

    private void StartNextPuzzle()
    {
        if (_puzzlesQueue.Count == 0)
        {
            GeneratePuzzlesQueue();
        }

        StartCoroutine(WaitAndBreakNext(Random.Range(_minTime, _maxTime)));
    }

    private void FixPuzzle()
    {
        OnPuzzleFix?.Invoke();
        _currentPuzzle.OnFix -= FixPuzzle;
        StartNextPuzzle();
    }

    private IEnumerator WaitAndBreakNext(float time)
    {
        yield return new WaitForSeconds(time);
        OnBreak?.Invoke();
        _currentPuzzle = _puzzlesQueue.Dequeue();
        _currentPuzzle.Break();
        _currentPuzzle.OnFix += FixPuzzle;
        _solvedPuzzles.Add(_currentPuzzle);        
    }

    private void GeneratePuzzlesQueue()
    {
        while (_solvedPuzzles.Count > 0)
        {
            int i = Random.Range(0, _solvedPuzzles.Count);
            _puzzlesQueue.Enqueue(_solvedPuzzles[i]);
            _solvedPuzzles.RemoveAt(i);
        }
    }
}
