using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShotEnemy : BasicEnemy
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(CalculateNextPosition());
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
        else if (other.gameObject.CompareTag("Border"))
        {
            Die();
        }
    }
 

}
