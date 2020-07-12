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
            if (transform.rotation.eulerAngles.z != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
