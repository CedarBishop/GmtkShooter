using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : HealthSystem
{
    private AI ai;
    private int score;
    private void Start()
    {
        ai = GetComponent<AI>();
        score = ai.score;
    }

    protected override void Death()
    {
        GameManager.instance.waveSystem.AIDied(score);
        base.Death();
    }
}
