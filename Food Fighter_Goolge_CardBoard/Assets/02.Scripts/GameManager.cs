using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Bread Create Info")]
    public Transform[] points;
    public GameObject[] bread;
    public float createTime = 1.0f;
    public int maxBread = 20;
    public bool isGameOver = false;
    public static GameManager instance = null;
    public List<GameObject> breadPool = new List<GameObject>();

    public int randomIdx;



    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);        
    }


    void Start()
    {
        points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        if (points.Length > 0)
        {
            StartCoroutine(CreateBread());
        }

    }

    private IEnumerator CreateBread()
    {
        while (!isGameOver)
        {
            int breadCount = GameObject.FindGameObjectsWithTag("Bread").Length;
            if (breadCount < maxBread)
            {
                yield return new WaitForSeconds(createTime);
                if (isGameOver) break;
                int idx = Random.Range(1, points.Length);
                randomIdx = Random.Range(0, bread.Length);
                Instantiate(bread[randomIdx], points[idx].position, points[idx].rotation);
                for (int i = 0; i < breadPool.Count; i++)
                {
                    if (breadPool[i].activeSelf == false)
                    {
                        breadPool[i].tag = "Bread";
                        breadPool[i].transform.position = points[idx].position;
                        breadPool[i].transform.rotation = points[idx].rotation;
                        breadPool[i].SetActive(true);
                        break;
                    }
                }
            }
            else yield return null;
        }
    }
}
