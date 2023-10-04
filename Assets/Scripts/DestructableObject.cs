using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    public GameObject breakVfx;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Instantiate(breakVfx, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }
}
