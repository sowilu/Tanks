using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    public float speedIncrease = 1.2f;
    public float time = 15;
    
    private Player player;
    private float oldSpeed;
    
    private void OnCollisionEnter(Collision other)
    {
        print(other.gameObject.name);
        if (other.gameObject.CompareTag("Tank"))
        {
            //teleport out of view
            transform.position = new Vector3(0, 100, 0);
            
            player = other.gameObject.GetComponent<Player>();
            oldSpeed = player.moveSpeed;

            StartCoroutine(ChangeSpeed());
        }
    }

    IEnumerator ChangeSpeed()
    {
        player.moveSpeed *= speedIncrease;
        yield return new WaitForSeconds(time);
        player.moveSpeed = oldSpeed;
        Destroy(gameObject);
    }
}
