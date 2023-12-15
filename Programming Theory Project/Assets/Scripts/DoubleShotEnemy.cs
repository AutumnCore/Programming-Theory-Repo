using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class DoubleShotEnemy : BasicEnemy
{
    bool _shotUp = false;
    float _verticalBulletOffset = 0.1f;
    float _verticalBulletDirection = 0.25f;
    [SerializeField]
    float _doubleShotShootTime = 2f;

    
    void Update()
    {
        transform.Translate(CalculateNextPosition());
    }

    // POLYMORPHISM
    public override void Initialize(PooledObjectName name)
    {
        base.Initialize(name);
        ShootTime = _doubleShotShootTime;
    }

    // POLYMORPHISM
    protected override Vector3 CalculateBulletPos()
    {
        return  base.CalculateBulletPos() + (_shotUp ? new Vector3(0, _verticalBulletOffset, 0) : new Vector3(0, -_verticalBulletOffset, 0));
    }

    // POLYMORPHISM
    protected override void TimerFinishedEventHandler()
    {
        DirectionToShoot = CalculateDirectionToShoot();
        base.TimerFinishedEventHandler();
        _shotUp = !_shotUp;
        DirectionToShoot = CalculateDirectionToShoot();
        base.TimerFinishedEventHandler();
    }

    // ABSTRACTION
    private Vector3 CalculateDirectionToShoot() 
    { 
        return new Vector3(-1, _shotUp ? _verticalBulletDirection : -_verticalBulletDirection, 0);
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
