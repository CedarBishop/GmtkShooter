using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Powerups {Double, Tripple }
public class PowerupSystem : MonoBehaviour
{
    public List<Powerups> powerups = new List<Powerups>();
    public List<float> powerupTimers = new List<float>();
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

    public void GainPowerup (Powerups power, float timeLastsfor)
    {
        powerups.Add(power);
        powerupTimers.Add(timeLastsfor);
    }

    public void LosePowerup (int index)
    {
        powerups.RemoveAt(index);
        powerupTimers.RemoveAt(index);

    }

}
