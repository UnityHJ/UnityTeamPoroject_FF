using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EyeChicks : MonoBehaviour
{
    private Transform tr;
    public GameObject targetTr;

    private Ray ray;
    private RaycastHit hit;


    private Transform frontPos;


    public float dist = 100.0f;
    
    public float selectTime = 2.0f;

    private float passTime = 0.0f;
    private bool isClicked = false;

    void Start()
    {
        tr = GetComponent<Transform>();
        frontPos = GetComponent<Transform>();
        //CrossHair = GameObject.Find("CrossHair").GetComponent<CrossHairByAnim>();
        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);
    }

    void Update()
    {
        Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);

        ray = new Ray(tr.position, tr.forward * dist);

        if (Physics.Raycast(ray, out hit, dist, 1 << LayerMask.NameToLayer("CHICKEN")))
        {

           
            


        
        }
        else
        {
            
        }

        

    }

    //void TakeChicken()
    //{
    //    if (Physics.Raycast(ray, out hit, dist, 1 << LayerMask.NameToLayer("CHICKEN")))
    //    {
    //        var selectChick = hit.collider.gameObject;


    //        if (selectChick.CompareTag("Chicks"))
    //        {
    //            var trPos = GameObject.FindGameObjectWithTag("FrontPosition");

    //            selectChick.transform.Translate(trPos.transform.position);
    //        }
    //        else return;
    //    }
    //}

    //private void CheckGazeButton()
    //{
    //    PointerEventData data = new PointerEventData(EventSystem.current);
    //    if (Physics.Raycast(ray, out hit, dist, 1 << 9))
    //    {
    //        currButton = hit.collider.gameObject;
    //        circleBar = currButton.GetComponentsInChildren<Image>()[1];
    //        if (currButton != prevButton)
    //        {
    //            passTime = 0.0f;
    //            circleBar.fillAmount = 0.0f;
    //            isClicked = false;
    //            if (prevButton != null)
    //            {
    //                prevButton.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
    //            }
    //            ExecuteEvents.Execute(currButton, data, ExecuteEvents.pointerEnterHandler);
    //            ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
    //            prevButton = currButton;
    //        }
    //        else if (!isClicked)
    //        {
    //            passTime += Time.deltaTime;
    //            circleBar.fillAmount = passTime / selectTime;
    //            if (passTime >= selectTime)
    //            {
    //                //Debug.Log(currButton.name + " is Clicked!!!");
    //                ExecuteEvents.Execute(currButton, data, ExecuteEvents.pointerClickHandler);
    //                isClicked = true;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (prevButton != null)
    //        {
    //            ExecuteEvents.Execute(prevButton, data, ExecuteEvents.pointerExitHandler);
    //            prevButton.GetComponentsInChildren<Image>()[1].fillAmount = 0.0f;
    //            prevButton = null;
    //            passTime = 0.0f;
    //        }
    //    }
    //}
}
