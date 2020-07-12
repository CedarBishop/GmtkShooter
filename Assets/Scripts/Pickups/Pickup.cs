using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float timeToLive;

    void Start()
    {
        StartCoroutine("DestroySelf");
        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_JamSpawn");
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
