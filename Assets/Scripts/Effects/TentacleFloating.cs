using UnityEngine;

public class TentacleFloating : MonoBehaviour
{
    private float _floatingStartTime;

    private Vector3 _floatingCenter;
    
    private void Start()
    {
        _floatingCenter = transform.localPosition;
        _floatingStartTime = Random.Range(0f, 500f);
    }
    
    private void Update()
    {
        transform.localPosition = _floatingCenter + 0.05f * Mathf.Sin((Time.time - _floatingStartTime) / 2f) * Vector3.up;
    }
}
