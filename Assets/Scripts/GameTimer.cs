using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private float _timer = 60f;

    [SerializeField]
    private TextMeshProUGUI _timerLabel;
    
    private void Awake()
    {
        GetComponent<PuzzleRandomizer>().OnPuzzleFix += ResetTimer;
        _timerLabel.text = _timer.ToString("F0");
    }

    private void ResetTimer()
    {        
        _timer = 60f;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        _timerLabel.text = _timer.ToString("F0");
        if (_timer < 0)
        {
            //gameover
        }
    }
}
