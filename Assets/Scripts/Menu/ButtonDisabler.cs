using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        _image.color = Color.black;
    }

    private void Enable()
    {
        _image.color = Color.white;
    }

    private void Disable()
    {
        _image.color = Color.black;
    }
}
