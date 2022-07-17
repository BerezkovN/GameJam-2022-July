using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private bool _playMusic = true;

    private bool _isEnabled = false;
    private AudioSource _backgroundMusic;

    void Start()
    {
        _backgroundMusic = GetComponentInChildren<AudioSource>();

        Hide();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isEnabled)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    public void Hide()
    {
        Time.timeScale = 1;

        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        _backgroundMusic.Stop();

        _isEnabled = false;
    }

    public void Show()
    {
        Time.timeScale = 0;

        RectTransform[] transforms = GetComponentsInChildren<RectTransform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        _backgroundMusic.Play();

        _isEnabled = true;
    }
}
