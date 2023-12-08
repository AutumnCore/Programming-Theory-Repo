using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Ship
{
    float _speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        SetHP(1); //   GET RID OF MAGIC NUMBER!!!
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(5);  // HOIST MAGIC NUMBER TO GAMECONSTANTS!!!
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            TakeDamage(10); // HOIST MAGIC NUMBER TO GAMECONSTANTS!!!
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
