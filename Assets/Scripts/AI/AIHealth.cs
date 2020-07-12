using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : HealthSystem
{
    private AI ai;
    private int score;
    public GameObject floatingPoints;

    protected override void Death()
    {
        ai = GetComponent<AI>();
        score = ai.score;
        floatingPoints.transform.GetChild(0).GetComponent<TextMesh>().text = "+" + score.ToString();
        Instantiate(floatingPoints, transform.position, Quaternion.identity);
        GameManager.instance.waveSystem.AIDied(score);
        base.Death();
    }
}
