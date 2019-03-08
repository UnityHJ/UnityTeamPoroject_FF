using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{

    //private float h = 0f;
    private float v = 0f;
    private float r = 0f;
    private Transform tr;

    private float rotationangley1;
    private float rotationangley2;

    public float rotSpeed = 90.0f;
    public float viewAngle = 90.0f;
    public float _maxAngle = 45f;
    public float _minAngle = -45f;

    

    void Start()
    {
        tr = GetComponent<Transform>();
        Vector3 axis = tr.position;
    }

    
    void Update()
    {        
        v = Input.GetAxis("Mouse Y");
        r = Input.GetAxis("Mouse X");

        //rotationangley1 = transform.eulerAngles.y;
        //rotationangley2 = transform.eulerAngles.y;

        //if (rotationangley1 > 330 && rotationangley1 < 340)
        //{

        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, 10, transform.eulerAngles.z),
        //        Time.deltaTime * 1.5f);
        //    tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
        //}

        //if (rotationangley2 > 30 && rotationangley2 < 40)
        //{

        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, -10, transform.eulerAngles.z),
        //        Time.deltaTime * 1.5f);
        //    tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
        //}

        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
        tr.Rotate(Vector3.right * rotSpeed * Time.deltaTime * v);

    }

}
