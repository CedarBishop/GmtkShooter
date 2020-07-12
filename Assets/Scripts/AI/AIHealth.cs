using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : HealthSystem
{
    private AI ai;
    private int score;
    public GameObject deathEffect;

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_EnemyHit");
    }

    protected override void Death()
    {
        ai = GetComponent<AI>();
        score = ai.score;
        GameManager.instance.waveSystem.AIDied(score, transform.position);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        base.Death();

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_EnemyDeath");
    }
}
