using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{
    protected override void Start()
    {
        UIManager.instance.UpdateHealth(health);
        base.Start();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (invincible)
        {
            return;
        }

        // Update UI
        UIManager.instance.UpdateHealth(health);
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_PlayerHit");
    }

    protected override void Death ()
    {
        base.Death();
        GameManager.instance.GameOver();
    }
}
