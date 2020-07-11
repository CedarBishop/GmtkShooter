using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buff {Multishot, Scale, RapidFire, Lightning, DamageUp, SunExplode  }
public enum Nerf {Knockback, Zigzag, Inaccuracy, HealEnemies, MoveToAim, InverseControl, BlurScreen, EnemySpeedUp  }
public class PowerupSystem : MonoBehaviour
{
    public List<PowerUp> powerups = new List<PowerUp>();
    public List<float> powerupTimers = new List<float>();

    [Header("Buff Amounts")]
    public int multiShotIncrease;
    public int bulletScaleIncrease;
    public float rapidFireMultiplier;
    public int lightningEnemyCount;
    public int damageIncrease;
    public int sunExplodeBulletIncrease;

    [Header("Nerf Amounts")]
    public float knockbackIncrease;
    public float zigzagAngleIncrease;
    public float bulletDeviationIncrease;
    public int healthToEnemyIncrease;
    public float enemySpeedIncreaseOnHit;


    private PlayerMovement playerMovement;
    private PlayerShoot playerShoot;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerShoot = GetComponent<PlayerShoot>();
    }

    void Update()
    {
        if (powerupTimers != null)
        {
            for (int i = 0; i < powerupTimers.Count; i++)
            {
                powerupTimers[i] -= Time.deltaTime;
                if (powerupTimers[i] <= 0.0f)
                {
                    LosePowerup(i);
                }
            }
        }        
    }

    public void GainPowerup(Buff[] buffs, Nerf[] nerfs, float timeLastsfor)
    {
        PowerUp powerUp = new PowerUp() { buff = buffs[Random.Range(0, buffs.Length)], nerf = nerfs[Random.Range(0, nerfs.Length)]};

        switch (powerUp.buff)
        {
            case Buff.Multishot:
                playerShoot.bulletsPerShot += 1;
                break;
            case Buff.Scale:
                playerShoot.bulletScale += 1.0f;
                break;
            case Buff.RapidFire:
                playerShoot.fireRate /= rapidFireMultiplier;
                break;
            case Buff.Lightning:
                playerShoot.lightningBounceAmount += lightningEnemyCount;
                break;
            case Buff.DamageUp:
                playerShoot.damage += damageIncrease;
                break;
            case Buff.SunExplode:
                playerShoot.sunExplodeBulletAmount += sunExplodeBulletIncrease;
                break;
            default:
                break;
        }

        switch (powerUp.nerf)
        {
            case Nerf.Knockback:
                playerShoot.knockBackAmount += knockbackIncrease;
                break;
            case Nerf.Zigzag:
                playerShoot.redirectAngle += zigzagAngleIncrease;
                break;
            case Nerf.Inaccuracy:
                playerShoot.bulletDeviation += bulletDeviationIncrease;
                break;
            case Nerf.HealEnemies:
                playerShoot.enemyHealAmount += healthToEnemyIncrease;
                break;
            case Nerf.MoveToAim:
                playerMovement.moveToAimReferences++;
                break;
            case Nerf.InverseControl:
                playerMovement.inverseControlReferences++;
                break;
            case Nerf.BlurScreen:
                break;
            case Nerf.EnemySpeedUp:
                playerShoot.enemySpeedupAmount += enemySpeedIncreaseOnHit;
                break;
            default:
                break;
        }


        powerups.Add(powerUp);
        powerupTimers.Add(timeLastsfor);
    }

    public void LosePowerup (int index)
    {
        switch (powerups[index].buff)
        {
            case Buff.Multishot:
                playerShoot.bulletsPerShot -= 1;
                break;
            case Buff.Scale:
                playerShoot.bulletScale -= 1.0f;
                break;
            case Buff.RapidFire:
                playerShoot.fireRate *= rapidFireMultiplier;
                break;
            case Buff.Lightning:
                playerShoot.lightningBounceAmount -= lightningEnemyCount;
                break;
            case Buff.DamageUp:
                playerShoot.damage -= damageIncrease;
                break;
            case Buff.SunExplode:
                playerShoot.sunExplodeBulletAmount -= sunExplodeBulletIncrease;
                break;
            default:
                break;
        }

        switch (powerups[index].nerf)
        {
            case Nerf.Knockback:
                playerShoot.knockBackAmount -= knockbackIncrease;
                break;
            case Nerf.Zigzag:
                playerShoot.redirectAngle -= zigzagAngleIncrease;
                break;
            case Nerf.Inaccuracy:
                playerShoot.bulletDeviation -= bulletDeviationIncrease;
                break;
            case Nerf.HealEnemies:
                playerShoot.enemyHealAmount -= healthToEnemyIncrease;
                break;
            case Nerf.MoveToAim:
                playerMovement.moveToAimReferences--;
                break;
            case Nerf.InverseControl:
                playerMovement.inverseControlReferences--;
                break;
            case Nerf.BlurScreen:
                break;
            case Nerf.EnemySpeedUp:
                playerShoot.enemySpeedupAmount -= enemySpeedIncreaseOnHit;
                break;
            default:
                break;
        }

        powerups.RemoveAt(index);
        powerupTimers.RemoveAt(index);
    }

}

[System.Serializable]
public struct PowerUp
{
    public Buff buff;
    public Nerf nerf;
}
