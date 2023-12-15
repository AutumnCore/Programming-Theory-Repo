using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ObjectPool.Initialize();
        StartCoroutine(SpawnCoroutine());
        EventsMediator.AddPlayerDiedListener(PlayerDiedEventHandler);
    }

    IEnumerator SpawnCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(GameConstants.SpawnDelay);
            GameObject enemy = ObjectPool.GetEnemy(ChooseRandomEnemy());
            enemy.transform.position = ChooseRandomPosition();
            enemy.SetActive(true);
        }
    }

    private Vector3 ChooseRandomPosition()
    {
        return new Vector3(GameConstants.XSpawnPosition, UnityEngine.Random.Range(-3.8f, 5.5f), GameConstants.ZPosition);
    }

    private PooledObjectName ChooseRandomEnemy()
    {
        int randomEnemy = UnityEngine.Random.Range(1,4);
        return (PooledObjectName)randomEnemy;
    }

    private void PlayerDiedEventHandler()
    {
        StopAllCoroutines();
    }
}
