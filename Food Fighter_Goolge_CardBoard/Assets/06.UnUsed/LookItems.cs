using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LookItems : MonoBehaviour, IPointerEnterHandler
    , IPointerExitHandler, IGvrPointerHoverHandler
{
    

    public void OnGvrPointerHover(PointerEventData eventData)
    {
        //Debug.Log("Pointer Hover!!!");
        
    }

    public void OnLookItemBox(bool isLookAt)
    {
        Debug.Log(isLookAt);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        Debug.Log("Enter!!!");
        
        SendMessage("ItemIsSelected", SendMessageOptions.DontRequireReceiver);
            
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    //public void movingChick()
    //{
    //    var trPos = GameObject.FindGameObjectWithTag("FrontPosition");

    //    this.gameObject.transform.Translate(trPos.transform.position);

    //    //yield return new WaitForSeconds(2.0f);


    //}


}


