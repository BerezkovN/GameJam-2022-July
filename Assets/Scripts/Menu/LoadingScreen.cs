using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2);

        AsyncOperation operation = SceneManager.LoadSceneAsync(LevelLoader.SceneToLoad, LoadSceneMode.Single);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            _loadingBar.value = progress;
            yield return null;
        }
    }
}
