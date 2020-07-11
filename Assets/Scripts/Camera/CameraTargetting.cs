using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTargetting : MonoBehaviour
{
    public float maxRadiusFromPlayer;
    public Transform playerPos;
    private Vector2 mousePos;
    private float dist;

    private PlayerInput input;
    private PlayerShoot playerShoot;
    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
        playerShoot = FindObjectOfType<PlayerShoot>();
    }

    void Update()
    {
        // Gamepad camera controls
        if (input.currentControlScheme == "Gamepad")
        {
            transform.position = playerPos.position;

            transform.position += playerPos.GetChild(0).right + (playerShoot.controllerAim * maxRadiusFromPlayer);
        }     

        // Keyboard camera controls
        else
        {
            transform.position = playerPos.position;

            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dist = Vector2.Distance(playerPos.position, mousePos);  // distance from player to mouse pos

            if (dist > maxRadiusFromPlayer)
            {
                transform.position += playerPos.GetChild(0).transform.right * maxRadiusFromPlayer;
            }
            else
            {
                transform.position += playerPos.GetChild(0).transform.right * dist;
            }
        }
    }
}
