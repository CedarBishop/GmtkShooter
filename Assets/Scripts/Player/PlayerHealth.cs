using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class PlayerHealth : HealthSystem
{

    protected override void Start()
    {
        UIManager.instance.UpdateHealth(health);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        print("PlayerHealth: Take Damage");
        // Update UI

        UIManager.instance.UpdateHealth(health);
    }

    protected override void Death ()
    {
        GameManager.instance.GameOver();
    }
}
