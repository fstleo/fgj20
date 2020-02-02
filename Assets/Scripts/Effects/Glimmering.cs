using UnityEngine;

public class Glimmering : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objects;
    
    [SerializeField]
    private int _timeStepMs = 50;

    [SerializeField]
    private float _probability = 0.2f;

    private float _nextCheckTime;
    
    private void Update()
    {
        if (Time.time >= _nextCheckTime)
        {
            _nextCheckTime += _timeStepMs / 1000f;
            foreach (GameObject o in _objects)
            {
                if (Random.Range(0f, 1f) <= _probability)
                {
                    o.SetActive(!o.activeSelf);                
                }                
            }
        }
    }
}
