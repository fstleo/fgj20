using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private const float PICKING_TIME = 0.4f; // in seconds

    private Color _defaultMaterialColor;
    private Vector3 _initialPosition;
    private Vector3? _moveToPosition;
    private float _movingStartTime;

    private void Awake()
    {
        _defaultMaterialColor = GetComponent<Renderer>().material.color;
        _initialPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_moveToPosition != null)
        {
            float t = (Time.time - _movingStartTime) / PICKING_TIME;
            if (t >= 1f)
            {
                transform.position = _moveToPosition.Value;
                _moveToPosition = null;
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _moveToPosition.Value, t);
            }
        }
    }

    public void OnPicked()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnReleased()
    {
        GetComponent<Renderer>().material.color = _defaultMaterialColor;
    }

    public void SetPosition(Vector3 position)
    {
        if (_moveToPosition != null)
        {
            _moveToPosition = position;
        }
        else
        {
            transform.position = position;
        }
    }

    public void SetPositionAnimated(Vector3 position)
    {
        _moveToPosition = position;
        _movingStartTime = Time.time;
    }
}