using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Transform target;    

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + Vector3.down;            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
