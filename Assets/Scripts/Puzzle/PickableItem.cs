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
    private Renderer[] _renderers;

    [SerializeField]
    private Material _ghostMaterial;

    public InitialPositionState initialPositionState
    {
        get => _initialPositionState;
        set => _initialPositionState = value;
    }

    public Transform[] sectionPoints => _sectionPoints;
    public int size => _sectionPoints.Length;

    private Vector3? _moveToPosition;
    private float _movingStartTime;
    private Action<PickableItem> _onMovingEnd;
    private PickableItemGhost _ghost;
    private InitialPositionState _initialPositionState;

    private void Awake()
    {
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
        if (_ghost == null)
        {
            GameObject ghostGo = Instantiate(gameObject);
            PickableItem ghostPickableItem = ghostGo.GetComponent<PickableItem>();
            ghostPickableItem.enabled = false;
            _ghost = ghostGo.AddComponent<PickableItemGhost>();
            _ghost.gameObject.layer = LayerMask.NameToLayer("PickableItemGhost");
            
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
    }

    public void OnReleased()
    {
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