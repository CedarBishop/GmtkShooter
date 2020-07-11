﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform gunOriginTransform;
    public Bullet bulletPrefab;
    public float fireRate;
    public float cameraShakeDuration;
    public float cameraShakeMagnitude;
    public float sprayAmount;
    public int damage;
    public float force;

    public Vector3 controllerAim;
 
    [HideInInspector] public bool isHoldingShootButton;
    
    [Header("Buff Stats")]
    public int bulletsPerShot = 1;
    public float bulletScale;
    public int lightningBounceAmount;
    public int sunExplodeBulletAmount;

    [Header("Nerf Stats")]
    public float knockBackAmount;
    public float knockbackAngle;
    public float redirectAngle;
    public float redirectTime;
    public float bulletDeviation;
    public int enemyHealAmount;
    public float enemySpeedupAmount;

    private PlayerInput playerInput;
    private Camera mainCamera;
    private CameraShake cameraShake;
    private PlayerMovement playerMovement;

    private bool isGamepad;
    private bool canShoot;


    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
        mainCamera = Camera.main;
        cameraShake = mainCamera.GetComponent<CameraShake>();
        isGamepad = (playerInput.currentControlScheme == "Gamepad");
        canShoot = true;
    }

    void OnAim (InputValue value)
    {
        controllerAim = (value.Get<Vector2>());
        Vector2 aim = controllerAim.normalized;
        if (Mathf.Abs(aim.x) > 0.25f || Mathf.Abs(aim.y) > 0.25f)
        {
            gunOriginTransform.right = aim;
        }
    }

    private void Update()
    {
        if (isGamepad == false)
        {
            Vector2 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            gunOriginTransform.right = (direction).normalized;
        }
    }

    void FixedUpdate()
    {
        if (isHoldingShootButton)
        {
            Shoot();
        }
    }

    void OnStartShoot()
    {
        isHoldingShootButton = true;
    }

    void OnEndShoot()
    {
        isHoldingShootButton = false;
    }

    void Shoot ()
    {
        if (canShoot == false)
        {
            return;
        }

        if (bulletsPerShot > 1)
        {
            float originalZRotation = gunOriginTransform.rotation.eulerAngles.z;
            float zRotation = originalZRotation - ((bulletsPerShot / 2) * sprayAmount);
            for (int i = 0; i < bulletsPerShot; i++)
            {
                gunOriginTransform.rotation = Quaternion.Euler(0, 0, zRotation);
                Bullet bullet = Instantiate(bulletPrefab,
                    bulletSpawnPoint.position,
                    gunOriginTransform.rotation);

                bullet.Initialise(damage,force,redirectAngle,redirectTime, bulletDeviation);
                bullet.transform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
                zRotation += sprayAmount;

            }
            gunOriginTransform.rotation = Quaternion.Euler(0, 0, originalZRotation);
        }
        else
        {
            Bullet bullet = Instantiate(bulletPrefab,
                   bulletSpawnPoint.position,
                   gunOriginTransform.rotation);

            bullet.Initialise(damage, force, redirectAngle, redirectTime, bulletDeviation);
            bullet.transform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
        }        
        

        if (cameraShake != null)
        {
            cameraShake.StartShake(cameraShakeDuration, cameraShakeMagnitude);
        }

        if (playerMovement != null)
        {
            Vector3 addedRotation = new Vector3(0,0, Random.Range(-knockbackAngle, knockbackAngle));
            playerMovement.Knockback(gunOriginTransform.right + addedRotation, knockBackAmount);
        }

        StartCoroutine("DelayShoot");
    }

    IEnumerator DelayShoot ()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
