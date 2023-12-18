using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class Player : Ship
{
    [SerializeField]
    float _normalVerticalSpeed;
    [SerializeField]
    float _increasedVerticalSpeed;
    bool _fastSpeedEnabled = false;

    Timer _timer;
    [SerializeField]
    float _shootDelay;

    public event Action<int> PlayerHPChanged;
    public event Action PlayerDied;

    // Start is called before the first frame update
    void Start()
    {
        SetHP(GameConstants.InitialPlayerHP);
        EventsMediator.AddHealthChangedInvoker(this);
        EventsMediator.AddPlayerDiedInvoker(this);
        _timer = gameObject.AddComponent<Timer>();
        _timer.StartTimer(_shootDelay);
        _timer.AddTimerFinishedEventListener(TimerFinishedEventHandler);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveUpOrDown(true);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveUpOrDown(false);
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            _fastSpeedEnabled = true;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            _fastSpeedEnabled = false;
        }
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }*/
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(GameConstants.BulletDamage);
            PlayerHPChanged?.Invoke(HP);
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(GameConstants.ShipCollisionDamage);
            PlayerHPChanged?.Invoke(HP);
        }
    }

    // ABSTRACTION
    void MoveUpOrDown(bool moveUp)
    {
        transform.Translate(Time.deltaTime * (_fastSpeedEnabled ? _increasedVerticalSpeed : _normalVerticalSpeed) * (moveUp ? Vector3.up : Vector3.down), Space.World);
        StayInsideScreen();
    }
    // POLYMORPHISM
    protected override void Shoot()
    {
        base.Shoot();
        Bullet.StartMoving(Vector3.right);

    }
    // POLYMORPHISM
    protected override Vector3 CalculateBulletPos()
    {
        Vector3 bulletPos = transform.position;
        bulletPos.x += GameConstants.XShipBulletOffset;
        bulletPos.y += GameConstants.YShipBulletOffset;
        return bulletPos;
    }

    // ABSTRACTION
    private void StayInsideScreen()
    {
        Vector3 position = transform.position;
        if (position.y + GameConstants.ColliderPlayer > ScreenUtilities.ScreenTop)
        {
            position.y = ScreenUtilities.ScreenTop - GameConstants.ColliderPlayer;
        }
        else if (position.y - GameConstants.ColliderPlayer < ScreenUtilities.ScreenBottom)
        {
            position.y = ScreenUtilities.ScreenBottom + GameConstants.ColliderPlayer;
        }
        transform.position = position;
    }
    // POLYMORPHISM
    protected override void Die()
    {
        Debug.Log(gameObject.name + " just died");
        PlayerDied?.Invoke();
        _timer.StopTimer();
        this.enabled = false;
    }

    public void AddPlayerHPChangedListener(Action<int> listener)
    {
        PlayerHPChanged += listener;
    }

    public void AddPlayerDiedListener(Action listener)
    {
        PlayerDied += listener;
    }

    void TimerFinishedEventHandler()
    {
        Shoot();
        _timer.StartTimer(_shootDelay);
    }
}
