using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public enum Type { NORMAL, WAYPOINT };
    public Type _type = Type.NORMAL;
    private const string wayPointFile = "Enemy";    

    public Color _color = Color.yellow;
    public float _radius = 0.1f;
    
    private void OnDrawGizmos()
    {
        if (_type == Type.NORMAL)
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, _radius);
        }
        else
        {
            Gizmos.color = _color;
            Gizmos.DrawIcon(transform.position + Vector3.up * 1.0f, wayPointFile, true);
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}