using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    public void Initialize() 
    {
        rb = GetComponent<Rigidbody>();
    }

    public void StartMoving(Vector3 direction)
    {
        rb.AddForce(direction * GameConstants.BulletSpeed);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        StopMoving();
        ObjectPool.ReturnBullet(gameObject);
    }
}
