using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float startDelay = 3.0f;
    public float spawnInterval = 5.0f;
    public Transform[] enemiesSpawnPoints;    //array with spawns point assigned in editor


    // Start is called before the first frame update
    void Start()
    {      
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }

    //randomizing spawn point
    Transform GetSpawnPoint()
    {
        int locationIndex = Random.Range(0, enemiesSpawnPoints.Length);
        return enemiesSpawnPoints[locationIndex];
    }
    //randomizing type of enemy
    void SpawnRandomEnemy()
    {       
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GetSpawnPoint());       
    }
}
