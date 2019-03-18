using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsCtrl : MonoBehaviour
{
    private const string TAG = "FoodsCtrl";
    private GameManager _gm;
    private Transform eatingTr;
    private Rigidbody rb;

    public float speed = 5.0f;

    void Start()
    {
        _gm = GameManager.Instance;
        rb = GetComponent<Rigidbody>();
        eatingTr = GameObject.Find("PlayerPos").GetComponent<Transform>();
    }

    public void OnClick()
    {
        Debug.Log(TAG + " [OnClick] ");
        StartCoroutine(MoveToPos());
    }

    
    private IEnumerator MoveToPos()
    {
        Debug.Log(TAG + " [MoveToPos] ");
        while (_gm.chikState == ChickState.MOVING)
        {

            float dis = Vector3.Distance(transform.position, eatingTr.position);
            if(dis > 0.1f)
            {
                transform.position = Vector3.Lerp(transform.position, eatingTr.position, Time.deltaTime * speed);
            }
            else
            {
                rb.isKinematic = true;
                Debug.Log(TAG + " [MoveToPos] is Held");
                _gm.chikState = ChickState.HELD;
                yield return new WaitForSeconds(3.0f);
                _gm.chikState = ChickState.NORMAL;
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }
}
