using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//음식 전반을 다룬다.
public class FoodsCtrl : MonoBehaviour
{
    private const string TAG = "FoodsCtrl";
    private Transform chickTr;
    private Transform drinkingTr;
    private Transform eatingTr;
    private Vector3 originPos;
    private Rigidbody rb;
    
    //치킨 먹을때 mesh 데이터 교체 용
    private MeshFilter _meshFilter;
    private MeshRenderer _renderer;
    private MeshCollider _meshColl;

    public float throwForce = 100.0f; //뼈다귀 던지는 힘
    public float spinForce = 10.0f; //뼈다귀 던질때 스핀
    public float speed = 5.0f;
    public float eatVal = 10.0f; //한입에 게이지 올라가는 수치
    public int eatCal = 79; //한입당 칼로리
    public int drinkCal = 14; //콜라 한모금 칼로리
    public Mesh[] meshs; //교체할 Mesh 데이터
    public Material[] textures; //교체할 Material 데이터

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        chickTr = GameObject.Find("ChickPos").GetComponent<Transform>();
        drinkingTr = GameObject.Find("DrinkPos").GetComponent<Transform>();
        eatingTr = GameObject.Find("EatPos").GetComponent<Transform>();
        _meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<MeshRenderer>();
        _meshColl = GetComponent<MeshCollider>();
    }

    //클릭 이벤트 리시버
    public void OnClick()
    {
        Debug.Log(TAG + " [OnClick] ");
        originPos = transform.position;
        StartCoroutine(MoveToPos());
    }

    //치킨or 콜라 먹는 포지션까지 이동
    private IEnumerator MoveToPos()
    {
        Debug.Log(TAG + " [MoveToPos] ");
        rb.isKinematic = true;
        while (GameManager.Instance.itemState == ItemState.MOVING) 
        {
            Transform tr = chickTr;
            if (tag == "COKE")
            {
                tr = drinkingTr;
            }

            float dis = Vector3.Distance(transform.position, tr.position);
            if (dis > 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, tr.position, Time.deltaTime * speed);
                transform.rotation = Quaternion.Lerp(transform.rotation, tr.rotation, Time.deltaTime * speed);
            }
            else
            {

                Debug.Log(TAG + " [MoveToPos] is Held");
                GameManager.Instance.itemState = ItemState.HELD;
                if (tag == "COKE")
                {
                    StartCoroutine(Drink());
                }
                else
                {
                    StartCoroutine(EatChicken());
                }

                break;
            }
            yield return null;
        }
    }


    //치킨 먹는 메소드
    private IEnumerator EatChicken()
    {
        Debug.Log(TAG + " [EatChicken]");
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.isTimeOver)
            {
                Destroy(gameObject);
                break;
            }
            while (true) //치킨 포인트에서 이팅 포인트로 이동
            {
                transform.position = Vector3.Slerp(transform.position, eatingTr.position, Time.deltaTime * speed * 1.5f);
                float dis = Vector3.Distance(transform.position, eatingTr.position);

                if(dis < 0.005f)
                {
                    transform.position = eatingTr.position;
                    break;
                }
                yield return null;
            }
            float audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.BITE);
            yield return new WaitForSeconds(audioTime * 0.5f);
            _renderer.material = textures[i];
            _meshFilter.sharedMesh = meshs[i];
            _meshColl.sharedMesh = meshs[i];
            transform.rotation = chickTr.rotation;
            while (true) //이팅포인트에서 치킨 포인트로 이동
            {
                transform.position = Vector3.Slerp(transform.position, chickTr.position, Time.deltaTime * speed * 2.5f);
                float dis = Vector3.Distance(transform.position, chickTr.position);
                if (dis < 0.001f)
                {
                    transform.position = chickTr.position;
                    break;
                }
                yield return null;
            }
            audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.CHEW);
            yield return new WaitForSeconds(audioTime + GameManager.Instance.mukGauge.fillAmount * 2);
            if (i == 2)
            {
                GameManager.Instance.itemState = ItemState.NORMAL;
                //Destroy(gameObject);
                ThrowBone();
            }
            audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.SWALLOW);
            GameManager.Instance.EatSome(eatVal);
            GameManager.Instance.UpdateCal(eatCal);
            //yield return new WaitForSeconds(audioTime + GameManager.Instance.mukGauge.fillAmount * 2);
            yield return new WaitForSeconds(audioTime);
        }

    }

    //뼈다귀 던지기
    private void ThrowBone()
    {
        gameObject.tag = "TRASH";
        GameManager.Instance.chickCount--;
        rb.isKinematic = false;
        float randomForce = Random.Range(throwForce * 0.8f, throwForce * 1.2f);
        rb.AddForce(new Vector3(1.0f, 2.0f, 1.0f).normalized * randomForce, ForceMode.Force);
        rb.AddTorque(Random.insideUnitSphere.normalized * spinForce, ForceMode.Force);
    }

    //콜라 마시기
    private IEnumerator Drink()
    {
        Debug.Log(TAG + " [Drink]");
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.Instance.isTimeOver)
            {
                break;
            }
            float audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.DRINK);
            yield return new WaitForSeconds(audioTime + 0.5f);
            GameManager.Instance.UpdateCal(drinkCal);
        }
        Debug.Log(TAG + " [Drink] reducingTime : " + GameManager.Instance.reducingTime);
        //콜라 연속으로 마시는 경우에는 트림이 난다.
        if (!Mathf.Approximately(GameManager.Instance.reducingTime, 1.0f)) 
        {
            float audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.BURP);
            yield return new WaitForSeconds(audioTime + 0.5f);
        }
        else
        {
            SoundCtrl.Instance.MakeSounds(SoundEffects.KYAH);
        }
        GameManager.Instance.itemState = ItemState.NORMAL;
        GameManager.Instance.DrinkSome(3.0f);


        while (true)
        {
            transform.position = Vector3.Slerp(transform.position, originPos, Time.deltaTime * speed);
            yield return null;
            float dis = Vector3.Distance(transform.position, originPos);
            if (dis < 0.01f)
            {
                transform.position = originPos;
                Debug.Log(TAG + " [Drink] break");
                break;
            }
        }

    }
}
