using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Buff {Multishot, Scale, RapidFire, Lightning, SunExplode  }
public enum Nerf {Knockback, Zigzag, Inaccuracy, HealEnemies, MoveToAim, InverseControl, BlurScreen, EnemySpeedUp  }
public class PowerupSystem : MonoBehaviour
{
    public List<PowerUp> powerups = new List<PowerUp>();
    public List<float> powerupTimers = new List<float>();

    public PowerupStats powerupStats;

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

    public void GainPowerup(BuffData[] buffs, NerfData[] nerfs, float timeLastsfor)
    {
        PowerUp powerUp = new PowerUp() { buff = buffs[Random.Range(0, buffs.Length)], nerf = nerfs[Random.Range(0, nerfs.Length)]};

        switch (powerUp.buff.buff)
        {
            case Buff.Multishot:
                playerShoot.bulletsPerShot += powerupStats.multiShotIncrease;
                break;
            case Buff.Scale:
                playerShoot.bulletScale += powerupStats.bulletScaleIncrease;
                playerShoot.damage += powerupStats.damageIncrease;
                break;
            case Buff.RapidFire:
                playerShoot.fireRate /= powerupStats.rapidFireMultiplier;
                break;
            case Buff.Lightning:
                playerShoot.lightningBounceAmount += powerupStats.lightningEnemyCount;
                break;
            case Buff.SunExplode:
                playerShoot.sunExplodeBulletAmount += powerupStats.sunExplodeBulletIncrease;
                break;
            default:
                break;
        }

        switch (powerUp.nerf.nerf)
        {
            case Nerf.Knockback:
                playerShoot.knockBackAmount += powerupStats.knockbackIncrease;
                break;
            case Nerf.Zigzag:
                playerShoot.redirectAngle += powerupStats.zigzagAngleIncrease;
                break;
            case Nerf.Inaccuracy:
                playerShoot.bulletDeviation += powerupStats.bulletDeviationIncrease;
                break;
            case Nerf.HealEnemies:
                playerShoot.enemyHealAmount += powerupStats.healthToEnemyIncrease;
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
                playerShoot.enemySpeedupAmount += powerupStats.enemySpeedIncreaseOnHit;
                break;
            default:
                break;
        }

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_PickUp");

        powerups.Add(powerUp);
        powerupTimers.Add(timeLastsfor);
        UIManager.instance.CreateBuffBadge(powerUp.buff.badgeColor, powerUp.buff.buffIcon, timeLastsfor);
        UIManager.instance.CreateNerfBadge(powerUp.nerf.badgeColor, powerUp.nerf.nerfIcon, timeLastsfor);
    }

    public void LosePowerup (int index)
    {
        switch (powerups[index].buff.buff)
        {
            case Buff.Multishot:
                playerShoot.bulletsPerShot -= powerupStats.multiShotIncrease;
                break;
            case Buff.Scale:
                playerShoot.bulletScale -= powerupStats.bulletScaleIncrease;
                playerShoot.damage -= powerupStats.damageIncrease;
                break;
            case Buff.RapidFire:
                playerShoot.fireRate *= powerupStats.rapidFireMultiplier;
                break;
            case Buff.Lightning:
                playerShoot.lightningBounceAmount -= powerupStats.lightningEnemyCount;
                break;
            case Buff.SunExplode:
                playerShoot.sunExplodeBulletAmount -= powerupStats.sunExplodeBulletIncrease;
                break;
            default:
                break;
        }

        switch (powerups[index].nerf.nerf)
        {
            case Nerf.Knockback:
                playerShoot.knockBackAmount -= powerupStats.knockbackIncrease;
                break;
            case Nerf.Zigzag:
                playerShoot.redirectAngle -= powerupStats.zigzagAngleIncrease;
                break;
            case Nerf.Inaccuracy:
                playerShoot.bulletDeviation -= powerupStats.bulletDeviationIncrease;
                break;
            case Nerf.HealEnemies:
                playerShoot.enemyHealAmount -= powerupStats.healthToEnemyIncrease;
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
                playerShoot.enemySpeedupAmount -= powerupStats.enemySpeedIncreaseOnHit;
                break;
            default:
                break;
        }

        if (SoundManager.instance != null)
            SoundManager.instance.PlaySFX("SFX_WornOff");

        powerups.RemoveAt(index);
        powerupTimers.RemoveAt(index);
    }

}

[System.Serializable]
public struct PowerUp
{
    public BuffData buff;
    public NerfData nerf;
}