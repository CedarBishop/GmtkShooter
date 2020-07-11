using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public Pickup pickup;
    public int waveStartsSpawning;
    public float timeBetweenSpawns;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private void Start()
    {
        minX = GameManager.instance.minSpawnX;
        maxX = GameManager.instance.maxSpawnX;
        minY = GameManager.instance.minSpawnY;
        maxY = GameManager.instance.maxSpawnY;

        GameManager.instance.waveSystem.pickupSpawners.Add(this);

    }

    private void OnDestroy()
    {
        GameManager.instance.waveSystem.pickupSpawners.Remove(this);
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
