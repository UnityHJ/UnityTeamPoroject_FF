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
    public float shakingAngle = 100.0f;
    public float shakeCheckTime = 0.5f;


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
            timeSum += Time.deltaTime;
            angleSum += Vector3.Angle(camerTr.forward, prevAngle);
            prevAngle = camerTr.forward;
            if (timeSum >= shakeCheckTime)
            {
                isShaking = angleSum >= shakingAngle;
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

    private IEnumerator ShakeBoost()
    {
        GameManager.Instance.reducingTime *= 0.1f;
        yield return new WaitForSeconds(shakeCheckTime);
        GameManager.Instance.reducingTime *= 10.0f;
    }

}