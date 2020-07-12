using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{

    public GameObject deathEffect;

    protected override void Start()
    {
        UIManager.instance.UpdateHealth(health);
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (invincible== false)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX("SFX_PlayerHit");
        }

        // Update UI
        UIManager.instance.UpdateHealth(health);
        
    }

    protected override void Death ()
    {

        Instantiate(deathEffect, transform.position, Quaternion.identity);
        base.Death();
        GameManager.instance.GameOver();
    }
}
