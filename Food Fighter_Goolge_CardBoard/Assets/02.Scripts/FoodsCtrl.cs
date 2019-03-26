﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodsCtrl : MonoBehaviour
{
    private const string TAG = "FoodsCtrl";
    private Transform chickTr;
    private Transform drinkingTr;
    private Transform eatingTr;
    private Vector3 originPos;
    private Rigidbody rb;
    private MeshFilter _meshFilter;
    private MeshRenderer _renderer;
    private MeshCollider _meshColl;

    public float throwForce = 100.0f;
    public float spinForce = 10.0f;
    public float speed = 5.0f;
    public float eatVal = 10.0f;
    public int eatCal = 79;
    public int drinkCal = 14;
    public Mesh[] meshs;
    public Material[] textures;

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
            while (true)
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
            while (true)
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
            if (i == 2)
            {
                GameManager.Instance.itemState = ItemState.NORMAL;
                //Destroy(gameObject);
                ThrowBone();
            }
            yield return new WaitForSeconds(audioTime + GameManager.Instance.mukGauge.fillAmount * 2);
            audioTime = SoundCtrl.Instance.MakeSounds(SoundEffects.SWALLOW);
            GameManager.Instance.EatSome(eatVal);
            GameManager.Instance.UpdateCal(eatCal);
            
            yield return new WaitForSeconds(audioTime + GameManager.Instance.mukGauge.fillAmount * 2);
        }

    }

    private void ThrowBone()
    {
        rb.isKinematic = false;
        float randomForce = Random.Range(throwForce * 0.8f, throwForce * 1.2f);
        rb.AddForce(new Vector3(1.0f, 2.0f, 1.0f).normalized * randomForce, ForceMode.Force);
        rb.AddTorque(Random.insideUnitSphere.normalized * spinForce, ForceMode.Force);
    }

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
        if (GameManager.Instance.reducingTime != 1.0f)
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
