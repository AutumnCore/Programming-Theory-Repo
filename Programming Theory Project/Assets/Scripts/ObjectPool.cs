using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Provides object pooling for bullets and enemies
/// </summary>
public class ObjectPool : MonoBehaviour
{
    static GameObject prefabBullet;
    static GameObject prefabBasicEnemy;
    static GameObject prefabSwervingEnemy;
    static GameObject prefabDoubleShotEnemy;
    static Dictionary<PooledObjectName, List<GameObject>> pools;


    public static void Initialize()
    {
        prefabBullet = Resources.Load<GameObject>("Prefabs/Bullet");
        prefabBasicEnemy = Resources.Load<GameObject>("Prefabs/BasicEnemy");
        prefabSwervingEnemy = Resources.Load<GameObject>("Prefabs/SwervingEnemy");
        prefabDoubleShotEnemy = Resources.Load<GameObject>("Prefabs/DoubleShotEnemy");

        // initialize dictionary
        pools = new Dictionary<PooledObjectName, List<GameObject>>
        {
            {
                PooledObjectName.Bullet,
                new List<GameObject>(GameConstants.InitialBulletPoolCapacity)
            },
            {
                PooledObjectName.BasicEnemy,
                new List<GameObject>(GameConstants.InitialEnemyPoolCapacity)
            },
            {
                PooledObjectName.SwervingEnemy,
                new List<GameObject>(GameConstants.InitialEnemyPoolCapacity)
            },
            {
                PooledObjectName.DoubleShotEnemy,
                new List<GameObject>(GameConstants.InitialEnemyPoolCapacity)
            }
        };

        // fill bullet pool
        for (int i = 0; i < pools[PooledObjectName.Bullet].Capacity; i++)
        {
            pools[PooledObjectName.Bullet].Add(GetNewObject(PooledObjectName.Bullet));
        }
        // fill enemy pool
        for (int i = 0; i < pools[PooledObjectName.BasicEnemy].Capacity; i++)
        {
            pools[PooledObjectName.BasicEnemy].Add(GetNewObject(PooledObjectName.BasicEnemy));
        }
        for (int i = 0; i < pools[PooledObjectName.SwervingEnemy].Capacity; i++)
        {
            pools[PooledObjectName.SwervingEnemy].Add(GetNewObject(PooledObjectName.SwervingEnemy));
        }
        for (int i = 0; i < pools[PooledObjectName.DoubleShotEnemy].Capacity; i++)
        {
            pools[PooledObjectName.DoubleShotEnemy].Add(GetNewObject(PooledObjectName.DoubleShotEnemy));
        }
    }

    
    public static GameObject GetBullet()
    {
        return GetPooledObject(PooledObjectName.Bullet);
    }

    
    public static GameObject GetEnemy(PooledObjectName enemyType)
    {
        return GetPooledObject(enemyType);
    }

    // ABSTRACTION
    static GameObject GetPooledObject(PooledObjectName name)
    {
        List<GameObject> pool = pools[name];

        // check for available object in pool
        if (pool.Count > 0)
        {
            GameObject gameObject = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return gameObject;
        }
        else
        {
            pool.Capacity++;
            return GetNewObject(name);
        }
    }

    
    public static void ReturnBullet(GameObject bullet)
    {
        ReturnPooledObject(PooledObjectName.Bullet, bullet);
    }

    
    public static void ReturnEnemy(GameObject enemy, PooledObjectName enemyType)
    {
        ReturnPooledObject(enemyType, enemy);
    }

    // ABSTRACTION
    public static void ReturnPooledObject(PooledObjectName name, GameObject obj)
    {
        obj.SetActive(false);
        switch (name)
        {
            case PooledObjectName.Bullet:
                obj.GetComponent<Bullet>().StopMoving();
                break;
            case PooledObjectName.BasicEnemy:
                obj.GetComponent<BasicEnemy>().Deactivate();
                break;
            case PooledObjectName.SwervingEnemy:
                obj.GetComponent<BasicEnemy>().Deactivate();  // I wonder if it will work, if yes then we can change switch statement to if-else
                break;
            case PooledObjectName.DoubleShotEnemy:
                obj.GetComponent<BasicEnemy>().Deactivate();
                break;
        }
            
        pools[name].Add(obj);
    }

    // ABSTRACTION
    static GameObject GetNewObject(PooledObjectName name)
    {
        GameObject obj;

        switch (name)
        {
            case PooledObjectName.Bullet:
                obj = GameObject.Instantiate(prefabBullet);
                obj.GetComponent<Bullet>().Initialize();
                break; 
            case PooledObjectName.BasicEnemy:
                obj = GameObject.Instantiate(prefabBasicEnemy);
                obj.GetComponent<BasicEnemy>().Initialize();
                break;
            case PooledObjectName.SwervingEnemy:
                obj = GameObject.Instantiate(prefabSwervingEnemy);
                obj.GetComponent<SwervingEnemy>().Initialize();
                break;
            case PooledObjectName.DoubleShotEnemy:
                obj = GameObject.Instantiate(prefabDoubleShotEnemy);
                obj.GetComponent<DoubleShotEnemy>().Initialize();
                break;
            default:obj=null; break;
        }

        obj.SetActive(false);
        GameObject.DontDestroyOnLoad(obj);
        return obj;
    }

    public static void EmptyPools()
    {
        foreach (var name in Enum.GetValues(typeof(PooledObjectName)).Cast<PooledObjectName>())
        {
            pools[name].Clear();
        }
    }

}
