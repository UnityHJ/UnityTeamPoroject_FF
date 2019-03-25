using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public CanvasGroup fadeCg;

    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;


    // Start is called before the first frame update
    private IEnumerator Start()
    {
        fadeCg.alpha = 1.0f;
        yield return StartCoroutine(LoadScene("FoodFighter", LoadSceneMode.Additive));
        StartCoroutine(FadeIn());

    }

    private IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        yield return SceneManager.LoadSceneAsync(sceneName, mode);
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    private IEnumerator FadeIn()
    {
        Debug.Log("[FadeIn]");
        fadeCg.blocksRaycasts = true;
        float fadeSpeed = Mathf.Abs(fadeCg.alpha) / fadeDuration;
        while (!Mathf.Approximately(fadeCg.alpha, 0.0f))
        {
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, 0.0f, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        fadeCg.blocksRaycasts = false;
        SceneManager.UnloadSceneAsync("SceneChanger");

    }
}
