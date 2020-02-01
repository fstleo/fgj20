using UnityEngine;

public class PickableItem : MonoBehaviour
{
    private Color _defaultMaterialColor;
    private Vector3 _initialPosition;
    
    private void Awake()
    {
        _defaultMaterialColor = GetComponent<Renderer>().material.color;
        _initialPosition = transform.localPosition;
    }

    public void OnPicked()
    {
        Debug.Log("OnPicked");
        GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnReleased()
    {
        Debug.Log("OnReleased");
        GetComponent<Renderer>().material.color = _defaultMaterialColor;
    }
}
