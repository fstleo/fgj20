using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRandomizer : MonoBehaviour
{
    [SerializeField] private PuzzlePlace[] _puzzles;

    private readonly Queue<PuzzlePlace> _puzzlesQueue = new Queue<PuzzlePlace>();
    private readonly List<PuzzlePlace> _solvedPuzzles = new List<PuzzlePlace>();

    private PuzzlePlace _currentPuzzle;
    
    private void Start()
    {
        _solvedPuzzles.AddRange(_puzzles);        
        StartNextPuzzle();
    }

    private void StartNextPuzzle()
    {
        if (_puzzlesQueue.Count == 0)
        {
            GeneratePuzzlesQueue();
        }
        _currentPuzzle = _puzzlesQueue.Dequeue();
        _currentPuzzle.Break();
        _currentPuzzle.OnFix += FixPuzzle;
        _solvedPuzzles.Add(_currentPuzzle);
    }

    private void FixPuzzle()
    {
        _currentPuzzle.OnFix -= FixPuzzle;
        StartNextPuzzle();
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
