using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    public AISpawnParams[] ais;

    public int roundNumber;
    public int extraAISpawnedPerRound;
    public int roundOneAISpawnAmount;
    public float timeBetweenSpawns;
    public float timeBetweenRounds;

    public float currentScore;

    [HideInInspector] public List<PickupSpawner> pickupSpawners = new List<PickupSpawner>();

    private int aisToBeSpawnedThisRound;
    private int currentAisSpawnedThisRound;
    private int aisDiedThisRound;
    private List<AI> aitypesSpawnedThisRound = new List<AI>();

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
    }

    public void StartGame ()
    {
        aisToBeSpawnedThisRound = roundOneAISpawnAmount;
        roundNumber = 0;
        currentScore = 0;
        StartCoroutine("DelayBetweenRounds");    
    }

    void StartRound ()
    {
        currentAisSpawnedThisRound = 0;
        aitypesSpawnedThisRound.Clear();
        aisDiedThisRound = 0;
        foreach (var ai in ais)
        {
            if (roundNumber >= ai.roundWhenStartSpawning)
            {
                aitypesSpawnedThisRound.Add(ai.ai);
            }
        }

        StartCoroutine("SpawnAI");
        foreach (var spawner in pickupSpawners)
        {
            spawner.StartRound(roundNumber);
        }
        UIManager.instance.roundText.text = "Round: " + roundNumber;
    }

    void EndRound ()
    {
        foreach (var spawner in pickupSpawners)
        {
            spawner.EndRound();
        }
        aisToBeSpawnedThisRound += extraAISpawnedPerRound;
        StartCoroutine("DelayBetweenRounds");
    }

    IEnumerator SpawnAI ()
    {
        while (currentAisSpawnedThisRound < aisToBeSpawnedThisRound)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            Instantiate(aitypesSpawnedThisRound[Random.Range(0, aitypesSpawnedThisRound.Count)], GameManager.instance.GetClearLocationOnMap(minX,maxX, minY, maxY), Quaternion.identity);
            currentAisSpawnedThisRound++;
        }
    }

    IEnumerator DelayBetweenRounds ()
    {
        yield return new WaitForSeconds(timeBetweenRounds);
        roundNumber++;
        StartRound();
    }

    public void AIDied (int score)
    {
        aisDiedThisRound++;
        currentScore += score;

        if (aisDiedThisRound >= aisToBeSpawnedThisRound)
        {
            EndRound();
        }
    }
}

[System.Serializable]
public struct AISpawnParams 
{
    public AI ai;
    public int roundWhenStartSpawning;
}
