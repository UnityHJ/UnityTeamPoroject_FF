using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleCast : MonoBehaviour
{
    private RaycastHit hit;
    private Image circle;
    private Transform camTr;
    private float maxDistance = 20.0f;
    private float minDistance = 0.2f;
    private Canvas _canvas;

    public float scaleParam = 0.02f;


    void Start()
    {
        camTr = Camera.main.transform;
        circle = GameObject.Find("Circle").GetComponent<Image>();
        _canvas = GetComponentInChildren<Canvas>();
    }

    void Update()
    {
        Debug.DrawRay(camTr.position, camTr.forward * maxDistance, Color.yellow);
        if(Physics.Raycast(camTr.position, camTr.forward, out hit, 100.0f))
        {
            //Vector3 dir = camTr.transform.position - hit.point;
            _canvas.transform.position = hit.point;
            _canvas.transform.localScale = Vector3.one * hit.distance * scaleParam;
            //Debug.Log(_canvas.transform.position.ToString());
            Debug.Log(hit.distance);
        }

    }
}
