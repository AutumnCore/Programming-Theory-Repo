using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants
{
    // experimantally determined values for keeping the player inside screen, have to be redetermined if size of the player changes
    public const float ColliderPlayer = 1.2f;

    public const int InitialPlayerHP = 100;
    public const int BasicEnemyHP = 1;

    public const int BulletDamage = 5;
    public const int ShipCollisionDamage = 10;

    public const int InitialBulletPoolCapacity = 100;
    public const int InitialEnemyPoolCapacity = 15;
}
