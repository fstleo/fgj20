using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    private float _timer = 60f;

    [SerializeField]
    private TextMeshProUGUI [] _timerLabels;
    
    private void Awake()
    {
        GetComponent<PuzzleRandomizer>().OnPuzzleFix += IncreaseTimer;

        ShowTime();   
    }

    private void ShowTime()
    {
        foreach (var timerLabel in _timerLabels)
        {
            timerLabel.text = _timer.ToString("F0");
        }
    }

    private void IncreaseTimer()
    {        
        _timer += 15f;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        ShowTime();
        if (_timer < 0)
        {
            SceneManager.LoadScene("FPSTest");
            //gameover
        }
    }
}
