using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChickState { NORMAL = 0, MOVING = 1, HELD = 2 }
public enum CokeState { NORMAL, MOVING, HELD }
public enum ItemState { NORMAL, MOVING, HELD, WAIT }

public class GameManager : MonoBehaviour
{
    private const string TAG = "GameManager";
    public ChickState chikState = ChickState.NORMAL;
    public CokeState cokeState = CokeState.NORMAL;
    public ItemState itemState = ItemState.NORMAL;

    public static GameManager Instance { get; set; }

    [Header("Chicks Create Info")]
    
    public Transform[] points;

    public GameObject[] chicks;     //접시에 생성되는 치킨오브젝트
    public Image mukGauge;
    public float reducingTime = 1.0f;
    public float reducingValue = 0.002f;
    public float createTime = 1.0f;
    public float gaugeTime = 0.5f;
    public int maxChicks;
    public bool isTimeOver = false;
    //public List<GameObject> ChicksPool = new List<GameObject>();

    //public int randomIdx;
    public GameObject handChicken;  //손에 들린 치킨오브젝트
    
    public bool isGameOver = false;
    public static GameManager instance = null;
    //public List<GameObject> ChicksPool = new List<GameObject>();

    public List<GameObject> chickPool = new List<GameObject>();



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);
        
    }


    void Start()
    {
        //points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length > 0)
        {
            StartCoroutine(CreateChicks());
        }
        StartCoroutine(ReducingGauge());

    }

    private IEnumerator CreateChicks()
    {
        int idx = 0;
        while (!isTimeOver)
        {
            int chickCount = GameObject.FindGameObjectsWithTag("CHICKEN").Length;
            if (chickCount < maxChicks)
            {
                yield return new WaitForSeconds(createTime);
                if (isTimeOver) break;
                idx = idx % points.Length;

                //int idx = Random.Range(2, points.Length);
                int randomIdx = Random.Range(0, chicks.Length);

                GameObject.Instantiate(chicks[randomIdx], points[idx].position, points[idx].rotation);
                idx++;

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
        StartCoroutine(GaugeUp(value));
    }

    public void DrinkSome(float value)
    {
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
        while(true)
        {
            if(mukGauge.fillAmount > 0)
            {
                mukGauge.fillAmount -= reducingValue;
            }
            yield return new WaitForSeconds(reducingTime);
        }
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
}
