using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum ChickState { NORMAL = 0, MOVING = 1, HELD = 2 };
    public ChickState state = ChickState.NORMAL;

    [Header("Chicks Create Info")]
    public Transform[] points;
    public GameObject[] chicks;     //접시에 생성되는 치킨오브젝트
    public float createTime = 1.0f;
    public int maxChicks;
    public bool isTimeOver = false;
    //public List<GameObject> ChicksPool = new List<GameObject>();

    public int randomIdx;
    public GameObject handChicken;  //손에 들린 치킨오브젝트


    public static GameManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);        
    }


    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length > 0)
        {
            StartCoroutine(CreateChicks());
        }


    }

    private IEnumerator CreateChicks()
    {
        while (!isTimeOver)
        {
            int chickCount = GameObject.FindGameObjectsWithTag("Chicken").Length;
            if (chickCount < maxChicks)
            {
                yield return new WaitForSeconds(createTime);
                if (isTimeOver) break;
                int idx = Random.Range(2, points.Length);
                randomIdx = Random.Range(0, chicks.Length);
                
                Instantiate(chicks[randomIdx], points[idx].position, points[idx].rotation);



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
}
