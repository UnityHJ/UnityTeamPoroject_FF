using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsCtrl : MonoBehaviour
{
    private const string TAG = "FoodsCtrl";
    private Transform eatingTr;
    private Transform drinkingTr;
    private Vector3 originPos;
    private Rigidbody rb;

    public float speed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        eatingTr = GameObject.Find("EatPos").GetComponent<Transform>();
        drinkingTr = GameObject.Find("DrinkPos").GetComponent<Transform>();
    }

    public void OnClick()
    {
        Debug.Log(TAG + " [OnClick] ");
        originPos = transform.position;
        StartCoroutine(MoveToPos());
    }


    private IEnumerator MoveToPos()
    {
        Debug.Log(TAG + " [MoveToPos] ");
        rb.isKinematic = true;
        while (GameManager.Instance.itemState == ItemState.MOVING)
        {
            Transform tr = eatingTr;
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


    private IEnumerator EatChicken()
    {
        Debug.Log(TAG + " [EatChicken]");
        for (int i = 0; i < 3; i++)
        {
            float audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.BITE);
            yield return new WaitForSeconds(audioTime);
            audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.CHEW);
            GameManager.Instance.EatSome(5.0f);
            if (i == 2)
            {
                GameManager.Instance.itemState = ItemState.NORMAL;
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(audioTime);
        }

    }

    private IEnumerator Drink()
    {
        Debug.Log(TAG + " [Drink]");
        float audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.DRINK);
        GameManager.Instance.DrinkSome(3.0f);
        yield return new WaitForSeconds(audioTime);
        GameManager.Instance.itemState = ItemState.NORMAL;
        float dis = Vector3.Distance(transform.position, originPos);
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, originPos, Time.deltaTime * speed * 2);
            yield return null;
        }

    }
}
