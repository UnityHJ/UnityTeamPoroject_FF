using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CokeCtrl : MonoBehaviour
{
    public static CokeCtrl Instance;    

    private Transform tr;
    private Transform cokeTr;
    private Ray ray;
    private RaycastHit hit;
    
    private Rigidbody rb;

    private string chickTag = "Chicken";
    private string cokeTag = "Coke";


<<<<<<< HEAD
    //#region
    //private void OnEnable()
    //{
    //    EyeCast.OnClick += this.OnClick;
    //}

    //private void OnDisable()
    //{
    //    EyeCast.OnClick -= this.OnClick;
    //}
    //#endregion
=======
    #region
    private void OnEnable()
    {
        EyeCast.OnClick += this.OnClick;
    }

    private void OnDisable()
    {
        EyeCast.OnClick -= this.OnClick;
    }
    #endregion
>>>>>>> 8455f2ef3fd18386132250a27e916532554f977e

    private void Start()
    {
        if (Instance == null) Instance = this;
        else if (Instance != null) Destroy(this);
        DontDestroyOnLoad(this);        

        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        cokeTr = GameObject.Find("cokePos").GetComponent<Transform>();
    }






    public void OnClick()
    {
        //yield return new WaitForSeconds(3.0f);
             
        
    }

    public void MoveCoke()
    {        
        EyeCast.Instance.emptyCoke = false;
        //tr.transform.position = new Vector3(0, -3, -2);
        
        this.transform.SetParent(cokeTr);
        Debug.Log("콜라 이벤트 발생!!");

    }


}
