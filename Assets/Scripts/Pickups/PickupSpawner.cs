using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public Pickup pickup;
    public int waveStartsSpawning;
    public float timeBetweenSpawns;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private void Start()
    {
        StartRound(0);
    }

    public void StartRound (int roundNumber)
    {
        if (roundNumber >= waveStartsSpawning)
        {
            StartCoroutine("SpawnPickups");
        }
    }

    public void EndRound ()
    {
        StopAllCoroutines();
    }

    IEnumerator SpawnPickups ()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Vector3 pos = new Vector3(Random.Range(minX,maxX), Random.Range(minY, maxY) ,0);
            Instantiate(pickup, pos, Quaternion.identity);
        }
    }
}
