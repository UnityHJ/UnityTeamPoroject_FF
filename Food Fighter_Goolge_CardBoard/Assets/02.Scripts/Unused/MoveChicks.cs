using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveChicks : MonoBehaviour
{
    
    private Transform tr;
    private Ray ray;
    private RaycastHit hit;
    
    

    void Start()
    {

       
        
    }
    
    void Update()
    {
        

        //Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);

        //ray = new Ray(tr.position, tr.forward * dist);

        //if (Physics.Raycast(ray, out hit, dist, 1 << LayerMask.NameToLayer("CHICKEN")))
        //{


        //    Vector3 dir = (targetTr.position - tr.position).normalized;
        //    //tr.position = transform.Translate(tr.transform.position * dir *10 * Time.deltaTime;



        //}
        //else
        //{

        //}
    }

    private IEnumerator moveChicken()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(this.gameObject);
    }

    
}
