using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CreditsScroller : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 2;

    private Text _credits;
    private float _dest;
    

    private void Start()
    {
        _credits = GetComponent<Text>();
        _dest = _credits.rectTransform.offsetMin.y;
    }

    private void Update()
    {
        Vector3 vector = _credits.rectTransform.position;
        vector.y = _credits.rectTransform.position.y + _scrollSpeed * 10 * Time.deltaTime;
        _credits.rectTransform.position = vector;
    }

    private void FixedUpdate()
    {
        print(_dest + "..." + _credits.rectTransform.offsetMax.y);
        if (-_credits.rectTransform.offsetMax.y <= _dest)
        {
            LevelLoader.LoadScene("MainMenu");
        }
    }
}
