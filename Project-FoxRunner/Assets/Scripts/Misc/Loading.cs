using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private AsyncOperation loadingOperation;
    public string sceneToLoad;

    private void Awake()
    {

        Application.targetFrameRate = 60;
    }
    private void Start()
    {

        // Start loading the target scene asynchronously
        loadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        loadingOperation.allowSceneActivation = false; // We'll manually activate it later

        // Start a coroutine to check when loading is complete
        StartCoroutine(CheckLoadingComplete());
    }

    private IEnumerator CheckLoadingComplete()
    {
        while (!loadingOperation.isDone)
        {
            // Check if the loading progress has reached a certain point (e.g., 90%)
            if (loadingOperation.progress >= 0.9f)
            {
                // Activate the loaded scene
                loadingOperation.allowSceneActivation = true;
            }

            yield return null; // Wait for the next frame
        }
    }
}
