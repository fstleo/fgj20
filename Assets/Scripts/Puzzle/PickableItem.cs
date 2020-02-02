using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickableItem : MonoBehaviour
{
    private const float PICKING_TIME = 0.4f; // in seconds

    public enum InitialPositionState
    {
        OnInitialPosition,
        PickedFromInitialPosition,
        CanBeReturnedToInitialPosition,
        MovingToInitialPosition,
    }

    [SerializeField]
    private Transform[] _sectionPoints;

    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Material _ghostMaterial;

    public InitialPositionState initialPositionState
    {
        get => _initialPositionState;
        set => _initialPositionState = value;
    }

    public bool isBroken
    {
        get => _isBroken;
        set => _isBroken = value;
    }

    public Transform[] sectionPoints => _sectionPoints;
    public int size => _sectionPoints.Length;
    public Collider collider => _collider;

    private Renderer[] _renderers;
    private Vector3? _moveToPosition;
    private float _movingStartTime;
    private Action<PickableItem> _onMovingEnd;
    private PickableItemGhost _ghost;
    private InitialPositionState _initialPositionState;
    private bool _isPicked;
    private Vector3 _floatingCenter;
    private float _floatingStartTime;
    private float _floatingFrequencyCoeff;
    private float _floatingMagnitudeCoeff;
    private bool _isBroken;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _initialPositionState = InitialPositionState.OnInitialPosition;
        _floatingFrequencyCoeff = Random.Range(4f, 8f);
        _floatingMagnitudeCoeff = Random.Range(0.2f, 0.5f) * (Random.Range(0, 2) == 0 ? +1f : -1f);
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

        if (!_isPicked && _initialPositionState == InitialPositionState.CanBeReturnedToInitialPosition)
        {
            transform.position = _floatingCenter + _floatingMagnitudeCoeff * Mathf.Sin((Time.time - _floatingStartTime) / _floatingFrequencyCoeff) * Vector3.up;
        }
    }

    public void OnPicked()
    {
        _isPicked = true;
        if (_ghost == null)
        {
            GameObject ghostGo = Instantiate(gameObject);
            PickableItem ghostPickableItem = ghostGo.GetComponent<PickableItem>();
            ghostPickableItem.enabled = false;
            _ghost = ghostGo.AddComponent<PickableItemGhost>();
            ghostPickableItem._collider.gameObject.layer = LayerMask.NameToLayer("PickableItemGhost");

            Transform ghostTransform = _ghost.transform;
            Transform itemTransform = transform;
            ghostTransform.SetParent(itemTransform.parent);
            ghostTransform.position = itemTransform.position;
            ghostTransform.rotation = itemTransform.rotation;
            ghostTransform.localScale = itemTransform.localScale * 0.95f;

            foreach (Renderer renderer in ghostPickableItem._renderers)
            {
                renderer.material = _ghostMaterial;
            }
        }

        _ghost.gameObject.SetActive(true);
        foreach (Renderer renderer in _ghost.GetComponent<PickableItem>()._renderers)
        {
            renderer.material.color = _isBroken ? new Color(1f, 0f, 0f, 0.5f) : new Color(0f, 1f, 0f, 0.5f);
        }
    }

    public void OnReleased()
    {
        _isPicked = false;
        _ghost.gameObject.SetActive(false);
        _floatingCenter = transform.position;
        _floatingStartTime = Time.time;
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