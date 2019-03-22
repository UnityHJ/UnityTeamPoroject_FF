using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    public CanvasGroup fadeCg;

    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    private void Start()
    {
        fadeCg.alpha = 0.0f;
    }

    public void OnClick()
    {
        Debug.Log("[OnClick]");
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        fadeCg.blocksRaycasts = true;
        Debug.Log("[FadeOut]");
        float fadeSpeed = fadeDuration;
        while (!Mathf.Approximately(fadeCg.alpha, 1.0f))
        {
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, 1.0f, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        yield return SceneManager.LoadSceneAsync("SceneChanger", LoadSceneMode.Single);
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
        fadeCg.blocksRaycasts = false;
    }
}
