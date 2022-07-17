using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public static string SceneToLoad { get; private set; }

    [SerializeField] private GameObject _introPane;
    [SerializeField] private GameObject _levelSelectorPane;

    public void ShowLevelMenu()
    {
        _introPane.SetActive(false);
        _levelSelectorPane.SetActive(true);
    }

    public void LoadLevel(string id)
    {
        LoadScene(id);
    }

    public static void LoadScene(string id)
    {
        SceneToLoad = id;
        SceneManager.LoadScene("LevelLoadProgress", LoadSceneMode.Single);
    }

    public void Exit()
    {
        Application.Quit(0);
    }
}
