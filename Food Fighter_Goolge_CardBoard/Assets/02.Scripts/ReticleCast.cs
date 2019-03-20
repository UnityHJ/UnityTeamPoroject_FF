using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReticleCast : MonoBehaviour
{
    private const string TAG = "ReticleCast";
    private const string chickTag = "CHICKEN";
    private const string colaTag = "COLA";

    private RaycastHit hit;
    private Image circle;
    private Transform camTr;
    private Canvas _canvas;
    private float maxDistance = 20.0f;
    private float minDistance = 0.2f;
    private float gazeValue = 0.0f;
    private int prevItemID;

    public float scaleParam = 0.02f;
    public float gazeTime = 1.0f;
    


    void Start()
    {
        camTr = Camera.main.transform;
        circle = GameObject.Find("Circle").GetComponent<Image>();
        _canvas = GetComponentInChildren<Canvas>();
        circle.fillAmount = 0.0f;
        gazeValue = 1 / gazeTime;
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
            if(GameManager.Instance.itemState == ItemState.NORMAL
                && hit.collider.gameObject.layer == LayerMask.NameToLayer("ITEM")
                && (prevItemID == 0 || hit.collider.GetInstanceID() == prevItemID)
                )
            {
                circle.fillAmount += Time.deltaTime * gazeValue;
                prevItemID = hit.collider.GetInstanceID();
                if (circle.fillAmount == 1.0f)
                {
                    Debug.Log(TAG + "[Update] onGaze!!");
                    GameManager.Instance.itemState = ItemState.MOVING;
                    hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
                    prevItemID = 0;
                }
            }
            else
            {
                prevItemID = 0;
                circle.fillAmount = 0;
            }
        }

    }
}
