using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickEffect : MonoBehaviour
{
    private bool isCrashed = false;

    public GameObject effectParticle;

    private void OnCollisionEnter(Collision collision)
    {
        if (isCrashed) return;
        Vector3 hit = collision.contacts[0].point;
        GameObject effect = Instantiate(effectParticle, hit, Quaternion.identity);
        isCrashed = true;
        Destroy(effect, 3.0f);
    }

}
