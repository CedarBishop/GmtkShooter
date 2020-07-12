using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Bullet bulletPrefab;
    public float timeToLive;
    public LayerMask enemyLayer;
    private float force;
    private int damage;
    public Shadow shadow;

    private Rigidbody2D rigidbody;
    private float redirectAngle;
    private float redirectTime;
    private int lightningCount;
    private int explosionProjectileCount;
    private float enemySpeedUpAmount;
    private int enemyHealAmount;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        shadow.transform.parent = null;
    }

    public void Initialise (int Damage, float Force, float RedirectAngle, float RedirectTime, float BulletDeviation, int LightningCount, int ExplosionProjectileCount, int EnemyHealAmount, float EnemySpeedupAmount)
    {       
        StartCoroutine("DestroySelf");
        StartCoroutine("Redirect");
        redirectAngle = RedirectAngle;
        redirectTime = RedirectTime;
        force = Force;
        damage = Damage;
        lightningCount = LightningCount;
        explosionProjectileCount = ExplosionProjectileCount;
        enemyHealAmount = EnemyHealAmount;
        enemySpeedUpAmount = EnemySpeedupAmount;

        float zRotation = transform.rotation.eulerAngles.z + Random.Range(-BulletDeviation, BulletDeviation);
        transform.rotation = Quaternion.Euler(0, 0, zRotation);
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
            if (enemySpeedUpAmount > 0)
            {
                collision.GetComponent<AI>().movementSpeed += enemySpeedUpAmount;
            }
            Destroy(gameObject);

            //if (lightningCount > 0)
            //{
            //    Collider2D collider = Physics2D.OverlapCircle(transform.position, 3, enemyLayer);
            //    if (collider != null)
            //    {
            //        Vector2 direction = (collider.transform.position - transform.position).normalized;
            //        transform.right = direction;
            //        lightningCount--;
            //    }
            //}
            //if (explosionProjectileCount > 0)
            //{
            //    for (int i = 0; i < explosionProjectileCount; i++)
            //    {
            //        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0,0, Random.Range(0, 359.9f)));
            //        bullet.Initialise(damage, force, redirectAngle, redirectTime,0,0,0, enemyHealAmount,enemySpeedUpAmount);
            //    }
            //}
            //if (explosionProjectileCount <= 0 && lightningCount <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }
    }
}
