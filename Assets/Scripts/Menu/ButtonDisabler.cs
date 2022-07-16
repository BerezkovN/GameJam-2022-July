using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisabler : MonoBehaviour
{
    [SerializeField] private ushort _levelNumber = 1;

    private Image _image;
    private Button _btn;

    private void Start()
    {
        _image = GetComponent<Image>();
        _btn = GetComponent<Button>();

        if (LevelData.Default.OpenedLevel < _levelNumber)
        {
            _image.color = Color.black;
            _btn.interactable = false;
        }
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
