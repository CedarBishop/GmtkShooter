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

    public Color[] coloursForComboText;

    public GameObject floatingPoints;
    public int currentScore;
    private int currentKillStreak;
    private float comboTimer;
    private float timeToLoseCombo = 5;
    private int currentComboMultiplier;
    private Color currentComboColor;

    [HideInInspector] public List<PickupSpawner> pickupSpawners = new List<PickupSpawner>();

    private int aisToBeSpawnedThisRound;
    private int currentAisSpawnedThisRound;
    private int aisDiedThisRound;
    private List<AI> aitypesSpawnedThisRound = new List<AI>();

    private PlayerMovement player;

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
        UIManager.instance.UpdateWave(roundNumber);
        currentScore = 0;
        UIManager.instance.UpdateScore(currentScore);
        StartCoroutine("DelayBetweenRounds");    
    }

    void StartRound ()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
        }

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
        UIManager.instance.UpdateWave(roundNumber);

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_WaveSpawn");
    }

    void EndRound ()
    {
        foreach (var spawner in pickupSpawners)
        {
            spawner.EndRound();
        }
        aisToBeSpawnedThisRound += extraAISpawnedPerRound;
        UIManager.instance.roundText.text = "Wave Complete";
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

    public void AIDied (int score, Vector3 pos)
    {
        aisDiedThisRound++;
        currentKillStreak++;
        comboTimer = timeToLoseCombo;
        if (currentKillStreak == 5)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX("SFX_Combo1");
            currentComboMultiplier = 2;

            currentComboColor = coloursForComboText[1];
            GameObject go = Instantiate(floatingPoints, player.transform.position, Quaternion.identity);
            TextMesh textMesh = go.transform.GetChild(0).GetComponent<TextMesh>();
            textMesh.text = "x" + currentComboMultiplier.ToString();
            textMesh.color = currentComboColor;
            Destroy(go, 1.0f);
        }
        else if (currentKillStreak == 10)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX("SFX_Combo2");
            currentComboMultiplier = 3;

            currentComboColor = coloursForComboText[2];
            GameObject go = Instantiate(floatingPoints, player.transform.position, Quaternion.identity);
            TextMesh textMesh = go.transform.GetChild(0).GetComponent<TextMesh>();
            textMesh.text = "x" + currentComboMultiplier.ToString();
            textMesh.color = currentComboColor;
            Destroy(go, 1.0f);
        }
        else if (currentKillStreak == 15)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX("SFX_Combo3");
            currentComboMultiplier = 4;

            currentComboColor = coloursForComboText[3];
            GameObject go = Instantiate(floatingPoints, player.transform.position, Quaternion.identity);
            TextMesh textMesh = go.transform.GetChild(0).GetComponent<TextMesh>();
            textMesh.text = "x" + currentComboMultiplier.ToString();
            textMesh.color = currentComboColor;
            Destroy(go, 1.0f);
        }
        else if (currentKillStreak == 20)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX("SFX_Combo4");
            currentComboMultiplier = 5;

            currentComboColor = coloursForComboText[4];
            GameObject go = Instantiate(floatingPoints, player.transform.position, Quaternion.identity);
            TextMesh textMesh = go.transform.GetChild(0).GetComponent<TextMesh>();
            textMesh.text = "x" + currentComboMultiplier.ToString();
            textMesh.color = currentComboColor;
            Destroy(go, 1.0f);
        }
        else if (currentKillStreak == 25)
        {
            if (SoundManager.instance != null)
                SoundManager.instance.PlaySFX("SFX_Combo5");
            currentComboMultiplier = 6;

            currentComboColor = coloursForComboText[5];
            GameObject go = Instantiate(floatingPoints, player.transform.position, Quaternion.identity);
            TextMesh textMesh = go.transform.GetChild(0).GetComponent<TextMesh>();
            textMesh.text = "x" + currentComboMultiplier.ToString();
            textMesh.color = currentComboColor;
            Destroy(go, 1.0f);
        }

        currentScore += (score * currentComboMultiplier);


        GameObject go2 = Instantiate(floatingPoints, pos, Quaternion.identity);
        TextMesh textMesh2 = go2.transform.GetChild(0).GetComponent<TextMesh>();
        textMesh2.color = currentComboColor;
        textMesh2.text = "+" + (score * currentComboMultiplier).ToString();
        Destroy(go2, 1.0f);

        UIManager.instance.UpdateScore(currentScore);
        if (aisDiedThisRound >= aisToBeSpawnedThisRound)
        {
            EndRound();
        }
    }

    private void FixedUpdate()
    {
        if (comboTimer <= 0)
        {
            currentKillStreak = 0;
            currentComboMultiplier = 1;
            currentComboColor = coloursForComboText[0];
        }
        else
        {
            comboTimer -= Time.fixedDeltaTime;
        }
    }
}

[System.Serializable]
public struct AISpawnParams 
{
    public AI ai;
    public int roundWhenStartSpawning;
}
