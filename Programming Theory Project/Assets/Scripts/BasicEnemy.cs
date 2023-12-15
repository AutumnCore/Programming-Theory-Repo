using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BasicEnemy : Ship
{
    float _speed = 2;
    [SerializeField]
    float _shootTime = 1.5f;
    Timer _timer;
    // ENCAPSULATION
    protected virtual float ShootTime { get => _shootTime; set => _shootTime = value; } // not auto implemented because wanted to change this value in inspector
    protected virtual Vector3 DirectionToShoot { get; set; }

    public virtual void Initialize(PooledObjectName name)
    {
        SetHP(GameConstants.BasicEnemyHP);
        ObjectName = name;
        DirectionToShoot = Vector3.left; 
        _timer = gameObject.AddComponent<Timer>();
        _timer.AddTimerFinishedEventListener(TimerFinishedEventHandler);
        EventsMediator.AddPlayerDiedListener(PlayerDiedEventHandler);
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

    // POLYMORPHISM
    protected override Vector3 CalculateBulletPos()
    {
        Vector3 bulletPos = transform.position;
        bulletPos.x -= GameConstants.XEnemyBulletOffset; 
        return bulletPos;
    }

    protected virtual void TimerFinishedEventHandler()
    {
        if (_timer != null)
        {
            Shoot();
            Bullet.StartMoving(DirectionToShoot);
            _timer.StartTimer(_shootTime);
        }
    }

    public void PlayerDiedEventHandler()
    {
        Die();
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
