using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    protected int health;
    public virtual void TakeDamage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death ()
    {

    }
}
