using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    [Range(-0.5f, 0.5f)]
    private float v = 0f;

    [Range(-0.5f, 0.5f)]
    private float h = 0f;

    private Transform tr;
    private Quaternion rot;
    private Transform camerTr;


    //public float damping = 3.0f;
    public float rotSpeed = 90.0f;




    void Start()
    {
        tr = GetComponent<Transform>();
        rot = tr.rotation;
        camerTr = Camera.main.GetComponent<Transform>();

    }


    void Update()
    {
        h = Input.GetAxis("Mouse X");   //X axis
        v = Input.GetAxis("Mouse Y");   //Y axis


        //tr.rotation = Quaternion.Euler(Vector3.zero);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * h);
        tr.Rotate(Vector3.right * rotSpeed * Time.deltaTime * -v);
        //tr.Rotate(Vector3.forward * rotSpeed * 0f,Space.Self);
        camerTr.rotation = Quaternion.Euler(tr.rotation.eulerAngles.x, tr.rotation.eulerAngles.y, 0f);
    }
    
}
