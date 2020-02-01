using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fabricator : MonoBehaviour
{
    [SerializeField]
    private GameObject _possibleObjects;

    [SerializeField]
    private GameObject _possibleColors;
    
    private void Awake()
    {
        
    }
}
