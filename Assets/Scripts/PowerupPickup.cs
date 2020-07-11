using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : Pickup
{
    public Buff[] possibleBuffs;
    public Nerf[] possibleNerfs;
    public float duration;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PowerupSystem>())
        {
            other.GetComponent<PowerupSystem>().GainPowerup(possibleBuffs, possibleNerfs,duration);
            Destroy(gameObject);
        }
    }

    
}
