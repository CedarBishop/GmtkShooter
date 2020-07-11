using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public Powerup powerup;
    public float duration;
    public float effectAmount;
    public float timeToLive;

    void Start()
    {
        StartCoroutine("DestroySelf");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerupSystem>())
        {
            other.GetComponent<PowerupSystem>().GainPowerup(powerup,duration);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
