using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    private GameObject _itemDragPlane;
    private PickableItem[] _pickableItems;
    private Action _onWin;
    private bool _isRunning;

    public GameObject itemDragPlane
    {
        get => _itemDragPlane;
        set => _itemDragPlane = value;
    }

    public void StartPuzzle(Action onWin)
    {
        _onWin = onWin;
        _pickableItems = transform.GetComponentsInChildren<PickableItem>();
        int brokenItemsCount = Random.Range(0, 5);
        for (int i = 0; i < brokenItemsCount; ++i)
        {
            PickableItem brokenElement = _pickableItems[Random.Range(0, _pickableItems.Length)];
            brokenElement.OnPicked();
            brokenElement.transform.localPosition = new Vector3
            (
                Random.Range(-1f, 1f),
                itemDragPlane.transform.localPosition.y,
                Random.Range(-1f, 1f)
            );
            brokenElement.initialPositionState = PickableItem.InitialPositionState.CanBeReturnedToInitialPosition;
            brokenElement.OnReleased();
        }

        _isRunning = true;
    }

    private void Update()
    {
        if (!_isRunning)
        {
            return;
        }

        if (AreAllOnInitialPosition())
        {
            _isRunning = false;
            if (_onWin != null)
            {
                _onWin();
                _onWin = null;
            }
        }
    }

    private bool AreAllOnInitialPosition()
    {
        foreach (PickableItem pickableItem in _pickableItems)
        {
            if (pickableItem.initialPositionState != PickableItem.InitialPositionState.OnInitialPosition)
            {
                return false;
            }
        }

        return true;
    }
}