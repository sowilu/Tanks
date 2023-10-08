using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;
    public float bodyRotationSpeed = 3f;
    
    public Transform turret;
    public Transform body;
    
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public AudioClip shootSound;
    public float cooldown = 0.5f;
    
    private float lastShot = 0f;
    private Vector2 movementInput;
    private Vector2 rotationInput;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        var renderer = turret.gameObject.GetComponent<Renderer>();
        //check which player this is
        if(GameManager.instance.playerCount == 1)
            renderer.material.color = GameManager.instance.player1;
        else
            renderer.material.color = GameManager.instance.player2;
    }

    void Update()
    {
        //move relative to turret's rotation
        var input = new Vector3(movementInput.x, 0, movementInput.y);
        var direction = (input.x * turret.right + input.z * turret.forward);
        transform.position +=  direction * moveSpeed * Time.deltaTime;
        
        //rotate body smoothly
        if (direction != Vector3.zero)
        {
            var targetRotation = Quaternion.LookRotation(direction);
            body.rotation = Quaternion.Slerp(body.rotation, targetRotation, bodyRotationSpeed * Time.deltaTime);
        }
        
        //rotate turret
        float rotation = rotationInput.x * rotationSpeed * Time.deltaTime;
        turret.Rotate(Vector3.up, rotation);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        movementInput = value.ReadValue<Vector2>(); 
    }
    
    public void OnLook(InputAction.CallbackContext value)
    {
        rotationInput = value.ReadValue<Vector2>();
    }
    
    public void OnShoot(InputAction.CallbackContext value)
    {
        if (Time.time > lastShot + cooldown)
        { 
            audioSource.PlayOneShot(shootSound, 5f);
            Instantiate(bulletPrefab, shootingPoint.position, turret.rotation);
            
            lastShot = Time.time;
        }
    }
}
