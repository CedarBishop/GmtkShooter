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
    public GameObject floorSplat;
    public float splatYScale = 0.15f;


    private Rigidbody2D rigidbody;
    private float redirectAngle;
    private float redirectTime;
    private int lightningCount;
    private int explosionProjectileCount;
    private float enemySpeedUpAmount;
    private int enemyHealAmount;
    private Collider2D colliderToIgnore;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        shadow.transform.parent = null;
    }

    public void Initialise (int Damage, float Force, float RedirectAngle, float RedirectTime, float BulletDeviation, int LightningCount, int ExplosionProjectileCount, int EnemyHealAmount, float EnemySpeedupAmount, Collider2D ColliderToIgnore)
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
        colliderToIgnore = ColliderToIgnore;

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
        SpawnSplat();
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
        if (colliderToIgnore)
        {
            return;
        }
        if (collision.GetComponent<AIHealth>())
        {
            collision.GetComponent<AIHealth>().TakeDamage(damage);
            SpawnSplat();

            if (enemySpeedUpAmount > 0)
            {
                collision.GetComponent<AI>().movementSpeed += enemySpeedUpAmount;
            }



            if (explosionProjectileCount > 0)
            {
                for (int i = 0; i < explosionProjectileCount; i++)
                {
                    Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 359.9f)));
                    bullet.Initialise(damage, force, redirectAngle, redirectTime, 0, 0, 0, enemyHealAmount, enemySpeedUpAmount, collision);
                }
            }
            if (lightningCount > 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3, enemyLayer);
                Collider2D colliderToHit = null;
                if (colliders != null)
                {
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        if (colliderToHit != null)
                        {
                            continue;
                        }

                        if (colliders[Random.Range(0,colliders.Length)] != collision)
                        {
                            colliderToHit = colliders[i];
                        }
                    }

                    if (colliderToHit != null)
                    {
                        Vector2 direction = (colliderToHit.transform.position - transform.position).normalized;
                        transform.right = direction;
                        lightningCount--;
                    }                    
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void SpawnSplat()
    {
        float splatSize = Random.Range(0.8f, 1.1f);
        GameObject splat = Instantiate(floorSplat, transform.position, Quaternion.identity);
        splat.transform.localScale.Set(splatSize, splatSize - 0.15f, 1);
    }
}
