using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Transform gunOriginTransform;
    public Bullet bulletPrefab;
    public float timeBetweenShots;
    public float cameraShakeDuration;
    public float cameraShakeMagnitude;
    public float sprayAmount;
    public int bulletsPerShot = 1;
    public int damage;
    public float knockBackAmount;

    private PlayerInput playerInput;
    private Camera mainCamera;
    private CameraShake cameraShake;
    private PlayerMovement playerMovement;

    private bool isGamepad;
    private bool canShoot;
    private bool isHoldingShootButton;


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
        Vector2 aim = (value.Get<Vector2>()).normalized;
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
                bullet.damage = damage;
                zRotation += sprayAmount;

            }
            gunOriginTransform.rotation = Quaternion.Euler(0, 0, originalZRotation);
        }
        else
        {
            Bullet bullet = Instantiate(bulletPrefab,
                   bulletSpawnPoint.position,
                   gunOriginTransform.rotation);
            bullet.damage = damage;
        }        
        

        if (cameraShake != null)
        {
            cameraShake.StartShake(cameraShakeDuration, cameraShakeMagnitude);
        }

        if (playerMovement != null)
        {
            playerMovement.Knockback(gunOriginTransform.right, knockBackAmount);
        }

        StartCoroutine("DelayShoot");
    }

    IEnumerator DelayShoot ()
    {
        canShoot = false;
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }
}
