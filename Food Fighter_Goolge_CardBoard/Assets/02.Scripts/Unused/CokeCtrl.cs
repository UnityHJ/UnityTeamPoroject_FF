using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CokeCtrl : MonoBehaviour
{
    [Header("item Tag Name")]
    private string chickTag = "Chicken";
    private string cokeTag = "Coke";

    [Header("GameManager Instance")]
    private GameManager _gm;
    
    private Transform cokeTr;
   
    private Rigidbody rb;
    private Vector3 targetTr = new Vector3(0, 0, 0);

    public Transform playerTr;



    private void Start()
    { 
        cokeTr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();        
        _gm = GameManager.Instance;
        _gm.cokeState = CokeState.NORMAL;
    }

    public void OnClick()
    {
 
        StartCoroutine(MoveCoke());
        Debug.Log("콜라 이벤트 발생!!");

    }

    public IEnumerator MoveCoke()
    {
        //EyeCast.Instance.emptyChick = false;
        while (_gm.cokeState == CokeState.MOVING)
        {

            float dist = Vector3.Distance(playerTr.position, cokeTr.position);
            if (dist > 1f)
            {
                //_gm.cokeState = GameManager.CokeState.MOVING;
                targetTr = playerTr.position - cokeTr.position;  //치킨과 플레이어 앞 위치까지 방향
                cokeTr.position += targetTr * Time.deltaTime * 5f;
            }
            else
            {
                _gm.cokeState = CokeState.HELD;
            }

            Debug.Log("[MoveCoke] 콜.라.가~~ 이.동.한.다.");
            yield return null;
        }
    }


}
