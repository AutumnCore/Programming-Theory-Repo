using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShotEnemy : BasicEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    protected override void Die()
    {
        ObjectPool.ReturnEnemy(gameObject, PooledObjectName.DoubleShotEnemy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(GameConstants.BulletDamage);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(GameConstants.ShipCollisionDamage);
        }
    }

    private void OnBecameInvisible()
    {
        Die();
    }
}
