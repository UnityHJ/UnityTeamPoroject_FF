using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum ChickState { NORMAL = 0, MOVING = 1, HELD = 2 }
public enum CokeState { NORMAL, MOVING, HELD }
public enum ItemState { NORMAL, MOVING, HELD, WAIT }
public enum UIState { NORMAL, WAIT }

public class GameManager : MonoBehaviour
{
    private const string TAG = "GameManager";
    private readonly Color initColor = new Vector4(0f, 1f, 0f, 1f);
    private Color currColor;

    public ChickState chikState = ChickState.NORMAL;
    public CokeState cokeState = CokeState.NORMAL;
    public ItemState itemState = ItemState.NORMAL;
    public UIState uiState = UIState.NORMAL;

    public int Kcal { get; set; }

    public static GameManager Instance { get; set; }


    [Header("Fade In")]
    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;
    public CanvasGroup fadeCg;

    public Text calTxt;

    [Header("Chicks Create Info")]
    public Transform[] points;

    public GameObject[] chicks;     //접시에 생성되는 치킨오브젝트
    public GameObject chickSpawnEffect;     //치킨 생성시 이펙트
    public Image mukGauge;
    public float reducingTime = 1.0f;
    public float reducingValue = 0.002f;
    public float createTime = 1.0f;
    public float gaugeTime = 0.5f;
    public int maxChicks;
    public int chickCount;
    public bool isTimeOver = false;
    public bool isFull = false;
    //public List<GameObject> ChicksPool = new List<GameObject>();

    //public int randomIdx;
    public GameObject handChicken;  //손에 들린 치킨오브젝트
    
    //public static GameManager instance = null;
    //public List<GameObject> ChicksPool = new List<GameObject>();

    public List<GameObject> chickPool = new List<GameObject>();



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        //DontDestroyOnLoad(this);
        
    }


    void Start()
    {
        //points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length > 0)
        {
            StartCoroutine(CreateChicks());
        }
        fadeCg.alpha = 1.0f;
        StartCoroutine(FadeIn());
        StartCoroutine(ReducingGauge());
        Kcal = 0;
        UpdateCal(0);

    }

    private IEnumerator CreateChicks()
    {
        int idx = 0;
        chickCount = 0;
        while (!isTimeOver)
        {
            //int chickCount = GameObject.FindGameObjectsWithTag("CHICKEN").Length;
            if (chickCount < maxChicks)
            {
                yield return new WaitForSeconds(createTime);
                if (isTimeOver) break;
                //idx = idx % points.Length;

                idx = UnityEngine.Random.Range(2, points.Length);
                int randomIdx = UnityEngine.Random.Range(0, chicks.Length);
                if (points[idx] == null) break;
                GameObject.Instantiate(chicks[randomIdx], points[idx].position,UnityEngine.Random.rotation);
                Instantiate(chickSpawnEffect, points[idx].position + Vector3.up * 0.05f, points[idx].rotation);
                //idx++;
                chickCount++;

                //GameObject.Instantiate(chicks[randomIdx], points[2].position, points[pointIdx].rotation);

                //for (int i = 0; i < breadPool.Count; i++)
                //{
                //    if (breadPool[i].activeSelf == false)
                //    {
                //        breadPool[i].tag = "Chicks";
                //        breadPool[i].transform.position = points[idx].position;
                //        breadPool[i].transform.rotation = points[idx].rotation;
                //        breadPool[i].SetActive(true);
                //        break;
                //    }
                //}
            }
            else yield return null;
        }
    }
    public void EatSome(float value)
    {
        if (isTimeOver) return;
        StartCoroutine(GaugeUp(value));
    }

    public void DrinkSome(float value)
    {
        if (isTimeOver) return;
        StartCoroutine(GaugeBoost(value));
    }

    private IEnumerator GaugeUp(float value)
    {
        Debug.Log(TAG + "[GaugeUp] gaugeSum");
        float gaugeSum = 0.0f;
        float damp = value / gaugeTime / 100.0f;
        while(true)
        {
            gaugeSum += Time.deltaTime * damp;
            mukGauge.fillAmount += Time.deltaTime * damp;
            SetMukGaugeColor();
            yield return null;
            if (mukGauge.fillAmount >= 1.0f || gaugeSum >= value / 100.0f)
                break;
        }
    }

    private IEnumerator GaugeBoost(float value)
    {
        Debug.Log(TAG + "[GaugeBoost] value : " + value);
        reducingTime /= value;
        yield return new WaitForSeconds(10.0f);
        reducingTime *= value;
    }
    
    private IEnumerator ReducingGauge()
    {
        currColor = initColor;
        while(true)
        {
            SetMukGaugeColor();
            if (mukGauge.fillAmount > 0)
            {
                isFull = mukGauge.fillAmount > 0.97f;
                mukGauge.fillAmount -= reducingValue;
            }
            yield return new WaitForSeconds(reducingTime);
        }
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

    //public void CreatePooling()
    //{
    //    GameObject enemyPools = new GameObject("EnemyPools");
    //    for (int i = 0; i < maxChicks; i++)
    //    {
    //        var obj = Instantiate<GameObject>(enemy, enemyPools.transform);
    //        obj.name = "Enemy_" + i.ToString("00");
    //        obj.tag = "Untagged";
    //        obj.SetActive(false);
    //        chickPool.Add(obj);
    //    }
    //}
    private IEnumerator FadeOut(Action callback)
    {
        Debug.Log("[FadeOut]");
        fadeCg.blocksRaycasts = true;
        float fadeSpeed = fadeDuration;
        while (!Mathf.Approximately(fadeCg.alpha, 1.0f))
        {
            fadeCg.alpha = Mathf.MoveTowards(fadeCg.alpha, 1.0f, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        fadeCg.blocksRaycasts = false;
        callback();
    }

    public void ReTry()
    {
        Debug.Log("[ReTry]");
        uiState = UIState.WAIT;
        StartCoroutine(FadeOut(delegate ()
        {
            SceneManager.LoadScene("FoodFighter");
        }));

    }

    public void GoTitle()
    {
        Debug.Log("[GoTitle]");
        uiState = UIState.WAIT;
        StartCoroutine(FadeOut(delegate ()
        {
            SceneManager.LoadScene("Intro");
        }));
    }

    public void UpdateCal(int kcal)
    {
        if (isTimeOver) return;
        Kcal += kcal;
        calTxt.text = Kcal + " Kcal";
    }

    private void SetMukGaugeColor()
    {
        currColor.r = mukGauge.fillAmount * 2f;
        currColor.g = (1f - mukGauge.fillAmount) * 2f;
        mukGauge.color = currColor;
    }
}
