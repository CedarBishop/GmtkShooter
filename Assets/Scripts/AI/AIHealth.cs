using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : HealthSystem
{
    private AI ai;
    private int score;

    protected override void Death()
    {
        ai = GetComponent<AI>();
        score = ai.score;
        GameManager.instance.waveSystem.AIDied(score);
        base.Death();
    }
}
