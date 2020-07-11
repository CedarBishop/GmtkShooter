using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthSystem
{

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        print("PlayerHealth: Take Damage");
        // Update UI
    }

    protected override void Death ()
    {
        GameManager.instance.GameOver();
    }
}
