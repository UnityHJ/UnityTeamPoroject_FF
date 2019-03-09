using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl : MonoBehaviour
{
    public float speed = 1.0f;
    public float damping = 3.0f;
    public Transform targetTr;
    private Transform tr;
    void Start()
    {
        tr = GetComponent<Transform>();
       
    }
    void Update()
    {
        //Vector3 direction = targetTr.position - tr.position;
        
        //Quaternion rot = Quaternion.LookRotation(direction);
        //float dist = Vector3.Distance(targetTr.position, tr.position);
        //speed = 0.5f * dist;
        tr.rotation = Quaternion.Slerp(tr.rotation, tr.rotation, Time.deltaTime * damping);
        //tr.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}