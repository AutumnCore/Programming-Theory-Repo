using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    public const float ZPosition = -1.0f;

    // experimantally determined values for keeping the player inside screen, have to be redetermined if size of the player changes
    public const float ColliderPlayer = 1.2f;

    public const int InitialPlayerHP = 100;
    public const int BasicEnemyHP = 1;

    public const float XShipBulletOffset = .6f;
    public const float YShipBulletOffset = 0.05f;
    public const float XEnemyBulletOffset = 0.5f;

    public const float BulletSpeed = 500.0f;

    public const int BulletDamage = 5;
    public const int ShipCollisionDamage = 10;

    public const int InitialBulletPoolCapacity = 100;
    public const int InitialEnemyPoolCapacity = 10;

    public const float SpawnDelay = 1.0f;
    public const float XSpawnPosition = 10.0f;
}
