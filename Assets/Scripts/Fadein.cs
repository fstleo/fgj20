using UnityEngine;
using UnityEngine.UI;

public class Fadein : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    private void Start()
    {        
        _image.CrossFadeAlpha(0, 10f, true);
    }

}
