using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Ship
{
    float _speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        SetHP(GameConstants.InitialBasicEnemyHP); 
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    protected virtual void Move()
    {
        transform.Translate(_speed * Time.deltaTime * Vector3.up);
    }

    public void Initialize()
    {

    }

    public void Deactivate()
    {

    }
}
