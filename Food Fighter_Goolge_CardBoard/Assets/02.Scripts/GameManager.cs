using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; set; }

    [Header("Chicks Create Info")]
    public Transform[] points;
    public GameObject[] chicks;
    public float createTime = 1.0f;
    public int maxChicks;
    public bool isGameOver = false;
    public static GameManager instance = null;
    //public List<GameObject> ChicksPool = new List<GameObject>();

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
            StartCoroutine(CreateChicks());
        }
    }

    private IEnumerator CreateChicks()
    {
        while (!isGameOver)
        {
            int chickCount = GameObject.FindGameObjectsWithTag("Chicken").Length;
            if (chickCount < maxChicks)
            {
                yield return new WaitForSeconds(createTime);
                if (isGameOver) break;
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
