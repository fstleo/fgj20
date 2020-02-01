using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Puzzle : MonoBehaviour
{
    private GameObject _itemDragPlane;
    private Action _onWin;

    public GameObject itemDragPlane
    {
        get => _itemDragPlane;
        set => _itemDragPlane = value;
    }

    public void StartPuzzle(Action onWin)
    {
        _onWin = onWin;
        PickableItem[] pickableItems = transform.GetComponentsInChildren<PickableItem>();
        PickableItem brokenElement = pickableItems[Random.Range(0, pickableItems.Length)];
        brokenElement.OnPicked();
        brokenElement.transform.position = new Vector3
        (
            Random.Range(-3f, 3f),
            itemDragPlane.transform.position.y,
            Random.Range(-2f, 2f)
        );
        brokenElement.initialPositionState = PickableItem.InitialPositionState.CanBeReturnedToInitialPosition;
        brokenElement.OnReleased();
    }
}