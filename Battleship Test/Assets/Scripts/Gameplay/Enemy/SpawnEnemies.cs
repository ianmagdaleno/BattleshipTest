using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [Header("Spawn Enemy Settings")]
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] allEnemiesPrefab;
    [SerializeField] private int enemiesPerWave;
    [SerializeField] private Transform parentEnemy;

    public bool gameplayOn;

    public void Initialize()
    {
        gameplayOn = true;
        InvokeRepeating("SpawnWave", 0f,DataManager.GetEnemySpawnTimer());
    }
    public void SpawnWave() 
    {
        if(gameplayOn)
        {
            foreach(Transform currentPoint in spawnPoints)
            {
                for(int i = 0; i < enemiesPerWave; i++)
                {
                    int randomIndex = Random.Range(1, 100);
                    if(randomIndex % 2 == 0)
                    {
                        randomIndex = 0;
                    }
                    else
                    {
                        randomIndex = 1;
                    }
                    Instantiate(allEnemiesPrefab[randomIndex], currentPoint);
                }
            }
        }
    }
}
