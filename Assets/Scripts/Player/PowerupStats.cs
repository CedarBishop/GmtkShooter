using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerup Effect Amounts")]
public class PowerupStats : ScriptableObject
{
    [Header("Buff Amounts")]
    public int multiShotIncrease;
    public int bulletScaleIncrease;
    public float rapidFireMultiplier;
    public int lightningEnemyCount;
    public int damageIncrease;
    public int sunExplodeBulletIncrease;

    [Header("Nerf Amounts")]
    public float knockbackIncrease;
    public float knockbackAngleIncrease;
    public float zigzagAngleIncrease;
    public float bulletDeviationIncrease;
    public int healthToEnemyIncrease;
    public float enemySpeedIncreaseOnHit;
}
