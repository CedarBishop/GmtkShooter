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
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_PlayerHit");
        UIManager.instance.UpdateHealth(health);
    }

    protected override void Death ()
    {
        base.Death();
        GameManager.instance.GameOver();
    }
}
