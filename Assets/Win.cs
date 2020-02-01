using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private GameObject _text;
    
    private void OnMouseDown()
    {
        _text.SetActive(true);
    }
}
