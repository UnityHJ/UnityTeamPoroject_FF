using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChickCtrl : MonoBehaviour
{
<<<<<<< HEAD
    private GameManager _gm;

    [Header("Transform Position")]
    private Transform chickTr;  //치킨 위치
    private Vector3 targetTr = new Vector3(0, 0, 0);
    public Transform playerTr;  //플레이어 앞에 치킨이 놓일 위치

    //public bool isMoving = false;
    //public bool isHeld = false;

    private Rigidbody rb;    
    

    //#region
    //private void OnEnable()
    //{
    //    EyeCast.OnClick += this.OnClick;
    //}

    //private void OnDisable()
    //{
    //    EyeCast.OnClick -= this.OnClick;
    //}
    //#endregion  //OnClick 으로 작용될 

    private void Start()
    {
        chickTr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        _gm = GameManager.Instance;        
=======
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
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e

    }

    void Update()
    {
<<<<<<< HEAD

        //if (_gm.state == GameManager.ChickState.MOVING)
        //{            
        //    float dist = Vector3.Distance(playerTr.position, chickTr.position);
        //    if (dist > 1f)
        //    {
        //        StartCoroutine(MoveChicken());
        //        //gm.state = GameManager.ChickState.MOVING;
        //    }
        //    else
        //    {
        //        _gm.state = GameManager.ChickState.HELD;
        //    }
        //}
        
=======
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
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e
    }

    public void OnClick()
    {
        //yield return new WaitForSeconds(3.0f);
<<<<<<< HEAD
        Debug.Log("치킨 이벤트 발생!!" + gameObject.name);
        StartCoroutine(MoveChicken());
=======
        Debug.Log("치킨 이벤트 발생!!");
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e
    }

    public IEnumerator MoveChicken()
    {
<<<<<<< HEAD
        //EyeCast.Instance.emptyChick = false;
        while (_gm.state == GameManager.ChickState.MOVING)
        {
            
            float dist = Vector3.Distance(playerTr.position, chickTr.position);
            if (dist > 1f)
            {
                _gm.state = GameManager.ChickState.MOVING;
                targetTr = playerTr.position - chickTr.position;  //치킨과 플레이어 앞 위치까지 방향
                chickTr.position += targetTr * Time.deltaTime * 5f;
            }
            else
            {
                _gm.state = GameManager.ChickState.HELD;
            }

            Debug.Log("치.킨.이~~ 이.동.한.다.");
            yield return null;
        }

        
=======

        
        //EyeCast.Instance.emptyChick = false;

        Debug.Log("치.킨.이~~ 이.동.한.다.");
        targetTr = playerTr.position - chickTr.position;  //치킨과 플레이어 앞 위치까지 방향
        chickTr.position += targetTr * Time.deltaTime * 5f;
        yield return null;
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e

    }
}
