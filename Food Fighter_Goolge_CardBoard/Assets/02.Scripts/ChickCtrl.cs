using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChickCtrl : MonoBehaviour
{
    public static ChickCtrl Instance;
 
    [Header("Transform Position")]
    private Transform chickTr;  //치킨 위치
    public Transform playerTr;  //플레이어 앞에 치킨이 놓일 위치
    public Vector3 targetTr = new Vector3(0, 0, 0);

    public bool isMoving = false; 

    private Rigidbody rb;

    

    #region
    private void OnEnable()
    {
        EyeCast.OnClick += this.OnClick;
    }

    private void OnDisable()
    {
        EyeCast.OnClick -= this.OnClick;
    }
    #endregion  //OnClick 으로 작용될 

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(gameObject);
        DontDestroyOnLoad(this);

        chickTr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (isMoving)
        {            
            float dist = Vector3.Distance(playerTr.position, chickTr.position);
            if (dist > 1f)
            {
                StartCoroutine(MoveChicken());
             
            }
            return;
        }
        else isMoving = false;
    }

    public void OnClick()
    {
        //yield return new WaitForSeconds(3.0f);
        Debug.Log("치킨 이벤트 발생!!");
    }

    public IEnumerator MoveChicken()
    {

        
        //EyeCast.Instance.emptyChick = false;

        Debug.Log("치.킨.이~~ 이.동.한.다.");
        targetTr = playerTr.position - chickTr.position;  //치킨과 플레이어 앞 위치까지 방향
        chickTr.position += targetTr * Time.deltaTime * 5f;
        yield return null;

    }
}
