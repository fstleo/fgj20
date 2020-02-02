using UnityEngine;

public class MaterrialToggler : MonoBehaviour
{
    [SerializeField]
    private Material _newMaterial;

    [SerializeField]
    private int _timeStepMs = 50;

    [SerializeField]
    private float _probability = 0.2f;
    
    private Material _nextMaterial;
    private Renderer _renderer;
    private float _nextCheckTime;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _nextMaterial = _newMaterial;
    }

    private void Update()
    {
        if (Time.time >= _nextCheckTime)
        {
            _nextCheckTime += _timeStepMs / 1000f;
            if (Random.Range(0f, 1f) <= _probability)
            {
                Material m = _renderer.material;
                _renderer.material = _nextMaterial;
                _nextMaterial = m;
            }
        }
    }
}