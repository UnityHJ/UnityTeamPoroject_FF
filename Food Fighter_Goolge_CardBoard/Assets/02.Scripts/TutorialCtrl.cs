using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCtrl : MonoBehaviour
{
    public Material texture;
    public float playTime = 1.0f;

    private Transform camTr;

    void Start()
    {
        camTr = Camera.main.transform;
        texture.mainTextureOffset = new Vector2(0, 0);
        StartCoroutine(FlowImage());
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camTr.rotation;
    }

    private IEnumerator FlowImage()
    {
        float v;
        while (true)
        {
            float x = texture.mainTextureOffset.x;
            v = x;
            yield return new WaitForSeconds(playTime);
            while ( x <= v + 0.25f)
            {
                Debug.Log(x);
                //Debug.Log(0.25f * Time.deltaTime);
                x = x + (0.25f * Time.deltaTime * 3f);
                texture.mainTextureOffset = new Vector2(x, 0f);
                yield return null;
            }
            x = v + 0.25f;
            texture.mainTextureOffset = new Vector2(x, 0f);
        }
    }
}
