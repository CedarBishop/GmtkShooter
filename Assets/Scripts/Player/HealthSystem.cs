using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health;
    public int flashes;
    public float flashTime = 0.2f;

    private SpriteRenderer spriteRenderer;
    private Material material;
    private Color originalColor;
    protected bool invincible;
    bool isDead;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
        invincible = false;
        isDead = false;
    }

    public virtual void TakeDamage (int damage)
    {
        if (invincible)
        {
            return;
        }
        health -= damage;
        originalColor = spriteRenderer.color;
        StartCoroutine("HurtFlash");
        if (health <= 0)
        {
            if (isDead == false)
            {
                Death();
            }            
        }
    }

    public virtual void Heal (int amount)
    {
        health += amount;
    }

    protected virtual void Death ()
    {
        isDead = true;
        Destroy(gameObject);
    }

    IEnumerator HurtFlash ()
    {
        spriteRenderer.color = Color.white;
        invincible = true;
        for (int i = 0; i < flashes; i++)
        {
            material.SetFloat("_IsHurt", 1.0f);
            yield return new WaitForSeconds(flashTime/2);
            material.SetFloat("_IsHurt", 0.0f);
            yield return new WaitForSeconds(flashTime/2);
        }
        invincible = false;
        spriteRenderer.color = originalColor;
    }
}
