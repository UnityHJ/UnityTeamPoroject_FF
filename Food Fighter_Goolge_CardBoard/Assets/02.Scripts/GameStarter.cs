using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    private AudioSource _audio;
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;

    public AudioSource titleBGM;
    public CanvasGroup fadeCg;
    public Mesh biteMesh;
    public Material biteTx;

    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshFilter = GetComponent<MeshFilter>();
        fadeCg.alpha = 1.0f;
        StartCoroutine(FadeIn());

    }

    public void OnClick()
    {
        Debug.Log("[OnClick]");
        StartCoroutine(FadeOut());
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
    }

    private IEnumerator FadeOut()
    {
        _audio.Play();
        yield return new WaitForSeconds(_audio.clip.length * 0.5f );
        _meshFilter.sharedMesh = biteMesh;
        _meshRenderer.material = biteTx;
        yield return new WaitForSeconds(_audio.clip.length * 0.5f);
        fadeCg.blocksRaycasts = true;
        Debug.Log("[FadeOut]");
        float fadeSpeed = fadeDuration;
        while (!Mathf.Approximately(fadeCg.alpha, 1.0f))
        {
            titleBGM.volume = 1 - fadeCg.alpha;
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, 1.0f, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        yield return SceneManager.LoadSceneAsync("FoodFighter", LoadSceneMode.Single);
        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
        fadeCg.blocksRaycasts = false;
    }
}
