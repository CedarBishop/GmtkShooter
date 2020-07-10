using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public Bullet bulletPrefab;
    public float timeBetweenShots;
    public float cameraShakeDuration;
    public float cameraShakeMagnitude;

    private PlayerInput playerInput;
    private Camera mainCamera;
    private CameraShake cameraShake;

    private bool isGamepad;
    private bool canShoot;
    private bool isHoldingShootButton;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
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
            transform.right = aim;
        }
    }

    private void Update()
    {
        if (isGamepad == false)
        {
            Vector2 direction = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.right = (direction).normalized;
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

        Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);

        if (cameraShake != null)
        {
            cameraShake.StartShake(cameraShakeDuration, cameraShakeMagnitude);
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
