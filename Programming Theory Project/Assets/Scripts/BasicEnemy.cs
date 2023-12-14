using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : Ship
{
    float _speed = 2;
    [SerializeField]
    float _shootTime = 1.0f;
    Timer _timer;

    public void Initialize(PooledObjectName name)
    {
        SetHP(GameConstants.BasicEnemyHP);
        ObjectName = name;
        _timer = gameObject.AddComponent<Timer>();
        _timer.AddEventListener(TimerFinishedHandler);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(CalculateNextPosition());
        
    }
    private void OnEnable()
    {
        _timer?.StartTimer(_shootTime);
    }
    private void OnDisable()
    {
        _timer?.StopTimer();
    }
    protected virtual Vector3 CalculateNextPosition()
    {
        return _speed * Time.deltaTime * Vector3.up;
    }

    protected override Vector3 CalculateBulletPos()
    {
        Vector3 bulletPos = transform.position;
        bulletPos.x += -0.5f; // GET RID OF MAGIC NUMBER
        return bulletPos;
    }

    private void TimerFinishedHandler()
    {
        if (_timer != null)
        {
            Shoot();
            Bullet.StartMoving(Vector3.left);
            _timer.StartTimer(_shootTime);
        }
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
