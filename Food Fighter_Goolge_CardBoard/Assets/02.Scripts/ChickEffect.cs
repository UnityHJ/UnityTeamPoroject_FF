using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 치킨이 충돌시 가벼운 파편 생성
 */
public class ChickEffect : MonoBehaviour
{
    private bool isCrashed = false;

    public GameObject effectParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (isCrashed) return; //충돌한 적이 있는지 확인
        Vector3 hit = collision.contacts[0].point;
        GameObject effect = Instantiate(effectParticle, hit, Quaternion.identity);
        isCrashed = true;
        Destroy(effect, 3.0f);
    }

}
