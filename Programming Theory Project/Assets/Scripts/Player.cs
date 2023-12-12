using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship
{
    [SerializeField]
    float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        SetHP(100);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(GameConstants.BulletDamage);  
        }
        else if(other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(GameConstants.ShipCollisionDamage); 
        }
    }

    void MoveUpOrDown(bool moveUp)
    {
        transform.Translate(Time.deltaTime * verticalSpeed * (moveUp ? Vector3.up : Vector3.down), Space.World);
        StayInsideScreen();
    }

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
}
