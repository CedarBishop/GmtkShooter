using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health;
    private SpriteRenderer spriteRenderer;
    private Material material;
    private float flashTime = 0.2f;
    Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    public virtual void TakeDamage (int damage)
    {
        health -= damage;
        //originalColor = spriteRenderer.color;
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
        //spriteRenderer.color = Color.white;
        //material.SetFloat("_IsHurt", 1.0f);
        //yield return new WaitForSeconds(flashTime);
        //material.SetFloat("_IsHurt", 0.0f);
        //spriteRenderer.color = originalColor;
    }
}
