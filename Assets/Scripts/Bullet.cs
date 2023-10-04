using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public Vector2Int damageLimits = new(10, 20);
    public float lifetime = 5f;
    public GameObject explotionVfx;
    private Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 1000f);
        
        Invoke(nameof(Explode), lifetime);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tank") || other.gameObject.CompareTag("Destructable"))
        {
            var health = other.gameObject.GetComponent<Health>();

            if (health != null)
            {
                var damage = Random.Range(damageLimits.x, damageLimits.y);
                health.TakeDamage(damage);
            }
            Explode();
        } 
    }

    void Explode()
    {
        Instantiate(explotionVfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
