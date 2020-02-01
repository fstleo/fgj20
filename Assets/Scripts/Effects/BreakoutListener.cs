using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutListener : MonoBehaviour
{
    [SerializeField] private CameraShakes _shaker;

    private void Awake()
    {
        GetComponent<PuzzleRandomizer>().OnBreak += ShowEffects;
    }

    private void ShowEffects()
    {
        _shaker.Shake(0.5f);
    }
}
