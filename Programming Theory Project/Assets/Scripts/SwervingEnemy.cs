using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class SwervingEnemy : BasicEnemy
{
    [SerializeField]
    int amplitudeModifier;
   
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

    // POLYMORPHISM
    protected override Vector3 CalculateNextPosition()
    {
        Vector3 next = base.CalculateNextPosition();
        float x = Mathf.Sin(transform.position.x) / amplitudeModifier;
        next.x = x;
        return next;
    }
}
