using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroReticleCtrl : MonoBehaviour
{
    private RaycastHit hit;
    private Image circle;
    private Transform camTr;
    private Canvas _canvas;
    private float maxDistance = 20.0f;
    private float minDistance = 0.2f;
    private float gazeValue = 0.0f;
    private bool clicked = false;

    public float scaleParam = 0.02f;
    public float gazeTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        camTr = Camera.main.transform;
        circle = GameObject.Find("Circle").GetComponent<Image>();
        _canvas = GetComponentInChildren<Canvas>();
        circle.fillAmount = 0.0f;
        gazeValue = 1 / gazeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked) return;
        Debug.DrawRay(camTr.position, camTr.forward * maxDistance, Color.yellow);
        if (Physics.Raycast(camTr.position, camTr.forward, out hit, maxDistance))
        {
            _canvas.transform.position = hit.point;
            _canvas.transform.localScale = Vector3.one * hit.distance * scaleParam;
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("ITEM"))
            {
                circle.fillAmount += Time.deltaTime * gazeValue;
                if (circle.fillAmount == 1.0f)
                {
                    Debug.Log("Look Item");
                    hit.collider.SendMessage("OnClick", SendMessageOptions.DontRequireReceiver);
                    circle.fillAmount = 0.0f;
                    clicked = true;
                }
            }
            else
            {
                circle.fillAmount = 0;
            }
        }
        else
        {
            circle.fillAmount = 0;
        }
    }
}
