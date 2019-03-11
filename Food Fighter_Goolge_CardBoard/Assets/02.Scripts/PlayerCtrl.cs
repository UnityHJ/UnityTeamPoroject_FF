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
    

    //public float damping = 3.0f;
    public float rotSpeed = 90.0f;
    

    

    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    
    void Update()
    {
        h = Input.GetAxis("Mouse X");   //X axis
        v = Input.GetAxis("Mouse Y");   //Y axis
       

        

        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * h);
        tr.Rotate(Vector3.right * rotSpeed * Time.deltaTime * v);

       
    }

}
