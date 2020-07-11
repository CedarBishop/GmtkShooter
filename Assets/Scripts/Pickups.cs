using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public Buff[] possibleBuffs;
    public Nerf[] possibleNerfs;
    public float duration;
    public float timeToLive;

    void Start()
    {
        StartCoroutine("DestroySelf");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PowerupSystem>())
        {
            other.GetComponent<PowerupSystem>().GainPowerup(possibleBuffs, possibleNerfs,duration);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
