using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health;
    private SpriteRenderer spriteRenderer;
    private Material material;
    private float flashTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    public virtual void TakeDamage (int damage)
    {
        health -= damage;
        StartCoroutine("HurtFlash");
        if (health <= 0)
        {
            Death();
        }
    }

    public virtual void Heal (int amount)
    {
        health += amount;
    }

    protected virtual void Death ()
    {
        Destroy(gameObject);
    }

    IEnumerator HurtFlash ()
    {
        material.SetFloat("", 1.0f);
        yield return new WaitForSeconds(flashTime);
        material.SetFloat("", 1.0f);
    }
}
