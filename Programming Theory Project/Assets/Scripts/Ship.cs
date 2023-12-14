using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int _hp;
    private Bullet _bullet;
    private PooledObjectName _objectName;

    protected Bullet Bullet { get { return _bullet; } private set { _bullet = value; /*Debug.Log("Set a new bullet");*/ } }
    protected virtual int HP { get { return _hp; } private set { _hp = value; } }
    protected virtual PooledObjectName ObjectName { get { return _objectName; } set { _objectName = value; } }

    protected virtual void SetHP(int hp) => HP = hp;

    protected void TakeDamage(int damage)
    {
        HP -= damage;
        CheckForDeath();
    }
    protected virtual void CheckForDeath()
    {
        if (_hp <= 0) { Die(); }
    }

    protected virtual void Die()
    {
        ObjectPool.ReturnEnemy(gameObject, ObjectName);
    }

    protected virtual void Shoot()
    {
        GameObject bullet = ObjectPool.GetBullet();
        bullet.transform.position = CalculateBulletPos();
        bullet.SetActive(true);
        Bullet = bullet.GetComponent<Bullet>();
    }

    protected virtual Vector3 CalculateBulletPos()
    {
        Vector3 bulletPos = transform.position;
        return bulletPos;
    }
}
