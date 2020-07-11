using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float timeToLive;
    
    private float force;
    private int damage;

    private Rigidbody2D rigidbody;
    private float redirectAngle;
    private float redirectTime;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Initialise (int Damage, float Force, float RedirectAngle, float RedirectTime)
    {       
        StartCoroutine("DestroySelf");
        StartCoroutine("Redirect");
        redirectAngle = RedirectAngle;
        redirectTime = RedirectTime;
        force = Force;
        damage = Damage;
    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.right * force * Time.fixedDeltaTime;
    }

    IEnumerator DestroySelf () 
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }

    IEnumerator Redirect ()
    {
        yield return new WaitForSeconds(redirectTime);
        transform.Rotate(0, 0, (redirectAngle) * -0.5f);
        while (true)
        {  
            yield return new WaitForSeconds(redirectTime);
            transform.Rotate(0,0,redirectAngle);
            redirectAngle *= -1;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<AIHealth>())
        {
            collision.GetComponent<AIHealth>().TakeDamage(damage);
        }
    }
}
