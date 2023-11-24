using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
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
            Debug.Log("wave spawned");
            foreach(Transform currentPoint in spawnPoints)
            {
                for(int i = 0; i <= enemiesPerWave; i++)
                {
                    Instantiate(allEnemiesPrefab[Random.Range(0, allEnemiesPrefab.Length - 1)], currentPoint);
                }
            }
        }
    }
}
