using UnityEngine;

public class BreakoutListener : MonoBehaviour
{
    [SerializeField] private CameraShakes _shaker;

    private void Awake()
    {
        GetComponent<PuzzleRandomizer>().OnBreak += ShowEffects;
    }

    private void ShowEffects()
    {
        _shaker.Shake(0.8f, 0.3f);
    }

    private void Update()
    {
        if (Random.Range(0, 1f) < 0.001f)
        {
            RandomEffect();
        }
    }

    private void RandomEffect()
    {
        _shaker.Shake(0.2f, 0.05f);
    }
    
}
