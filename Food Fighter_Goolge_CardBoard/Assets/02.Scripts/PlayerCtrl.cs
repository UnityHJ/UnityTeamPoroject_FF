﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{

    [Range(-0.5f, 0.5f)]
    private float v = 0f;

    [Range(-0.5f, 0.5f)]
    private float h = 0f;

    private Transform tr;
    private Quaternion rot;
    private Transform camerTr;
    private Vector3 prevAngle;


    //public float damping = 3.0f;
    //public float rotSpeed = 90.0f;
    public Canvas shakeUI;
    public Image mukGage;
    public float shakeSpeed = 0.1f;
    public float shakingAngle = 100.0f; //쉐이크 체크 각도
    public float shakeCheckTime = 0.5f; //쉐이크 체킹 시간 


    private bool isShaking;
    private int shakeSum;
    private float timeSum = 0.0f;
    private float angleSum = 0.0f;


    void Start()
    {
        tr = GetComponent<Transform>();
        rot = tr.rotation;
        camerTr = Camera.main.GetComponent<Transform>();
        prevAngle = camerTr.rotation.eulerAngles;
    }


    void Update()
    {
        if (GameManager.Instance.itemState == ItemState.NORMAL)
        {
            timeSum += Time.deltaTime;//프레임마다 시간을 합산 
            angleSum += Vector3.Angle(camerTr.forward, prevAngle); //프레임마다 이동 angle값을 합산 
            prevAngle = camerTr.forward;
            if (timeSum >= shakeCheckTime) //쉐이크 체크시간이 지나면 
            {
                isShaking = angleSum >= shakingAngle; //지정 각도보다 angleSum 이 크면 쉐이킹 중으로 판단.
                timeSum = 0;
                angleSum = 0.0f;
                Debug.Log("isShaking " + isShaking);
                if (isShaking)
                {
                    mukGage.fillAmount = GameManager.Instance.mukGauge.fillAmount;
                    StartCoroutine(ShakeBoost());
                }
            }
        }
        else
        {
            isShaking = false;
            timeSum = 0;
            angleSum = 0.0f;
        }
        shakeUI.gameObject.SetActive(isShaking);
    }

    //쉐이킹 중 게이지 감소속도 부스트 
    private IEnumerator ShakeBoost()
    {
        GameManager.Instance.reducingTime *= 0.1f;
        yield return new WaitForSeconds(shakeCheckTime);
        GameManager.Instance.reducingTime *= 10.0f;
    }

}