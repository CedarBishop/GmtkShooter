using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float force;
    public float damage;
    public float timeToLive;
    public float rotationSpeed;

    private Rigidbody2D rigidbody;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.AddForce(transform.right * force);
        StartCoroutine("DestroySelf");
    }

    void Update()
    {
        transform.Rotate(0,0, rotationSpeed * Time.deltaTime);
    }

    IEnumerator DestroySelf () 
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
