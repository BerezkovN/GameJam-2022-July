using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _introPane;
    [SerializeField] private GameObject _levelSelectorPane;

    public void ShowLevelMenu()
    {
        _introPane.SetActive(false);
        _levelSelectorPane.SetActive(true);
    }

    public void LoadLevel(string id)
    {
        SceneManager.LoadScene(id, LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}
