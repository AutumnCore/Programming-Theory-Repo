using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int _hp;
    private Bullet _bullet;
    private PooledObjectName _objectName;

    // ENCAPSULATION
    protected Bullet Bullet { get { return _bullet; } private set { _bullet = value; } }
    protected virtual int HP { get { return _hp; } private set { _hp = value; } }
    protected virtual PooledObjectName ObjectName { get { return _objectName; } set { _objectName = value; } }

    protected virtual void SetHP(int hp) => HP = hp;

    // ABSTRACTION
    protected void TakeDamage(int damage)
    {
        HP -= damage;
        CheckForDeath();
    }
    // ABSTRACTION
    protected virtual void CheckForDeath()
    {
        if (_hp <= 0) { Die(); }
    }

    // ABSTRACTION
    protected virtual void Die()
    {
        ObjectPool.ReturnEnemy(gameObject, ObjectName);
    }

    // ABSTRACTION
    protected virtual void Shoot()
    {
        GameObject bullet = ObjectPool.GetBullet();
        bullet.transform.position = CalculateBulletPos();
        bullet.SetActive(true);
        Bullet = bullet.GetComponent<Bullet>();
    }

    // ABSTRACTION
    protected virtual Vector3 CalculateBulletPos()
    {
        Vector3 bulletPos = transform.position;
        return bulletPos;
    }
}
