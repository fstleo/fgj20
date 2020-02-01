using System;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private const float PICKING_TIME = 0.4f; // in seconds

    public enum InitialPositionState
    {
        OnInitialPosition,
        PickedFromInitialPosition,
        CanBeReturnedToInitialPosition,
    }

    [SerializeField]
    private Transform[] _sectionPoints;

    [SerializeField]
    private Material _ghostMaterial;

    public InitialPositionState initialPositionState
    {
        get => _initialPositionState;
        set => _initialPositionState = value;
    }

    private Color _defaultMaterialColor;
    private Vector3 _initialPosition;
    private Vector3? _moveToPosition;
    private float _movingStartTime;
    private Action<PickableItem> _onMovingEnd;
    private PickableItemGhost _ghost;
    private InitialPositionState _initialPositionState;

    private void Awake()
    {
        _defaultMaterialColor = GetComponent<Renderer>().material.color;
        _initialPosition = transform.position;
        _initialPositionState = InitialPositionState.OnInitialPosition;
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
                if (_onMovingEnd != null)
                {
                    _onMovingEnd.Invoke(this);
                    _onMovingEnd = null;
                }
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
        if (_ghost == null)
        {
            GameObject ghostGo = Instantiate(gameObject);
            Destroy(ghostGo.GetComponent<PickableItem>());
            _ghost = ghostGo.AddComponent<PickableItemGhost>();
            Transform ghostTransform = _ghost.transform;
            ghostTransform.SetParent(transform.parent);
            ghostTransform.position = transform.position;
            ghostTransform.localScale = transform.localScale * 0.95f;
            _ghost.GetComponent<Renderer>().material = _ghostMaterial;
            _ghost.gameObject.layer = LayerMask.NameToLayer("PickableItemGhost");
        }

        _ghost.gameObject.SetActive(true);
    }

    public void OnReleased()
    {
        GetComponent<Renderer>().material.color = _defaultMaterialColor;
        _ghost.gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        if (_moveToPosition != null)
        {
            _moveToPosition = position;
            _onMovingEnd = null;
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
        _onMovingEnd = null;
    }

    public void SetPositionAnimated(Vector3 position, Action<PickableItem> onAnimationEnd)
    {
        SetPositionAnimated(position);
        _onMovingEnd = onAnimationEnd;
    }
}