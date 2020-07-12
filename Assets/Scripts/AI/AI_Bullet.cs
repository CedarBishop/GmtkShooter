using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bullet : MonoBehaviour
{
    public float timeToLive;
    public Shadow shadow;
    private Rigidbody2D rigidbody;

    private int damage;
    private float force;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        shadow.transform.parent = null;
    }

    public void Initialise(int Damage, float Force, float BulletDeviation)
    {
        StartCoroutine("DestroySelf");

        damage = Damage;
        force = Force;

        float zRotation = transform.rotation.eulerAngles.z + Random.Range(-BulletDeviation, BulletDeviation);
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
    }

    void FixedUpdate()
    {
        rigidbody.velocity = transform.right * force * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
