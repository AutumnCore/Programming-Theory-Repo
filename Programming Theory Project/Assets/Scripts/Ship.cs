using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    private int _hp;
    protected virtual int HP { get { return _hp; } private set { _hp = value; } }
    protected virtual void SetHP(int hp) => HP = hp;
    protected void TakeDamage(int damage)
    {
        HP -= damage;
        CheckForDeath();
    }
    protected virtual void CheckForDeath()
    {
        if (_hp <= 0) { Debug.Log(gameObject.name + " just died"); }
    }
    protected void Shoot()
    {
        // Not implemented yet
        Debug.Log(gameObject.name + " has fired a shot");
    }
}
