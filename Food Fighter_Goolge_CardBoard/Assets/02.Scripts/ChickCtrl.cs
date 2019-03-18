using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChickCtrl : MonoBehaviour
{
    [Header("GameManager Instance")]
    private GameManager _gm;

    [Header("Transform Position")]
    private Transform chickTr;  //치킨 위치
    private Vector3 targetTr = new Vector3(0, 0, 0);
    private Transform playerTr;  //플레이어 앞에 치킨이 놓일 위치

    //public bool isMoving = false;
    //public bool isHeld = false;

    private Rigidbody rb;

    private void Start()
    {
        chickTr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        _gm = GameManager.Instance;
        playerTr = GameObject.Find("PlayerPos").GetComponent<Transform>();
    }

    void Update()
    {

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

    }

    public void OnClick()
    {
        //_gm.chikState = ChickState.NORMAL;   
        StartCoroutine(MoveChicken());
        Debug.Log("[OnClic]치킨 이벤트 발생!!");
    }

    public IEnumerator MoveChicken()
    {
        Debug.Log("[MoveChicken]치킨 이벤트 발생!!");
        //EyeCast.Instance.emptyChick = false;
        while (_gm.chikState == ChickState.MOVING)
        {
            
            double dist = Vector3.Distance(playerTr.position, chickTr.position);
            if (dist >= 0.0)
            {
                //_gm.chikState = GameManager.ChickState.MOVING;
                targetTr = playerTr.position - chickTr.position;  //치킨과 플레이어 앞 위치까지 방향

                chickTr.position += targetTr * Time.deltaTime * 5f;
                Debug.Log("치.킨.이~~ 이.동.한.다.");
                
            }
            else
            {                
                _gm.chikState = ChickState.HELD;
                Debug.Log("[MoveChicken] 치킨이 손.안.에 있다!!");
            }

            yield return new WaitForSeconds(3f);

            Destroy(this.gameObject);
            _gm.chikState = ChickState.NORMAL;
        }
    }

    
}
